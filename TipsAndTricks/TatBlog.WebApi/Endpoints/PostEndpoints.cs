using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SlugGenerator;
using System.Net;
using TatBlog.Core.Collections;
using TatBlog.Core.DTO;
using TatBlog.Core.Entities;
using TatBlog.Services.Blogs;
using TatBlog.Services.Media;

using TatBlog.WebApi.Models;

namespace TatBlog.WebApi.Endpoints
{
    public static class PostEndpoints
    {
        public static WebApplication MapPostEndpoints(this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/posts");

            routeGroupBuilder.MapGet("/", GetPosts)
                .WithName("GetPosts")
                .Produces<ApiResponse<PaginationResult<PostItem>>>();

            routeGroupBuilder.MapGet("/featured/{limit:int}", GetFeaturedPost)
                .WithName("GetFeaturedPost")
                .Produces<ApiResponse<IList<PostItem>>>();

           /* routeGroupBuilder.MapGet("/random/{limit:int}", GetRandomPost)
                .WithName("GetRandomPost")
                .Produces<ApiResponse<IList<Post>>>();

            routeGroupBuilder.MapGet("/archives/{limit:int}", GetArchivesPost)
                .WithName("GetArchivesPost")
                .Produces<ApiResponse<IList<DateItem>>>();


            routeGroupBuilder.MapGet("/{id:int}", GetPostDetails)
                .WithName("GetPostById")
                 .Produces<ApiResponse<PostDto>>(); ;*/

            routeGroupBuilder.MapGet("/{slug::regex(^[a-z0-9_-]+$)}/posts", GetPostBySlug)
                 .WithName("GetPostByPostSlug")
                 .Produces<ApiResponse<PaginationResult<PostDto>>>();

            routeGroupBuilder.MapPost("/", AddPost)
                .WithName("AddNewPost")
                .Accepts<PostEditModel>("multipart/form-data")
                .Produces(401)
                .Produces<ApiResponse<PostItem>>();

            routeGroupBuilder.MapDelete("/{id:int}", DeletePost)
                  .WithName("DeletePost")
                  .Produces(401)
                  .Produces<ApiResponse<string>>();


            routeGroupBuilder.MapPost("/{id:int}/picture", SetPostPicture)
                  .WithName("SetPostPicture")
                  .Accepts<IFormFile>("multipart/formdata")
                  .Produces(401)
                  .Produces<string>();
           /* routeGroupBuilder.MapGet("/get-posts-filter", GetFilteredPosts)
                    .WithName("GetFilteredPost")
                    .Produces<ApiResponse<PostDto>>();*/

            routeGroupBuilder.MapGet("/get-filter", GetFilter)
            .WithName("GetFilter")
            .Produces<ApiResponse<PostFilterModel>>();

            return app;
        }

