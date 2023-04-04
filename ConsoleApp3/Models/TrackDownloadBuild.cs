using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    //Модель для удобства)
    public class InvocationInfo
    {
        [JsonProperty("req-id")]
        public string reqid { get; set; }
        public string hostname { get; set; }

        [JsonProperty("exec-duration-millis")]
        public int execdurationmillis { get; set; }
    }

    public class Result
    {
        public string codec { get; set; }
        public bool gain { get; set; }
        public bool preview { get; set; }
        public string downloadInfoUrl { get; set; }
        public bool direct { get; set; }
        public int bitrateInKbps { get; set; }
    }

    public class TrackDownloadBuild
    {
        public InvocationInfo invocationInfo { get; set; }
        public List<Result> result { get; set; }
    }

}
