using System;
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
                new() { Name =".Net Core", Description =".Net Core", UrlSlug = "net-core", ShowOnMenu = true },
                new() { Name ="Architecture", Description ="Architecture", UrlSlug = "architecture", ShowOnMenu = true},
                new() { Name ="Messaging", Description ="Messaging", UrlSlug = "messaging", ShowOnMenu = true},

        };

        _dbContext.AddRange(categories);
        _dbContext.SaveChanges();

        return categories;
    }

    private IList<Tag> AddTags()
    {
        var tags = new List<Tag>()
        {
                new() {Name = "Google", Description = "Google applications", UrlSlug="google-applications"},
                new() {Name = "ASP.NET MVC", Description = "ASP.NET MVC", UrlSlug="asp.net-mvc"},
                new() {Name = "Razor Page", Description = "Razor Page", UrlSlug="razor-page"},
                new() {Name = "Blazor", Description = "Blazor", UrlSlug="blazor"},
                new() {Name = "Neural Network", Description = "Neural Network", UrlSlug="neural-network"},
                new() {Name = "JS", Description = "JavaScript", UrlSlug="javascript"},
                new() {Name = "Golang", Description ="Golang", UrlSlug = "golang"},
                new() {Name = "Dart", Description ="Dart", UrlSlug = "dart"},
                new() {Name = "Blazor", Description = "Blazor", UrlSlug="blazor"},
                new() {Name = "Neural Network", Description = "Neural Network", UrlSlug="neural-network"},
                new() {Name = "Google", Description = "Google applications", UrlSlug="google-applications"},
                new() {Name = "Ruby", Description ="Ruby", UrlSlug = "ruby"},
                new() {Name = "Razor Page", Description = "Razor Page", UrlSlug="razor-page"},
                new() {Name = "Blazor", Description = "Blazor", UrlSlug="blazor"},
                new() {Name = "Neural Network", Description = "Neural Network", UrlSlug="neural-network"},
                new() {Name = "Google", Description = "Google applications", UrlSlug="google-applications"},
                new() {Name = "Messaging", Description ="Messaging", UrlSlug = "messaging"},

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

            },
            new()
                {
                    Title ="Xe máy điện giá rẻ tầm 30 triệu",
                    ShortDescription = "Những năm gần đây, thị trường xe máy điện đã có những bước phát triển vượt bậc tại thị trường Việt Nam",
                    Description = "Và một điều cũng rất quan trọng là giá bán ngày càng rẻ, với tầm giá 30 triệu đồng người sử dụng đã có khá đa dạng các lựa chọn để đáp ứng nhu cầu di chuyển hàng ngày của mình",
                    Meta = "Xe máy điện giá rẻ tầm 30 triệu",
                    UrlSlug = "Xe-may-dien-gia-re-tam-30-trieu",
                    Published = true,
                    PostedDate = new DateTime(2018,7,27,9,25,7),
                    ModifiedDate = null,
                    ViewCount = 20,
                    Author = authors[1],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[1]
                    }
                },

            new()
                {
                    Title ="IPhone cũ đáng mua nhất hiện nay",
                    ShortDescription = "IPhone cũ vẫn đang được nhiều người dùng công nghệ lựa chọn nhờ trải nghiệm ổn định cùng mức giá hợp lý",
                    Description = "iPhone 11 Pro Max cũ là một trong những model bán chạy nhất trên thị trường iPhone cũ",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 20,
                    Author = authors[1],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
        };

        _dbContext.AddRange(posts);
        _dbContext.SaveChanges();

        return posts;
    }

}
