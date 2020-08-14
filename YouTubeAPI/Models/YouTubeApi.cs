using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;

namespace YouTubeAPI.Models
{
    public class YouTubeApi
    {
        private static YouTubeService _youTubeService = Auth();

        private static YouTubeService Auth()
        {
            UserCredential credentials;

            string filePath = HttpRuntime.AppDomainAppPath + "client_secret_youtube.json";

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.YoutubeForceSsl, YouTubeService.Scope.Youtubepartner }, //YouTubeService.Scope.YoutubeReadonly, 
                    "SingleUser",
                    CancellationToken.None,
                    new FileDataStore("YouTubeAPI")
                    ).Result;
            }

            var service = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "YouTubeAPI"
            });

            return service;
        }

        public static List<YouTubeVideo> GetPlaylistInfo(string playlistId)
        {
            var request = _youTubeService.PlaylistItems.List("contentDetails");
            request.PlaylistId = playlistId;
            
            LinkedList<YouTubeVideo> videos = new LinkedList<YouTubeVideo>();

            string nextPage = "";

            while (nextPage != null)
            {
                request.PageToken = nextPage;

                var response = request.Execute();

                foreach (var item in response.Items)
                {
                    videos.AddLast(new YouTubeVideo(item.ContentDetails.VideoId));
                }

                nextPage = response.NextPageToken;
            }

            return videos.ToList<YouTubeVideo>();
        }

        public static void GetVideoInfo(YouTubeVideo video)
        {
            var videoRequest = _youTubeService.Videos.List("snippet");
            videoRequest.Id = video.Id;

            var response = videoRequest.Execute();

            if(response.Items.Count > 0)
            {
                video.Title = response.Items[0].Snippet.Title;
                video.Description = response.Items[0].Snippet.Description;
                video.PublishedDate = response.Items[0].Snippet.PublishedAt.Value;
            }
            else
            {

            }
        }        
    }
}