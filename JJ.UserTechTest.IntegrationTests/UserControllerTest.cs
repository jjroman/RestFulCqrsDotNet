using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JJ.UserTechTest.Api.Controllers;
using JJ.UserTechTest.Bus.Services;
using JJ.UserTechTest.Models;
using JJ.UserTechTest.Persistence.Base;
using JJ.UserTechTest.Persistence.InMemory;
using JJ.Common.Dates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace JJ.UserTechTest.IntegrationTests
{
    public class UserControllerTest
    {
        #region Get All Testing

        [Fact]
        public async void ItShouldReturnAListOfOneUser()
        {
            // Arrange
            // We add only 1 user, we use the EF only in this arrange,
            // What we want to test is the controller.
            // This is not async because we need to have this data before of the test what we want to do.
            Func<UserDbContext, int> addOneUserCall = dbContext =>
            {
                var user = User.CreateForGettingAge("Javier", new DateTime(2000, 1, 1));
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
                return 1;
            };

            // Call to test
            Func<UserDbContext, Task<IActionResult>> controllerCall = dbContext =>
                GetUserController(dbContext).GetUsers();

            Func<UserDbContext, Task<IActionResult>> mergedCall = async dbContext =>
            {
                addOneUserCall(dbContext);
                var result = await controllerCall(dbContext);
                return result;
            };

            // Act
            var result = await RunIntoDbContext(mergedCall);

            // Assert

            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResult = (List<User>) okResult.Value;

            Assert.Single(userResult);
        }

        [Fact]
        public async void ItShouldReturnAnEmptyListUsers()
        {
            // Arrange
            Func<UserDbContext, Task<IActionResult>> controllerCall = dbContext =>
                GetUserController(dbContext).GetUsers();

            // Act
            var result = await RunIntoDbContext(controllerCall);

            // Assert

            var okResult = Assert.IsType<OkObjectResult>(result);
            var userResult = (List<User>) okResult.Value;

            Assert.Equal(new List<User>(), userResult);
        }
        #endregion

        #region Support methods
        private UserDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDb")
                .Options;
            return new UserDbContext(options);
        }

        private IMediator GetMockedMediator(IUserRepository repository)
        {
            // Mock mediator.
            var mockedMediator = new Mock<IMediator>();
            mockedMediator.Setup(m => m.Send(It.IsAny<GetUsersRequest>(), new CancellationToken())
            ).Returns((GetUsersRequest request, CancellationToken token) => new GetUsersHandler(repository).Handle(request, token));

            return mockedMediator.Object;
        }

        private UserController GetUserController(UserDbContext userContext)
        {
            IUserRepository repository = new UserRepositoryInMemory(userContext);
            IMediator mockedMediator = GetMockedMediator(repository);
            IDateTimeProvider defaultDateTimeProvider = new DefaultDateTimeProvider();
            var controller = new UserController(repository, mockedMediator, defaultDateTimeProvider);
            return controller;
        }

        /// <summary>
        ///     This method is to be more clear in the test.
        ///     It allows to use the using statement for the db context
        ///     and in this way to limit the db connections.
        /// </summary>
        /// <typeparam name="T">What return the action</typeparam>
        /// <param name="action">action to run</param>
        /// <returns></returns>
        private T RunIntoDbContext<T>(Func<UserDbContext, T> action)
        {
            T result;

            using (var context = GetDbContext())
            {
                result = action(context);
                context.Database.EnsureDeleted(); // We ensure that our database is always clean.
            }

            return result;
        }

        #endregion
    }
}