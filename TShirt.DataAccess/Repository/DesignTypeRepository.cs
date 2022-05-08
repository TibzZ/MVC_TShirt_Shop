using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.DataAccess.Repository.IRepository;
using TShirt.Models;

namespace TShirt.DataAccess.Repository
{
    public class DesignTypeRepository : Repository<DesignType>, IDesignTypeRepository
    {
        private ApplicationDbContext _db;

        public DesignTypeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;   
        }

        public void Update(DesignType obj)
        {
            _db.DesignTypes.Update(obj); 
        }

    }
}
