﻿using System;
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

    Task<List<Post>> GetPopularArticlesAsync(
        int numPosts,
        CancellationToken cancellationToken = default);
    Task<Post> GetPostByIdAsync(
       int id,
       bool v,
        CancellationToken cancellationToken = default);

    Task<bool> IsPostSlugExistedAsync(
        int postId, string slug,
        CancellationToken cancellationToken = default);

    Task IncreaseViewCountAsync(
        int postId,
        CancellationToken cancellationToken = default);

    Task CreateOrUpdatePostAsync(
     int postId,
     CancellationToken cancellationToken = default);

    Task<IList<CategoryItem>> GetCategoriesAsync(
        bool showOnMenu = false,
        CancellationToken cancellationToken = default);
    Task<IList<AuthorItem>> GetAuthorsAsync(
            CancellationToken cancellationToken = default);

    Task<IPagedList<TagItem>> GetPagedTagsAsync(
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
    
}
