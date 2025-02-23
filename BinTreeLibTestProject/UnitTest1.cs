using BinTreeLib;

namespace BinTreeLibTestProject
{
    public class UnitTest1
    {
        [Fact]
        public void CountIncreaseAfterAdding()
        {
            var tree = new AVLTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            Assert.Equal(n, tree.Count);
        }
        [Fact]
        public void ItemsExistAfterAdding()
        {
            var tree = new AVLTree<int, int>();
            var a = new[] { 22, 30, 15, 5, 17, 24, 33, 10, 16, 26 };
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                tree.Add(a[i], i);
            }
            Assert.Equal(n, tree.Count);
            Array.Sort(a);
            int j = 0;
            foreach (var pair in tree)
            {
                Assert.Equal(a[j], pair.Key);
                j++;
            }

        }

        [Fact]
        public void ContainsExistingElement()
        {
            var tree = new AVLTree<int, int>();
            var arrayInts = new[] { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
            foreach (var number in arrayInts)
            {
                tree.Add(number, 0);
            }

            foreach (var number in arrayInts)
            {
                Assert.True(tree.ContainsKey(number));
            }
        }

        [Fact]
        public void ContainsNotExistingElement()
        {
            var tree = new AVLTree<int, int>();
            var a = new[] { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
            for (int i = 0; i < a.Length; i++)
            {
                tree.Add(a[i], 0);
            }
            Assert.False(tree.ContainsKey(37));
        }
        [Fact]
        public void ConstructorFromOtherDictionaryWorks()
        {
            Dictionary<string, string> openWith =
            new Dictionary<string, string>
                (StringComparer.CurrentCultureIgnoreCase)
            {
                // Add some elements to the dictionary.
                { "txt", "notepad.exe" },
                { "Bmp", "paint.exe" },
                { "DIB", "paint.exe" },
                { "rtf", "wordpad.exe" }
            };
            AVLTree<string, string> copy =
                    new AVLTree<string, string>(openWith,
                        StringComparer.CurrentCultureIgnoreCase);
            Assert.True(openWith.Count == copy.Count);
            foreach(var pair in openWith)
            {
                Assert.True(copy.ContainsKey(pair.Key));
                Assert.True(copy.ContainsValue(pair.Value));
            }
        }
        [Fact]
        public void AddingExistingKeyThrowsException()
        {
            var tree = new AVLTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            Assert.Throws<ArgumentException>(()=> tree.Add(n-1, n-1));
        }
        [Fact]
        public void IfKeyNotFoundGetThrowsKeyNotFoundException()
        {
            var tree = new AVLTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            Assert.Throws<KeyNotFoundException>(() => tree[n + 1]);
        }
        [Fact]
        public void IfKeyNotFoundSetAddNewItem()
        {
            var tree = new AVLTree<int, int>();
            int n = 10;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, i);
            }
            tree[n] = n;
            Assert.True(tree.ContainsKey(n) );
            Assert.True(tree.ContainsValue(n));
        }
        [Fact]
        public void IfKeyEqualsNullThrowsArgumentNullException()
        {
            var tree = new AVLTree<string, int>();
            Assert.Throws<ArgumentNullException>(() => tree[null]=6);
        }
        [Fact]
        public void IsBalanced()
        {
            var tree = new AVLTree<int, int>();
            int n = 127;
            int expectedHeight = 7;
            for (int i = 0; i < n; i++)
            {
                tree.Add(i, 0);
            }
            Assert.Equal(tree.Height, 7);
        }
    }
}