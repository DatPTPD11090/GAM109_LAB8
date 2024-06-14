using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bài_1
{
    class Program
    {
        static Random random = new Random();
        static object lockobj = new object();
        static int randomNumber;
        static bool completed = false;

        static void Thread1()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (lockobj)
                {
                    randomNumber = random.Next(1,11);
                    Console.WriteLine($"Thread1: sinh so tu nhien:{ randomNumber}");
                    Monitor.Pulse(lockobj);
                    Monitor.Wait(lockobj);
                    
                }
                Thread.Sleep(2000);

            }
            completed = true;
        }
        static void Thread2()
        {
            for (int i = 0; i < 100; i++)
            {
                lock (lockobj)
                {
                    Monitor.Wait(lockobj);
                    int squaredNumber = randomNumber * randomNumber;
                    Console.WriteLine($"Thread2: Binh phuong cua so: { squaredNumber }");
                    Monitor.Pulse(lockobj);

                }
                Thread.Sleep(1000);


            }
        }
        static void Main()
        {
            Thread thread1 = new Thread(Thread1);
            Thread thread2 = new Thread(Thread2);

            thread1.Start();
            thread2.Start();
            while (!completed)
            {
                Thread.Sleep(100);

            }
            Console.WriteLine("Ket thuc chuong trinh");
        }
        
        
    }
}
