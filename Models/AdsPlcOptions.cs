namespace BeckhoffMiddleware.Models;

public class AdsPlcOptions
{
    public string AmsNetId { get; set; } = string.Empty;
    public int Port { get; set; } = 851;
    public string IpAddress { get; set; } = "127.0.0.1";
    public int TimeoutMs { get; set; } = 3000;
}
