using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.Models;

namespace TShirt.DataAccess.Repository.IRepository
{
    public interface IDesignTypeRepository : IRepository<DesignType>
    {
        void Update(DesignType obj); 

    }
}
