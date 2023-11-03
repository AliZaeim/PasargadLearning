using PLDataLayer.Entities.Blog;
using PLDataLayer.Entities.SubEntities;
using PLDataLayer.Entities.Training;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLCore.DTOs.PUser
{
    public class SearchAllViewModel
    {
        public string SearchText { get; set; }
        public List<News> News { get; set; }
        public List<PLDataLayer.Entities.Training.Course> Courses { get; set; }
        public List<SiteFAQ> FAQs { get; set; }
        public List<InstaPost> InstaPosts { get; set; }
        public List<TimelineComponent> TimelineComponents { get; set; }
    }
}
