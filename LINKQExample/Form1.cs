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

namespace LINQExample
{
    public partial class Form1 : Form
    {
        class Book
        {
            public string Title { get; set; }
            public string Genre { get; set; }
        }

        // 샘플 데이터
        List<Book> books = new List<Book>
        {
            new Book { Title = "미움받을 용기", Genre = "자기계발" },
            new Book { Title = "데미안", Genre = "소설" },
            new Book { Title = "어린 왕자", Genre = "소설" },
            new Book { Title = "철학이 필요한 시간", Genre = "인문" },
            new Book { Title = "총 균 쇠", Genre = "인문" },
            new Book { Title = "아몬드", Genre = "소설" }
        };

        public Form1()
        {
            InitializeComponent();

            foreach (Book book in books)
            {
                textBox1.Text += book.Title;
                textBox1.Text += ", ";
                textBox1.Text += book.Genre;
                textBox1.Text += Environment.NewLine;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // IsAvailable 이 True 인 책만 textBox2에 출력
            var groupByGenre = from book in books
                               group book by book.Genre into genreGroup  // 그룹핑 결과를 저장하는 변수 : genreGroup
                               orderby genreGroup.Count() descending
                               select new { Genre = genreGroup.Key, Count = genreGroup.Count() };

            foreach (var group in groupByGenre)
            {
                /*
                textBox2.Text += book.Title;
                textBox2.Text += book.IsAvailable ? "True" : "False";
                textBox2.Text += Environment.NewLine;
                */

                textBox2.Text += $"{group.Genre} : {group.Count}권";
                //textBox2.Ttext += group.Genre + " : " + group.Count.ToString() + "권";
                textBox2.Text += Environment.NewLine;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //
        }
    }
}
