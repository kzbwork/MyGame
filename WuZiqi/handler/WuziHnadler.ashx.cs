using common.common;
using common.cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WuZiqi
{
    /// <summary>
    /// WuziHnadler 的摘要说明
    /// </summary>
    public class WuziHnadler : BaseResp
    {
        public override bool Handler(HttpContext context)
        {
            string action = context.Request["action"];
            string ip = context.Request.UserHostAddress;
            if (ip.Equals("::1"))
            {
                ip = "192.168.157.166";
            }
            if (action.Equals("join"))
            {
                if (BaseResp.IsCreateServer == false)
                {
                    //创建socket
                    this.BaseFleck();
                }

                //当已有两人时返回不允许加入
                if (mUserList.Count == 2 && !mUserList.ContainsKey(ip))
                {
                    return this.SetRespMsg("people enough");
                }

                //当没有人时
                if (mUserList.Count == 0)
                {
                    User user = new User(ip, 0, 180, 1);
                    if (mUserList.TryAdd(ip, user))
                    {
                        return this.SetRespMsg(new Dictionary<String, String>()
                        {
                            { "type","frist" },
                            { "ip", ip},
                            { "winnum","0"},
                            { "lesstime","180"},
                            { "choose","1"}
                        }, true);
                    }
                    else
                    {
                        return this.SetRespMsg("false");
                    }
                }

                //当没有人时
                if (mUserList.Count == 1)
                {
                    User user = new User(ip, 0, 180, 2);
                    if (mUserList.TryAdd(ip, user))
                    {
                        return this.SetRespMsg(new Dictionary<String, String>()
                        {
                            { "type","second"},
                            { "ip", ip},
                            { "winnum","0"},
                            { "lesstime","180"},
                            { "choose","2"}
                        }, true);

                    }
                    else
                    {
                        return this.SetRespMsg("false");
                    }
                }
            }

            if (action.Equals("getduishou"))
            {
                if (!mUserList.ContainsKey(ip))
                {
                    return this.SetRespMsg("false");
                }

                string re = context.Request["re"];
                if (!String.IsNullOrEmpty(re))
                {
                    this.ResetChese();
                }

                foreach (var a in mUserList)
                {
                    if (!a.Key.Equals(ip))
                    {
                        return this.SetRespMsg(new Dictionary<String, String>()
                        {
                            { "ip", a.Value.UserIp},
                            { "winnum",a.Value.UserWinNum.ToString()},
                            { "lesstime",a.Value.UserLessTime.ToString()},
                            { "choose",a.Value.UserChoose.ToString()}
                        }, true);
                    }
                }
            }
           
            return this.SetRespMsg("false");
        }
    }
}