using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using JJ.Common.Dates;
using JJ.UserTechTest.Models;
using JJ.UserTechTest.Persistence.Base;
using MediatR;

namespace JJ.UserTechTest.Bus.Services
{
    public class UpdateUserRequest : IRequest<int>
    {
        [Required] public int Id { get; set; }

        [Required] 
        [MinLength(3)] 
        public string Name { get; set; }

        [Required]
        [CustomValidation(typeof(UserBirthdayValidation), nameof(UserBirthdayValidation.Range))] 
        public DateTime Birthday { get; set; }
    }

    public sealed class UpdateUserHandler : IRequestHandler<UpdateUserRequest, int>
    {
        private readonly IUserRepository _repository;

        public UpdateUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = User.CreateForUpdating(request.Id, request.Name, request.Birthday);
            var updatedUser = await _repository.UpdateAsync(user);
            return updatedUser.Id;
        }
    }
}