using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Core.Contracts;

public interface IPagingParams
{
    int PageSize { get; set; }
    int PageNumber { get; set; }
    int SortColumn { get; set; }
    int SortOrder { get; set; }

}
