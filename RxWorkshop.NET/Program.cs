using System;
using System.Linq;
using System.Reactive.Linq;

namespace RxWorkshop.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            new RxWorkshopRunner().Run();
            new Program().GenerateKeyPressEvents();
        }


        /// <summary>
        /// KeyPress events to Observable from the console
        /// </summary>
        /// <returns></returns>
        public static IObservable<string> KeyPresses()
        {
            return Observable.FromEventPattern<KeyPressEventHandler, KeyPressEventArgs>(
                k => KeyPressed += k, k => KeyPressed -= k)
                .Select(i => i.EventArgs.PressedKey.ToString());
        }


        /// <summary>
        /// Damn it's hard to get key events from the console..
        /// </summary>
        void GenerateKeyPressEvents()
        {
            var key = ConsoleKey.A;
            do
            {
                while (!Console.KeyAvailable)
                {
                }
                key = Console.ReadKey(true).Key;

                OnKeyPress(new KeyPressEventArgs()
                {
                    PressedKey = key
                });

            } while (key != ConsoleKey.Escape);
        }

        public delegate void KeyPressEventHandler(object sender, KeyPressEventArgs e);
        static public event KeyPressEventHandler KeyPressed;

        public void OnKeyPress(KeyPressEventArgs e)
        {
            KeyPressed?.Invoke(this, e);
        }
        public class KeyPressEventArgs : EventArgs
        {
            public ConsoleKey PressedKey;
        }

        //Print print and more print
        public static void Print(string i)
        {
            Console.WriteLine(i);
        }

        public static void Print(int i)
        {
            Console.WriteLine(i.ToString());
        }

        public static void Print(long i)
        {
            Console.WriteLine(i.ToString());
        }

        public static void Print(DateTime i)
        {
            Console.WriteLine(i.ToString());
        }

        public static void Print(double i)
        {
            Console.WriteLine(i.ToString());
        }

        public static void Print(char i)
        {
            Console.WriteLine(i.ToString());
        }

        public static void Print(object i)
        {
            Console.WriteLine(i.ToString());
        }
    }
}
