using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace common.cs
{
    public class User
    {
        public User()
        {

        }

        public User(String ip,int winnum,int date,int choose)
        {
            this.UserIp = ip;
            this.UserWinNum = winnum;
            this.UserLessTime = date;
            this.UserChoose = choose;
        }
        /// <summary>
        /// 用户IP
        /// </summary>
        public String UserIp { set; get; }

        /// <summary>
        /// 用户胜场
        /// </summary>
        public int UserWinNum { set; get; }

        /// <summary>
        /// 用户剩余时长
        /// </summary>
        public int UserLessTime { set; get; }

        /// <summary>
        /// 用户选边
        /// </summary>
        public int UserChoose { set; get; }
    }
}