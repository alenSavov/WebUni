using System.Collections.Generic;

namespace SchoolDiary.Services.Articles.Models
{
    public class AllArticlesCollectionViewModel
    {
        public ICollection<BaseArticleViewModel> Articles { get; set; }
        
    }
}
