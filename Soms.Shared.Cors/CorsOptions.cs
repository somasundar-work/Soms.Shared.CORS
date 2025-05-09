namespace Soms.Shared.Cors;

/// <summary>
/// Represents configuration options for setting up Cross-Origin Resource Sharing (CORS) policies.
/// </summary>
public sealed class CorsOptions
{
    /// <summary>
    /// Gets or sets a value indicating whether CORS is enabled.
    /// </summary>
    /// <value><c>true</c> if CORS is enabled; otherwise, <c>false</c>.</value>
    public bool IsEnabled { get; set; } = false;

    /// <summary>
    /// Gets or sets the name of the CORS policy.
    /// </summary>
    /// <value>The name used to identify the CORS policy.</value>
    public string Name { get; set; } = Constants.DefaultPolicyName;

    /// <summary>
    /// Gets or sets the list of allowed origins.
    /// </summary>
    /// <value>A list of origins (URLs) that are permitted to access the resource.</value>
    public List<string> Origins { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of allowed headers.
    /// </summary>
    /// <value>A list of HTTP headers that can be used during the actual request.</value>
    public List<string> Headers { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of allowed HTTP methods.
    /// </summary>
    /// <value>A list of HTTP methods (e.g., GET, POST, PUT) permitted by the CORS policy.</value>
    public List<string> Methods { get; set; } = [];

    /// <summary>
    /// Gets or sets the list of headers that can be exposed to the client.
    /// </summary>
    /// <value>A list of headers that the browser is allowed to access from the response.</value>
    public List<string> ExposedHeaders { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether user credentials (cookies, authorization headers) are supported in cross-origin requests.
    /// </summary>
    /// <value><c>true</c> to allow credentials in cross-origin requests; otherwise, <c>false</c>.</value>
    public bool AllowCredentials { get; set; } = false;
}
