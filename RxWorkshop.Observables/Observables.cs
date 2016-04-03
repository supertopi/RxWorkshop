using System;
using System.Linq;
using System.Reactive.Linq;


namespace RxWorkshop
{
    public class WorkshopObservables
    {

        /// <summary>
        /// Produces 100 random integers 100 times
        /// </summary>
        /// <returns></returns>
        public static IObservable<int> RandomIntegers()
        {
            return Observable.Create<int>(obs =>
            {
                var rng = new Random();
                return Observable.Range(0, 100).Select(i => rng.Next(100)).Subscribe(obs);
            });
        }

        /// <summary>
        /// Produces alphabets from A to Z every second
        /// </summary>
        /// <returns></returns>
        public static IObservable<char> Alphabets()
        {
            return Observable.Interval(TimeSpan.FromSeconds(1))
                             .Select(i => (char)(i + 65))
                             .Take(26);
        }

        /// <summary>
        /// Produces random SalesItems from our "kesäkiska"
        /// </summary>
        /// <returns></returns>
        public static IObservable<SalesItem> SummerPOSSales()
        {
            return Observable.Create<SalesItem>(obs =>
            {
                var products = new SalesItem[]
                {
                    new SalesItem { Product = "CHEEZECAKE" , SalesPrice = 4.20, ManufacturingPrice = 2.10 },
                    new SalesItem { Product = "Coffee" , SalesPrice = 2.50, ManufacturingPrice = 0.8 },
                    new SalesItem { Product = "Soda" , SalesPrice = 1.50 , ManufacturingPrice = 1.00 }
                };

                var rng = new Random();
                return Observable.Range(0, 100).Select( i => products[ rng.Next(0,3)]).Subscribe(obs);
            });
        }

        public class SalesItem
        {
            public string Product;
            public double SalesPrice;
            public double ManufacturingPrice;
        } 

    }
}
