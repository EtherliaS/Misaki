using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.Configuration
{
    public class BotConfig
    {
        /*
         * potential usage:
         * token (already)
         * music storage time (inf) -> app config (may be, idk)
         * ver -> app config
         * may be extended
         * also may be deprecated
         */
        public string token 
        {
            get => token;
            set
            {
                if (String.IsNullOrEmpty(value: value) || value.Length != 70)
                {
                    throw new ArgumentException("Token Length must be 70 characters!");
                }
                token = value;
            }
        }
        public BotConfig(string _token) { token = _token; }
        public BotConfig() { }

    }
}
