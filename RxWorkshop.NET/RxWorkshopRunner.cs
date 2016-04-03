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
            Observable.Return(1)
                      .Subscribe( _ => Program.Print("Welcome to Rx Workshop!"));

            // ASSIGNMENTS
            // 1. Observe Console key presses via Program.KeyPresses() and print them.


            // 2. Print the current datetime every second.
            //    TIPS: Observable.Interval(), Observable.Select()


            // 3. Observe WorkshopObservables.RandomIntegers() once.
            //    Print out the average of values.
            //    TIPS: Observable.Average()

            // 4. Observe WorkshopObservables.RandomIntegers() once.
            //    Print out the average of values in groups of 10.
            //    TIPS: Observable.Window()


            // 5. Observe WorkshopObservables.RandomIntegers() 5 times.
            //    Print out how many times the average of values was over 50
            //    TIPS: Observable.Repeat() , Observable.Where() 


            // 6. Observe alphabets from A to Z with WorkShopObservables.Alphabets()
            //    Print whenever Program.KeyPresses() latest value is equal with the latest emited alphabet. 
            //    TIPS: Observable.CombineLatest()


            // 7. Observe our "kesäkiska" sales with WorkshopObservables.SummerPOSSales()
            //    Generate a profit report per product.
            //    TIPS: Observable.GroupBy() , Observable.ToList()


            // 8. Observe user input via Program.KeyPresses().
            //    When the user starts input, wait for 3 seconds of silence and then print all the given input.
            //    TIPS: Observable.GroupByUntil() , Observable.Throttle()


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
                   .Subscribe( Program.Print );


            //// 3. Observe WorkshopObservables.RandomIntegers() print out the average of produced values.
            WorkshopObservables.RandomIntegers()
                               .Average()
                               .Subscribe(Program.Print);

            // 4. Observe WorkshopObservables.RandomIntegers() once.
            //    Print out the average of values in groups of 10.
            //    TIPS: Observable.Window()
            WorkshopObservables.RandomIntegers()
                               .Window(10)
                               .Select(i => i.Average())
                               .Merge()
                               .Subscribe(Program.Print);


            // 5. Observe WorkshopObservables.RandomIntegers() 5 times and Print out how many times the Average of emited values was over 50
            WorkshopObservables.RandomIntegers()
                   .Average()
                   .Do(Program.Print) // just to verify the results..
                   .Repeat(5)
                   .Where(i => i > 50)
                   .Count()
                   .Subscribe(i => Program.Print($"The average of 100 random integers between 0 and 100 was over 50, times {i} / 5 "));


            // 6. Observe alphabets from A to Z with WorkShopObservables.Alphabets()
            //    Print whenever Program.KeyPresses() latest value are equal with the latest alphabet. 
            WorkshopObservables.Alphabets()
                               .Do(Program.Print) //helps out to see where we are going
                               .CombineLatest( Program.KeyPresses(), (alphabet, keyPress) => new { alphabet, keyPress })
                               .Where(comb => comb.alphabet.ToString() == comb.keyPress)
                               .Subscribe(i => Program.Print($"{i} was equal!"));



            //7. Observe our "kesäkiska" sales with WorkshopObservables.SummerPOSSales().
            //   Generate a profit report per product
            WorkshopObservables.SummerPOSSales()
                   .GroupBy(i => i.Product)
                   .SelectMany(group => 
                       group.ToList() //blocks until OnCompleted
                            .Do(i => Program.Print($"Product {group.Key} sold {i.Count}")) //just to verify the results..
                            .Where(i => i.Count > 0)
                            .Select( sales =>
                            {
                                var profit = sales.Count * (sales[0].SalesPrice - sales[0].ManufacturingPrice);  //since the Sales and Manufacturing costs are default
                                return new { group.Key, profit };
                            }))
                   .Subscribe(i => Program.Print($"Product {i.Key} profited {i.profit}e"));


            // 8. Observe user input via Program.KeyPresses().
            //    When the user starts input, wait for 3 seconds of silence and then print all the given input.
            Program.KeyPresses().GroupByUntil(_ => true, g => g.Throttle(TimeSpan.FromSeconds(3))) // New Rx version will include BufferUntil
                                .SelectMany( g => g.ToList() ) 
                                .Select(i => string.Concat(i))
                                .Subscribe(Program.Print);

        }

    }
}