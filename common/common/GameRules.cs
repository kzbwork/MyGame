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
            if ((LastX + 4 <= 14) && 
                (chese[LastX,lastY] == chese[LastX + 1,lastY]) &&
                (chese[LastX, lastY] == chese[LastX + 2, lastY]) &&
                (chese[LastX, lastY] == chese[LastX + 3,lastY]) && 
                (chese[LastX, lastY] == chese[LastX + 4,lastY]))
            {
                return true;
            }
            if ((LastX + 3 <= 14) && 
                (LastX - 1 >= 0) && 
                (chese[LastX - 1,lastY] == chese[LastX,lastY]) &&
                (chese[LastX, lastY] == chese[LastX + 1, lastY]) &&
                (chese[LastX, lastY] == chese[LastX + 2,lastY]) &&
                (chese[LastX + 3,lastY]) == chese[LastX, lastY])
            {
                return true;
            }
            if ((LastX + 2 <= 14) && 
                (LastX - 2 >= 0) && 
                (chese[LastX, lastY] == chese[LastX - 1, lastY]) &&
                (chese[LastX - 2,lastY] == chese[LastX, lastY]) &&
                (chese[LastX,lastY] == chese[LastX + 1,lastY]) &&
                (chese[LastX + 2,lastY] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 1 <= 14) && 
                (LastX - 3 >= 0) && 
                (chese[LastX, lastY] == chese[LastX - 2, lastY]) &&
                (chese[LastX - 3,lastY] == chese[LastX, lastY]) &&
                (chese[LastX,lastY -1 ] == chese[LastX,lastY]) &&
                (chese[LastX + 1,lastY] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX - 4 >= 0) &&
                (chese[LastX, lastY] == chese[LastX - 4, lastY]) &&
                (chese[LastX, lastY] == chese[LastX - 3,lastY]) &&
                (chese[LastX - 2,lastY] == chese[LastX, lastY]) &&
                (chese[LastX,lastY] == chese[LastX - 1, lastY]))
            {
                return true;
            }
            //纵向
            if ((lastY + 4 <= 14) &&
                (chese[LastX,lastY] == chese[LastX,lastY + 1]) &&
                (chese[LastX, lastY] == chese[LastX, lastY + 2]) &&
                (chese[LastX, lastY] == chese[LastX,lastY + 3]) &&
                (chese[LastX,lastY + 4] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((lastY + 3 <= 14) && 
                (lastY - 1 >= 0) && 
                (chese[LastX,lastY - 1] == chese[LastX,lastY]) &&
                (chese[LastX, lastY] == chese[LastX, lastY + 1])&&
                (chese[LastX, lastY] == chese[LastX,lastY + 2]) &&
                (chese[LastX,lastY + 3] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((lastY + 2 <= 14) && 
                (lastY - 2 >= 0) && 
                (chese[LastX, lastY] == chese[LastX, lastY - 2]) &&
                (chese[LastX, lastY] == chese[LastX,lastY - 1]) &&
                (chese[LastX,lastY] == chese[LastX,lastY + 1]) &&
                (chese[LastX,lastY + 2] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((lastY + 1 <= 14) && 
                (lastY - 3 >= 0) && 
                (chese[LastX, lastY] == chese[LastX, lastY - 3])&&
                (chese[LastX, lastY] == chese[LastX,lastY - 2]) &&
                (chese[LastX,lastY - 1] == chese[LastX,lastY]) &&
                (chese[LastX,lastY + 1] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((lastY - 4 >= 0) && 
                (chese[LastX, lastY] == chese[LastX, lastY - 4]) &&
                (chese[LastX, lastY] == chese[LastX,lastY - 3]) &&
                (chese[LastX,lastY - 2] == chese[LastX, lastY]) &&
                (chese[LastX,lastY] == chese[LastX, lastY - 1]))
            {
                return true;
            }
            //斜向左到右
            if ((LastX + 4 <= 14) && 
                (lastY + 4 <= 14) && 
                (chese[LastX,lastY] == chese[LastX + 1,lastY + 1]) &&
                (chese[LastX, lastY] == chese[LastX + 2, lastY + 2]) &&
                (chese[LastX, lastY] == chese[LastX + 3,lastY + 3]) &&
                (chese[LastX + 4,lastY + 4] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 3 <= 14) && 
                (LastX - 1 >= 0) && 
                (lastY + 3 <= 14) && 
                (lastY - 1 >= 0) && 
                (chese[LastX - 1,lastY - 1] == chese[LastX,lastY]) &&
                (chese[LastX, lastY] == chese[LastX + 1, lastY + 1]) &&
                (chese[LastX, lastY] == chese[LastX + 2,lastY + 2]) &&
                (chese[LastX + 3,lastY + 3] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 2 <= 14) && 
                (LastX - 2 >= 0) && 
                (lastY + 2 <= 14) && 
                (lastY - 2 >= 0) && 
                (chese[LastX, lastY] == chese[LastX - 2, lastY - 2]) &&
                (chese[LastX, lastY] == chese[LastX - 1,lastY - 1]) &&
                (chese[LastX,lastY] == chese[LastX + 1,lastY + 1]) &&
                (chese[LastX + 2,lastY + 2] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 1 <= 14) && 
                (LastX - 3 >= 0) && 
                (lastY + 1 <= 14) && 
                (lastY - 3 >= 0) &&
                (chese[LastX, lastY] == chese[LastX - 3, lastY - 3]) &&
                (chese[LastX, lastY] == chese[LastX - 2,lastY - 2]) &&
                (chese[LastX - 1,lastY - 1] == chese[LastX,lastY]) &&
                (chese[LastX + 1,lastY + 1] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX - 4 >= 0) && 
                (lastY - 4 >= 0) && 
                (chese[LastX, lastY] == chese[LastX - 4, lastY - 4]) &&
                (chese[LastX, lastY] == chese[LastX - 3,lastY - 3]) &&
                (chese[LastX - 2,lastY - 2] == chese[LastX, lastY]) &&
                (chese[LastX,lastY] == chese[LastX - 1, lastY - 1]))
            {
                return true;
            }
            //斜向右到左
            if ((LastX - 4 >= 0) && 
                (lastY + 4 <= 14) && 
                (chese[LastX,lastY] == chese[LastX - 1,lastY + 1]) &&
                (chese[LastX,lastY] == chese[LastX - 2, lastY + 2]) &&
                (chese[LastX, lastY] == chese[LastX - 3,lastY + 3]) &&
                (chese[LastX - 4,lastY + 4] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 1 <= 14) && 
                (LastX - 3 >= 0) && 
                (lastY + 3 <= 14) && 
                (lastY - 1 >= 0) &&
                (chese[LastX + 1,lastY - 1] == chese[LastX,lastY]) &&
                (chese[LastX, lastY] == chese[LastX - 1, lastY + 1]) &&
                (chese[LastX, lastY] == chese[LastX - 2,lastY + 2]) &&
                (chese[LastX - 3,lastY + 3] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 2 <= 14) && 
                (LastX - 2 >= 0) && 
                (lastY + 2 <= 14) && 
                (lastY - 2 >= 0) && 
                (chese[LastX, lastY] == chese[LastX + 2, lastY - 2]) &&
                (chese[LastX, lastY] == chese[LastX + 1,lastY - 1]) &&
                (chese[LastX,lastY] == chese[LastX - 1,lastY + 1]) &&
                (chese[LastX - 2,lastY + 2] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 3 <= 14) && 
                (LastX - 1 >= 0) && 
                (lastY + 1 <= 14) && 
                (lastY - 3 >= 0) && 
                (chese[LastX, lastY] == chese[LastX + 3, lastY - 3]) &&
                (chese[LastX, lastY] == chese[LastX + 2,lastY - 2]) &&
                (chese[LastX + 1,lastY - 1] == chese[LastX,lastY]) &&
                (chese[LastX - 1,lastY + 1] == chese[LastX, lastY]))
            {
                return true;
            }
            if ((LastX + 4 <= 14) && 
                (lastY - 4 >= 0) && 
                (chese[LastX, lastY] == chese[LastX + 4, lastY - 4]) &&
                (chese[LastX, lastY] == chese[LastX + 3,lastY - 3]) &&
                (chese[LastX + 2,lastY - 2] == chese[LastX, lastY]) &&
                (chese[LastX,lastY] == chese[LastX + 1, lastY - 1]))
            {
                return true;
            }
            return false;
        }
    }
}
