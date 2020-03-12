using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    public class TypeHelper
    {
        public static byte[] hex2Bytes(string hex) 
        {
            byte[] raw = new byte[hex.Length / 2];

            for (int i = 0; i < raw.Length; i++)
            {
                try
                {
                    raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                } 
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return raw;
        }

        public static byte[] string2Bytes(string data)
        {
            return Encoding.Default.GetBytes(data);
        }

        public static string hex2String(string hex)
        {
            byte[] raw = hex2Bytes(hex);

            return Encoding.Default.GetString(raw); 
        }

        public static string bytes2Hex(byte[] raw)
        {
            return BitConverter.ToString(raw);
        }

        public static string bytes2String(byte[] raw)
        {
            return System.Text.Encoding.Default.GetString(raw);
        }
    }
}
