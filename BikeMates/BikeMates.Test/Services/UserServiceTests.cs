using BikeMates.Application.Services;
using BikeMates.Contracts.Managers;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.DataAccess;
using BikeMates.DataAccess.Managers;
using BikeMates.DataAccess.Repository;
using BikeMates.Domain.Entities;
using Microsoft.AspNet.Identity;
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
        private Mock<BikeMatesDbContext> context;

        private IUserService userService;
        private User expectedUser;
        private IdentityResult expectedResult;
        private IUserManager userManager;

        [SetUp]
        public void Initialization()
        {
            //Arrange
            userRepository = new Mock<IUserRepository>();

            userService = new UserService(userRepository.Object);

            expectedUser = new User
            {
                Id = "cc77d640-6eb7-4eb1-8e12-6b67ebda750b",
                Email = "admin@admin.com"
            };

            expectedResult = new IdentityResult();

            userRepository.Setup(s => s.GetUserByEmail(It.IsAny<string>())).Returns(expectedUser);
            userRepository.Setup(s => s.Find(It.IsAny<string>())).Returns(expectedUser);
            userRepository.Setup(s => s.Register(It.IsAny<User>(), It.IsAny<string>())).Returns(expectedResult);
        }

        [Test]
        [TestCase("cc77d640-6eb7-4eb1-8e12-6b67ebda750b", TestName = "ExistentUserShouldReturnValidUserById")]
        public void GetUser(string expectedId)
        {
            //Act   
            User user = userService.GetUser(expectedId);
            //Assert
            Assert.AreEqual(user.Id, expectedId);
        }

        [Test]
        [TestCase("admin@admin.com", TestName = "ExistentUserShouldReturnValidUserByEmail")]
        public void GetUserByEmail(string expectedEmail)
        {              
            User user = userService.GetUserByEmail(expectedEmail);        
            Assert.AreEqual(user.Email,expectedEmail);
        }

        [Test]
        [TestCase(TestName = "RegistrationShouldReturnPositiveResult")]
        public void Register()
        {    
            userService.Register(expectedUser, "Qwerty1#");
            Assert.AreEqual(expectedResult.Errors.Count(), 0);
        }
    }
}
