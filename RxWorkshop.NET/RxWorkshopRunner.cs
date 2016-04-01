using System;
using System.Reactive.Linq;

namespace RxWorkshop.NET
{
    internal class RxWorkshopRunner
    {

        /// <summary>
        ///  Rx code goes here.
        //   Use Program.Print to print.
        /// </summary>
        internal void Run()
        {
            Program.Print("Welcome to Rx Workshop!");

            // ASSIGNMENTS
            // 1. Print the current datetime every second by using Observable.Interval()


            // 2. Observe Console key presses via Program.KeyPresses() and print them.


            // 3. Observe WorkshopObservables.RandomIntegers() once.
            //    Print out the average of values.


            // 4. Observe WorkshopObservables.RandomIntegers() 5 times.
            //    Print out how many times the average of values was over 50


            // 5. Observe alphabets from A to Z with WorkShopObservables.Alphabets()
            //    Print whenever Program.KeyPresses() latest value is equal with the latest emited alphabet. 


            // 6. Observe our "kesäkiska" sales with WorkshopObservables.SummerPOSSales()
            //    Generate a profit report per product.


            // 7. Observe user input via Program.KeyPresses().
            //    When the user starts input, wait for 3 seconds of silence and then print all the given input.

        }




        internal void Solutions()
        {
            //// SOLUTIONS

            // 1. Print the current datetime every second using Observable.Interval()
            Observable.Interval( TimeSpan.FromSeconds(1) )
                      .Select( _ => DateTime.Now)
                      .Subscribe( Program.Print );


            // 2. Print console key presses  
            Program.KeyPresses()
                   .Select(i => i.ToString()) //ConsoleKey to string
                   .Subscribe( Program.Print );



            //// 3. Observe WorkshopObservables.RandomIntegers() print out the average and maximum of produced values.
            WorkshopObservables.RandomIntegers()
                               .Average()
                               .Subscribe(Program.Print);


            //// 4. Observe WorkshopObservables.RandomIntegers() 5 times and Print out how many times the Average of emited values was over 50
            WorkshopObservables.RandomIntegers()
                   .Average()
                   .Do(Program.Print) // just to verify the results..
                   .Repeat(5)
                   .Where(i => i > 50)
                   .Count()
                   .Subscribe(i => Program.Print($"The average of 100 random integers between 0 and 100 was over 50 times {i} / 5 "));


            // 5. Observe alphabets from A to Z with WorkShopObservables.Alphabets()
            //    Print whenever Program.KeyPresses() latest value are equal with the latest alphabet.
            WorkshopObservables.Alphabets()
                               .Do(Program.Print) //helps out to see where we are going
                               .CombineLatest(Program.KeyPresses(), (alphabet, keyPress) => new { alphabet, keyPress })
                               .Where(comb => comb.alphabet.ToString() == comb.keyPress.ToString())
                               .Subscribe(i => Program.Print($"{i} was equal!"));



            //6. Observe our "kesäkiska" sales with WorkshopObservables.SummerPOSSales().
            //   Generate a profit report per product
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


            // 7. Observe user input via Program.KeyPresses().
            //    When the user starts input, wait for 3 seconds of silence and then print all the given input.
            Program.KeyPresses().GroupByUntil(_ => true, g => g.Throttle(TimeSpan.FromSeconds(3)))
                                .SelectMany( g => g.Select(i => i.ToString()).ToList() )
                                .Select(i => string.Concat(i))
                                .Subscribe(Program.Print);

        }

    }
}