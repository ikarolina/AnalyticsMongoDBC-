using LogdeTela.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LogdeTela.Controllers
{
    public class LogController : Controller
    {
        private readonly IMongoCollection<Log> _log;

        public LogController(IMongoClient client)
        {
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
            var filter = Builders<Log>.Filter.Eq(c => c.Usuario, "Iara");
            var logConsulta = _log.Find(filter).ToList();

            var tempoTela = logConsulta[0].TempoTotalNaTela;
            var visualizacoes = logConsulta[0].VisualizacoesTotais;

            var update = Builders<Log>.Update
                .Set(L => L.UltimoAcessoDataHora, DateTime.Now)
                .Set(L => L.VisualizacoesTotais, visualizacoes++)
                .Set(L => L.TempoTotalNaTela, tempoTela);
            var result =  _log.UpdateOne(filter, update);

            return Ok();
        }


    }
}

