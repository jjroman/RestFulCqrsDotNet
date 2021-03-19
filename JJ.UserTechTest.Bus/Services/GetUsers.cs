using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JJ.UserTechTest.Models;
using JJ.UserTechTest.Persistence.Base;
using MediatR;

namespace JJ.UserTechTest.Bus.Services
{
    public class GetUsersRequest : IRequest<List<User>>
    {
    }

    public sealed class GetUsersHandler : IRequestHandler<GetUsersRequest, List<User>>
    {
        private readonly IUserRepository _repository;

        public GetUsersHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<User>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
        {
            // This handler could be manage a special user to get details, to simplify we use the user of the domain.
            var users = await _repository.GetAllAsync();
            return users.ToList();
        }
    }
}