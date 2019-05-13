using System;

namespace ClassLibrary.model
{
    public class Ocorrencia : Model
    {
        public int IdOcorrencia { get; set; }
        public string Descricao { get; set; }
        public bool Monitoramento { get; set; }
        public string Notas { get; set; }
        public Empresa Empresa_ { get; set; }
        public Responsavel Usuario { get; set; }
        public DateTime Date { get; set; }
        public Objetivo Tipo { get; set; }
    }
}
