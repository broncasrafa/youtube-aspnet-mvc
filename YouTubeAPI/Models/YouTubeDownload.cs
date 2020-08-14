using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using YoutubeExtractor;

namespace YouTubeAPI.Models
{
    public class YouTubeDownload
    {
        public static void DownloadVideo(IEnumerable<VideoInfo> videoInfos, int index)
        {
            try
            {
                VideoInfo video = videoInfos.First(info => info.VideoType == VideoType.Mp4 && info.Resolution == 360);

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\YouTubeVideos";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var filename = $"{ index } - { YouTubeHelpers.RemoveIllegalPathCharacters(video.Title) + video.VideoExtension }";

                var savePath = Path.Combine(path, filename);

                var videoDownloader = new VideoDownloader(video, savePath);

                videoDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage);

                videoDownloader.Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DownloadAudio(IEnumerable<VideoInfo> videoInfos)
        {
            try
            {
                VideoInfo video = videoInfos.Where(info => info.CanExtractAudio).OrderByDescending(info => info.AudioBitrate).First();

                if (video.RequiresDecryption)
                {
                    DownloadUrlResolver.DecryptDownloadUrl(video);
                }

                string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\YouTubeAudios";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var savePath = Path.Combine(path, YouTubeHelpers.RemoveIllegalPathCharacters(video.Title) + video.AudioExtension);

                var audioDownloader = new AudioDownloader(video, savePath);

                audioDownloader.DownloadProgressChanged += (sender, args) => Console.WriteLine(args.ProgressPercentage * 0.85);

                audioDownloader.AudioExtractionProgressChanged += (sender, args) => Console.WriteLine(85 + args.ProgressPercentage * 0.15);

                audioDownloader.Execute();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}