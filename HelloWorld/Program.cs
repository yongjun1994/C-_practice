using System; // 네임스페이스
using System.Buffers;
using static System.Console;    // 클래스

namespace HelloWorld
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //string str = " Hello World ";

            //WriteLine("str is --{0}--", str);   // str 출력

            //string trimmed = str.Trim();
            //WriteLine("str is --{0}--", trimmed); // trimed str 출력

            //string uppercaseStr = trimmed.ToUpper();
            //WriteLine("str is --{0}--", uppercaseStr);  // 대문자 str 출력

            //str = "" + str + "가나다라마";
            //WriteLine("str is --{0}--", str);   // 대문자 str 출력

            //int length = str.Length;
            //string substr = str.Substring(length-5, 5);
            //WriteLine("length = {0}, str is {1}", length, substr);  
            //WriteLine("str is --{0}{1}{2}{3}{4}--", str[20], str[21], str[22], str[23], str[24]);

            string str = "Hello World";  // Hello WORLD
            string frontSubstr = str.Substring(0, 6);
            string endSubstr = str.Substring(str.Length - 5, 5);
            string upperEndSubstr = endSubstr.ToUpper();

            Console.WriteLine("{0}", frontSubstr + upperEndSubstr);

            //WriteLine("str is --{0}--", str[0]);

            // overflow = uint 의 최대값
            //uint overflow = 4294967295;
            // overflow 를 그대로 출력
            //WriteLine("overflow is {0}", overflow);
            // overflow 변수를 overflow 상태로 만든다
            //overflow = overflow + 1;
            // overflow의 예상값 = ??
            //WriteLine("overflow is {0}", overflow);

            // 첫번째 출력문
            //WriteLine("Hello, World!");

            // 두번째 출력문
            /* 두번째 출력문
               hello world2를 콘솔에 출력하는 문장입니다. */
            //WriteLine("Hello, World2!!");
        }
    }
}
