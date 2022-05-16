using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.Models;

namespace TShirt.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //Save is now in UnitOfWork

        public void Update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
