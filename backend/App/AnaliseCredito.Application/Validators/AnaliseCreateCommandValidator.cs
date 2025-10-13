using AnaliseCredito.Application.Analises.Commands;
using AnaliseCredito.Utils.Helpers;
using FluentValidation;

namespace AnaliseCredito.Application.Validators;

public class AnaliseCreateCommandValidator : AbstractValidator<AnaliseCreateCommand>
{
    public AnaliseCreateCommandValidator()
    {
        RuleFor(x => x.Cpf)
            .NotEmpty().Must(ValidateCPF).WithMessage("CPF é obrigatorio");

        RuleFor(x => x.NomeCliente)
            .NotEmpty().WithMessage("Nome é obrigatorio");

        RuleFor(x => x.Telefone)
            .NotEmpty().WithMessage("O telefone é obrigatorio!");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("O Email é obrigatorio!");
        
        RuleFor(x => x.Logradouro)
            .NotEmpty().WithMessage("O Logradouro é  obrigatorio!");
        RuleFor(x => x.Bairro)
            .NotEmpty().WithMessage("Bairro é obrigatorio!");

        RuleFor(x => x.Cidade)
            .NotEmpty().WithMessage("Cidade é obrigatoria!");

        RuleFor(x => x.Estado)
            .NotEmpty().WithMessage("Estado é obrigatorio!");

    }

    private bool ValidateCPF(string cpf)
    {
        return CPFHelper.Validar(cpf);
    }
}