using PLDataLayer.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.General
{
    public class ArchiveViewModel
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public List<News> News { get; set; }
        public List<NewsFile> NewsFiles { get; set; }
    }
}
