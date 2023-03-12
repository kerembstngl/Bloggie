using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagInterface tagRepository;

        // Dependency Injection - Daha öncesinde DbContexte özgü bir class tanımladık ve program.cs içerisinde bu classı service özelliklerinden ugulamamıza tanıttık. Bunu yapmaktaki amacımız ihtiyaç duyulan herhangi bir nesne içerisinde ihtiyaç duyduğumuz bu nesneyi çağırabilmek ve nesnenin içerisinde yeniden bu dbcontexti oluşturmadan, mevcutta olan dbcontext üzerine erişim sağlamaktı. Yani DbContextimizi istediğimizi her classa enjekte edebiliyoruz. Bu işleme dependency injection denmektedir. Bu örnekte bunu kullanabilmek için mevcut classımızda bir constructor oluşturduk ve parametre olarak DBCONTEXT nesnemizden bir argüman yolladık bu argüman classın tamamında kullanılamayacağı için bir private field oluşturduk ve bu field ile contructorlardan gelen argümanı eşitleyerek classımız içerisinde field'ımız üzerinden DBCONTEXT nesnesini kullanabiliyor hale geldik.

        //private tagRepository _tagRepository;
        public AdminTagsController(ITagInterface tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        // Tag Ekleme GET Metodu
        [HttpGet]
        public async Task<IActionResult> Add()
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
        public async Task<IActionResult> Add (AddTagRequest addTagRequest)
        {
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName,
            };

            await tagRepository.AddAsync(tag);
           
            return RedirectToAction("List");
        }

        // Tagleri Listeleme Metodu
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllAsync();
            return View(tags);
        }
        
        // Edit sayfasının görüntülenme action'u
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            // 1. Metod
            //var tag = tagRepository.Tags.Find(id);

            // 2. Metod
            var tag = await tagRepository.GetAsync(id);


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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            
            var updatedTag = await tagRepository.UpdateAsync(tag);          

            if (updatedTag != null)
            {               
                return RedirectToAction("List");
            }            
            return RedirectToAction("Edit", new {id = editTagRequest.Id });


        }

        // Delete Metodu
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest) 
        {   
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null) 
            { 
                // success notification
                return RedirectToAction("List"); 
            }
            // error notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id});
        }


    }
}
