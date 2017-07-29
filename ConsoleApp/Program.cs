using System;
using Logic;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Fibonacci
            //IEnumerable<int> numerator = Fibonacci.Generate(8);
            //foreach(int i in numerator)
            //{
            //    Console.WriteLine(i);
            //}
            #endregion

            #region Set
            Set<Point> set1 = new Set<Point>();
            Set<Point> set2 = new Set<Point>();
            Console.WriteLine(set1);
            //add
            set1.Add(new Point(1, 1));
            set1.Add(new Point(2, 2));
            set1.Add(new Point(3, 3));
            set1.Add(new Point(4, 4));
            Console.WriteLine(set1);
            //remove
            set1.Remove(new Point(2, 2));
            Console.WriteLine(set1);
            Console.WriteLine(set1.Contains(new Point(1, 1))); //true
            //clear
            set1.Clear();
            Console.WriteLine(set1); //empty
            set1.Add(new Point(1, 1));
            set1.Add(new Point(2, 2));
            set2.Add(new Point(1, 1));
            set2.Add(new Point(2, 2));
            //setequals
            Console.WriteLine(set1.SetEquals(set2)); //true
            Point[] points = new Point[4];
            //copyto
            set1.CopyTo(points, 0);
            foreach (var p in points)
            {
                Console.WriteLine(p);
            }

            set2.Add(new Point(5, 5));
            set2.Add(new Point(6, 6));
            //intersect
            set1.IntersectWith(set2);
            Console.WriteLine(set1);

            //union
            set1.UnionWith(set2);
            Console.WriteLine(set1);//1,2,5,6

            //ExceptWith
            set2 = new Set<Point> { new Point(1, 1) };
            set1.ExceptWith(set2);
            Console.WriteLine(set1); //6,2,5

            //SymmetricExceptWith
            set2 = new Set<Point> { new Point(1, 1), new Point(2, 2) };
            set1.SymmetricExceptWith(set2);
            Console.WriteLine(set1); //6,1,5

            //IsSubsetOf
            set2 = new Set<Point> { new Point(1, 1), new Point(2, 2), new Point(5, 5), new Point(6, 6) };
            Console.WriteLine(set1.IsSubsetOf(set2)); //true

            //IsSupersetOf
            Console.WriteLine(set2.IsSupersetOf(set1)); //true

            //IsProperSupersetOf
            Console.WriteLine(set2.IsProperSupersetOf(set1)); //true
            set2 = new Set<Point> { new Point(1, 1), new Point(5, 5), new Point(6, 6) };
            Console.WriteLine(set2.IsProperSupersetOf(set1)); //false

            //IsProperSubsetOf
            set2 = new Set<Point> { new Point(1, 1), new Point(2, 2), new Point(5, 5), new Point(6, 6) };
            Console.WriteLine(set1.IsProperSubsetOf(set2)); //true
            set2 = new Set<Point> { new Point(1, 1), new Point(5, 5), new Point(6, 6) };
            Console.WriteLine(set1.IsProperSubsetOf(set2)); //false

            //Overlaps
            set2 = new Set<Point> { new Point(1, 1) };
            Console.WriteLine(set1.Overlaps(set2)); //true
            set2 = new Set<Point> { new Point(10, 10) };
            Console.WriteLine(set1.Overlaps(set2)); //false

            #endregion

            #region Matrices
            SquareMatrix<int> matr = new SquareMatrix<int>(2, new int[] { 1, 2, 3 });
            SquareMatrix<int> smatr = new SymmetricMatrix<int>(3, new int[] { 1 });
            SquareMatrix<int> dmatr = new DiagonalMatrix<int>(4, new int[]{ 1, 0, 0, 0, 0, 2, 0, 0, 0, 0, 3, 0, 0, 0, 0, 4 });

            smatr.NewElement += dmatr.NewElementMessage; //diagonal matrix subscribes to the event
            smatr.NewElement += matr.NewElementMessage; //square matrix subscribes to the event

            smatr.ChangeElement(9, 1, 1);
            
            smatr.NewElement -= matr.NewElementMessage; //square matrix unsubscribes to the event

            smatr.ChangeElement(9, 2, 2);
            Console.WriteLine(smatr);
            #endregion

        }
    }
}
