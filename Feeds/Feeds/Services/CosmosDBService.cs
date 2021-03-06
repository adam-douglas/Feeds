﻿using Microsoft.Azure.Documents;
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

        static async Task<bool> Initialize(string collectionName)
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

        public static async Task<User> GetByUsernameAsync(string username)
        {
            if (!await Initialize("Users"))
                return new User();

           List<User> users = new List<User>();

            var docUri = UriFactory.CreateDocumentCollectionUri(databaseName, "Users");
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

        public static async Task<User> GetByIdAsync(string userId)
        {
            if (!await Initialize("Users"))
                return new User();

            User user = new User();
            var docUri = UriFactory.CreateDocumentUri(databaseName, "Users", userId);
            user = await docClient.ReadDocumentAsync<User>(docUri);
            return user;
        }

        public static async Task CreateUser(User newUser)
        {
            if (!await Initialize("Users"))
                return;
            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, "Users"),
                newUser);
        }

        public static async Task<List<Donation>> GetDonationsAsync(string city)
        {
            List<Donation> donations = new List<Donation>();

            if (!await Initialize("Donations"))
                return donations;            

            var docUri = UriFactory.CreateDocumentCollectionUri(databaseName, "Donations");
            var feedOptions = new FeedOptions { MaxItemCount = -1 };
            var donationsQuery = docClient.CreateDocumentQuery<Donation>(docUri, feedOptions).Where(dntn => dntn.Address.City == city && dntn.AcceptedBy == null).OrderBy(dntn => dntn.PickupDate).AsDocumentQuery();

            while (donationsQuery.HasMoreResults)
            {
                var returnedDonation = await donationsQuery.ExecuteNextAsync<Donation>();
                donations.AddRange(returnedDonation);
            }

            return donations;
        }

        public static async Task<List<Donation>> GetAcceptedDonations(string userId)
        {
            List<Donation> donations = new List<Donation>();

            if (!await Initialize("Donations"))
                return donations;

            var docUri = UriFactory.CreateDocumentCollectionUri(databaseName, "Donations");
            var feedOptions = new FeedOptions { MaxItemCount = -1 };
            var donationsQuery = docClient.CreateDocumentQuery<Donation>(docUri, feedOptions).Where(dntn => dntn.AcceptedBy == userId).AsDocumentQuery();

            while (donationsQuery.HasMoreResults)
            {
                var returnedDonation = await donationsQuery.ExecuteNextAsync<Donation>();
                donations.AddRange(returnedDonation);
            }

            return donations;
        }

        public static async Task<List<Donation>> GetUsersDonations(string userId)
        {
            List<Donation> donations = new List<Donation>();

            if (!await Initialize("Donations"))
                return donations;

            var docUri = UriFactory.CreateDocumentCollectionUri(databaseName, "Donations");
            var feedOptions = new FeedOptions { MaxItemCount = -1 };
            var donationsQuery = docClient.CreateDocumentQuery<Donation>(docUri, feedOptions).Where(dntn => dntn.CreatedBy == userId).AsDocumentQuery();

            while (donationsQuery.HasMoreResults)
            {
                var returnedDonation = await donationsQuery.ExecuteNextAsync<Donation>();
                donations.AddRange(returnedDonation);
            }

            return donations;
        }

        public static async Task CreateDonation(Donation newDonation)
        {
            if (!await Initialize("Donations"))
                return;
            await docClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(databaseName, "Donations"),
                newDonation);
        }

        public static async Task UpdateDonation(Donation updatedDonation)
        {
            if (!await Initialize("Donations"))
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, "Donations", updatedDonation.Id);
            await docClient.ReplaceDocumentAsync(docUri, updatedDonation);
        }

        public static async Task DeleteDonation(string donationId)
        {
            if (!await Initialize("Donations"))
                return;

            var docUri = UriFactory.CreateDocumentUri(databaseName, "Donations", donationId);
            await docClient.DeleteDocumentAsync(docUri);
        }
    }
}
