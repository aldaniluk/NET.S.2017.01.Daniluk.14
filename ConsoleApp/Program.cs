using System;
using Logic;
using System.Collections.Generic;
using System.Numerics;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Fibonacci
            //IEnumerable<BigInteger> numerator = Fibonacci.Generate(100);
            //foreach (var i in numerator)
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();
            #endregion

            #region Set
            //Set<Point> set1 = new Set<Point>();
            //Set<Point> set2 = new Set<Point>();
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine();
            ////add
            //set1.Add(new Point(1, 1));
            //set1.Add(new Point(2, 2));
            //set1.Add(new Point(3, 3));
            //set1.Add(new Point(4, 4));
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine();
            ////remove
            //set1.Remove(new Point(2, 2));
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine();
            //Console.WriteLine(set1.Contains(new Point(1, 1))); //true
            ////clear
            //set1.Clear();
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine(); //empty
            //set1.Add(new Point(1, 1));
            //set1.Add(new Point(2, 2));
            //set2.Add(new Point(1, 1));
            //set2.Add(new Point(2, 2));
            ////setequals
            //Console.WriteLine(set1.SetEquals(set2)); //true
            //Point[] points = new Point[4];
            ////copyto
            //set1.CopyTo(points, 0);
            //Console.WriteLine("Copy to: " + set1.Count);
            //foreach (var p in points)
            //{
            //    Console.WriteLine(p);
            //}

            //set2.Add(new Point(5, 5));
            //set2.Add(new Point(6, 6));
            ////intersect
            //set1.IntersectWith(set2);
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine();

            ////union
            //set1.UnionWith(set2);
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine();//1,2,5,6

            ////ExceptWith
            //set2 = new Set<Point> { new Point(1, 1) };
            //set1.ExceptWith(set2);
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine(); //6,2,5

            ////SymmetricExceptWith
            //set2 = new Set<Point> { new Point(1, 1), new Point(2, 2) };
            //set1.SymmetricExceptWith(set2);
            //foreach (var i in set1)
            //{
            //    Console.Write(i.ToString() + " ");
            //}
            //Console.WriteLine(); //6,1,5

            ////IsSubsetOf
            //set2 = new Set<Point> { new Point(1, 1), new Point(2, 2), new Point(5, 5), new Point(6, 6) };
            //Console.WriteLine(set1.IsSubsetOf(set2)); //true

            ////IsSupersetOf
            //Console.WriteLine(set2.IsSupersetOf(set1)); //true

            ////IsProperSupersetOf
            //Console.WriteLine(set2.IsProperSupersetOf(set1)); //true
            //set2 = new Set<Point> { new Point(1, 1), new Point(5, 5), new Point(6, 6) };
            //Console.WriteLine(set2.IsProperSupersetOf(set1)); //false

            ////IsProperSubsetOf
            //set2 = new Set<Point> { new Point(1, 1), new Point(2, 2), new Point(5, 5), new Point(6, 6) };
            //Console.WriteLine(set1.IsProperSubsetOf(set2)); //true
            //set2 = new Set<Point> { new Point(1, 1), new Point(5, 5), new Point(6, 6) };
            //Console.WriteLine(set1.IsProperSubsetOf(set2)); //false

            ////Overlaps
            //set2 = new Set<Point> { new Point(1, 1) };
            //Console.WriteLine(set1.Overlaps(set2)); //true
            //set2 = new Set<Point> { new Point(10, 10) };
            //Console.WriteLine(set1.Overlaps(set2)); //false


            //Set<string> sset1 = new Set<string>(new string[] {"1", "2", "3", "4"});
            //Set<string> sset2 = new Set<string>(new string[] { "1", "2", "5", "6" });

            //Set<string> ssetunion = Set<string>.Union(sset1, sset2);
            //foreach (var i in ssetunion)
            //{
            //    Console.Write(i.ToString() + " "); // 1 2 3 4 5 6
            //}
            //Console.WriteLine();

            //Set<string> ssetintersect = Set<string>.Intersect(sset1, sset2);
            //foreach (var i in ssetintersect)
            //{
            //    Console.Write(i.ToString() + " "); // 1 2
            //}
            //Console.WriteLine();

            //Set<string> ssetexcept = Set<string>.Except(sset1, sset2);
            //foreach (var i in ssetexcept)
            //{
            //    Console.Write(i.ToString() + " "); // 3 4
            //}
            //Console.WriteLine();

            //Set<string> ssetsymmexcept = Set<string>.SymmetricExcept(sset1, sset2);
            //foreach (var i in ssetsymmexcept)
            //{
            //    Console.Write(i.ToString() + " "); // 5 6
            //}
            //Console.WriteLine();

            #endregion

            #region Matrices
            //SquareMatrix<int> matr = new SquareMatrix<int>(2, new int[] { 1, 2, 3 });
            ////matr.ChangeElement(9, 2, 2); 
            //foreach(var i in matr)
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();
            ////Console.WriteLine(matr[2,2]);

            //SymmetricMatrix<int> smatr = new SymmetricMatrix<int>(3, new int[] { 1, 2, 3 });
            ////smatr.ChangeElement(9, 3, 2); //two elements change, because matrix is symmetric
            //foreach (var i in smatr)
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();
            ////Console.WriteLine(smatr[2, 3]); //==Console.WriteLine(smatr[3, 2]);

            //DiagonalMatrix<int> dmatr = new DiagonalMatrix<int>(4, new int[] { 1, 2, 3, 4 });
            ////dmatr.ChangeElement(9, 1, 2); //exception - we can't change an element
            //foreach (var i in dmatr)
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();
            //Console.WriteLine(dmatr[3,3]);

            ////event >
            //smatr.NewElement += dmatr.NewElementMessage; //diagonal matrix subscribes to the event
            //smatr.NewElement += matr.NewElementMessage; //square matrix subscribes to the event

            //smatr.ChangeElement(9, 1, 1);

            //smatr.NewElement -= matr.NewElementMessage; //square matrix unsubscribes to the event

            //smatr.ChangeElement(9, 2, 2);

            ////sum >
            //AbstractMatrix<int> matr1 = new SquareMatrix<int>(2, new int[] { 1, 2, 3, 4 });
            //AbstractMatrix<int> smatr1 = new SquareMatrix<int>(2, new int[] { 1, -2, -3, 4 });
            //AbstractMatrix<int> actualResult = matr1.Sum<int>(smatr1);

            //Console.WriteLine(actualResult.GetType()); //diagonal!!!
            //Console.WriteLine(actualResult);
            #endregion

            #region BinarySearchTree
            #region int BST
            //Console.WriteLine("INT");
            //BinarySearchTree<int> itree = new BinarySearchTree<int>(new int[] { -5, -6, 3, -20, -1, 10, -4, 2 });
            //foreach (var i in itree.PreorderTraversal())
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();

            //BinarySearchTree<int> itreeMyComparer = new BinarySearchTree<int>(new int[] { -5, -7, 3, -20, -1, 10, -4, 2 }, new Int32ComparerByAbs());
            ////foreach (var i in itreeMyComparer.PreorderTraversal())
            ////{
            ////    Console.WriteLine(i);
            ////}
            //itreeMyComparer.Add(21);
            ////foreach (var i in itreeMyComparer.PreorderTraversal())
            ////{
            ////    Console.WriteLine(i);
            ////}
            //itreeMyComparer.Remove(21);

            //Console.WriteLine("Preorder");
            //foreach (var i in itreeMyComparer.PreorderTraversal())
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();
            //Console.WriteLine("Inorder");
            //foreach (var i in itreeMyComparer.InorderTraversal())
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();
            //Console.WriteLine("Postorder");
            //foreach (var i in itreeMyComparer.PostorderTraversal())
            //{
            //    Console.Write(i + " ");
            //}
            //Console.WriteLine();

            #endregion

            #region string BST
            //Console.WriteLine("STRING");
            //BinarySearchTree<string> stree = new BinarySearchTree<string>(new string[] { "k", "x", "a", "b", "r", "c" });
            //foreach (var i in stree.PreorderTraversal())
            //{
            //    Console.Write(i + " "); //k a b c x r
            //}
            //Console.WriteLine();
            //stree.Add("z");
            //foreach (var i in stree.PreorderTraversal())
            //{
            //    Console.Write(i + " "); //k a b c x r z
            //}
            //Console.WriteLine();
            //stree.Remove("x");
            //foreach (var i in stree.PreorderTraversal())
            //{
            //    Console.Write(i + " "); //k a b c z r 
            //}
            //Console.WriteLine();

            //BinarySearchTree<string> streeMyComparer = new BinarySearchTree<string>(new string[] { "333", "1", "4444", "55555", "22" }, new Logic.StringComparerByLength());
            //foreach (var i in streeMyComparer.PreorderTraversal())
            //{
            //    Console.Write(i + " "); //333 1 22 4444 55555
            //}
            //Console.WriteLine();
            #endregion

            #region Book BST
            //Console.WriteLine("BOOK");
            //BinarySearchTree<Book> btree = new BinarySearchTree<Book>(new Book[] {
            //    new Book("Martin Eden", "London Jack", 1909),
            //    new Book("Harry Potter and the Philosopher's Stone", "Rowling Joanne", 1997),
            //    new Book("The Alpine Ballad", "Bykau Vasil", 1964),
            //    new Book("CLR via C#, Fourth Edition", "Richter Jeffrey", 2012),
            //    new Book("Alice's Adventures in Wonderland", "Carroll Lewis", 1865),
            //}); //default comparison (by author)

            //foreach (var i in btree.PreorderTraversal())
            //{
            //    Console.WriteLine(i); //London Bykau Carroll Rowling Richter
            //}
            //Console.WriteLine();

            //BinarySearchTree<Book> btreeMyComparer = new BinarySearchTree<Book>(new Book[] {
            //    new Book("Martin Eden", "London Jack", 1909),
            //    new Book("Harry Potter and the Philosopher's Stone", "Rowling Joanne", 1997),
            //    new Book("The Alpine Ballad", "Bykau Vasil", 1964),
            //    new Book("CLR via C#, Fourth Edition", "Richter Jeffrey", 2012),
            //    new Book("Alice's Adventures in Wonderland", "Carroll Lewis", 1865),
            //}, new BookComparerByYear()); //compare by year

            //foreach (var i in btreeMyComparer.PreorderTraversal())
            //{
            //    Console.WriteLine(i); //Carroll London Rowling Bykau Richter
            //}
            #endregion

            #region Point BST
            //Console.WriteLine("POINT");
            ////BinarySearchTree<PointStruct> ptree = new BinarySearchTree<PointStruct>(new PointStruct[] {
            ////    new PointStruct(4,4),
            ////    new PointStruct(1,1),
            ////    new PointStruct(3,3),
            ////    new PointStruct(5,5),
            ////    new PointStruct(2,2)
            ////});

            ////foreach (var i in ptree.PreorderTraversal())
            ////{
            ////    Console.WriteLine(i); // Exception!!!!!! because in thar structure there isn't default comparer
            ////}
            ////Console.WriteLine();

            //BinarySearchTree<PointStruct> ptreeMyComparer = new BinarySearchTree<PointStruct>(new PointStruct[] {
            //    new PointStruct(4,4),
            //    new PointStruct(1,1),
            //    new PointStruct(3,3),
            //    new PointStruct(5,5),
            //    new PointStruct(2,2)
            //}, new PointStructComparerByX());

            //foreach (var i in ptreeMyComparer.PreorderTraversal())
            //{
            //    Console.WriteLine(i); //4 1 3 2 5
            //}
            #endregion

            #endregion
        }
    }
}
