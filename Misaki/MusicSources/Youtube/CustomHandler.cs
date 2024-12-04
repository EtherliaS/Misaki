using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.MusicSources.Youtube
{
    public class CustomHandler
    {
        public HttpMessageHandler GetHandler()
        {
            CookieContainer cookieContainer = new CookieContainer();
            cookieContainer.Add(new Cookie("CONSENT", "YES+cb", "/", "youtube.com"));
            return new HttpClientHandler
            {
                UseCookies = true,
                CookieContainer = cookieContainer
            };

        }
    }
}
