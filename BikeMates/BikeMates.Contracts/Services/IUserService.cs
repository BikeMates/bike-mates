using BikeMates.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Contracts //TODO: fix namespace
{
    public interface IUserService
    {
        User GetUser(string Id);
        void Delete(string Id); //TODO: all argument names should start with small letter
        void Update(User entity);

    }
}
