using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.common
{
    class GameRules
    {

        /// <summary>
        /// 五子棋判断输赢
        /// </summary>
        /// <param name="chese">当前棋盘</param>
        /// <param name="LastX">最后一手X</param>
        /// <param name="lastY">最后一手Y</param>
        /// <param name="type">黑、白</param>
        /// <returns></returns>
        public static bool CheckWuZi(int[][] chese, int LastX, int lastY,int type)
        {
            //横向
            for (int i = 0; i < 5; i++)
            {
                //当x为第一位时
                if(chese[LastX][lastY] == )
            }

            //纵向
            for (int i = 0; i < 5; i++)
            {

            }

            //斜向左到右
            for (int i = 0; i < 5; i++)
            {

            }

            //斜向右到左
            for (int i = 0; i < 5; i++)
            {

            }
        }
    }
}
