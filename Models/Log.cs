using MongoDB.Bson;
using System;

namespace LogdeTela.Models
{
    public class Log  
    {
        public ObjectId Id { get; set; }
        public string Usuario { get; set; }
        public string TelaVisualizada { get; set; }
        public string TempoTotalNaTela { get; set; }
        public int VisualizacoesTotais { get; set; }
        public DateTime UltimoAcessoDataHora { get; set; }
        //public DateTime SaidaAcessoDataHora { get; set; }
    }
}
