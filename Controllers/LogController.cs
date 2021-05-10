using AspNetCore.QuizAspNetCore;
using LogdeTela.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using QuizAspNetCore.Config;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogdeTela.Controllers
{
    public class LogController : Controller
    {
        private readonly Context _context;
        private readonly ILogger<LogController> _logger;

        public LogController(IOptions<ConfigDB> opcoes)
        {
            _context = new Context(opcoes);
        }

        //public LogController(ILogger<LogController> logger)
        //{
        //    _logger = logger;
        //}

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
            //var client = new MongoClient("mongodb://127.0.0.1:27017");
            //var database = client.GetDatabase("Log");
            //var logTable = database.GetCollection<BsonDocument>("Log");
            //var _books = database.GetCollection<Log>("Log");

            //var entity = _books.Find(document => document.Usuario == "Iara").FirstOrDefault();
            //var teste = entity.ToString();

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> InserirLog(Log log)
        {
            var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("Log");
            var logTable = db.GetCollection<BsonDocument>("Log");

            var document = new BsonDocument();
            document.Add("Usuario", log.Usuario);
            document.Add("TelaVisualizada", log.TelaVisualizada);
            document.Add("TempoNaTela", log.TempoTotalNaTela);
            document.Add("VisualizacoesTotal", log.VisualizacoesTotais);
            document.Add("UltimoAcessoDataHora", log.UltimoAcessoDataHora);

            await logTable.InsertOneAsync(document);

            return Redirect("Index");
        }

        public IActionResult AtualizarLog()
        {
            string usuario = "Iara";
            DateTime ultimoAcesso = DateTime.Today;
            int visualizacoes = 2; //pega o valor do banco + 1
            //string tempoNaTela; //soma o que veio no front + o que já existe no banco

            var dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("Log");
            var logTable = db.GetCollection<BsonDocument>("Log");

            var filter = new BsonDocument("Usuario", usuario);
            var update = Builders<BsonDocument>.Update.Set("UltimoAcessoDataHora", ultimoAcesso);

            var result = logTable.FindOneAndUpdate(filter, update);

            //db.Log.update({ "Usuario":"Iara"},{ "TelaVisualizada":"Título foi atualizado"});

            return Ok();
        }


    }
}

