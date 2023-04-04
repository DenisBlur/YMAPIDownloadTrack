using ConsoleApp3.Models;
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
        static string baseUrl = "https://api.music.yandex.net/";
        static string userToken = "y0_AgAAAAAV_ACCAAG8XgAAAADgGYHzG_11cfSmSSuXwiBFskpnlLi3aYc";

        static async Task Main(string[] args)
        {

            //ID трека
            string trackId = "87360749";

            //Получение информации для скачивания
            var downloadInfo = await GetRequest(baseUrl + "tracks/" + trackId + "/download-info");

            //Преобразование Json в модель
            var jsonDownloadInfo = JsonConvert.DeserializeObject<TrackDownloadBuild>(downloadInfo);
            //Проверка на null
            if (jsonDownloadInfo.result != null)
            {
                //Получение данных для создания ссылки на трек
                var trackXML = await GetRequest(jsonDownloadInfo.result[0].downloadInfoUrl.ToString());
                //Получение ссылки на трек
                var trackUrl = CreateDownloadUrl(trackXML);
                //отображение её в консоли
                Console.WriteLine("Track URL: ");
                Console.WriteLine(trackUrl);
                Console.WriteLine("");
                Console.WriteLine("Track info: ");
                //Получние данных о треке
                var trackData = await GetRequest(baseUrl + "tracks/" + trackId);
                //Вставка в модель
                var jsonTrackData = JsonConvert.DeserializeObject<TrackInfo>(trackData);
                //Вывод названия title трека
                Console.WriteLine("Track name: " + jsonTrackData.result[0].title);
                //Вывод всех артистов трека
                foreach (var item in jsonTrackData.result[0].artists)
                {
                    Console.WriteLine("Artict name: " + item.name);
                }
                //Вывод всех альбомов трека
                foreach (var item in jsonTrackData.result[0].albums)
                {
                    Console.WriteLine("Album name: " + item.title);
                }
                Console.WriteLine("");
                Console.WriteLine(CreateImageLink(jsonTrackData.result[0].ogImage, 200));
            }
            else
            {
                Console.WriteLine("error check token: " + JsonConvert.SerializeObject(jsonDownloadInfo));
            }
            Console.ReadLine();
        }

        static string CreateImageLink(string url, int size)
        {
            string imageUrl = url.Substring(0, url.IndexOf("%"));
            return "https://"+ imageUrl + size + "x" + size;
        }

        static string CreateDownloadUrl(string trackXML)
        {

            //Создание ссылки для получения трека

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Downloadinfo));
            TextReader reader = new StringReader(trackXML);
            var xml = xmlSerializer.Deserialize(reader) as Downloadinfo;


            //Создание подписи, лучше ничего не менять
            string sign = GetMdHesh(MD5.Create(), $"XGRlBW9FXlekgbPrRHuSiA{xml.Path.Substring(1, xml.Path.Length - 1)}{xml.S}");

            return "https://" + xml.Host + "/get-mp3/" + sign + "/" + xml.Ts + xml.Path;
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
