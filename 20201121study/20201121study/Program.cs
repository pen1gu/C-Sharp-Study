using System;

namespace _20201121study
{
    [Flags]
    enum Border
    {
        None = 0,
        Top = 1,
        Right = 2,
        Bottom = 4,
        Left = 8
    }

    class Program
    {
        static void Main(string[] args)
        {
            // OR 연산자로 다중 플래그 할당
            Border b = Border.Top | Border.Bottom;

            // & 연산자로 플래그 체크
            if ((b & Border.Top) != 0)
            {
                //HasFlag()이용 플래그 체크
                if (b.HasFlag(Border.Top))
                {
                    // "Top, Bottom" 출력
                    Console.WriteLine(b);
                }
            }
        }
    }
}
