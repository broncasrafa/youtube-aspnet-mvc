using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace YouTubeAPI.Models
{
    public static class YouTubeHelpers
    {
        public static string RemoveIllegalPathCharacters(string path)
        {
            string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            var regex = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));

            return regex.Replace(path, "");
        }
    }
}