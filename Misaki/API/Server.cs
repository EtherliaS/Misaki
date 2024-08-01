using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Misaki.API
{
    internal class Server
    {
        int Port;
        string Host;
        public Server(string host, int port)
        {
            Host = host;
            Port = port;
            //ipPoint = new IPEndPoint(IPAddress., 8888);
        }

        async Task ProcessClientAsync(TcpClient tcpClient)
        {
            NetworkStream stream = tcpClient.GetStream();
            var response = new List<byte>();
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            while (true)
            {
                // считываем данные до конечного символа
                while ((bytesRead = stream.ReadByte()) != '\n')
                {
                    // добавляем в буфер
                    response.Add((byte)bytesRead);
                }
                var word = Encoding.UTF8.GetString(response.ToArray());

                // если прислан маркер окончания взаимодействия,
                // выходим из цикла и завершаем взаимодействие с клиентом
                if (word == "END") break;

                Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} запросил перевод слова {word}");
                // находим слово в словаре и отправляем обратно клиенту
                //if (!words.TryGetValue(word, out var translation)) translation = "не найдено в словаре";
                // добавляем символ окончания сообщения 
                //translation += '\n';
                // отправляем перевод слова из словаря
                await stream.WriteAsync(Encoding.UTF8.GetBytes("translation"));
                response.Clear();
            }
            tcpClient.Close();
        }


    }
}
