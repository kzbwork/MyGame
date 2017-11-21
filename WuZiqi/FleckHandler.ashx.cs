using Fleck;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebSockets;

namespace WuZiqi
{
    /// <summary>
    /// FleckHandler 的摘要说明
    /// </summary>
    public class FleckHandler : IHttpHandler
    {

        public static ConcurrentDictionary<String, String> users = new ConcurrentDictionary<String, String>();

        private static bool IsCreate = false;

        private object LockLogin = new object();
        public void CreateServer()
        {
            lock (LockLogin)
            {
                IsCreate = true;
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
                        if (allSockets.Contains(socket))
                        {
                            allSockets.ToList().ForEach(s => {
                                if (users.ContainsKey(socket.ConnectionInfo.ClientIpAddress))
                                {
                                    s.Send(socket.ConnectionInfo.ClientIpAddress + ":" + message);
                                }
                            });
                        }
                    };
                });
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            String name = context.Request["name"];
            if (!String.IsNullOrEmpty(name))
            {
                if (context.Request.UserHostAddress == "::1")
                {
                    users.TryAdd("192.168.157.166", name);
                }
                else
                {
                    users.TryAdd(context.Request.UserHostAddress, name);
                }
            }
            if (IsCreate == false)
            {
                CreateServer();
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}