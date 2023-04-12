<<<<<<< Updated upstream
﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;
=======
﻿

using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Globalization;

>>>>>>> Stashed changes
namespace TatBlog.WebApi.Models;
public class PostFilterModel
{
<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
    [DisplayName("Từ khóa")]
    public string Keyword { get; set; }
    [DisplayName("Tác giả")]
    public int? AuthorId { get; set; }
    [DisplayName("Chủ đề")]
    public int? CategoryId { get; set; }
    [DisplayName("Năm")]
    public int? Year { get; set; }
    [DisplayName("Tháng")]
    public int? Month { get; set; }
<<<<<<< Updated upstream
=======
    
>>>>>>> Stashed changes
    public IEnumerable<SelectListItem> AuthorList { get; set; }
    public IEnumerable<SelectListItem> CategoryList { get; set; }
    public IEnumerable<SelectListItem> MonthList { get; set; }
    public PostFilterModel()
    {
        MonthList = Enumerable.Range(1, 12)
        .Select(m => new SelectListItem()
        {
            Value = m.ToString(),
            Text =
       CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(m)
        })
       .ToList();
    }
}