namespace TableForTwo.API.Tests.Infrastructure;

[CollectionDefinition(Name)]
public sealed class IntegrationCollection : ICollectionFixture<PostgresContainerFixture>
{
    public const string Name = "api-integration";
}
