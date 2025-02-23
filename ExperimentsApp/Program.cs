using BinTreeLib;
using System.Diagnostics;


int n = 10000;
int[] array = new int[n];
AVLTree<int, int> bintree = new AVLTree<int, int>();

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

Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

foreach (var t in array)
{
    bintree.Add(t, 0);
}
foreach (var t in array)
{
    bintree.ContainsKey(t);
}
stopWatch.Stop();
Console.WriteLine("AVL Tree: {0}", stopWatch.ElapsedMilliseconds);

SortedDictionary<int, int> sortdict = new SortedDictionary<int, int>();

Stopwatch watch = new Stopwatch();
watch.Start();

foreach (var t in array)
{
    sortdict.Add(t, 0);
}
foreach (var t in array)
{
    sortdict.ContainsKey(t);
}

watch.Stop();
Console.WriteLine("Sorted Dictionary: {0}", watch.ElapsedMilliseconds);
Console.ReadKey();