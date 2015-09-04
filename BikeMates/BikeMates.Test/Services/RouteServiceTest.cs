using BikeMates.Application.Services;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;


namespace BikeMates.Test
{
    [TestFixture]
    class RouteServiceTests
    {
        private Mock<IUserRepository> userRepository;
        private Mock<IRouteRepository> routeRepository;
        private Mock<IMailService> mailService;

        private IUserService userService;
        private IRouteService routeService;
        

        [TestFixtureSetUp]
        public void InitialSetup()
        {
            userRepository = new Mock<IUserRepository>();
            mailService = new Mock<IMailService>();
            routeRepository = new Mock<IRouteRepository>();

            userService = new UserService(userRepository.Object, mailService.Object);
            routeService = new RouteService(routeRepository.Object, userRepository.Object);
        }

        [Test]
        [TestCase("test@test.com", "Dima", "Plahta", "Like cycling", "Route description", 2.44, "Shevchenka blv", 1, TestName = "User info is properly added")]
        public void AddRouteTest(string expectedEmail, string expectedName, string expectedSurname, string expectedDescription, string expectedAbout
            , double expectedDistance, string expectedPlace, int expectedId)
        {
            

            User expectedUser = new User
            {
                Email = expectedEmail,
                About = expectedAbout,
                FirstName = expectedName,
                SecondName = expectedSurname
            };

            Route expectedRoute = new Route
            {
                Id = expectedId,
                Author = expectedUser,
                Description = expectedDescription,
                Start = new DateTime(2015, 12, 22),
                Distance = expectedDistance,
                MeetingPlace = expectedPlace,
          
            };

           // routeService.Add();
            routeRepository.Setup(r => r.Find(It.IsAny<int>())).Returns(expectedRoute);

            Route testroute = routeService.Find(1);

            Assert.AreEqual(expectedEmail ,      testroute.Author.Email ,     " Emails are not equal" );
            Assert.AreEqual(expectedName,        testroute.Author.FirstName,  " First Names are not equal");
            Assert.AreEqual(expectedSurname,     testroute.Author.SecondName, " Second Names are not equal");
            Assert.AreEqual(expectedDescription, testroute.Description,       " Descriptions are not equal");
            Assert.AreEqual(expectedAbout,       testroute.Author.About,      " Abouts are not equal");
            Assert.AreEqual(expectedDistance,    testroute.Distance,          " Distances are not equal");
            Assert.AreEqual(expectedPlace,       testroute.MeetingPlace,      " Places are not equal");
            Assert.AreEqual(expectedId,          testroute.Id,                " Ids are not equal");
        }

        [Test]
        [TestCase("test@test.com", "Dima", "Plahta", "Like cycling", "Route description", 2.44, "Shevchenka blv", 1, TestName = "User is subscribed")]
        public void CheckIsUserSubscribedToRouteTest(string expectedEmail, string expectedName, string expectedSurname, string expectedDescription, string expectedAbout
            , double expectedDistance, string expectedPlace, int expectedId)
     
        {
            // set up test objects

            User expectedUser = new User()
            {
                Email = expectedEmail,
                About = expectedAbout,
                FirstName = expectedName,
                SecondName = expectedSurname
            };

            List<User> expextedSubscribers = new List<User>();
            expextedSubscribers.Add(expectedUser);

            Route expectedRoute = new Route()
            {
                Id = expectedId,
                Author = expectedUser,
                Description = expectedDescription,
                Start = new DateTime(2015, 12, 22),
                Distance = expectedDistance,
                MeetingPlace = expectedPlace,
                Subscribers = expextedSubscribers
             
            };

            routeRepository.Setup(r => r.Find(It.IsAny<int>())).Returns(expectedRoute);
            userRepository.Setup(r => r.Find(It.IsAny<string>())).Returns(expectedUser);

            // check the method

            bool actualResult =   routeService.CheckIsUserSubscribedToRoute(expectedRoute.Id, expectedUser.Id);
            bool expectedResult = true;

            Assert.AreEqual(expectedResult, actualResult, "wrong subscription logic");
        
        
        }

        [Test]
        [TestCase("test@test.com", "Dima", "Plahta", "Like cycling", "Route description", 2.44, "Shevchenka blv", 1, TestName = "Check is user unsubscribed")]
        public void UnsubscribeUserTest(string expectedEmail, string expectedName, string expectedSurname, string expectedDescription, string expectedAbout
            , double expectedDistance, string expectedPlace, int expectedId)
        {
            // set up test objects

            User expectedUser = new User()
            {
                Email = expectedEmail,
                About = expectedAbout,
                FirstName = expectedName,
                SecondName = expectedSurname
            };

            List<User> expextedSubscribers = new List<User>();
            expextedSubscribers.Add(expectedUser);

            Route expectedRoute = new Route()
            {
                Id = expectedId,
                Author = expectedUser,
                Description = expectedDescription,
                Start = new DateTime(2015, 12, 22),
                Distance = expectedDistance,
                MeetingPlace = expectedPlace,
                Subscribers = expextedSubscribers

            };

            routeRepository.Setup(r => r.Find(It.IsAny<int>())).Returns(expectedRoute);
            userRepository.Setup(r => r.Find(It.IsAny<string>())).Returns(expectedUser);
           // routeRepository.Setup(r => r.UnsubscribeUser(It.IsAny<Route>(), It.IsAny<User>()))
            // check the method

            routeService.UnsubscribeUser(expectedRoute.Id, expectedUser.Id);

            bool actualResult = routeService.CheckIsUserSubscribedToRoute(expectedRoute.Id, expectedUser.Id);
            bool expectedResult = false;

            Assert.AreEqual(expectedResult, actualResult, "wrong subscription logic");


        }

    }
}
