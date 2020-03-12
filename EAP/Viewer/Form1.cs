using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EAP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            init();
        }

        private void init()
        {
            customizeDesing();
        }

        private void customizeDesing()
        {
            panelEAPSubMenu.Visible = false;
            panelPortTestSubMenu.Visible = false;
            panelASubMenu.Visible = false;
        }

        private void hideSubMenu()
        {
            if (panelEAPSubMenu.Visible == true)
            {
                panelEAPSubMenu.Visible = false;
            }

            if (panelPortTestSubMenu.Visible == true)
            {
                panelPortTestSubMenu.Visible = false;
            }

            if (panelASubMenu.Visible == true)
            {
                panelASubMenu.Visible = false;
            }
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
                //Animation.ShowControl(subMenu, true, AnchorStyles.Top);
            }
            else 
            {
                subMenu.Visible = false;
            }
        }

        private Form activeForm = null;

        private void openChildForm(Form form)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
            }

            activeForm = form;

            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelChildForm.Controls.Add(form);
            panelChildForm.Tag = form;

            form.BringToFront();
            form.Show();
        }

        private void btnEAP_Click(object sender, EventArgs e)
        {
            showSubMenu(panelEAPSubMenu);
        }

        //EAP下载按钮按下
        private void button2_Click(object sender, EventArgs e)
        {
            FormEAP formEAP = new FormEAP();

            IComController controller = new ComController(formEAP, new ComModel());
            formEAP.setController(controller);

            openChildForm(formEAP);
            //hideSubMenu();
        }

        private void btnPortTest_Click(object sender, EventArgs e)
        {
            showSubMenu(panelPortTestSubMenu);
        }

        private void btnA_Click(object sender, EventArgs e)
        {
            showSubMenu(panelASubMenu);
        }

        //EAP列表页面按钮按下
        private void button3_Click(object sender, EventArgs e)
        {
            FormEAPList formEAPList = new FormEAPList();

            IListController listController = new ListController(formEAPList, new ListModel());
            formEAPList.setController(listController);

            openChildForm(formEAPList);
        }

        //logo按下
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (activeForm != null)
            {
                activeForm.Close();
                activeForm.Dispose();
            }
        }
    }
}
