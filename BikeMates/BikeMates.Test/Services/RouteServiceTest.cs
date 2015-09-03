using BikeMates.Application.Services;
using BikeMates.Contracts.Repositories;
using BikeMates.DataAccess.Managers;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using BikeMates.Domain.Entities;
using BikeMates.DataAccess.Managers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Test
{
    [TestFixture]
    class RouteServiceTests
    {
        [Test]
        public void AddRouteTest()
        {
         
            Route r1 = new Route() {   Id = 1 , Title = "loool"};
            Route r2 = new Route() {   Id = 2 , Title = "loool1"};
           
            
            Assert.AreEqual(r1.Title , r2.Title);
        }
    }
}
