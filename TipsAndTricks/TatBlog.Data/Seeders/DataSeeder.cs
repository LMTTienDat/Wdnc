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
                    FullName = "Dang Phuong Tay",
                    UrlSlug="dang-phuong-tay",
                    Email="tay@dlu.edu.vn",
                    JoinedDate=new DateTime(2019, 5, 24)
            },

            new()
            {
                    FullName = "Luong Thanh Nam",
                    UrlSlug="luong-thanh-nam",
                    Email="namluong@dlu.edu.vn",
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
                new() {Name = "Vscode", Description ="Vscode", UrlSlug = "vscode"},
                new() {Name = "Nam", Description ="Nam", UrlSlug = "nam"},
                new() {Name = "Dat", Description ="Dat", UrlSlug = "dat"},
                new() {Name = "Thu", Description ="Thu", UrlSlug = "thu"},
                new() {Name = "Ruba", Description ="Ruba", UrlSlug = "ruba"},
                new() {Name = "Long", Description ="Long", UrlSlug = "long"},
                new() {Name = "Nhi", Description ="Nhi", UrlSlug = "nhi"},
                new() {Name = "Luong", Description ="Luong", UrlSlug = "luong"},
                new() {Name = "Hue", Description ="Hue", UrlSlug = "hue"},
                new() {Name = "Tay", Description ="Tay", UrlSlug = "tay"},
                new() {Name = "Thao", Description ="Thao", UrlSlug = "thao"},
                new() {Name = "CocCoc", Description ="CocCoc", UrlSlug = "coccoc"},
                new() {Name = "Fadebook", Description ="Fadebook", UrlSlug = "fadebook"},
                new() {Name = "Network", Description ="Network", UrlSlug = "network"},


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
                    tags[5]
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
                    Category = categories[2],
                    Tags = new List<Tag>()
                    {
                        tags[20]
                    }
                },

            new()
                {
                    Title ="Where can I get some?",
                    ShortDescription = "There are many variations of passages of Lorem Ipsum available",
                    Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable",
                    Meta = "Where can I get some",
                    UrlSlug = "where-can-get-some",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2],
                        tags[25]
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
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[5],
                    Tags = new List<Tag>()
                    {
                        tags[14]
                    }
                },
              new()
                {
                    Title ="Lorem Ipsum",
                    ShortDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry",
                    Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    Meta = "Lorem Ipsum",
                    UrlSlug = "lorem-ipsum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[5],
                    Category = categories[7],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
               new()
                {
                    Title ="The standard Lorem Ipsum passage",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    Meta = "The standard Lorem Ipsum passage",
                    UrlSlug = "lorem-ipsum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[4],
                    Category = categories[9],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title ="Why do we use it?",
                    ShortDescription = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout",
                    Description = "he point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[5],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    { tags[1], tags[8] }
                },
                 new()
                {
                    Title = "Finibus Bonorum et Malorum",
                    ShortDescription = "omnis voluptas assumenda est, omnis dolor repellendus.",
                    Description = "Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus,",
                    Meta = "Finibus Bonorum et Malorum",
                    UrlSlug = "finibus-bonorum-et-malorum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 14,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2],
                        tags[5]
                    }
                },
                  new()
                {
                    Title ="Section 1.10.33",
                    ShortDescription = "\"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident,",
                    Description = "similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est,",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 16,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                   new()
                {
                    Title ="1914 translation by H. Rackham",
                    ShortDescription = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system",
                    Description = "expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 19,
                    Author = authors[3],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[3],
                        tags[8]
                    }
                },
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
                Author = authors[2],
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
                    Author = authors[3],
                    Category = categories[4],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },

            new()
                {
                    Title ="Where can I get some?",
                    ShortDescription = "There are many variations of passages of Lorem Ipsum available",
                    Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable",
                    Meta = "Where can I get some",
                    UrlSlug = "where-can-get-some",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[4],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[20]
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
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
              new()
                {
                    Title ="Lorem Ipsum",
                    ShortDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry",
                    Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    Meta = "Lorem Ipsum",
                    UrlSlug = "lorem-ipsum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
               new()
                {
                    Title ="The standard Lorem Ipsum passage",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    Meta = "The standard Lorem Ipsum passage",
                    UrlSlug = "lorem-ipsum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[3],
                    Category = categories[3],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title ="Why do we use it?",
                    ShortDescription = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout",
                    Description = "he point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    { tags[1], tags[20] }
                },
                 new()
                {
                    Title = "Finibus Bonorum et Malorum",
                    ShortDescription = "omnis voluptas assumenda est, omnis dolor repellendus.",
                    Description = "Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus,",
                    Meta = "Finibus Bonorum et Malorum",
                    UrlSlug = "finibus-bonorum-et-malorum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2],
                        tags[5]
                    }
                },
                  new()
                {
                    Title ="Section 1.10.33",
                    ShortDescription = "\"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident,",
                    Description = "similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est,",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 11,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                   new()
                {
                    Title ="1914 translation by H. Rackham",
                    ShortDescription = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system",
                    Description = "expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 17,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[3],
                        tags[8]
                    }
                },
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
                    Category = categories[2],
                    Tags = new List<Tag>()
                    {
                        tags[1]
                    }
                },

            new()
                {
                    Title ="Where can I get some?",
                    ShortDescription = "There are many variations of passages of Lorem Ipsum available",
                    Description = "There are many variations of passages of Lorem Ipsum available, but the majority have suffered alteration in some form, by injected humour, or randomised words which don't look even slightly believable",
                    Meta = "Where can I get some",
                    UrlSlug = "where-can-get-some",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
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
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
              new()
                {
                    Title ="Lorem Ipsum",
                    ShortDescription = "Lorem Ipsum is simply dummy text of the printing and typesetting industry",
                    Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.",
                    Meta = "Lorem Ipsum",
                    UrlSlug = "lorem-ipsum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
               new()
                {
                    Title ="The standard Lorem Ipsum passage",
                    ShortDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua",
                    Description = "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                    Meta = "The standard Lorem Ipsum passage",
                    UrlSlug = "lorem-ipsum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                new()
                {
                    Title ="Why do we use it?",
                    ShortDescription = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout",
                    Description = "he point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 15,
                    Author = authors[5],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    { tags[1], tags[8] }
                },
                 new()
                {
                    Title = "Finibus Bonorum et Malorum",
                    ShortDescription = "omnis voluptas assumenda est, omnis dolor repellendus.",
                    Description = "Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus,",
                    Meta = "Finibus Bonorum et Malorum",
                    UrlSlug = "finibus-bonorum-et-malorum",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 17,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2],
                        tags[5]
                    }
                },
                  new()
                {
                    Title ="Section 1.10.33",
                    ShortDescription = "\"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident,",
                    Description = "similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est,",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 16,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[2]
                    }
                },
                   new()
                {
                    Title ="1914 translation by H. Rackham",
                    ShortDescription = "But I must explain to you how all this mistaken idea of denouncing pleasure and praising pain was born and I will give you a complete account of the system",
                    Description = "expound the actual teachings of the great explorer of the truth, the master-builder of human happiness. No one rejects, dislikes, or avoids pleasure itself, because it is pleasure, but because those who do not know how to pursue pleasure rationally encounter consequences that are extremely painful",
                    Meta = "IPhone cũ đáng mua",
                    UrlSlug = "Iphone-cu-dang-mua",
                    Published = true,
                    PostedDate = new DateTime(2019,5,24,6,29,6),
                    ModifiedDate = null,
                    ViewCount = 18,
                    Author = authors[2],
                    Category = categories[1],
                    Tags = new List<Tag>()
                    {
                        tags[3],
                        tags[8]
                    }
                },
        };

        _dbContext.AddRange(posts);
        _dbContext.SaveChanges();

        return posts;
    }

}
