﻿using BikeMates.Application.Services;
using BikeMates.Contracts.Repositories;
using BikeMates.Contracts.Services;
using BikeMates.Domain.Entities;
using Moq;
using NUnit.Framework;
using System;

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
        [TestCase("test@test.com", "Dima", "Plahta", "Like cycling", "Route description", 2.44, "Shevchenka blv", 1, TestName = "AddedInfoShouldBeIdentical")]
        public void AddRouteTest(string expectedEmail, string expectedName, string expectedSurname, string expectedDescription, string expectedAbout
            , double expectedDistance, string expectedPlace, int expectedId)
        {
            var expectedUser = new User
            {
                Email = expectedEmail,
                About = expectedAbout,
                FirstName = expectedName,
                SecondName= expectedSurname
            };

            var expectedRoute = new Route 
            {
               Id = expectedId,
               Author = expectedUser,
               Description = expectedDescription,
               Start = new DateTime(2015,12,22),
               Distance = expectedDistance,
               MeetingPlace = expectedPlace 
            };

          
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


    }
}
