using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConsoleApp3
{
    internal class Program
    {

        static HttpClient httpClient = new HttpClient();
        static string userToken = "your token";

        static async Task Main(string[] args)
        { 
            //Получение информации для скачивания
            var downloadInfo = await GetRequest("https://api.music.yandex.net/tracks/111673390/download-info");
            //Преобразование Json в модель
            var jsonDownloadInfo = JsonConvert.DeserializeObject<Root>(downloadInfo);
            //Получение данных для создания ссылки на трек
            var trackXML = await GetRequest(jsonDownloadInfo.result[0].downloadInfoUrl.ToString());
            //Получение ссылки на трек
            var trackUrl = CreateDownloadUrl(trackXML);
            //отображение её в консоли
            Console.WriteLine(trackUrl);
            Console.ReadLine();
        }

        static string CreateDownloadUrl(string trackXML)
        {

            //Создание ссылки для получения трека

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Downloadinfo));
            TextReader reader = new StringReader(trackXML);
            var xml = xmlSerializer.Deserialize(reader) as Downloadinfo;


            //Создание подписи, лучше ничего не менять
            string sign = GetMdHesh(MD5.Create(), $"XGRlBW9FXlekgbPrRHuSiA{xml.Path.Substring(1, xml.Path.Length - 1)}{xml.S}");

            return "https://"+xml.Host+"/get-mp3/"+ sign + "/" + xml.Ts+ xml.Path;
        }

        //Функция для создания подписи
        static string GetMdHesh(MD5 md5, string str)
        {
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sBuilder = new StringBuilder();

            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        //Функция для отправки запросов
        static async Task<string> GetRequest(string urlLink)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, urlLink);
            //Добавление хедеров для авторизации пользователя 
            requestMessage.Headers.Add("Authorization", "OAuth " + userToken);

            var response = await httpClient.SendAsync(requestMessage);
            var responseText = await response.Content.ReadAsStringAsync();
            return responseText;
        }
    }
}


//XML модель для удобства)
[XmlRoot(ElementName = "download-info")]
public class Downloadinfo
{

    [XmlElement(ElementName = "host")]
    public string Host { get; set; }

    [XmlElement(ElementName = "path")]
    public string Path { get; set; }

    [XmlElement(ElementName = "ts")]
    public string Ts { get; set; }

    [XmlElement(ElementName = "region")]
    public int Region { get; set; }

    [XmlElement(ElementName = "s")]
    public string S { get; set; }
}


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

public class Root
{
    public InvocationInfo invocationInfo { get; set; }
    public List<Result> result { get; set; }
}

