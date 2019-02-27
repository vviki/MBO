using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using MarvelBO.Creators.MarvelModel;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MarvelBO.Creators
{
    public class CreatorsCache : ICreatorsCache
    {
        IDatabase _database;
        IServer _server;
        const string CREATOR_PREFIX = "c";

        public CreatorsCache(IDatabase database, IServer server)
        {
            _server = server;
            _database = database;
        }

        public void Fill(IEnumerable<Creator> creators)
        {
            TimeSpan expirationTime = new TimeSpan(24, 0, 0);

            creators.ToList().ForEach(
                creator => _database.StringSet(
                    CREATOR_PREFIX + creator.Id, 
                    JsonConvert.SerializeObject(creator), 
                    expirationTime));
        }

        public Creator Get(int id)
        {
            return JsonConvert.DeserializeObject<Creator>(
                _database.StringGet(CREATOR_PREFIX + id));
        }

        public bool IsEmpty()
        {
            var keys = _server.Keys(pattern: CREATOR_PREFIX + "*");

            return !keys.Any();
        }

        public IEnumerable<Creator> List()
        {
            var keys = _server.Keys(pattern: CREATOR_PREFIX + "*");

            return keys.Select(
                key => JsonConvert.DeserializeObject<Creator>(
                    _database.StringGet(key)));
        }

        public bool Exists(int id)
        {
            var creatorString = _database.StringGet(CREATOR_PREFIX + id);
            return creatorString.HasValue;
        }
    }
}
