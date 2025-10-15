namespace AnaliseCredito.Utils.Helpers;

public static class StringUtils
{
    public static string MascararCpf(this string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf) || cpf.Length < 11)
            return cpf;
            
        return $"{cpf.Substring(0, 3)}.***.***-{cpf.Substring(9, 2)}";
    }
    
    
}