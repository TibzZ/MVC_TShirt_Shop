using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.DataAccess.Repository.IRepository;

namespace TShirt.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            DesignType = new DesignTypeRepository(_db);
            Product = new ProductRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }

        public IDesignTypeRepository DesignType { get; private set; }

        public IProductRepository Product { get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
