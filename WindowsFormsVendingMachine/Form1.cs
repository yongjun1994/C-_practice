using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public class Menu
        {
            private int Cnt;    // 각 메뉴의 주문 개수
            private int Sales;  // 각 메뉴의 매출
            public int Price { get; set; }

            public static int TotalCnt = 0;
            public static int TotalSales = 0;

            public Menu()
            {
                Cnt = 0;
                Sales = 0;
            }

            public void IncCnt()
            {
                Cnt++;
                TotalCnt++;
            }

            public void IncSales()
            {
                Sales += Price;
                TotalSales += Price;
            }

            public int GetCnt() => Cnt;
            public int GetSales() => Sales;

            public static int GetTotalCnt() => TotalCnt;
            public static int GetTotalSales() => TotalSales;
        }

        public class ComboMenu : Menu
        {
            public int ComboPrice { get; set; }
            public int ComboSales { get; private set; }

            public void IncComboSales()
            {
                ComboSales += ComboPrice;
            }

            public int GetComboSales()
            {
                return ComboSales;
            }
        }

        ComboMenu burger;
        ComboMenu pizza;
        Menu drink;

        public Form1()
        {
            InitializeComponent();

            burger = new ComboMenu();
            pizza = new ComboMenu();
            drink = new Menu();

            burger.Price = 5000;
            pizza.Price = 10000;
            drink.Price = 2000;

            burger.ComboPrice = 1000;
            pizza.ComboPrice = 5000;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = $"햄버거를 주문하셨습니다. 가격은 {burger.Price}원 입니다.";
            burger.IncCnt();
            burger.IncSales();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = $"피자를 주문하셨습니다. 가격은 {pizza.Price}원 입니다.";
            pizza.IncCnt();
            pizza.IncSales();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = $"음료를 주문하셨습니다. 가격은 {drink.Price}원 입니다.";
            drink.IncCnt();
            drink.IncSales();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text =
                $"햄버거 : {burger.GetCnt()}개, {burger.GetSales()}원{Environment.NewLine}" +
                $"피자 : {pizza.GetCnt()}개, {pizza.GetSales()}원{Environment.NewLine}" +
                $"음료 : {drink.GetCnt()}개, {drink.GetSales()}원{Environment.NewLine}" +
                $"총 판매량 : {Menu.GetTotalCnt()}개{Environment.NewLine}" +
                $"총 매출 : {Menu.GetTotalSales()}원{Environment.NewLine}" +
                $"햄버거 추가 판매 : {burger.GetComboSales()}원{Environment.NewLine}" +
                $"피자 추가 판매 : {pizza.GetComboSales()}원";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            burger.InComboSales();
            textBox1.Text += Environment.NewLine + $"치즈를 추가했습니다. {burger.ComboPrice}원 추가됩니다.";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pizza.InComboSales();
            textBox1.Text += Environment.NewLine + $"페페로니를 추가했습니다. {pizza.ComboPrice}원 추가됩니다.";
        }
    }
}
