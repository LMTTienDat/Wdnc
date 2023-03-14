using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.DTO;

public class CategoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UrlSlug { get; set; }
    public string Description { get; set; }
    public string ShowOnMenu { get; set; }
    public string PostCount { get; set; }

}
