using System.Collections.Generic;
using System.Threading.Tasks;
using JJ.Common.Dates;
using JJ.UserTechTest.Bus.Services;
using JJ.UserTechTest.Models;
using JJ.UserTechTest.Persistence.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JJ.UserTechTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IMediator _mediator;
        private IUserRepository _repository;
        private IDateTimeProvider _dateTimeProvider;

        public UserController(IUserRepository repository, IMediator mediator, IDateTimeProvider dateTimeProvider)
        {
            _repository = repository;
            _mediator = mediator;
            _dateTimeProvider = dateTimeProvider;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUsers()
        {
            var request = new GetUsersRequest();
            var users = await _mediator.Send(request);

            return Ok(users);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            GetUserRequest request = new GetUserRequest() {Id = id};

            // We check if the user exists.
            ExistsUserRequest existsRequest = new ExistsUserRequest() { Id = request.Id };
            var exists = await _mediator.Send(existsRequest);

            if (!exists)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The user was not found.");
            }

            var user = await _mediator.Send(request);
            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), 481)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUser([FromBody] AddUserRequest request)
        {
            // Validation is using each type of the request, 
            // It helps a lot because the request is consistent in any part of the
            // command is needed, not only the API.
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Custom validation
            if (request.Name.ToLower() == "Javier")
            {
                return StatusCode(481, "The name is reserved.");
            }

            var id = await _mediator.Send(request);
            return Ok(id);
        }

        [HttpPut]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), 481)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // We check if the user exists.
            ExistsUserRequest existsRequest = new ExistsUserRequest() {Id = request.Id};
            var exists = await _mediator.Send(existsRequest);

            if (!exists)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The user was not found.");
            }

            // Custom validation
            if (request.Name.ToLower() == "Javier")
            {
                return StatusCode(481, "The name is reserved.");
            }

            var id = await _mediator.Send(request);
            return Ok(id);
        }


        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // We check if the user exists.
            ExistsUserRequest existsRequest = new ExistsUserRequest() { Id = id };
            var exists = await _mediator.Send(existsRequest);

            if (!exists)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The user was not found.");
            }

            DeleteUserRequest request = new DeleteUserRequest() {Id = id};

            // Removing the user, this is a hard remove, we could create 
            // a soft if we can create a UNDO method.
            var deletedId = await _mediator.Send(request);
            return Ok(deletedId);
        }


        [HttpGet("age/{id}")]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<User>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAge([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // We check if the user exists.
            ExistsUserRequest existsRequest = new ExistsUserRequest() { Id = id };
            var exists = await _mediator.Send(existsRequest);

            if (!exists)
            {
                return StatusCode(StatusCodes.Status404NotFound, "The user was not found.");
            }

            var request = new GetUserRequest() { Id = id };

            // This is just to test the get age with the default datetime provider.
            var user = await _mediator.Send(request);
            return Ok(user.GetAge(_dateTimeProvider));
        }
    }
}
