using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileOrganiser.Utils
{
    public class Video
    {
        public bool Keep { get; set; }
        public string Filename { get; set; }
        private readonly Uri FullPathUri;
        private readonly string FullPath;
        private bool Seen;
        public Video(string fullPath, string filename, bool keep)
        {
            FullPath = fullPath;
            FullPathUri = new Uri(fullPath);
            Filename = filename;
            Keep = keep;
        }
        public string GetFullPath()
        {
            return FullPath;
        }

        public Uri GetFullPathUri()
        {
            return FullPathUri;
        }

        public void MarkAsSeen()
        {
            Seen = true;
        }

        public bool AlreadySeen()
        {
            return Seen;
        }
    }
}
