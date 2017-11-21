using Fleck;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WuZiqi
{
    /// <summary>
    /// IndexHandler 的摘要说明
    /// </summary>
    public class IndexHandler : IHttpHandler
    {

        public static List<Dictionary<String, String>> users = new List<Dictionary<String, String>>();

        private static int playernum = 0;
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
                        if (users.Count >= 2)
                        {
                            return;
                        }

                        allSockets.Add(socket);
                        allSockets.ToList().ForEach(s => {
                            s.Send(fastJSON.JSON.ToJSON(users));
                        });
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
                                s.Send(fastJSON.JSON.ToJSON(users));
                            });
                        }
                    };
                });
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            String action = context.Request["action"];
            if (!String.IsNullOrEmpty(action))
            {
                if (action.Equals("join"))
                {
                    if (context.Request.UserHostAddress == "::1")
                    {
                        Dictionary<string, string> user = new Dictionary<string, string>()
                        {
                            {"ip", "192.168.157.166"},
                            {"num",playernum.ToString() }
                        };
                        users.Add(user);
                    }
                    else
                    {
                        Dictionary<string, string> user = new Dictionary<string, string>()
                        {
                            {"ip", context.Request.UserHostAddress},
                            {"num",playernum.ToString() }
                        };
                        users.Add(user);
                    }
                    
                    if (playernum == 0)
                    {
                        playernum = 1;
                    }
                    else
                    {
                        playernum = 0;
                    }
                }

                if (action.Equals("logout"))
                {
                    String key = String.Empty;
                    if (context.Request.UserHostAddress == "::1")
                    {
                        key = "192.168.157.166";
                    }
                    else
                    {
                        key = context.Request.UserHostAddress;
                    }
                    foreach (var a in users)
                    {
                        if (a.ContainsKey(key))
                        {
                            users.Remove(a);
                            break;
                        }
                    }
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