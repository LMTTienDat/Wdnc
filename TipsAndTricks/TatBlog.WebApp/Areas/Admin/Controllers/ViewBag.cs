using TatBlog.Core.Contracts;
using TatBlog.Core.Entities;

namespace TatBlog.WebApp.Areas.Admin.Controllers
{
    public class ViewBag
    {
        public static IPagedList<Post>? PostsList { get; internal set; }
    }
}