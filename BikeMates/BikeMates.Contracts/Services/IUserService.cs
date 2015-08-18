using BikeMates.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts
{
    public interface IUserService
    {
        //bool Create(User entity);
        User GetUser(string Id);
        void Delete(string Id);
        void Update(User entity);

    }
}
