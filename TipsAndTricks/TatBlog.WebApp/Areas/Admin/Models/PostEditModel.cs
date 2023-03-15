using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Xunit.Abstractions;
using Xunit.Sdk;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace TatBlog.WebApp.Areas.Admin.Models
{
    public class PostEditModel
    {
        public int Id { get; set; }

        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [MaxLength(500, ErrorMessage = "Tiêu đề tối đa 500 ký tự")]

        public string Title { get; set; }

        [DisplayName("Giới thiệu")]
        [Required(ErrorMessage = "Giới thiệu không được để trống")]
        [MaxLength(2000, ErrorMessage = "Giới thiệu tối đa 2000 ký tự")]


        public string ShortDescription { get; set; }

        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Nội dung không được để trống")]
        [MaxLength(5000, ErrorMessage = "Nội dung tối đa 5000 ký tự")]


        public int Description { get; set; }

        [DisplayName("Metadata")]
        [Required(ErrorMessage = "Metadata không được để trống")]
        [MaxLength(1000, ErrorMessage = "Metadata tối đa 1000 ký tự")]


        public int Meta { get; set; }

        [DisplayName("Slug")]
        [Remote("VerifyPostSlug", "Posts", "Admin",
            HttpMethod = "POSTS", AdditionalFields = "Id")]
        [Required(ErrorMessage = "URL không được để trống")]
        [MaxLength(200, ErrorMessage = "Slug tối đa 200 ký tự")]


        public string UrlSlug { get; set; }
        [DisplayName("Chọn hình ảnh")]

        public string ImageFile { get; set; }
        [DisplayName("Hình hiện tại")]

        public string ImageUrl { get; set; }
        [DisplayName("Xuất bản ngay]")]

        public bool Publisted { get; set; }
        [DisplayName("Chủ đề")]
        [Required(ErrorMessage ="Bạn chưa chọn chủ đề")]

        public bool CategoryId { get; set; }
        [DisplayName("Tác giả")]
        [Required(ErrorMessage = "Bạn chưa chọn Tác giả")]

        public bool AuthorId { get; set; }
        [DisplayName("Từ khóa (mỗi người 1 dòng")]
        [Required(ErrorMessage = "Bạn chưa nhập tên thẻ")]

        public string SelectedTags { get; set; }

        public  IEnumerable<SelectListItem> AuthorList { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        public List<string> GetSelectedTags()
        {
            return (SelectedTags ?? "")
                .Split(new char[] {',' ,';','\r','\n'},
                StringSplitOptions.RemoveEmptyEntries)
                .ToList();
        }
    }
}
