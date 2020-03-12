using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public class ModbusHelper
    {
        public static byte[] getReadCmd(byte address, byte func, int reg, int count) 
        {
            byte[] cmd = new byte[8];

            cmd[0] = address;
            cmd[1] = func;
            cmd[2] = (byte)(reg / 256);
            cmd[3] = (byte)(reg % 256);
            cmd[4] = (byte)(count / 256);
            cmd[5] = (byte)(count % 256);

            UInt16 crc = CRCHelper.getCRC16(cmd, 0, 6);
            cmd[6] = (byte)(crc& 0xff);//lo
            cmd[7] = (byte)((crc >> 8) & 0xff);//hi

            return cmd;
        }

        /// <summary>
        /// 生成EAP发送byte[]: 0x66 英文, 0x67 中文
        /// </summary>
        /// <param name="address"></param>
        /// <param name="func"></param>
        /// <param name="reg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] getEapWriteCmd(byte address, bool isEN, int reg, byte[] data)
        {
            int count = data.Length;
            byte[] cmd = new byte[count + 8];

            cmd[0] = address;
            cmd[1] = isEN ? (byte)0x66 : (byte)0x67;
            cmd[2] = (byte)(reg / 256);
            cmd[3] = (byte)(reg % 256);
            cmd[4] = (byte)(count / 256);
            cmd[5] = (byte)(count % 256);

            for (int i = 0; i < data.Length; i++)
            {
                cmd[6 + i] = data[i];
            }

            UInt16 crc = CRCHelper.getCRC16(cmd, 0, cmd.Length - 2);
            cmd[cmd.Length - 1] = (byte)(crc & 0xff);//lo
            cmd[cmd.Length - 2] = (byte)((crc >> 8) & 0xff);//hi

            return cmd;
        }

        /// <summary>
        /// 生成读取byte[]
        /// </summary>
        /// <param name="address"></param>
        /// <param name="reg"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] getEapReadCmd(byte address, int reg, byte[] data)
        {
            byte[] cmd = new byte[8];

            cmd[0] = address;
            cmd[1] = (byte)0x69;
            cmd[2] = (byte)(reg / 256);
            cmd[3] = (byte)(reg % 256);
            cmd[4] = 0;
            cmd[5] = 0;

            UInt16 crc = CRCHelper.getCRC16(cmd, 0, cmd.Length - 2);
            cmd[6] = (byte)(crc & 0xff);//lo
            cmd[7] = (byte)((crc >> 8) & 0xff);//hi

            return cmd;
        }
    }
}
