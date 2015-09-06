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
        private Mock<IMailService> mailSender;

        private IUserService userService;
        private User expectedUser;

        [SetUp]
        public void Initialization()
        {
            //Arrange
            userRepository = new Mock<IUserRepository>();
            mailSender = new Mock<IMailService>();

            userService = new UserService(userRepository.Object, mailSender.Object);

            expectedUser = new User
            {
                Id = "cc77d640-6eb7-4eb1-8e12-6b67ebda750b",
                Email = "admin@admin.com"
            };

            
            userRepository.Setup(s => s.Find(It.IsAny<string>())).Returns(expectedUser);        
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
            userRepository.Setup(s => s.GetUserByEmail(It.IsAny<string>())).Returns(expectedUser);

            User user = userService.GetUserByEmail(expectedEmail);
            Assert.AreEqual(user.Email, expectedEmail);
        }

        [Test]
        [TestCase(TestName = "RegistrationShouldReturnPositiveResult")]
        public void Register()
        {
            var expectedResult = new IdentityResult();
            userRepository.Setup(s => s.Register(It.IsAny<User>(), It.IsAny<string>())).Returns(expectedResult);

            userService.Register(expectedUser, "Qwerty1#");
            Assert.AreEqual(expectedResult.Errors.Count(), 0);
        }

        [Test]
        [TestCase("admin@admin.com", "Qwerty1#", TestName = "LoginShouldReturnValidUser")]
        public void Login(string userEmail, string userPassword)
        {
            userRepository.Setup(r => r.Login(It.IsAny<string>(), It.IsAny<string>()));
            userService.Login(userEmail, userPassword);
            userRepository.Verify(r => r.Login(It.IsAny<string>(), It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Test]
        [TestCase("4d330858-27ac-4905-9776-1138eabd6c01", TestName = "DeleteShouldRemoveUserById")]
        public void Delete(string userId)
        {
            userRepository.Setup(r => r.Delete(It.IsAny<string>()));
            userService.Delete(userId);
            userRepository.Verify(r => r.Delete(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Test]
        [TestCase(TestName = "GetAllShouldReturnAllUsers")]
        public void GetAll()
        {
            userRepository.Setup(r => r.GetAll());
            userService.GetAll();
            userRepository.Verify(r => r.GetAll(), Times.AtLeastOnce());
        }

        [Test]
        [TestCase("4d330858-27ac-4905-9776-1138eabd6c01", TestName = "BanUserShouldBanUserById")]
        public void BanUser(string userId)
        {
            userRepository.Setup(r => r.BanUser(It.IsAny<string>()));
            userService.BanUser(userId);
            userRepository.Verify(r => r.BanUser(It.IsAny<string>()), Times.AtLeastOnce());
        }

        [Test]
        [TestCase(TestName = "UnbanUsersShouldUnbanUsersByIds")]
        public void UnbanUsers()
        {
            var userId = new List<string>();
            userRepository.Setup(r => r.UnbanUsers(It.IsAny<List<string>>()));
            userService.UnbanUsers(userId);
            userRepository.Verify(r => r.UnbanUsers(It.IsAny<List<string>>()), Times.AtLeastOnce());
        }

        [Test]
        [TestCase(TestName = "UpdateShouldUpdateUserData")]
        public void Update()
        {
            var user = new User();
            userRepository.Setup(r => r.Update(It.IsAny<User>()));
            userService.Update(user);
            userRepository.Verify(r => r.Update(It.IsAny<User>()), Times.AtLeastOnce());
        }
    }
}
