using System.Collections;
using System.Xml.Linq;

namespace BinTreeLib
{
    /// <summary>
    /// АВЛ-Дерево
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class AVLTree<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>> 
    {
        #region Fields
        private IComparer<TKey> _comparer;
        private Node<TKey, TValue> _root; //корень
        #endregion

        #region Properties
        /// <summary>
        /// Количество узлов в дереве
        /// </summary>
        public int Count { get; private set; }
        #endregion

        #region Constructors
        public AVLTree(): this(null, Comparer<TKey>.Default)
        { }
        public AVLTree(IComparer<TKey> comparer):this(null, comparer)
        { }
        public AVLTree(IDictionary<TKey,TValue> dictionary):this(dictionary, Comparer<TKey>.Default)
        { }
        public AVLTree(IDictionary<TKey,TValue> dictionary, IComparer<TKey> comparer)
        {
            _comparer = comparer;
            Count = 0;
            _root = null;
            if (dictionary != null && dictionary.Count > 0)
            {
                foreach (var pair in dictionary)
                {
                    Add(pair.Key, pair.Value);
                }
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Добавление нового узла в дерево
        /// </summary>
        /// <param name="key">Ключ нового узла</param>
        /// <param name="value">Значение нового узла</param>
        /// <exception cref="ArgumentException"></exception>
        public void Add(TKey key, TValue value)
        {
            var node = new Node<TKey, TValue>(key, value);
            if (_root == null)
            {
                _root = node;
                Count++;
                return;
            }
            var current = _root;
            var parent = _root;
            int result;
            while (current != null)
            {
                parent = current;
                result = _comparer.Compare(current.Key, node.Key);
                // если новый узел равен текущему
                if (result == 0)
                {
                    throw new ArgumentException("Such key is already added ! ! !");
                }
                // если меньше текущего
                if (result > 0)
                {
                    current = current.Left;
                    continue;
                }
                // если больше текущего
                current = current.Right;
            }
            result = _comparer.Compare(parent.Key, node.Key);
            if (result > 0)
            {
                parent.Left = node;
            }
            if (result < 0)
            {
                parent.Right = node;
            }
            node.Parent = parent;
            Count++;
            UpdateHeights(node.Parent);
            // ДОПИСАТЬ:
            // 1. Балансировка дерева
            _root = Rebalancing(_root);

        }
        /// <summary>
        /// Пересчёт высот до корня дерева
        /// </summary>
        /// <param name="node">Вершина, с которой начинается пересчёт</param>
        private void UpdateHeights(Node<TKey, TValue> node)
        {
            while (node != null)
            {
                node.UpdateHeight();
                node = node.Parent;
            }
        }
        /// <summary>
        /// Удаление элемента из дерева по ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <exception cref="ArgumentException"></exception>
        public void Remove(TKey key)
        {
            //// ищем удаляемый элемент
            //var removingNode = Find(key);
            //// если не нашли
            //if (removingNode == null)
            //{
            //    throw new ArgumentException("Such key does not exist ! ! !");
            //}


            //почему я закоментила? я короче почитала тот сайт и там сказано что
            //метод удаления в процессе и ищет еще, так что find тут не нужен по идее :)


            _root = Remove(_root, key);
            if (_root != null)
            {
                _root = Rebalancing(_root); // Балансировка корня
            }
        }
        //удаление рекурсивно
        private Node<TKey, TValue> Remove(Node<TKey, TValue> node, TKey key)
        {
            if (node == null)
                return null; //узел не найден

            int compareResult = _comparer.Compare(key, node.Key);

            // Ищем узел для удаления
            if (compareResult < 0)
                node.Left = Remove(node.Left, key);
            else if (compareResult > 0)
                node.Right = Remove(node.Right, key);
            else
            {
                // Узел найден
                if (node.Right == null)
                {
                    // Если нет правого поддерева, заменяем на левое поддерево
                    Count--;
                    return node.Left;
                }

                // Находим минимальный узел в правом поддереве
                Node<TKey, TValue> minNode = FindMin(node.Right);
                // Удаляем минимальный узел из правого поддерева
                minNode.Right = RemoveMin(node.Right);
                // Подвешиваем левое поддерево удаляемого узла к минимальному узлу
                minNode.Left = node.Left;
                // Заменяем удаляемый узел на минимальный узел
                node = minNode;
                Count--;
            }
            // Обновляем высоту и балансируем дерево
            node.UpdateHeight();
            return Rebalancing(node);
        }
        //ищем минимальный элемент
        private Node<TKey, TValue> FindMin(Node<TKey, TValue> node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }
        //удаляем минимальный элемент
        private Node<TKey, TValue> RemoveMin(Node<TKey, TValue> node)
        {
            if (node.Left == null)
            {
                // Если левого поддерева нет, возвращаем правое поддерево
                return node.Right;
            }

            // Рекурсивно удаляем минимальный узел
            node.Left = RemoveMin(node.Left);

            // Обновляем высоту и балансируем дерево
            node.UpdateHeight();
            return Rebalancing(node);
        }
        /// <summary>
        /// Балансировка АВЛ-дерева
        /// </summary>
        /// <param name="node">Узел, с которого проверяем балансировку</param>
        private Node<TKey, TValue> Rebalancing(Node<TKey, TValue> node)
        {
            // определение баланса поддеревьев и выполнение поворотов
            // у класса Node есть метод BalanceFactor()

            if (node == null)
                return null;

            // Обновляем высоту текущего узла
            node.UpdateHeight();

            // Получаем баланс-фактор
            int balanceFactor = node.BalanceFactor();

            // Левый поворот
            if (balanceFactor > 1)
            {
                // Левый-левый поворот
                if (node.Left.BalanceFactor() >= 0)
                    return RightRotation(node);
                // Левый-правый поворот
                else
                    return LeftRightRotation(node);
            }

            // Правый поворот
            if (balanceFactor < -1)
            {
                // Правый-правый поворот
                if (node.Right.BalanceFactor() <= 0)
                    return LeftRotation(node);
                // Правый-левый поворот
                else
                    return RightLeftRotation(node);
            }

            return node;
        }
        /// <summary>
        /// Левый поворот узла
        /// </summary>
        /// <param name="node">Узел, относительно которого выполняется поворот</param>
        /// <returns></returns>
        private Node<TKey, TValue> LeftRotation(Node<TKey, TValue> node)
        {
            var rightNode = node.Right;
            node.Right = rightNode.Left;
            rightNode.Left = node;
            node.UpdateHeight();
            rightNode.UpdateHeight();
            return rightNode;
        }
        /// <summary>
        /// Правый поворот узла
        /// </summary>
        /// <param name="node">Узел, относительно которого выполняется поворот</param>
        /// <returns></returns>
        private Node<TKey, TValue> RightRotation(Node<TKey, TValue> node)
        {
            var leftNode = node.Left;
            node.Left = leftNode.Right;
            leftNode.Right = node;
            node.UpdateHeight();
            leftNode.UpdateHeight();
            return leftNode;
        }
        //левый-правый поворот
        private Node<TKey, TValue> LeftRightRotation(Node<TKey, TValue> node)
        {
            node.Left = LeftRotation(node.Left);
            return RightRotation(node);
        }
        //правый-левый поворот
        private Node<TKey, TValue> RightLeftRotation(Node<TKey, TValue> node)
        {
            node.Right = RightRotation(node.Right);
            return LeftRotation(node);
        }
        /// <summary>
        /// Проверяет наличие узла по ключу
        /// </summary>
        /// <param name="key">Ключ, по которому проверяется наличие</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            // Поиск узла осуществляется другим методом.
            return Find(key) != null;
        }
        /// <summary>
        /// Поиск узла по ключу
        /// </summary>
        /// <param name="findKey">Ключ, по которому ищется узел</param>
        /// <returns></returns>
        private Node<TKey, TValue> Find(TKey findKey)
        {
            // Попробуем найти значение в дереве.
            var current = _root;

            // До тех пор, пока не нашли...
            while (current != null)
            {
                int result = _comparer.Compare(current.Key, findKey);
                if (result > 0)
                {
                    // Если искомое значение меньше, идем налево.
                    current = current.Left;
                    continue;
                }
                if (result < 0)
                {
                    // Если искомое значение больше, идем направо.
                    current = current.Right;
                    continue;
                }
                // Если равны, то останавливаемся
                break;

            }
            return current;
        }
        public TValue this[TKey key]
        {
            get 
            {
                if (key == null)
                {
                    throw new ArgumentNullException();
                }
                var node = Find(key);
                return node == null ? throw new KeyNotFoundException() : node.Value;
            }
            set 
            {
                if (key == null)
                {
                    throw new ArgumentNullException();
                }
                var node = Find(key);
                if (node == null)
                {
                    Add(key, value);
                    return;
                }
                node.Value = value;
            } 
        }
        /// <summary>
        /// Очистка дерева
        /// </summary>
        public void Clear()
        {
            _root = null;
            Count = 0;
        }
        /// <summary>
        /// Проверка наличия элемента по значению
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool ContainsValue( TValue value)
        {
            var comparer = EqualityComparer<TValue>.Default;
            foreach(var keyValuePair in Traverse())
            {
                if(comparer.Equals(value, keyValuePair.Value))
                {
                    return true;
                }
            }
            return false;
        }
        IEnumerable<KeyValuePair<TKey, TValue>> Traverse(Node<TKey, TValue> node)
        {
            var nodes = new List<KeyValuePair<TKey, TValue>>();
            if (node != null)
            {
                nodes.AddRange(Traverse(node.Left));
                nodes.Add(new KeyValuePair<TKey, TValue>(node.Key, node.Value));
                nodes.AddRange(Traverse(node.Right));
            }
            return nodes;
        }
        /// <summary>
        /// Симметричный обхолд дерева от корня
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> Traverse()
        {
            return Traverse(_root);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return Traverse().GetEnumerator();
        }
        #endregion
    }
}
