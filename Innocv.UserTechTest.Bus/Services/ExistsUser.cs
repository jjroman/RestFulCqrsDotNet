using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innocv.UserTechTest.Models;
using Innocv.UserTechTest.Persistence.Base;
using MediatR;

namespace Innocv.UserTechTest.Bus.Services
{
    public class ExistsUserRequest : IRequest<bool>
    {
        [Required]
        public int Id;
    }

    public sealed class ExistsUserHandler : IRequestHandler<ExistsUserRequest, bool>
    {
        private readonly IUserRepository _repository;

        public ExistsUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ExistsUserRequest request, CancellationToken cancellationToken)
        {
            // This handler could be manage a special user to get details, to simplify we use the user of the domain.
            return await _repository.ExistsAsync(request.Id);
        }
    }
}
