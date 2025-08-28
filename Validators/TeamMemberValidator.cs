using FluentValidation;
using StudioIncantare.Dtos;

namespace StudioIncantare.Validators
{


    public class CreateTeamMemberDtoValidator : AbstractValidator<TeamMemberCreateDto>
    {
        public CreateTeamMemberDtoValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty().WithMessage("Nome é obrigatorio ")
              .MinimumLength(3).WithMessage("O nome deve contar no minimo 3 caracteres ")
              .MaximumLength(20).WithMessage("O nome deve conter no maximo 20 caracteres ");


            RuleFor(x => x.Role)
              .NotEmpty().WithMessage("O cargo é obrigatorio ")
              .MinimumLength(3).WithMessage("O cargo deve conter no minimo 3 caracteres ")
              .MaximumLength(12).WithMessage("O cargo deve conter no maximo 12 caracteres");

            RuleFor(x => x.Bio)
              .NotEmpty().WithMessage("A biografia é obrigatoria ")
              .MinimumLength(10).WithMessage("A biografia deve conter no minimo 10 caracteres ")
              .MaximumLength(50).WithMessage("A biografia deve conter no maximo 50 caracteres ");

            RuleFor(x => x.Image_Url)
              .NotEmpty().WithMessage("A URL da imagem é obrigatoria ")
              .MaximumLength(255).WithMessage("A URL deve ter no maximo 255 caracteres ")
              .Must(url => Uri.IsWellFormedUriString(url, UriKind.Absolute))
              .WithMessage("A URL da imagem é invalida ");
        }
    }
}
