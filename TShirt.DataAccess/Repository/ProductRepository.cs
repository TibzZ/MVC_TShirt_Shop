using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.Models;

namespace TShirt.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }

        public void Update(Product obj)
        {
            // When lots of property this is too many operations for the db at it will update all the fields
            //_db.Products.Update(obj); 

            // retrieve actual object from DB. Okay to use DBContext in repo but shouldn't be used outside of this class
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                //Only listed items here will be updated
                objFromDb.TshirtTitle = obj.TshirtTitle;
                objFromDb.Description = obj.Description;
                objFromDb.ProductId = obj.ProductId;
                objFromDb.Designer = obj.Designer;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.MainPrice = obj.MainPrice;
                objFromDb.Price50Items = obj.Price50Items;
                objFromDb.Price100Items = obj.Price100Items;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.DesignTypeId = obj.DesignTypeId;
                if(obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }

        }

    }
}
