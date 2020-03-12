using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP
{
    public class CommonHelper
    {
        /// <summary>
        /// 等待 t 毫秒
        /// </summary>
        /// <param name="t"></param>
        public static void wait(long t)
        {
            long begin = DateTime.Now.Ticks / 10000 ;//1 Tick = 10^-7 s
            long end = 0, diff = 0;

            do
            {
                end = DateTime.Now.Ticks / 10000;
                diff = end - begin;
                Application.DoEvents();
            }
            while (diff < t);
        }

        /// <summary>
        /// 根据膨胀系数调整数组长度
        /// </summary>
        /// <param name="src"></param>
        /// <param name="offset"></param>
        /// <param name="coefficient"></param>
        /// <returns></returns>
        public static byte[] maybeExpand(byte[] src, int offset, float coefficient)
        {
            if (offset >= coefficient * src.Length)
            {
                byte[] dst = new byte[src.Length * 2];
                Array.Copy(src, 0, dst, 0, src.Length);

                return dst;
            }

            return src;
        }
    }
}
