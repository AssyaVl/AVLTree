namespace BinTreeLib
{
    /// <summary>
    /// Узел
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    internal class Node<TKey, TValue>
    {
        #region Properties
        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Key { get; internal set; }
        /// <summary>
        /// Значение по ключу
        /// </summary>
        public TValue Value { get; internal set; }
        /// <summary>
        /// Левое поддерево
        /// </summary>
        public Node<TKey, TValue> Left { get; internal set; }
        /// <summary>
        /// Правое поддерево
        /// </summary>
        public Node<TKey, TValue> Right { get; internal set; }
        /// <summary>
        /// Родительский узел
        /// </summary>
        public Node<TKey, TValue> Parent { get; internal set; }
        /// <summary>
        /// Высота дерева
        /// </summary>
        public int Height { get; internal set; }
        #endregion

        #region Constructors
        public Node(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            Left = null;
            Right = null;
            Parent = null;
            Height = 1;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Обновление высоты узла после операций над деревом
        /// </summary>
        public void UpdateHeight()
        {
            var leftHeight = GetLeftHeight();
            var rightHeight = GetRightHeight();
            if (leftHeight > rightHeight)
            {
                Height = leftHeight + 1;
                return;
            }
            Height = rightHeight + 1;
        }

        /// <summary>
        /// Определяет баланс поддеревьев узла
        /// </summary>
        /// <returns>Разница высот поддеревьев</returns>
        public int BalanceFactor()
        {
            var leftHeight = GetLeftHeight();
            var rightHeight = GetRightHeight();
            return leftHeight - rightHeight;
        }
        /// <summary>
        /// Получение высоты левого поддерева
        /// </summary>
        /// <returns>Высота левого поддерева (0 если левого узла не существует)</returns>
        private int GetLeftHeight()
        {
            return Left != null ? Left.Height : 0;
        }
        /// <summary>
        /// Получение высоты правого поддерева
        /// </summary>
        /// <returns>Высота правого поддерева (0 если правого узла не сущестует)</returns>
        private int GetRightHeight()
        {
            return Right != null ? Right.Height : 0;
        }
        #endregion
    }
}
