using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Gera
{

    public partial class Form1 : Form
    {
        Algorythm proc;
        public Form1()
        {
            InitializeComponent();
            proc = new Algorythm();
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Открывает файл </summary>
        ///
        /// <remarks>   Толя, 21.01.2016. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void OpenFile(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Файлы OrCAD (*.bom) | *.BOM";
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
            if (proc.OpenFile(openFileDialog1.FileName) == Status.failed)
            {
                button1.BackColor = Color.Red;
            }
            else
            {
                button1.BackColor = Color.LawnGreen;
            }
        }

        ///-------------------------------------------------------------------------------------------------
        /// <summary>   Выбор директории </summary>
        ///
        /// <remarks>   Толя, 21.01.2016. </remarks>
        ///
        /// <param name="sender">   Source of the event. </param>
        /// <param name="e">        Event information. </param>
        ///-------------------------------------------------------------------------------------------------

        private void ChooseDirectory(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();

            textBox2.Text = folderBrowserDialog1.SelectedPath;
            Status tmpStatus;
            tmpStatus = proc.OpenDirectory(folderBrowserDialog1.SelectedPath);

            switch (tmpStatus)
            {
                case Status.unauth_access:  button2.BackColor = Color.Yellow;
                                            MessageBox.Show("НСД, чувак, ты попал, ФСБ выехали за тобой...");
                                            break;
                case Status.failed:         button2.BackColor = Color.Red;
                                            break;
                case Status.success:        button2.BackColor = Color.LawnGreen;
                                            break; 
            }  
            
        }

        private void LaunchProc(object sender, EventArgs e)
        {
            proc.Start();
        }
    }
}
