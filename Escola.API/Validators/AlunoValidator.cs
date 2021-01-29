using Escola.API.Domain.Models.Request;
using FluentValidation;

namespace Escola.API.Validators
{
    public class AlunoValidator : AbstractValidator<AlunoRequest>
    {
        public AlunoValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            #region Validar IdAluno
            //When(x => x.IdAluno != null, () =>
            //{
            //    RuleFor(x => x.IdAluno)
            //        .NotNull().WithMessage("Informe o Aluno")
            //        .GreaterThan(0).WithMessage("O aluno informado é invalido.")
            //        .DependentRules(() =>
            //        {
            //            Validation();
            //        });
            //}).Otherwise(() => {
            //    Validation();
            //});
            #endregion

            RuleFor(x => x.Nome)
                           .NotEmpty().WithMessage("Informe o nome")
                           .MinimumLength(5).WithMessage("O nome deve ter no minimo 5 caracteres") // Não deixa cadastrar com alunos com menos de 5 letras.
                           .MaximumLength(150).WithMessage("O nome deve ter no maximo 150 caracteres")
                           .DependentRules(() =>
                           {
                               RuleFor(x => x.Idade)
                       .GreaterThan(0).WithMessage("A idade deve ser maior que 0.")
                       .DependentRules(() =>
                       {
                           RuleFor(x => x.IdUnidade)
                              .GreaterThan(0).WithMessage("Informe a unidade.");
                       });
                           });
        }

        //private void Validation()
        //{
        //    RuleFor(x => x.Nome)
        //                   .NotEmpty().WithMessage("Informe o nome")
        //                   .MinimumLength(5).WithMessage("O nome deve ter no minimo 5 caracteres") // Não deixa cadastrar com alunos com menos de 5 letras.
        //                   .MaximumLength(150).WithMessage("O nome deve ter no maximo 150 caracteres")
        //                   .DependentRules(() =>
        //                   {
        //                       RuleFor(x => x.Idade)
        //               .GreaterThan(0).WithMessage("A idade deve ser maior que 0.")
        //               .DependentRules(() =>
        //                           {
        //                               RuleFor(x => x.IdUnidade)
        //                                  .GreaterThan(0).WithMessage("Informe a unidade.");
        //                           });
        //                   });
        //}
    }
}
