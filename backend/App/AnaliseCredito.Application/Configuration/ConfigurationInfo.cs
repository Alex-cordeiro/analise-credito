namespace AnaliseCredito.Application.Configuration;

public static class ConfigurationInfo
{
    public static string ConnectionString { get; set; } = null!;
    public static string RabbitMqHost { get; set; } = null!;
    public static int RabbitMqPort { get; set; }
    public static string RabbitMqUserName { get; set; } = null!;
    public static string RabbitMqPassword { get; set; } = null!;
}