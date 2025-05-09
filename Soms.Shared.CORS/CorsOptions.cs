namespace Soms.Shared.CORS;

public sealed class CorsOptions
{
    public bool IsEnabled { get; set; } =  false;
    public string Name { get; set; } = Constants.DefaultPolicyName;
    public List<string> Origins { get; set; } = [];
    public List<string> Headers { get; set; } = [];
    public List<string> Methods { get; set; } = [];
    public List<string> ExposedHeaders { get; set; } = [];
    public bool AllowCredentials { get; set; } = false;
}