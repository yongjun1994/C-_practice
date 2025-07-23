using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Puyopuyo
{
    public partial class Form1 : Form
    {
        // 필드를 정의하는 배열 : 0이면 빈칸, 1~5까지 숫자 입력
        int[,] field = new int[12, 6];

        // 인접한 숫자를 찾는 동안 숫자의 배열 위치를 저장할 클래스
        public class Track
        {
            public int x { get; set; }  // 배열 인덱스의 i, field[i,j]
            public int y { get; set; }  // 배열 인덱스의 j, field[i,j]
        }

        Track[] track = new Track[10];

        public Form1()
        {
            InitializeComponent();

            // field 배열을 0으로 초기화
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    field[i, j] = 0;
                    textBox1.Text += field[i, j].ToString();
                }
                textBox1.Text += Environment.NewLine;
            }

            // 1~5까지 숫자 중 2개를 random 으로 만들어서 배열의 [0,3] [0,4] 자리에 위치

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
