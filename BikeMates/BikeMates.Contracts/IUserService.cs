using BikeMates.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts
{
    //TODO: Create folder Services and move this interface into it
    public interface IUserService
    {
        ApplicationUser GetUser(string Id);
        void Delete(string Id);

    }
}
