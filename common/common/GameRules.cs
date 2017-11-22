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
        public static bool CheckWuZi(int[,] chese, int LastX, int lastY, int type)
        {
            //横向
            if ((chese[LastX,lastY] + chese[LastX + 1,lastY] + chese[LastX + 2,lastY] + chese[LastX + 3,lastY] + chese[LastX + 4,lastY]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 1,lastY] + chese[LastX,lastY] + chese[LastX + 1,lastY] + chese[LastX + 2,lastY] + chese[LastX + 3,lastY]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 2,lastY] + chese[LastX - 1,lastY] + chese[LastX,lastY] + chese[LastX + 1,lastY] + chese[LastX + 2,lastY]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 3,lastY] + chese[LastX - 2,lastY] + chese[LastX,lastY -1 ] + chese[LastX,lastY] + chese[LastX + 1,lastY]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 4,lastY] + chese[LastX - 3,lastY] + chese[LastX - 2,lastY] + chese[LastX - 1,lastY] + chese[LastX,lastY]) / type == 5)
            {
                return true;
            }
            //纵向
            if ((chese[LastX,lastY] + chese[LastX,lastY + 1] + chese[LastX,lastY + 2] + chese[LastX,lastY + 3] + chese[LastX,lastY + 4]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX,lastY - 1] + chese[LastX,lastY] + chese[LastX,lastY + 1] + chese[LastX,lastY + 2] + chese[LastX,lastY + 3]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX,lastY - 2] + chese[LastX,lastY - 1] + chese[LastX,lastY] + chese[LastX,lastY + 1] + chese[LastX,lastY + 2]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX,lastY - 3] + chese[LastX,lastY - 2] + chese[LastX,lastY - 1] + chese[LastX,lastY] + chese[LastX,lastY + 1]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX,lastY - 4] + chese[LastX,lastY - 3] + chese[LastX,lastY - 2] + chese[LastX,lastY - 1] + chese[LastX,lastY]) / type == 5)
            {
                return true;
            }
            //斜向左到右
            if ((chese[LastX,lastY] + chese[LastX + 1,lastY + 1] + chese[LastX + 2,lastY + 2] + chese[LastX + 3,lastY + 3] + chese[LastX + 4,lastY + 4]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 1,lastY - 1] + chese[LastX,lastY] + chese[LastX + 1,lastY + 1] + chese[LastX + 2,lastY + 2] + chese[LastX + 3,lastY + 3]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 2,lastY - 2] + chese[LastX - 1,lastY - 1] + chese[LastX,lastY] + chese[LastX + 1,lastY + 1] + chese[LastX + 2,lastY + 2]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 3,lastY - 3] + chese[LastX - 2,lastY - 2] + chese[LastX - 1,lastY - 1] + chese[LastX,lastY] + chese[LastX + 1,lastY + 1]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX - 4,lastY - 4] + chese[LastX - 3,lastY - 3] + chese[LastX - 2,lastY - 2] + chese[LastX - 1,lastY - 1] + chese[LastX,lastY]) / type == 5)
            {
                return true;
            }
            //斜向右到左
            if ((chese[LastX,lastY] + chese[LastX - 1,lastY + 1] + chese[LastX - 2,lastY + 2] + chese[LastX - 3,lastY + 3] + chese[LastX - 4,lastY + 4]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX + 1,lastY - 1] + chese[LastX,lastY] + chese[LastX - 1,lastY + 1] + chese[LastX - 2,lastY + 2] + chese[LastX - 3,lastY + 3]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX + 2,lastY - 2] + chese[LastX + 1,lastY - 1] + chese[LastX,lastY] + chese[LastX - 1,lastY + 1] + chese[LastX - 2,lastY + 2]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX + 3,lastY - 3] + chese[LastX + 2,lastY - 2] + chese[LastX + 1,lastY - 1] + chese[LastX,lastY] + chese[LastX - 1,lastY + 1]) / type == 5)
            {
                return true;
            }
            if ((chese[LastX + 4,lastY - 4] + chese[LastX + 3,lastY - 3] + chese[LastX + 2,lastY - 2] + chese[LastX + 1,lastY - 1] + chese[LastX,lastY]) / type == 5)
            {
                return true;
            }
            return false;
        }
    }
}
