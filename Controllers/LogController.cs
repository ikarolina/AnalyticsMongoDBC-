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
            var usuarioLog = Builders<Log>.Filter.Eq(x => x.Usuario, log.Usuario);
            var telaVisualizadaLog = Builders<Log>.Filter.Eq(x => x.TelaVisualizada, log.TelaVisualizada);
            var logConsulta = _log.Find(usuarioLog & telaVisualizadaLog).ToList();

            if (logConsulta.ToString() != "" && logConsulta.Count() != 0)
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
            log.TelaVisualizada = log.TelaVisualizada;
            log.Usuario = log.Usuario;
            log.TempoTotalNaTela = log.TempoTotalNaTela;
            log.UltimoAcessoDataHora = DateTime.Now;
            log.VisualizacoesTotais = 1;

            await _log.InsertOneAsync(log);

            return Redirect("Index");
        }

        public IActionResult AtualizarLog(Log log)
        {

            var filter = Builders<Log>.Filter.Eq(c => c.Usuario, log.Usuario);
            var analyticsConsulta = _log.Find(filter).ToList();

            var tempoTelaAtual = analyticsConsulta[0].TempoTotalNaTela;
            var tempoTelaTotal = tempoTelaAtual + log.TempoTotalNaTela;
            var visualizacoes = analyticsConsulta[0].VisualizacoesTotais;
            var teste = (visualizacoes + 1);

            var update = Builders<Log>.Update
                .Set(L => L.UltimoAcessoDataHora, DateTime.Now)
                .Set(L => L.VisualizacoesTotais, teste)
                .Set(L => L.TempoTotalNaTela, tempoTelaTotal);
            var result = _log.UpdateOne(filter, update);
            return Ok();
        }


    }
}

