using common.cs;
using Fleck;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace common.common
{
    public abstract class BaseResp : IHttpAsyncHandler, IAsyncResult, IRequiresSessionState
    {
        /// <summary>
        /// 存储当前的用户
        /// </summary>
        protected static readonly ConcurrentDictionary<String, User> mUserList = new ConcurrentDictionary<String, User>();

        /// <summary>
        /// 存储当前的数组
        /// </summary>
        protected readonly static int [,] mChsesBag = new int[15, 15];
        /// <summary>
        /// 会话是否已经完成
        /// </summary>
        private bool mCompleted = false;

        /// <summary>
        /// 会话上下文
        /// </summary>
        protected HttpContext mContext = null;

        /// <summary>
        /// 请求返回消息
        /// </summary>
        private Object mRetObject;

        /// <summary>
        /// 请求是否被成功处理
        /// </summary>
        private bool mRespIsOk;

        /// <summary>
        /// 获取一个值，该值指示异步操作是否已完成
        /// </summary>
        bool IAsyncResult.IsCompleted { get { return this.mCompleted; } }

        /// <summary>
        /// 获取用于等待异步操作完成的 WaitHandle
        /// </summary>
        WaitHandle IAsyncResult.AsyncWaitHandle { get { return null; } }

        /// <summary>
        /// 获取用户定义的对象，它限定或包含关于异步操作的信息
        /// </summary>
        object IAsyncResult.AsyncState { get { return true; } }

        /// <summary>
        /// 获取一个值，该值指示异步操作是否同步完成
        /// </summary>
        bool IAsyncResult.CompletedSynchronously { get { return false; } }

        /// <summary>
        /// 通用http请求处理方法
        /// </summary>
        /// <param name="msg">处理完成后的消息返回</param>
        /// <returns>成功返回true，失败返回false</returns>
        public abstract bool Handler(HttpContext context);

        /// <summary>
        /// 消息回调
        /// </summary>
        /// <param name="msg"></param>
        protected virtual void Notify(String msg) { }

        protected void ResetChese()
        {
            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    mChsesBag[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// 设定返回消息类型
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        protected bool SetRespMsg(Object msg, bool ret = false)
        {
            this.mRetObject = msg;
            this.mRespIsOk = ret;
            return true;
        }

        /// <summary>
        /// 统一返回接口
        /// </summary>
        /// <param name="context"></param>
        /// <param name="time"></param>
        /// <param name="cb"></param>
        /// <param name="extraData"></param>
        private void SlefProcessRequest(HttpContext context)
        {      
            //通用应答消息
            String resp_msg = fastJSON.JSON.ToJSON(new Dictionary<string, object>()
            {
                { "status",this.mRespIsOk ? "ok":"error"},
                { "msg",this.mRetObject is Exception ?((Exception)this.mRetObject).Message:this.mRetObject },
            });

            //判断客户端是否允许进行压缩。能降低 30% 的带宽
            var enc = context.Request.Headers["Accept-Encoding"];
            if (!String.IsNullOrEmpty(enc) && enc.Contains("gzip"))
            {
                context.Response.AppendHeader("Content-Encoding", "gzip");
                GZipStream compressedzipStream = null;
                byte[] rawData = Encoding.UTF8.GetBytes(resp_msg);

                try
                {
                    compressedzipStream = new GZipStream(context.Response.OutputStream, CompressionMode.Compress, true);
                    compressedzipStream.Write(rawData, 0, rawData.Length);
                }
                finally
                {
                    compressedzipStream.Close();
                }
            }
            else
            {
                context.Response.Write(resp_msg);
            }
        }

        /// <summary>
        /// 启动对 HTTP 处理程序的异步调用。
        /// </summary>
        /// <param name="context">Http请求上下文</param>
        /// <param name="cb">异步完成的回调方法</param>
        /// <param name="extraData">附加数据</param>
        /// <returns></returns>
        public IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, Object extraData)
        {
            this.mContext = context;

            //统一返回                
            try
            {
                Handler(context);
            }
            catch (Exception ex)
            {
                this.mRespIsOk = false;
                this.mRetObject = ex;
            }
            finally
            {
                try
                {
                    this.SlefProcessRequest(context);
                }
                finally
                {
                    this.mCompleted = true;
                    cb(this);
                }
            }
            return this;
        }

        /// <summary>
        /// 进程结束时提供异步处理 End 方法。
        /// </summary>
        /// <param name="result">BeginProcessRequest结束之后的返回对象</param>
        public void EndProcessRequest(IAsyncResult result)
        {
        }

        /// <summary>
        /// 获取一个值，该值指示其他请求是否可以使用 IHttpHandler 实例。
        /// </summary>
        public bool IsReusable { get { return false; } }

        /// <summary>
        /// 通过实现 IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //使用异步操作时候，摒弃同步响应
            throw new InvalidOperationException();
        }

        public static bool IsCreateServer = false;
        /// <summary>
        /// 构造函数
        /// </summary>
        public void BaseFleck()
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
                        ResetChese();
                        allSockets.Add(socket);
                        if (allSockets.Count == 2)
                        {
                            allSockets.ToList().ForEach(s => {
                                s.Send("game start");
                                ResetChese();
                            });
                        }
                    };
                    socket.OnClose = () =>
                    {
                        allSockets.Remove(socket);
                    };
                    socket.OnMessage = message =>
                    {
                        try
                        {
                            var dct = fastJSON.JSON.ToObject<Dictionary<String, Object>>(message);
                            if (dct["task"].Equals("down"))
                            {
                                int x = Convert.ToInt32(dct["x"]);
                                int y = Convert.ToInt32(dct["y"]);
                                int type = Convert.ToInt32(dct["type"]);
                                mChsesBag[x, y] = type;
                                //修改当前二维数组
                                if (GameRules.CheckWuZi(mChsesBag, x, y, type))
                                {
                                    Dictionary<String, String> msg = new Dictionary<String, String>()
                                    {
                                        { "type",type.ToString()},
                                        { "is_win", "true"},
                                        { "x",x.ToString()},
                                        { "y",y.ToString()}
                                    };
                                    String resp_msg = fastJSON.JSON.ToJSON(new Dictionary<string, object>()
                                    {
                                        { "status","ok"},
                                        { "msg",msg},
                                    });
                                    allSockets.ToList().ForEach(s => {
                                        s.Send(resp_msg);
                                    });
                                }
                                else
                                {
                                    Dictionary<String, String> msg = new Dictionary<String, String>()
                                    {
                                        { "type",type.ToString()},
                                        { "is_win", "false"},
                                        { "x",x.ToString()},
                                        { "y",y.ToString()}
                                    };
                                    String resp_msg = fastJSON.JSON.ToJSON(new Dictionary<string, object>()
                                    {
                                        { "status","false"},
                                        { "msg",msg},
                                    });
                                    allSockets.ToList().ForEach(s => {
                                        s.Send(resp_msg);
                                    });
                                }
                            }
                        }
                        catch (Exception e){
                            string sql = e.ToString();
                        }
                       
                    };
                });
            }
        }
    }
}
