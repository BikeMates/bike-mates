using BikeMates.Application.Services;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Managers;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeMates.Test.Services
{
    [TestFixture]
    class UserServiceTests
    {
        private Mock<IUserRepository> userRepository;
        private IUserService userService;

        [SetUp]
        public void Initialization()
        {
            userRepository = new Mock<IUserRepository>();
            userService = new UserService(userRepository.Object);
        }

        [Test]
        [TestCase("admin@admin.com", TestName="ExtitentUserShouldReturnValidUser")]
        public void GetUserByEmail(string expectedEmail)
        {
            //Arange
                          
            var expectedUser = new User{
                Email=expectedEmail
            };
        
            userRepository.Setup(s=>s.GetUserByEmail(It.IsAny<string>())).Returns(expectedUser);

             //Act
               
            User user = userService.GetUserByEmail(expectedEmail);

             //Assert
            Assert.AreSame(user.Email,expectedEmail);
        }
    }
}
