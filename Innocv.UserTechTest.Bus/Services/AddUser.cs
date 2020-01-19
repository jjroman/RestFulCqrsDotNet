using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Innocv.Common.Dates;
using Innocv.UserTechTest.Models;
using Innocv.UserTechTest.Persistence.Base;
using MediatR;

namespace Innocv.UserTechTest.Bus.Services
{
    public class AddUserRequest : IRequest<int>
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
        
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(UserBirthdayValidation), nameof(UserBirthdayValidation.Range))]
        public DateTime Birthday { get; set; }
    }

    public sealed class AddUserHandler : IRequestHandler<AddUserRequest, int>
    {
        private readonly IUserRepository _repository;

        public AddUserHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            var user = User.CreateForGettingAge(request.Name, request.Birthday);

            var newUser = await _repository.AddAsync(user);
            return newUser.Id;
        }
    }
}