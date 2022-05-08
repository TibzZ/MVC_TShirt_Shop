using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TShirt.Models;

namespace TShirt.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        // When doing IRepo the Model will be Category, so will get all methods implemented inside the generic repository
        // Here we need to implement method to udpate the Category as it will be specific to each Model

        void Update(Category obj);

        //To Save updates when we have perform all the changes - Better to put here than in the Repository
        void Save(); 

    }
}
