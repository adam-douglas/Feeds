using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Feeds
{
    public class CosmosDBService
    {
        static DocumentClient docClient = null;

        static readonly string databaseName = "Feeds";
        static readonly string collectionName = "Users";

        static async Task<bool> Initialize()
        {
            if (docClient != null)
                return true;

            try
            {
                docClient = new DocumentClient(new Uri(APIKeys.CosmosEndpointUrl), APIKeys.CosmosAuthKey);

                // Create the database - this can also be done through the portal
                await docClient.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName });

                // Create the collection - make sure to specify the RUs - has pricing implications
                // This can also be done through the portal
                await docClient.CreateDocumentCollectionIfNotExistsAsync(
                    UriFactory.CreateDatabaseUri(databaseName),
                    new DocumentCollection { Id = collectionName },
                    new RequestOptions { OfferThroughput = 400 }
                );

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);

                docClient = null;

                return false;
            }

            return true;
        }

        public static async Task<User> getAsync(string userId)
        {
            if (!await Initialize())
                return new User();
            var docUri = UriFactory.CreateDocumentUri(databaseName, collectionName, userId);
            var user = await docClient.ReadDocumentAsync<User>(docUri);
            return user;
        }
    }
}
