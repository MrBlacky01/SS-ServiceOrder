using System.Collections.Generic;

namespace LocalizationServiceCore.Models
{
    public class Settings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public Dictionary<string,string> Collections { get; set; }
    }
}
