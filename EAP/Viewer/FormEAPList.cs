using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP
{
    public partial class FormEAPList : Form, IListViewer
    {
        private IListController controller;

        public FormEAPList()
        {
            InitializeComponent();
        }

        public void setController(IListController controller)
        {
            this.controller = controller;
        }

        public void updateEvent(object sender, EAPListEventArgs args)
        {
            //非创建线程调用UI,使用委托操作;
            if (this.InvokeRequired)
            { 
                try
                {
                    Invoke(new Action<Object, EAPListEventArgs>(updateEvent), sender, args);
                }
                catch (System.Exception)
                {
                    //disable form destroy exception
                }
                return;
            }

            //need modify
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("SLOW DOWN", "TEST");

            listViewEAPList.BeginUpdate();

            foreach (EAPListPojo pojo in args.pojos)
            {
                //need modify
                pojo.modifyContent(52, dic);

                ListViewItem item = new ListViewItem();

                item.UseItemStyleForSubItems = false;

                item.Text = pojo.address.ToString();
                item.SubItems.Add(pojo.content);

                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem();

                //合并EAP项处理：如1~6CYL滑油温度
                if (pojo.allInOne)
                {
                    if (pojo.isFirstOne)
                    {
                        subItem.Text = pojo.allInOneContent;
                    }
                    else 
                    {
                        subItem.Text = "";
                    }
                }
                else 
                {
                    subItem.Text = pojo.modifiedContent;
                }

                if (pojo.isModified())
                {
                    subItem.ForeColor = Color.SeaGreen;
                }

                if (pojo.needManual)
                {
                    subItem.ForeColor = Color.DarkRed;
                }

                item.SubItems.Add(subItem);

                listViewEAPList.Items.Add(item);
            }

            listViewEAPList.EndUpdate();
        }

        private void initList()
        {
            if (listViewEAPList.Columns.Count == 0)
            {
                listViewEAPList.Columns.Add("地址", 40, HorizontalAlignment.Center);
                listViewEAPList.Columns.Add("原内容", panel1.Width / 2 - 30, HorizontalAlignment.Left);
                listViewEAPList.Columns.Add("修正内容", panel1.Width / 2 - 30, HorizontalAlignment.Left);
            } 
            else
            {
                listViewEAPList.Columns[1].Width = panel1.Width / 2 - 30;
                listViewEAPList.Columns[2].Width = panel1.Width / 2 - 30;
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string path = "";

            dialog.Filter = "ACCESS数据库文件|*.mdb";
            dialog.InitialDirectory = "d:\\";
            dialog.RestoreDirectory = false;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = System.IO.Path.GetFullPath(dialog.FileName); 
            }

            if (path != "")
            {
                controller.update(path);
            } 
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            //初始化表头
            initList();
        }

        private void listViewEAPList_MouseClick(object sender, MouseEventArgs e)
        {
            //右键单击弹出菜单
            if (e.Button.Equals(MouseButtons.Right))
            {
                contextMenuStrip.Show(listViewEAPList, e.Location);
            }
        }

        //右键菜单编辑按下
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //右键菜单删除按下
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
