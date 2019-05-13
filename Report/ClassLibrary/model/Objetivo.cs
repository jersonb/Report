using Newtonsoft.Json;

namespace ClassLibrary.model
{
    public class Objetivo : Model
    {
        public int IdObjetivo { get; set; }
        public string Descricao { get; set; }

        public Objetivo(string descricao)
        {
            Descricao = descricao;
        }
       
        public override string ToString()
        {
            return Descricao;
        }
    }
}
