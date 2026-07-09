namespace TableForTwo.API.Infrastructure.Configuration.Options;

public sealed class MailpitOptions
{
    public const string SectionName = "Infrastructure:Mailpit";

    public string Host { get; init; } = "localhost";

    public int Port { get; init; } = 1025;

    public string SenderEmail { get; init; } = "noreply@tablefortwo.local";
}