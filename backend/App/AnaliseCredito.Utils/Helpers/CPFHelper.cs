namespace AnaliseCredito.Utils.Helpers;

public static class CPFHelper
{
    public static bool Validar(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;
        
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return (false);

        if (cpf.All(d => d == cpf[0]))
            return (false);
        
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += (cpf[i] - '0') * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if ((cpf[9] - '0') != digito1)
            return false;
        
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += (cpf[i] - '0') * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        if ((cpf[10] - '0') != digito2)
            return false;

        return true;
    }
    
    public static (bool isValid, string errorMessage) ValidarComDetalhes(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return (false, "CPF não pode ser vazio");
        
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return (false, "CPF deve conter 11 dígitos");

        if (cpf.All(d => d == cpf[0]))
            return (false, "CPF não pode ter todos os dígitos iguais");
        
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += (cpf[i] - '0') * (10 - i);

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        if ((cpf[9] - '0') != digito1)
            return (false, "Primeiro dígito verificador inválido");
        
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += (cpf[i] - '0') * (11 - i);

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        if ((cpf[10] - '0') != digito2)
            return (false, "Segundo dígito verificador inválido");

        return (true, "CPF válido");
    }
}