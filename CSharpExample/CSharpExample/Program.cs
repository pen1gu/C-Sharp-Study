using System;

namespace CSharpExample
{
    public class Program
    {
        static void Main(string[] args)
        {
            Member member1 = new Member();
            Member member2 = new Member();

            
        }
    }

    public class Member
    {
        static int count = 50;

        void AddCount(int val)
        {
            count += val;
            Console.WriteLine(count);
        }
    }

}

