using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;

namespace SetupForDB
{
    class Program
    {

        //This is a Prpgram to setup Cosmos DB. This will be moved into The App Settings. Assuming...

        private static readonly string EndpointUri = "https://nickowen.documents.azure.com:443/";
        private static readonly string PrimaryKey = "LsieNBTsezHzcfrgzjWNWpCYP7wJ0llYwWVznL3aVE0j9rBqC2Lh2m0T6aOacEDjGEKPb6nW6BpF19OmstCZ3A==";
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;

        private string databaseID = "RevisionCardsDB";
        private string CardContainerID = "Cards";
        private string SubjectContainerID = "Subjects";

        public static async Task Main(string[] args)
        {
            var p = new Program();
            await p.Go();
        }

        public async Task Go()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions()
            {
                ApplicationName = "WHATISTHIS"
            });

            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseID);
            Console.WriteLine("Created Database: {0}\n", this.database.Id);

            //Creating Container For Subjects
            this.container = await this.database.CreateContainerIfNotExistsAsync(SubjectContainerID, "/Subject", 400);
            Console.WriteLine("Created Container: {0}\n", this.container.Id);

            Cards NewCard = new Cards("Maths", "10 + 10 = ??", "20");
            ItemResponse<Cards> ummmwhat = await this.container.CreateItemAsync<Cards>(NewCard);

        }

    }
}
