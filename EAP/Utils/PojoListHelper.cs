using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAP
{
    /// <summary>
    /// index And content
    /// </summary>
    class Info 
    { 
        public int index { get; set; }
        public string content { get; set; }

        public Info(int index, string content)
        {
            this.index = index;
            this.content = content;
        }
    }

    public class PojoListHelper
    {
        /// <summary>
        /// 合并相同报警编号的内容
        /// </summary>
        /// <param name="pojos"></param>
        public static void merge(EAPListPojo[] pojos)
        {
            if (pojos == null)
            {
                return;
            }

            Dictionary<int, Info> addressAndInfo = new Dictionary<int, Info>();

            int index = 0;

            foreach(EAPListPojo pojo in pojos)
            {
                if (addressAndInfo.ContainsKey(pojo.address))
                {
                    pojo.allInOne = true;

                    //有空增加content合并规则
                    //pojos[addressAndInfo[pojo.address].index].allInOneContent = 
                }
                else 
                {
                    pojo.isFirstOne = true;
                    addressAndInfo.Add(pojo.address, new Info(index, pojo.content));
                }
            }
        }
    }
}
