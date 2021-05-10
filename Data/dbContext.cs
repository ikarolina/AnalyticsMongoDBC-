using LogdeTela.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QuizAspNetCore.Config;

namespace AspNetCore.QuizAspNetCore
{
    public class Context
    {
        private readonly IMongoDatabase _mongoDatabase;
        public Context(IOptions<ConfigDB> opcoes) 
        {
            MongoClient mongoClient = new MongoClient(opcoes.Value.ConnectionString);

            if(mongoClient != null)
            {
                _mongoDatabase = mongoClient.GetDatabase(opcoes.Value.Database);
            }
        }
       
        public IMongoCollection<Log> Log
        {
            get
            {
                return _mongoDatabase.GetCollection<Log>("Log");
            }
        }
    }
}
