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
            if (BaseFleck.IsCreateServer == false)
            {
                //实例化server类
                BaseFleck b = new BaseFleck();
            }

            if (ip.Equals("::1"))
            {
                ip = "192.168.157.166";
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
                mUserList.TryAdd(ip, user);
                return this.SetRespMsg(new Dictionary<String, String>()
                {
                    { "type","frist" },
                    { "ip", ip},
                    { "winnum","0"},
                    { "lesstime","180"},
                    { "choose","1"}
                }, true);
            }

            //当没有人时
            if (mUserList.Count == 1)
            {
                User user = new User(ip, 0, 180, 2);
                mUserList.TryAdd(ip, user);
                return this.SetRespMsg(new Dictionary<String, String>()
                {
                    { "type","second"},
                    { "ip", ip},
                    { "winnum","0"},
                    { "lesstime","180"},
                    { "choose","2"}
                }, true);
            }

            //if (this.mUserList.ContainsKey(ip))
            //{
            //    User user = new User();
            //    this.mUserList.TryGetValue(ip, out user);
            //    return this.SetRespMsg(new Dictionary<String, String>()
            //        {
            //            { "type","reback" },
            //            { "ip", user.UserIp},
            //            { "winnum",user.UserWinNum.ToString()},
            //            { "lesstime",user.UserLessTime.ToString()},
            //            { "choose",user.UserChoose.ToString()}
            //        }, true);
            //}
            return this.SetRespMsg("ok", true);
        }
    }
}