        private static async Task<IResult> GetPosts([AsParameters] PostFilterModel model, IBlogRepository blogRepository, IMapper mapper)
        {
            var postQuery = mapper.Map<PostQuery>(model);
            var postList = await blogRepository.GetPostByQueryAsync(postQuery, model, post => post.ProjectToType<PostItem>());

            var paginationResult = new PaginationResult<PostItem>(postList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> GetFeaturedPost(int limit, IBlogRepository blogRepository)
        {
            var posts = await blogRepository.GetPopularArticlesAsync(limit);
            return Results.Ok(ApiResponse.Success(posts));
        }

        /*private static async Task<IResult> GetRandomPost(int limit, IBlogRepository blogRepository)
        {
            var posts = await blogRepository.GetRandomPostAsync(limit);
            return Results.Ok(ApiResponse.Success(posts));
        }

        private static async Task<IResult> GetArchivesPost(int limit, IBlogRepository blogRepository)
        {
            var posts = await blogRepository.GetArchivesPostAsync(limit);
            return Results.Ok(ApiResponse.Success(posts));
        }

        private static async Task<IResult> GetPostDetails(int id, IBlogRepository blogRepository, IMapper mapper)
        {
            var post = await blogRepository.GetCachedPostByIdAsync(id);
            return post == null ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy bài viết có mã số{id}")) : Results.Ok(ApiResponse.Success(mapper.Map<PostItem>(post)));
        }*/

        private static async Task<IResult> GetPostBySlug([FromRoute] string slug, [AsParameters] PagingModel pagingModel, IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery
            {
                PostSlug = slug,
                PublishedOnly = true,
            };

            var postsList = await blogRepository.GetPostByQueryAsync(postQuery, pagingModel, posts => posts.ProjectToType<PostDto>());
            var post = postsList.FirstOrDefault();

            return Results.Ok(ApiResponse.Success(post));
        }

        private static async Task<IResult> AddPost(
             HttpContext context,
             IBlogRepository blogRepository,
             IMapper mapper,
             IMediaManager mediaManager)
        {
            var model = await PostEditModel.BindAsync(context);
            var slug = model.Title.GenerateSlug();
            if (await blogRepository.IsPostSlugExistedAsync(model.Id, slug))
            {
                return Results.Ok(ApiResponse.Fail(
                HttpStatusCode.Conflict, $"Slug '{slug}' đã được sử dụng cho bài viết khác"));
            }
            var post = model.Id > 0 ? await
                blogRepository.GetPostByIdAsync(model.Id) : null;
            if (post == null)
            {
                post = new Post()
                {
                    PostedDate = DateTime.Now
                };
            }
            post.Title = model.Title;
            post.AuthorId = model.AuthorId;
            post.CategoryId = model.CategoryId;
            post.ShortDescription = model.ShortDescription;
            post.Description = model.Description;
            post.Meta = model.Meta;
            post.Published = model.Published;
            post.ModifiedDate = DateTime.Now;
            post.UrlSlug = model.Title.GenerateSlug();
            if (model.ImageFile?.Length > 0)
            {
                string hostname =
               $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}/",
                uploadedPath = await
               mediaManager.SaveFileAsync(model.ImageFile.OpenReadStream(),
                model.ImageFile.FileName,
                model.ImageFile.ContentType);
                if (!string.IsNullOrWhiteSpace(uploadedPath))
                {
                    post.ImageUrl = uploadedPath;
                }
            }
            await blogRepository.CreateOrUpdatePostAsync(post,
                    model.GetSelectedTags());
            return Results.Ok(ApiResponse.Success(
            mapper.Map<PostItem>(post), HttpStatusCode.Created));
        }



        private static async Task<IResult> DeletePost(int id, IBlogRepository blogRepository)
        {
            return await blogRepository.DeletePostByIdAsync(id) ? Results.Ok(ApiResponse.Success("Post is deleted", HttpStatusCode.NoContent)) : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Could not find post with id = {id}"));
        }

        private static async Task<IResult> GetFilter(
            IAuthorRepository authorRepository,
            IBlogRepository blogRepository)
        {
            var model = new PostFilterModel()
            {
                AuthorList = (await authorRepository.GetAuthorsAsync())
            .Select(a => new SelectListItem()
            {
                Text = a.FullName,
                Value = a.Id.ToString()
            }),
                CategoryList = (await blogRepository.GetCategoryItemsAsync())
            .Select(c => new SelectListItem()
            {
                Text = c.Name,
                Value = c.Id.ToString()
            })
            };
            return Results.Ok(ApiResponse.Success(model));


        }
        private static async Task<IResult> GetFilteredPosts(
             [AsParameters] PostFilterModel model,
             [AsParameters] PagingModel pagingModel,
             IBlogRepository blogRepository)
        {
            var postQuery = new PostQuery()
            {
                Keyword = model.Keyword,
                CategoryId = model.CategoryId,
                AuthorId = model.AuthorId,
                Year = model.Year,
                Month = model.Month,
            };
            var postsList = await blogRepository.GetPagedPostsAsync(
            postQuery, pagingModel, posts =>
           posts.ProjectToType<PostDto>());
            var paginationResult = new PaginationResult<PostDto>(postsList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> SetPostPicture(int id, IFormFile imageFile,
            IBlogRepository blogRepository,
            IMediaManager mediaManager)
        {
            var post = await blogRepository.GetCachedPostByIdAsync(id);
            string newImagePath = string.Empty;

            if (imageFile?.Length > 0)
            {
                newImagePath = await mediaManager.SaveFileAsync(imageFile.OpenReadStream(), imageFile.FileName, imageFile.ContentType);
                if (string.IsNullOrWhiteSpace(newImagePath))
                {
                    return Results.Ok(ApiResponse.Fail(HttpStatusCode.BadRequest, "Không lưu dược tập tin"));

                }

                await mediaManager.DeleteFileAsync(post.ImageUrl);
                post.ImageUrl = newImagePath;
            }

            return Results.Ok(ApiResponse.Success(newImagePath));
        }

    }
}

