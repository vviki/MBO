using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MarvelBO.Creators.MarvelModel;
using Microsoft.Extensions.Options;
using RestSharp;

namespace MarvelBO.Creators
{
    public class MarvelClient : IMarvelClient
    {
        MarvelClientSettings _marvelClientSettings;

        public MarvelClient(IOptions<MarvelClientSettings> marvelClientSettings)
        {
            _marvelClientSettings = marvelClientSettings.Value;
        }

        public IEnumerable<Creator> GetCreators()
        {
            RestClient Client = new RestClient
            {
                BaseUrl = new Uri(_marvelClientSettings.Url)
            };

            var creators = new List<Creator>();

            int offset = 0;
            int total = 1000000000;
            int limit = _marvelClientSettings.ImportLimit;

            while(creators.Count < total && creators.Count < limit)
            {
                var request = new RestRequest("creators");

                var now = DateTime.UtcNow.Ticks.ToString();

                var toHash = now + _marvelClientSettings.PriveteKey + _marvelClientSettings.PublicKey;

                var hash = CalculateMd5Hash(toHash);

                request.AddParameter("ts", now);
                request.AddParameter("apikey", _marvelClientSettings.PublicKey);
                request.AddParameter("hash", hash);

                request.AddParameter("limit", "100");

                request.AddParameter("offset", offset.ToString());

                offset += 100;

                var response = Client.Execute<Result>(request);

                total = int.Parse(response.Data.Data.Total);

                creators.AddRange(response.Data.Data.Results);
            }
            return creators;
        }
        string CalculateMd5Hash(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (byte t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString().ToLower();
        }
    }
}
