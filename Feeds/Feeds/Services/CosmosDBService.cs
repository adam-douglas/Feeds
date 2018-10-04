using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public static async Task<User> getByUsernameAsync(string username)
        {
            if (!await Initialize())
                return new User();

           List<User> users = new List<User>();

            var docUri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
            var feedOptions = new FeedOptions { MaxItemCount = -1 };
            var userQuery = docClient.CreateDocumentQuery<User>(docUri, feedOptions)
                .Where(u => u.Username.Equals(username))
                .AsDocumentQuery();

            while (userQuery.HasMoreResults)
            {
                var returnedUsers = await userQuery.ExecuteNextAsync<User>();
                users.AddRange(returnedUsers);
            }

            if (!users.Any())
            {
                return null;
            }

            return users.First();
        }

        public static async Task createUser(User newUser)
        {
            if (!await Initialize())
                return;
            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, collectionName),
                newUser);
        }
    }
}
