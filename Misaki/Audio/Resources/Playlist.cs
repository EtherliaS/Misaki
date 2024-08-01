using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Audio.Resources
{
    internal class Playlist
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Id;
        public List<Track> Tracks;
        public int Count;
        public Playlist()
        {
            Tracks = new List<Track>();
        }
        public Playlist(string name, string description) {  Name = name; Description = description; Tracks = new List<Track>(); }
        public Playlist(string id) { Id = id; Tracks = new List<Track>(); }
    }
}
