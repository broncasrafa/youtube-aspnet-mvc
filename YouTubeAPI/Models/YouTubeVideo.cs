using System;

namespace YouTubeAPI.Models
{
    public class YouTubeVideo
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublishedDate { get; set; }

        public YouTubeVideo(string id)
        {
            this.Id = id;
            YouTubeApi.GetVideoInfo(this);
        }
    }
}