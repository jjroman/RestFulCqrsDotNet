using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innocv.UserTechTest.Models;
using Innocv.UserTechTest.Persistence.Base;
using MediatR;

namespace Innocv.UserTechTest.Bus.Services
{
    public class GetUserRequest : IRequest<User>
    {
        [Required]
        public int Id;
    }

    public sealed class GetUserHandler : IRequestHandler<GetUserRequest, User>
    {
        private readonly IUserRepository _repository;

        public GetUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            // This handler could be manage a special user to get details, to simplify we use the user of the domain.
            var user = await _repository.GetAsync(request.Id);
            return user;
        }
    }
}
