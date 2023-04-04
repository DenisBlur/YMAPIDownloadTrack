using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3.Models
{
    // Модель для получения всей информации о треке
    public class Album
    {
        public int id { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string metaType { get; set; }
        public string version { get; set; }
        public int year { get; set; }
        public DateTime releaseDate { get; set; }
        public string coverUri { get; set; }
        public string ogImage { get; set; }
        public string genre { get; set; }
        public int trackCount { get; set; }
        public int likesCount { get; set; }
        public bool recent { get; set; }
        public bool veryImportant { get; set; }
        public List<Artist> artists { get; set; }
        public List<Label> labels { get; set; }
        public bool available { get; set; }
        public bool availableForPremiumUsers { get; set; }
        public List<string> availableForOptions { get; set; }
        public bool availableForMobile { get; set; }
        public bool availablePartially { get; set; }
        public List<object> bests { get; set; }
        public TrackPosition trackPosition { get; set; }
    }

    public class Artist
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool various { get; set; }
        public bool composer { get; set; }
        public Cover cover { get; set; }
        public List<object> genres { get; set; }
    }

    public class Cover
    {
        public string type { get; set; }
        public string uri { get; set; }
        public string prefix { get; set; }
    }

    public class Label
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class LyricsInfo
    {
        public bool hasAvailableSyncLyrics { get; set; }
        public bool hasAvailableTextLyrics { get; set; }
    }

    public class Major
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class R128
    {
        public double i { get; set; }
        public double tp { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string realId { get; set; }
        public string title { get; set; }
        public string version { get; set; }
        public Major major { get; set; }
        public bool available { get; set; }
        public bool availableForPremiumUsers { get; set; }
        public bool availableFullWithoutPermission { get; set; }
        public List<string> availableForOptions { get; set; }
        public string storageDir { get; set; }
        public int durationMs { get; set; }
        public int fileSize { get; set; }
        public R128 r128 { get; set; }
        public int previewDurationMs { get; set; }
        public List<Artist> artists { get; set; }
        public List<Album> albums { get; set; }
        public string coverUri { get; set; }
        public string ogImage { get; set; }
        public bool lyricsAvailable { get; set; }
        public string type { get; set; }
        public bool rememberPosition { get; set; }
        public string trackSharingFlag { get; set; }
        public LyricsInfo lyricsInfo { get; set; }
        public string trackSource { get; set; }
    }

    public class TrackInfo
    {
        public InvocationInfo invocationInfo { get; set; }
        public List<Result> result { get; set; }
    }

    public class TrackPosition
    {
        public int volume { get; set; }
        public int index { get; set; }
    }


}
