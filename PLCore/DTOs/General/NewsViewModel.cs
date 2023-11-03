using PLDataLayer.Entities.Blog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.General
{
    public class NewsViewModel
    {
        public List<News> News { get; set; }
        public NewsGroup NewsGroup { get; set; }
        public Publisher Publisher { get; set; }
        public DateTime? Date { get; set; }
        public string Key { get; set; }
        public string PageTitle { get; set; }
    }
}
