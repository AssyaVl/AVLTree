using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BinTreeLib;
public class Program
{
    public static void Main(string[] args)
    {
        int n = 10000;
        int[] array = new int[n];
        AVLTree<int, int> avltree = new AVLTree<int, int>();
        SortedDictionary<int, int> sortdict = new SortedDictionary<int, int>();
        var watch = new Stopwatch();

        // Генерация массива случайных уникальных чисел
        Random randNum = new Random();
        for (int i = 0; i < array.Length; i++)
        {
            bool flag = true;
            while (flag)
            {
                int randInt = randNum.Next(0, 3 * n);
                if (!array.Contains(randInt))
                {
                    array[i] = randInt;
                    flag = false;
                }
            }
        }

        #region AVL Tree

        Console.WriteLine("AVLTree Insert");

        watch.Restart();
        // Вставка элементов в AVLTree
        foreach (var t in array)
        {
            avltree.Add(t, 0);
        }
        watch.Stop();
        Console.WriteLine($"Time: {watch.Elapsed}");

        Console.WriteLine("\n============\n");


        Console.WriteLine("AVLTree Remove");

        watch.Restart();
        // Удаление элементов с индексами от 5000 до 7000
        for (int i = 5000; i < 7000; i++)
        {
            avltree.Remove(array[i]);
        }
        watch.Stop();
        Console.WriteLine($"Time: {watch.Elapsed}");

        Console.WriteLine("\n============\n");

        Console.WriteLine("AVLTree Contains");

        watch.Restart();
        // Поиск всех элементов в AVLTree
        foreach (var t in array)
        {
            avltree.ContainsKey(t);
        }
        watch.Stop();
        Console.WriteLine($"Time: {watch.Elapsed}");

        Console.WriteLine("\n============\n");

        #endregion

        #region Sorted Dictionary

        Console.WriteLine("SortedDictionary Insert");

        watch.Restart();
        // Вставка элементов в AVLTree
        foreach (var t in array)
        {
            sortdict.Add(t, 0);
        }
        watch.Stop();
        Console.WriteLine($"Time: {watch.Elapsed}");

        Console.WriteLine("\n============\n");


        Console.WriteLine("SortedDictionary Remove");

        watch.Restart();
        // Удаление элементов с индексами от 5000 до 7000
        for (int i = 5000; i < 7000; i++)
        {
            sortdict.Remove(array[i]);
        }
        watch.Stop();
        Console.WriteLine($"Time: {watch.Elapsed}");

        Console.WriteLine("\n============\n");

        Console.WriteLine("SortedDictionary Contains");

        watch.Restart();
        // Поиск всех элементов в AVLTree
        foreach (var t in array)
        {
            sortdict.ContainsKey(t);
        }
        watch.Stop();
        Console.WriteLine($"Time: {watch.Elapsed}");

        Console.WriteLine("\n============\n");

        #endregion
    }
}