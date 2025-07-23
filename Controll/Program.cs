using System;
using static System.Console;

namespace Control
{
    public class Kiosk
    {
        // 메뉴를 출력하는 매서드
        public void MenuPrint(string[] menu)
        {
            WriteLine("\\n====== 음식점 키오스크 ======");
            WriteLine("1. {0} 주문", menu[0]);
            WriteLine("2. {0} 주문", menu[1]);
            WriteLine("3. {0} 주문", menu[2]);
            WriteLine("4. 현재 주문 내역 확인");
            WriteLine("5. 종료");
            Write("메뉴를 선택하세요 (1~5): ");

        }

        // 메뉴를 주문하면 주문 ""를 주문하셨습니다를 출력하고
        // 주문한 메뉴의 개수를 하나씩 늘리는 매서드
        public void MenuOrder(string menu, ref int menuOrder)
        {
            WriteLine("{0}를 주문하셨습니다", menu);
            menuOrder++;
        }

        //4. 현재 주문내역 확인 을 선택하면 현재 주문 현황을 출력하는 매서드 : 정한
        public void CurrentOrderStatus(string[] menu, int[] menuOrder)
        {
            for (int i = 0; i < 3; i++)
            {
                WriteLine("{0} : {1}개", menu[i], menuOrder[i]);
            }
        }

        /* 태성
        public void CurrentOrderStatus(string menu, int menuOrder)
        {
            WriteLine("{0} : {1}개", menu, menuOrder);
        }
        */

        //5. 종료를 선택하면 주문 현황을 출력하는 매서드 : 정한
        public void ClosingOrder(string[] menu, int[] menuOrder)
        {
            WriteLine("최종 주문 마감 현황");
            for (int i = 0; i < 3; i++)
            {
                WriteLine("{0} : {1}개", menu[i], menuOrder[i]);
            }
        }

        /*  태성
        public void ClosingOrder(string menu, int menuOrder)
        {
            WriteLine("{0} : {1}개", menu, menuOrder);
        }
        */
    }


    internal class Program
    {
        static void Main(string[] args)
        {
            string[] menu = { "햄버거", "피자", "음료" };
            int[] menuTotal = { 0, 0, 0 };

            int running = 1;
            string inputMenu = "";
            // int i = 0;

            // Kiosk class 의 인스턴스를 선언
            Kiosk Kio = new Kiosk();

            do
            {
                // 메뉴를 출력
                Kio.MenuPrint(menu);

                // 메뉴를 입력받는다
                inputMenu = ReadLine();

                // 메뉴에 따라서 행동
                // 1,2,3 이면 카운트++
                // 4 이면 현황 출력
                // 5 이면 running 을 0 으로 만들어서 while 종료
                switch (inputMenu)
                {
                    case "1":
                        Kio.MenuOrder(menu[0], ref menuTotal[0]);
                        break;

                    case "2":
                        Kio.MenuOrder(menu[1], ref menuTotal[1]);
                        break;

                    case "3":
                        Kio.MenuOrder(menu[2], ref menuTotal[2]);
                        break;

                    case "4":
                        Kio.CurrentOrderStatus(menu, menuTotal);
                        /*
                        for (i = 0; i < 3; i++)
                        {
                            Kio.CurrentOrderStatus(menu[i], menuTotal[i]);
                        }
                        */
                        break;

                    case "5":
                        Kio.ClosingOrder(menu, menuTotal);
                        /*
                        WriteLine("최종 주문 마감 현황");
                        for (i = 0; i < 3; i++)
                        {
                            Kio.ClosingOrder(menu[i], menuTotal[i]);
                        }
                        */
                        running = 0;
                        break;
                }
            } while (running > 0);
        }
    }
}
