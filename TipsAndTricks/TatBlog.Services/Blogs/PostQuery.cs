﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatBlog.Services.Blogs
{
    public class PostQuery
    {
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public string CategorySlug { get; set; }
        public string TitleSlug { get; set; }

        public string PostSlug { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
        public int? PostId { get; set; }
        public string Tag{ get; set; }

        public string Keyword { get; set; }

        public bool PublishedOnly { get; set; }
        public string AuthorSlug { get; set; }
        public string TagSlug { get; set; }
        public bool NotPublished { get; set; }
    }
}