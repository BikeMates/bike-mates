using BikeMates.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts
{
    public interface IUserService
    {
        ApplicationUser GetUser(string Id);
        void Delete(string Id);

    }
}
