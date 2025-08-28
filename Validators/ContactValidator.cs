using FluentValidation;

using StudioIncantare.Dtos;

namespace StudioIncantare.Validators
{
    public class CreateContactDtoValidator : AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("O nome é obrigatorio ")
              .MinimumLength(3).WithMessage("O nome deve ter no minimo 3 caracteres ");

            RuleFor(y => y.Email)
              .NotEmpty().WithMessage("O email é obrigatorio ")
              .EmailAddress().WithMessage("Email invalido ");

            RuleFor(x => x.Message)
              .NotEmpty().WithMessage("A mensagem é obrigatoria ")
              .MinimumLength(10).WithMessage("A mensagem deve conter no minimo 11 caracteres ");
        }
    }
}
