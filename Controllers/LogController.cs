using AspNetCore.QuizAspNetCore;
using LogdeTela.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using QuizAspNetCore.Config;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace LogdeTela.Controllers
{
    public class LogController : Controller
    {
        private readonly IMongoCollection<Log> _log;

        public LogController(IMongoClient client)
        {
            //var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            //IMongoDatabase db = dbClient.GetDatabase("Log");
            //var logTable = db.GetCollection<BsonDocument>("Log");
            var database = client.GetDatabase("Log");
            var collection = database.GetCollection<Log>(nameof(Log));

            _log = collection;
        }


        public IActionResult TelaDois()
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult VerificarLog(Log log)
        {
            var filter = Builders<Log>.Filter.Eq(L => L.Usuario, "Iara");
            var logConsulta = _log.Find(filter).ToList();

            if (logConsulta.ToString() != "")
            {
                AtualizarLog(log);
            }
            else
            {
                _ = InserirLog(log);
            }

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> InserirLog(Log log)
        {
            log.TelaVisualizada = "Index";
            log.Usuario = "Iara";
            log.TempoTotalNaTela = "2";
            log.UltimoAcessoDataHora = DateTime.Now;
            log.VisualizacoesTotais = 2;

            await _log.InsertOneAsync(log);

            return Redirect("Index");
        }

        public IActionResult AtualizarLog(Log log)
        {
            //int visualizacoesTotais = log.VisualizacoesTotais +1; 
            //string tempoTotalNaTela = log.TempoTotalNaTela; 

            //var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            //IMongoDatabase db = dbClient.GetDatabase("Log");
            //var logTable = db.GetCollection<BsonDocument>("Log");
            //var filter = new BsonDocument("Usuario", log.Usuario);
            //var update = Builders<BsonDocument>.Update.Set("UltimoAcessoDataHora", DateTime.Now)
            //                                            .Set("VisualizacoesTotal", visualizacoesTotais)
            //                                            .Set("TempoTotalNaTela", tempoTotalNaTela);

            //var result = logTable.FindOneAndUpdate(filter, update);
            //return Ok();
        }


    }
}

