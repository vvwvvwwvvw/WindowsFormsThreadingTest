using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

            //Load 이벤트 선언
            this.Load += FormLoad_Event;
        }
        /// <summary>
        /// Form 로드 이벤트 핸들러
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormLoad_Event(object sender, EventArgs e)
        {
            // 스레드 선언
            Thread aThread = new Thread(new ThreadStart(A));
            Thread bThread = new Thread(new ThreadStart(B));
            Thread cThread = new Thread(new ThreadStart(C));

            // 스레드 시작
            aThread.Start();
            bThread.Start();
            cThread.Start();
        }
        private void A()
        {
            CrossThread(textBox1, 100, "A");
        }
        private void B()
        {
            CrossThread(textBox1, 20, "B");
        }
        private void C()
        {
           CrossThread(textBox1, 100, "C");
        }
        /// <summary>
        /// UI 쓰레드와의 충돌로 인해
        /// 크로스 스레드 방지 메서드
        /// </summary>
        /// <param name="item"></param>
        /// <param name="idx"></param>
        /// <param name="text"></param>
        private void CrossThread(Control item, int idx, string text)
        {
            if (item.InvokeRequired)
            {
                item.BeginInvoke(new MethodInvoker(delegate ()
                {
                    for (int i = 0; i < idx; i++)
                    {
                        item.Text += text;
                    }
                }));
            }
        }
    }
}
