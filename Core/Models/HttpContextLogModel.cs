using System.Collections.Generic;

namespace SimpleLogger.Models
{
    public class HttpContextLogModel
    {
        public string IpAddress { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public string Method { get; set; }

        public string QueryString { get; set; }

        public Dictionary<string, string> Headers { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }
    }
}