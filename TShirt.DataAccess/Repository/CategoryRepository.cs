using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.Models;

namespace TShirt.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db; 
        }

        //Save is now in UnitOfWork

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
