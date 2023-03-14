using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bloggie.Web.Models.ViewModels
{
    public class AddBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PaageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // Tagleri listelemek için
        public IEnumerable<SelectListItem> Tags { get; set; }

        // Seçili tag için
        public string[] SelectedTags { get; set; }

    }
}
