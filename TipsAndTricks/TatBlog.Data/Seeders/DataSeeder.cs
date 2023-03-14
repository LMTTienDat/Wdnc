﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TatBlog.Core.Entities;
using TatBlog.Data.Contexts;
using TatBlog.Data.Seeders;


namespace TatBlog.Data.Seeders;

public class DataSeeder : IDataSeeder

{
    private readonly BlogDbContext _dbContext;


    public DataSeeder(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void Initialize()
    {
        _dbContext.Database.EnsureCreated();
        if (_dbContext.Posts.Any()) return;

        var authors = AddAuthors();
        var categories = AddCategories();
        var tags = AddTags();
        var posts = AddPosts(authors, categories, tags);
    }

    private IList<Author> AddAuthors()
    {
        var authors = new List<Author>()
        {
            new()
            {
                FullName = "Jason Mouth",
                UrlSlug = "jason-mouth",
                Email = "jason@gmail.com",
                JoinedDate = new DateTime(2022, 10, 21)
            },
            new()
            {
                FullName = "Jessica Wonder",
                UrlSlug = "jessica-wonder",
                Email = "jessica665@motip.com",
                JoinedDate = new DateTime(2020, 4, 19)
            },

            new()
            {
                    FullName = "Nguyen Tien Linh",
                    UrlSlug="nguyen-tien-linh",
                    Email="linhnt@dlu.edu.vn",
                    JoinedDate=new DateTime(2019, 5, 24)
            },

            new()
            {
                    FullName = "Luong Hai Nam",
                    UrlSlug="luong-hai-nam",
                    Email="namluong@dlu.edu.vn",
                    JoinedDate=new DateTime(2018, 2, 14)
            }
        };

        _dbContext.Authors.AddRange(authors);
        _dbContext.SaveChanges();

        return authors;
    }

    private IList<Category> AddCategories()
    {
        var categories = new List<Category>()
        {
            new()   {Name = ".Net Core", Description = ".NET Core", UrlSlug=".Net Core"},
            new()   {Name = "Architecture", Description = "Archirecture", UrlSlug = "Architecture"},
            new()   {Name = "Messaging", Description = "Messaging", UrlSlug = "Messaging"},
            new()   {Name = "OOP", Description = "Object-Oriented Program",UrlSlug = "Object-Oriented Program"},
            new()   {Name = "Desingn Patterns", Description = "Desingn Patterns",UrlSlug = "Desingn Patterns"}
        };

        _dbContext.AddRange(categories);
        _dbContext.SaveChanges();

        return categories;
    }

    private IList<Tag> AddTags()
    {
        var tags = new List<Tag>()
        {
            new() {Name = "Google", Description = "Google applications",UrlSlug = "Google applications"},
            new() {Name = "ASP.NET MVC", Description = "ASP.NET MVC",UrlSlug = "ASP.NET MVC"},
            new() {Name = "Razor Page", Description = "Razor Page",UrlSlug = "Razor Page"},
            new() {Name = "Blazor", Description = " Blazor",UrlSlug = "Blazor"},
            new() {Name = "Deep Learning", Description = "Deep Learning",UrlSlug = "Deep Learning"},
            new() {Name = "Neural Network", Description = "Neural Network",UrlSlug ="Neural Network"}
        };

        _dbContext.AddRange(tags);
        _dbContext.SaveChanges();

        return tags;
    }

    private IList<Post> AddPosts(
        IList<Author> authors,
        IList<Category> categories,
        IList<Tag> tags)
    {
        var posts = new List<Post>()
        {
            new()
            {
                Title = "ASP.NET Core Diagnostic Scenarios",
                ShortDescription = "David and friends has a great repos",
                Description = "Here's a fen great DON'T and 00 examples",
                Meta = "David and friends has a great repository filled",
                UrlSlug = "aspnet-core-diagnostic-scenarios",
                Published = true,
                PostedDate = new DateTime(2021, 9, 30, 10 , 20, 0),
                ModifiedDate = null,
                ViewCount = 10,
                Author = authors[0],
                Category = categories[0],
                Tags = new List<Tag>()
                {
                    tags[0]
                }

            }
        };

        _dbContext.AddRange(posts);
        _dbContext.SaveChanges();

        return posts;
    }

    public void Intitialize()
    {
        throw new NotImplementedException();
    }
}
