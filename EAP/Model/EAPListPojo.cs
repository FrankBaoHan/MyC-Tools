using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EAP
{
    public class EAPListPojo
    {
        public int address { get; set; }
        public string content { get; set; }

        public string modifiedContent { get; set; }
        public string allInOneContent { get; set; }

        public bool needManual  { get; set; }
        public bool allInOne { get; set; }
        public bool isFirstOne { get; set; }

        public EAPListPojo(int address, string content)
        {
            this.address = address;
            this.content = content;

            allInOne = false;
        }

        public void modifyContent(int limitedNumber, Dictionary<string, string> repalceDictionary)
        {
            modifiedContent = content.ToUpper().Trim();
            needManual = false;

            //去轮机号
            Regex regex = new Regex("^[0-9]{3,}");
            modifiedContent = regex.Replace(modifiedContent, "").Trim();

            if (modifiedContent.Length <= limitedNumber)
            {
                return;
            }

            //替换关键字
            foreach(var item in repalceDictionary)
            {
                modifiedContent = modifiedContent.Replace(item.Key, item.Value);
            }

            if (modifiedContent.Length > limitedNumber)
            {
                needManual = true;
            }
        }

        public bool isModified()
        {
            return !content.Equals(modifiedContent);
        }
    }
}
