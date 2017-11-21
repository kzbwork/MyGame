using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.common
{
    class BaseFleck
    {
        private static bool IsCreateServer = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        public BaseFleck()
        {
            if (!IsCreateServer)
            {
                IsCreateServer = true;
                var allSockets = new List<IWebSocketConnection>();
                FleckLog.Level = LogLevel.Debug;
                var server = new WebSocketServer("ws://0.0.0.0:7181");
                server.Start(socket =>
                {
                    socket.OnOpen = () =>
                    {
                        allSockets.Add(socket);
                    };
                    socket.OnClose = () =>
                    {
                        allSockets.Remove(socket);
                    };
                    socket.OnMessage = message =>
                    {
                        allSockets.ToList().ForEach(s => {
                            s.Send(message);
                        });
                    };
                });
            }
        }
    }
}
