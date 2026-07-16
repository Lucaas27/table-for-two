namespace TableForTwo.API.Infrastructure.Configuration.Options;

/// <summary>
/// Configuration for the API Mailpit integration.
/// </summary>
public sealed class MailpitOptions
{
    /// <summary>
    /// The configuration section name.
    /// </summary>
    public const string SectionName = "Infrastructure:Mailpit";

    /// <summary>
    /// The Mailpit host name.
    /// </summary>
    public string Host { get; init; } = "localhost";

    /// <summary>
    /// The Mailpit SMTP port.
    /// </summary>
    public int Port { get; init; } = 1025;

    /// <summary>
    /// The default sender email address.
    /// </summary>
    public string SenderEmail { get; init; } = "noreply@tablefortwo.local";
}
