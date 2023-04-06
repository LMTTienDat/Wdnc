using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SlugGenerator;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TatBlog.Core.Entities;
using TatBlog.Core.DTO;
using TatBlog.Data.Contexts;
using TatBlog.Core.Contracts;
using TatBlog.Services.Extensions;
using System.Globalization;
using Microsoft.Extensions.Caching.Memory;

namespace TatBlog.Services.Blogs
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDbContext _context;
        private readonly BlogDbContext _blogContext;
        private readonly IMemoryCache _memoryCache;
        
        private BlogDbContext context;

        public BlogRepository(BlogDbContext context, BlogDbContext blogDbContext, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _context = context;
            
        }

        public BlogRepository(BlogDbContext context)
        {
            this.context = context;
        }

        public async Task<Post> GetPostAsync(
            int year,
            int month,
            string slug,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Post> postsQuery = _context.Set<Post>()
                .Include(x => x.Category)
                .Include(x => x.Author);

            if (year > 0)
            {
                postsQuery = postsQuery.Where(x => x.PostedDate.Year == year);
            }
            if (year > 0)
            {
                postsQuery = postsQuery.Where(x => x.PostedDate.Month == month);
            }
            if (!string.IsNullOrEmpty(slug))
            {
                postsQuery = postsQuery.Where(x => x.UrlSlug == slug);
            }
            return await postsQuery.FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsPostSlugExistedAsync(
            int postId, string slug,
            CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>()
                .AnyAsync(x => x.Id != postId && x.UrlSlug == slug, cancellationToken);
        }

        public async Task IncreaseViewCountAsync(
            int postId,
            CancellationToken cancellationToken)
        {
            await _context.Set<Post>()
                .Where(x => x.Id == postId)
                .ExecuteUpdateAsync(p => p.SetProperty(x => x.ViewCount, x => x.ViewCount + 1),
                cancellationToken);
        }
        public async Task<IList<CategoryItem>> GetCategoryItemsAsync(
            bool showOnMenu = false,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Category> categories = _context.Set<Category>();
            if (showOnMenu)
            {
                categories = categories.Where(x => x.ShowOnMenu == showOnMenu);
            }
            return await categories
                .OrderBy(x => x.Name)
                .Select(x => new CategoryItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    ShowOnMenu = showOnMenu,
                    PostCount = x.Posts.Count(p => p.Published)
                })
                .ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<TagItem>> GetPagedTagAsync(
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default)
        {
            var tagQuery = _context.Set<Tag>()
            .Select(x => new TagItem()
            {
                Id = x.Id,
                Name = x.Name,
                UrlSlug = x.UrlSlug,
                Description = x.Description,
                PostCount = x.Posts.Count(p => p.Published)
            });
            return await tagQuery.ToPagedListAsync(pagingParams, cancellationToken);
        }
        public async Task<Tag> GetTagBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            IQueryable<Tag> tagQuery = _context.Set<Tag>().Include(i => i.Posts);

            if (!string.IsNullOrWhiteSpace(slug))
            {
                tagQuery = tagQuery.Where(x => x.UrlSlug == slug);
            }

            return await tagQuery.FirstOrDefaultAsync(cancellationToken);
        }
        private IQueryable<Post> FilterPosts(PostQuery query)
        {
            IQueryable<Post> postsQuery = _context.Set<Post>();
               
            

            if (query.PublishedOnly)
            {
                postsQuery = postsQuery.Where(x => x.Published == true);
            }

            if (query.NotPublished)
            {
                postsQuery = postsQuery.Where(x => !x.Published);
            }

            if (query.CategoryId > 0)
            {
                postsQuery = postsQuery.Where(x => x.CategoryId == query.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(query.CategorySlug))
            {
                postsQuery = postsQuery.Where(x => x.Category.UrlSlug == query.CategorySlug);
            }

            if (query.AuthorId > 0)
            {
                postsQuery = postsQuery.Where(x => x.AuthorId == query.AuthorId);
            }

            if (!string.IsNullOrWhiteSpace(query.AuthorSlug))
            {
                postsQuery = postsQuery.Where(x => x.Author.UrlSlug == query.AuthorSlug);
            }

            if (!string.IsNullOrWhiteSpace(query.TagSlug))
            {
                postsQuery = postsQuery.Where(x => x.Tags.Any(t => t.UrlSlug == query.TagSlug));
            }

            if (!string.IsNullOrWhiteSpace(query.Tag))
            {
                postsQuery = postsQuery.Where(x => x.Tags.Any(t => t.UrlSlug == query.Tag));
            }

            if (!string.IsNullOrWhiteSpace(query.Keyword))
            {
                postsQuery = postsQuery.Where(x => x.Title.Contains(query.Keyword) ||
                                         x.ShortDescription.Contains(query.Keyword) ||
                                         x.Description.Contains(query.Keyword) ||
                                         x.Category.Name.Contains(query.Keyword) ||
                                         x.Tags.Any(t => t.Name.Contains(query.Keyword)));
            }   

            if (query.Year > 0)
            {
                postsQuery = postsQuery.Where(x => x.PostedDate.Year == query.Year);
            }

            if (query.Month > 0)
            {
                postsQuery = postsQuery.Where(x => x.PostedDate.Month == query.Month);
            }

            if (!string.IsNullOrWhiteSpace(query.TitleSlug))
            {
                postsQuery = postsQuery.Where(x => x.UrlSlug == query.TitleSlug);
            }

            return postsQuery;
        }

        public async Task<IPagedList<Post>> GetPagedPostsAsync(
        PostQuery postQuery,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
        {
            return await FilterPosts(postQuery).ToPagedListAsync(
                pageNumber, pageSize,
                nameof(Post.PostedDate), "DESC",
                cancellationToken);
        }

        public async Task<Tag> GetTagAsync(
        string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Tag>()
                .FirstOrDefaultAsync(x => x.UrlSlug == slug, cancellationToken);
        }

        public async Task<Post> CreateOrUpdatePostAsync(
        Post post, IEnumerable<string> tags,
        CancellationToken cancellationToken = default)
        {
            if (post.Id > 0)
            {
                await _context.Entry(post).Collection(x => x.Tags).LoadAsync(cancellationToken);
            }
            else
            {
                post.Tags = new List<Tag>();
            }

            var validTags = tags.Where(x => !string.IsNullOrWhiteSpace(x))
                .Select(x => new
                {
                    Name = x,
                    Slug = x.GenerateSlug()
                })
                .GroupBy(x => x.Slug)
                .ToDictionary(g => g.Key, g => g.First().Name);


            foreach (var kv in validTags)
            {
                if (post.Tags.Any(x => string.Compare(x.UrlSlug, kv.Key, StringComparison.InvariantCultureIgnoreCase) == 0)) continue;

                var tag = await GetTagAsync(kv.Key, cancellationToken) ?? new Tag()
                {
                    Name = kv.Value,
                    Description = kv.Value,
                    UrlSlug = kv.Key
                };

                post.Tags.Add(tag);
            }

            post.Tags = post.Tags.Where(t => validTags.ContainsKey(t.UrlSlug)).ToList();

            if (post.Id > 0)
                _context.Update(post);
            else
                _context.Add(post);

            await _context.SaveChangesAsync(cancellationToken);

            return post;
        }

        public async Task<IList<CategoryItem>> GetCategoriesAsync(bool showOnMenu = false, CancellationToken cancellationToken = default)
        {
            IQueryable<Category> categories = _blogContext.Set<Category>().AsNoTracking();

            if (showOnMenu)
            {
                categories = categories.Where(x => x.ShowOnMenu);
            }

            return await categories.OrderByDescending(x => x.Name)
                                  .Select(x => new CategoryItem()
                                  {
                                      Id = x.Id,
                                      Name = x.Name,
                                      UrlSlug = x.UrlSlug,
                                      Description = x.Description,
                                      ShowOnMenu = x.ShowOnMenu,
                                      PostCount = x.Posts.Count(p => p.Published)
                                  }).ToListAsync(cancellationToken);
        }
        public async Task<IPagedList<Post>> GetPostByQueryAsync(PostQuery query, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            return await FilterPosts(query).ToPagedListAsync(
                                    pageNumber,
                                    pageSize,
                                    nameof(Post.PostedDate),
                                    "DESC",
                                    cancellationToken);
        }

        public async Task<IPagedList<Post>> GetPostByQueryAsync(PostQuery query, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            return await FilterPosts(query).ToPagedListAsync(
                                            pagingParams,
                                            cancellationToken);
        }

        public async Task<IPagedList<T>> GetPostByQueryAsync<T>(PostQuery query, IPagingParams pagingParams, Func<IQueryable<Post>, IQueryable<T>> mapper, CancellationToken cancellationToken = default)
        {
            IQueryable<T> result = mapper(FilterPosts(query));

            return await result.ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<TagItem>> GetPagedTagsAsync(
         IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            var tagQuery = _context.Set<Tag>()
                .OrderBy(x => x.Name)
                .Select(x => new TagItem()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UrlSlug = x.UrlSlug,
                    Description = x.Description,
                    PostCount = x.Posts.Count(p => p.Published)
                });

            return await tagQuery.ToPagedListAsync(pagingParams, cancellationToken);
        }
        public async Task<Post> GetPostByIdAsync(
        int postId, bool includeDetails = false,
        CancellationToken cancellationToken = default)
        {
            if (!includeDetails)
            {
                return await _context.Set<Post>().FindAsync(postId);
            }

            return await _context.Set<Post>()
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Include(x => x.Tags)
                .FirstOrDefaultAsync(x => x.Id == postId, cancellationToken);
        }

        public async Task<IList<Post>> GetPopularArticlesAsync(
        int numPosts, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Post>()
                .Include(x => x.Author)
                .Include(x => x.Category)
                .OrderByDescending(p => p.ViewCount)
                .Take(numPosts)
                .ToListAsync(cancellationToken);
        }

        public async Task<IPagedList<T>> GetPagedPostsAsync<T>(
        PostQuery condition,
        IPagingParams pagingParams,
        Func<IQueryable<Post>, IQueryable<T>> mapper)
        {
            var posts = FilterPosts(condition);
            var projectedPosts = mapper(posts);

            return await projectedPosts.ToPagedListAsync(pagingParams);
        }

        public async Task<IList<Post>> GetRandomPostAsync(int limit, CancellationToken cancellationToken = default)
        {
            return await _blogContext.Set<Post>().OrderBy(p => Guid.NewGuid()).Take(limit).ToListAsync(cancellationToken);
        }

        public async Task<IList<DateItem>> GetArchivesPostAsync(int limit, CancellationToken cancellationToken = default)
        {
            var lastestMonths = await GetLatestMonthList(limit);

            return await Task.FromResult(_blogContext.Set<Post>().AsEnumerable()
                    .GroupBy(p => new
                        {
                            p.PostedDate.Month,
                            p.PostedDate.Year
                        })
                        .Join(lastestMonths, d => d.Key.Month, m => m.Month,
                        (postDate, monthGet) => new DateItem
                        {
                            Month = postDate.Key.Month,
                            MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(postDate.Key.Month),
                            Year = postDate.Key.Year,
                            PostCount = postDate.Count()
                        }).ToList());
        }

        public async Task<Post> GetCachedPostByIdAsync(int id, bool published = false, CancellationToken cancellationToken = default)
        {
            return await _memoryCache.GetOrCreateAsync(
                $"post.by-id.{id}-{published}",
                async (entry) =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                    return await GetPostByIdAsync(id, published, cancellationToken);
                });
        }

        public async Task<IList<DateItem>> GetLatestMonthList(int limit)
        {
            return await Task.FromResult((from r in Enumerable.Range(1, 12) select DateTime.Now.AddMonths(limit - r))
                                .Select(x => new DateItem
                                {
                                    Month = x.Month,
                                    Year = x.Year
                                }).ToList());
        }


        public async Task<bool> DeletePostByIdAsync(int? id, CancellationToken cancellationToken = default)
        {
            var post = await _blogContext.Set<Post>().FindAsync(id);

            if (post is null) return false;

            _blogContext.Set<Post>().Remove(post);
            var rowsCount = await _blogContext.SaveChangesAsync(cancellationToken);
            return rowsCount > 0;
        }
    }
}


