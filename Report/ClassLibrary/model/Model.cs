using Newtonsoft.Json;

namespace ClassLibrary.model
{
    public abstract class Model
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
      
    }
}
