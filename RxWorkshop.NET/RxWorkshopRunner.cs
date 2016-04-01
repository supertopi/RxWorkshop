﻿using System;
using System.Reactive.Linq;

namespace RxWorkshop.NET
{
    internal class RxWorkshopRunner
    {
        public RxWorkshopRunner()
        {
        }

        internal void Run()
        {
            Solutions();
            // Rx code goes here.
            // Use Program.Print to print.

            // ASSIGNMENTS

            // 1. Print the current datetime every second

            // 2. Observe Console key presses via Program.KeyPresses() and print them.

            // 3. Observe alphabets from A to Z with WorkShopObservables.Alphabets()
            //    Print whenever Program.KeyPresses() latest value is equal with the latest emited alphabet. 

            // 4. Observe WorkshopObservables.RandomIntegers() once.
            //    Print out the average of values.

            // 5. Observe WorkshopObservables.RandomIntegers 5 times.
            //    Print out how many times the average of values was over 50

            // 6. Observe our "kesäkiska" sales with WorkshopObservables.SummerPOSSales.
            //    Generate a profit report per product.

        }




        internal void Solutions()
        {
            //// SOLUTIONS

            // 1. Print the current datetime every second
            Observable.Interval( TimeSpan.FromSeconds(1) )
                      .Select( _ => DateTime.Now)
                      .Subscribe( Program.Print );


            // 2. Print console key presses  
            Program.KeyPresses()
                   .Select(i => i.ToString()) //ConsoleKey to string
                   .Subscribe( Program.Print );


            // 3. Observe alphabets from A to Z with WorkShopObservables.Alphabets()
            //    Print whenever Program.KeyPresses() latest value are equal with the latest alphabet.
            WorkshopObservables.Alphabets()
                               .Do(Program.Print) //helps out to see where we are going
                               .CombineLatest( Program.KeyPresses(), (alphabet, keyPress) => new { alphabet, keyPress })
                               .Where(comb => comb.alphabet.ToString() == comb.keyPress.ToString())
                               .Subscribe(i => Program.Print($"{i} was equal!"));


            //// 4. Observe WorkshopObservables.RandomIntegers() print out the average and maximum of produced values.
            WorkshopObservables.RandomIntegers()
                               .Average()
                               .Subscribe(Program.Print);


            //// 5. Observe WorkshopObservables.RandomIntegers() 5 times and Print out how many times the Average of emited values was over 50
            WorkshopObservables.RandomIntegers()
                   .Average()
                   .Do(Program.Print) // just to verify the results..
                   .Repeat(5)
                   .Where(i => i > 50)
                   .Count()
                   .Subscribe(i => Program.Print($"The average of 100 random integers between 0 and 100 was over 50 times {i} / 5 "));


            //6. Observe our "kesäkiska" sales with WorkshopObservables. Generate a profit report per product
            WorkshopObservables.SummerPOSSales()
                   .GroupBy(i => i.Product)
                   .SelectMany(group =>
                       group.ToList()
                            .Do(i => Program.Print($"Product {group.Key} sold {i.Count}")) //just to verify the results..
                            .Where(i => i.Count > 0)
                            .Select(g =>
                            {
                                var profit = g.Count * (g[0].SalesPrice - g[0].ManufacturingPrice);
                                return new { group.Key, profit };
                            }))
                   .Subscribe(i => Program.Print($"Product {i.Key} profited {i.profit}e"));
        }

    }
}