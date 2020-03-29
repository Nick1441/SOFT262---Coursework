using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace SOFT262
{
    class CreatingData
    {

        static CreatingData DefualtInstance = new CreatingData();

        //Connecting to Correct Database. Links To Nick's Azure Account.
        private static readonly string EndpointUri = "https://nickowen.documents.azure.com:443/";
        private static readonly string PrimaryKey = "LsieNBTsezHzcfrgzjWNWpCYP7wJ0llYwWVznL3aVE0j9rBqC2Lh2m0T6aOacEDjGEKPb6nW6BpF19OmstCZ3A==";
        private CosmosClient cosmosClient;
        private Database database;
        private Container container;

        //Creating Database ID.
        private string databaseID = "FlashCards";

        //Creating Containers For Each Subject.
        private string ContainerID1 = "Maths";
        //private string ContainerID2 = "Physics";
        //private string ContainerID3 = "Geography";
        //private string ContainerID4 = "IT";

        //private Uri collectionLink = UriFactory.CreateDocumentCollectionUri(databaseID, ContainerID1);
        //private DocumentClient client;

        //private CreatingData ()
        //{
        //    client = new DocumentClient(new System.Uri ())
        //}

        public static async Task Main(string[] args)
        {
            var p = new CreatingData();
            await p.Go();
        }
        public async Task Go()
        {
            this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey, new CosmosClientOptions()
            {
                ApplicationName = "Flash"
            });

            //Creating New Database
            this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseID);

            //Create Container
            this.container = await this.database.CreateContainerIfNotExistsAsync(ContainerID1, "/Question");
            await AddQuestionIfDoesNotExist(new Question(1, "Is This Test Working?", "Yes"));
            await AddQuestionIfDoesNotExist(new Question(2, "Is This Test Working Yet?", "Maybe?"));

            //this.container = await this.database.CreateContainerIfNotExistsAsync(ContainerID2, "/Question");
            //this.container = await this.database.CreateContainerIfNotExistsAsync(ContainerID3, "/Question");
            //this.container = await this.database.CreateContainerIfNotExistsAsync(ContainerID4, "/Question");




        }

        async Task AddQuestionIfDoesNotExist(Question q)
        {
            try
            {
                ItemResponse<Question> questionResponce = await this.container.ReadItemAsync<Question>(q.question, new PartitionKey(q.answer));
            }
            catch
            {
                ItemResponse<Question> questionResponce = await this.container.CreateItemAsync<Question>(q, new PartitionKey(q.answer));
            }
        }

        public static CreatingData DefaultManager
        {
            get
            {
                return DefualtInstance;
            }
            private set
            {
                DefualtInstance = value;
            }
        }

    }
}
