using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using YouTubeAPI.Models;
using YoutubeExtractor;

namespace YouTubeAPI.Controllers
{
    public class HomeController : Controller
    {
        // link de uma playlist = https://www.youtube.com/playlist?list=PLb2HQ45KP0WstF2TXsreWRv-WUr5tqzy1
        // link de um video = https://www.youtube.com/watch?v=KxiHM3pQbGQ


        public ActionResult Index(string link = null)
        {
            List<YouTubeVideo> videos = new List<YouTubeVideo>();

            if (Request.IsAjaxRequest())
            {
                if (link.Contains("https://www.youtube.com/playlist?list="))
                {
                    string[] split = link.Split('=');

                    var playlistId = split[1].ToString();

                    videos = YouTubeApi.GetPlaylistInfo(playlistId);

                    return PartialView("_PlaylistVideos", videos);
                }
                else
                {
                    string[] split = link.Split('=');

                    var videoId = split[1].ToString();

                    YouTubeVideo video = new YouTubeVideo(videoId);

                    videos.Add(video);

                    return PartialView("_PlaylistVideos", videos);
                }
            }

            return View(videos);
        }

        [HttpGet]
        public ActionResult Download(string ids)
        {
            var videosIds = ids.Split(',').ToList();

            var index = 1;

            foreach (var id in videosIds)
            {
                var url = $"https://www.youtube.com/watch?v={ id }";

                try
                {
                    IEnumerable<VideoInfo> videoInfos = DownloadUrlResolver.GetDownloadUrls(url);
                                        
                    YouTubeDownload.DownloadVideo(videoInfos, index);

                    index++;
                }
                catch (Exception ex)
                {
                    return Json(new { Message = ex.Message, Status = "Erro" }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { Message = "OK", Status = "Sucesso" }, JsonRequestBehavior.AllowGet);
        }
    }
}