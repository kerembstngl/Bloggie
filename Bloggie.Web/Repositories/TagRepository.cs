using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagInterface
    {
        private readonly BloggieDbContext bloggieDbContext;

        // Tag repository isimli classımız daha öncesinde sadece metotların imzalarını tanıtmış olduğumuz interface üzerinden kalıtım alacak ve bu metotların body kısmını dolduracaktır. Fakat en nihayetinde amacımız Dbcontextimizin işlevini yerine getirmek olduğu için bu alanda bir yandan dbcontexi enjekte etmemiz gerekiyor.
        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }


        public async Task<Tag?> AddAsync(Tag tag)
        {
            await bloggieDbContext.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public Task<Tag?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tag>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Tag?> UpdateAsync(Tag tag)
        {
            throw new NotImplementedException();
        }
    }
}
