using Misaki.Python;
using Misaki.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Audio.Resources
{
    internal class Track
    {
        string Title;
        string Author;
        int Length;
        string Id;
        public Track(string id)
        {
            Id = id;
        }
        public async Task Download()
        {
            // Python download
            try
            {
                ScriptsManager.DownloadVideo(Id);
            }
            catch(Exception ex) { await Logger.Log("Failed to download " + Id, InfoSource.Python, InfoType.Error); }
            
        }
        public bool CheckFileExistence()
        {
            return File.Exists(Id);
        }
    }
}
