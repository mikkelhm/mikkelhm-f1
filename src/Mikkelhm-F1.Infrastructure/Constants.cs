namespace Mikkelhm_F1.Infrastructure
{
    public static class Constants
    {
        public static class CosmosDbSettings
        {
            public const string DatabaseId = "F1";
            public static class Seasons
            {
                public const string ContainerId = "Seasons";
                public const string PartitionKeyPath = "/partitionKey";
            }
            public static class Races
            {
                public const string ContainerId = "Races";
                public const string PartitionKeyPath = "/partitionKey";
            }
        }

        public static class EnvironmentVariableNames
        {
            public const string CosmosDbServiceEndpoint = "CosmosDbEndpoint";
            public const string CosmosDbServiceAuthKey = "CosmosDbPrimaryKey";

            public const string AppConfigurationEndpoint = "APPCONFIGURATION_ENDPOINT";
        }
    }
}
