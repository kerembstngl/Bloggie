using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly BloggieDbContext bloggieDbContext;

        // Dependency Injection - Daha öncesinde DbContexte özgü bir class tanımladık ve program.cs içerisinde bu classı service özelliklerinden ugulamamıza tanıttık. Bunu yapmaktaki amacımız ihtiyaç duyulan herhangi bir nesne içerisinde ihtiyaç duyduğumuz bu nesneyi çağırabilmek ve nesnenin içerisinde yeniden bu dbcontexti oluşturmadan, mevcutta olan dbcontext üzerine erişim sağlamaktı. Yani DbContextimizi istediğimizi her classa enjekte edebiliyoruz. Bu işleme dependency injection denmektedir. Bu örnekte bunu kullanabilmek için mevcut classımızda bir constructor oluşturduk ve parametre olarak DBCONTEXT nesnemizden bir argüman yolladık bu argüman classın tamamında kullanılamayacağı için bir private field oluşturduk ve bu field ile contructorlardan gelen argümanı eşitleyerek classımız içerisinde field'ımız üzerinden DBCONTEXT nesnesini kullanabiliyor hale geldik.

        //private BloggieDbContext _bloggieDbContext;
        public AdminTagsController(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }
        // Tag Ekleme GET Metodu
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        //[HttpPost]
        //[ActionName("Add")]
        //public IActionResult Add (AddTagRequest addTagRequest)
        //{
        //    //var name = Request.Form["name"];
        //    //var displayName = Request.Form["displayName"];

        //    var name = addTagRequest.Name;
        //    var displayName = addTagRequest.DisplayName;
        //    return View("Add");
        //}

        //[HttpPost]        
        //public IActionResult Add (AddTagRequest addTagRequest)
        //{          

        //    var name = addTagRequest.Name;
        //    var displayName = addTagRequest.DisplayName;


        //    return View("Add");
        //}

        // Tag Ekleme Post Metodu
        [HttpPost]
        [ActionName("Add")]
        public IActionResult Add (AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            bloggieDbContext.Add(tag);
            bloggieDbContext.SaveChanges();
            return RedirectToAction("List");
        }

        // Tagleri Listeleme Metodu
        [HttpGet]
        public IActionResult List()
        {
            var tags = bloggieDbContext.Tags.ToList();
            return View(tags);
        }
        
        // Edit sayfasının görüntülenme action'u
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            // 1. Metod
            //var tag = bloggieDbContext.Tags.Find(id);

            // 2. Metod
            var tag = bloggieDbContext.Tags.FirstOrDefault(t => t.Id == id);


            if (tag != null)
            {
                var editTagReq = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(editTagReq);
            }
            

            return View(null);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = bloggieDbContext.Tags.Find(tag.Id);
            //existingTag.Name = tag.Name;
            //existingTag.DisplayName = tag.DisplayName;

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;
                bloggieDbContext.SaveChanges();
                return RedirectToAction("List");
            }
            
            return RedirectToAction("Edit", new {id = editTagRequest.Id });


        }

        // Delete Metodu
        [HttpPost]
        public IActionResult Delete(EditTagRequest editTagRequest) 
        {
            var tag = bloggieDbContext.Tags.Find(editTagRequest.Id);
            if (tag != null)
            {
                bloggieDbContext.Remove(tag);
                bloggieDbContext.SaveChanges();
                return RedirectToAction("List");

            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }


    }
}
