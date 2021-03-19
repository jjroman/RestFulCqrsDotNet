using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using JJ.UserTechTest.Persistence.Base;
using MediatR;

namespace JJ.UserTechTest.Bus.Services
{
    public class DeleteUserRequest : IRequest<int>
    {
        [Required]
        public int Id;
    }

    public sealed class DeleteUserHandler : IRequestHandler<DeleteUserRequest, int>
    {
        private readonly IUserRepository _repository;

        public DeleteUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var newUser = await _repository.DeleteAsync(request.Id);
            return newUser.Id;
        }
    }
}