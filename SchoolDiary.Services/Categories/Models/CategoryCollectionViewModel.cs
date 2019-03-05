using System.Collections.Generic;

namespace SchoolDiary.Services.Categories.Models
{
    public class CategoryCollectionViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
    }
}
