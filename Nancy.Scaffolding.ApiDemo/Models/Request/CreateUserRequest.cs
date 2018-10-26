using FluentValidation;
using Nancy.Scaffolding.ApiDemo.Properties;
using System.ComponentModel.DataAnnotations;

namespace Nancy.Scaffolding.ApiDemo.Models.Request
{
    public class CreateUserRequest
    {
        public string Id { get; set; }

        [Display(ResourceType = typeof(Fields), Name = nameof(Fields.FirstName))]
        public string FirstName { get; set; }

        [Display(ResourceType = typeof(Fields), Name = nameof(Fields.LastName))]
        public string LastName { get; set; }

        [Display(ResourceType = typeof(Fields), Name = nameof(Fields.Email))]
        public string Email { get; set; }

        [Display(ResourceType = typeof(Fields), Name = nameof(Fields.Password))]
        public string Password { get; set; }
    }

    public class CreateUserValidation : AbstractValidator<CreateUserRequest>
    {
        public CreateUserValidation()
        {
            RuleFor(request => request.FirstName).NotEmpty().Length(3, 32);
            RuleFor(request => request.LastName).NotEmpty().Length(3, 32);
            RuleFor(request => request.Email).NotEmpty().Length(4, 100).EmailAddress();
            RuleFor(request => request.Password).NotEmpty().Length(8, 20);
        }
    }
}
