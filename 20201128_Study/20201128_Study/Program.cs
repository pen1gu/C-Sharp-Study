using System;

namespace _20201128_Study
{
    using System;
    using System.Collections;

    public class MyList
    {
        private int[] data = { 1, 2, 3, 4, 5 };

        public IEnumerator GetEnumerator()
        {
            int i = 0;
            while (i < data.Length)
            {
                yield return data[i];//외부호출자 리턴시 yield return이 하나 씩 return 됩니다.
                i++;
            }
        }
        //...
    }

    class Program
    {
        static void Main(string[] args)
        {
            // (1) foreach 사용하여 Iteration
            MyList list = new MyList();

            foreach (var item in list)
            {
                Console.WriteLine(item);//여기서 불러오는 부분?
            }

            // (2) 수동 Iteration
            IEnumerator it = list.GetEnumerator();
            it.MoveNext();
            Console.WriteLine(it.Current);  // 1
            it.MoveNext();
            Console.WriteLine(it.Current);  // 2
        }
    }
}
