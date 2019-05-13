using Newtonsoft.Json;

namespace ClassLibrary.model
{
    public class Segmento : Model
    {
        public int IdSegmento { get; set; }
        public string Descricao { get; set; }

        public Segmento(string descricao)
        {
            Descricao = descricao;
        }
        
        public override string ToString()
        {
            return Descricao;
        }
    }
}
