using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Contracts;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;


namespace TatBlog.Services.Blogs;

public interface IBlogRepository
{
    Task<Post> GetPostAsync(
        int year,
        int month,
        string slug,
        CancellationToken cancellationToken = default);
    Task<Tag> GetTagAsync(
        string slug, CancellationToken cancellationToken = default);
    Task<IList<Post>> GetPopularArticlesAsync(
        int numPosts,
        CancellationToken cancellationToken = default);
    Task<Post> GetPostByIdAsync(
       int postId,
       bool includeDetails = false,
        CancellationToken cancellationToken = default);

    Task<bool> IsPostSlugExistedAsync(
        int postId, string slug,
        CancellationToken cancellationToken = default);

    Task IncreaseViewCountAsync(
        int postId,
        CancellationToken cancellationToken = default);

    Task<Post> CreateOrUpdatePostAsync(
     Post post,
     IEnumerable<string> tags,
     CancellationToken cancellationToken = default);

    Task<IList<CategoryItem>> GetCategoryItemsAsync(
     bool showOnMenu = false,
     CancellationToken cancellationToken = default);

/*    Task<IList<AuthorItem>> GetAuthorItemsAsync(
        CancellationToken cancellationToken = default);*/

    Task<IPagedList<TagItem>> GetPagedTagAsync(
     IPagingParams pagingParams,
     CancellationToken cancellationToken = default);

    public Task<Tag> GetTagBySlugAsync(
            string slug,
            CancellationToken cancellationToken = default);
    Task<IPagedList<Post>> GetPagedPostsAsync(
    PostQuery postQuery,
    int pageNumber = 1,
    int pageSize = 10,

    CancellationToken cancellationToken = default);

    Task<IPagedList<T>> GetPagedPostsAsync<T>(
        PostQuery condition,
        IPagingParams pagingParams,
        Func<IQueryable<Post>, IQueryable<T>> mapper);

    Task<IPagedList<Post>> GetPostByQueryAsync(PostQuery query, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

    Task<IPagedList<Post>> GetPostByQueryAsync(PostQuery query, IPagingParams pagingParams, CancellationToken cancellationToken = default);

    Task<IPagedList<T>> GetPostByQueryAsync<T>(PostQuery query, IPagingParams pagingParams, Func<IQueryable<Post>, IQueryable<T>> mapper, CancellationToken cancellationToken = default);

    Task<IList<DateItem>> GetArchivesPostAsync(int limit, CancellationToken cancellationToken = default);

    Task<Post> GetCachedPostByIdAsync(int id, bool published = false, CancellationToken cancellationToken = default);
    Task<IList<Post>> GetRandomPostAsync(int limit, CancellationToken cancellationToken = default);

    Task<bool> DeletePostByIdAsync(int? id, CancellationToken cancellationToken = default);
    Task<IList<AuthorItem>> GetAuthorsAsync(
        CancellationToken cancellationToken = default);


}
