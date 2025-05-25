using System;
    using ColLab;

namespace Lab_13
{
    public class CollectionHandlerEventArgs : EventArgs
    {
        public string ChangeType { get; set; }
        public object ChangedItem { get; set; }

        public CollectionHandlerEventArgs(string changeType, object changedItem)
        {
            ChangeType = changeType;
            ChangedItem = changedItem;
        }
    }

    public class MyObservableCollection<T> : MyCollection<T> where T : ICloneable
    {
        public string CollectionName { get; set; }

        // Делегат для обработки событий
        public delegate void CollectionHandler(object source, CollectionHandlerEventArgs args);

        // События
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;

        // Конструкторы
        public MyObservableCollection() : base() { }
        public MyObservableCollection(string name) : base()
        {
            CollectionName = name;
        }
        public MyObservableCollection(int length, Func<T> generator) : base(length, generator) { }
        public MyObservableCollection(MyCollection<T> other) : base(other) { }

        // Добавление элемента с вызовом события
        public new void Add(T item)
        {
            base.Add(item);
            OnCollectionCountChanged("Добавлен элемент", item);
        }

        // Удаление элемента с вызовом события
        public new bool Remove(T item)
        {
            bool result = base.Remove(item);
            if (result)
            {
                OnCollectionCountChanged("Удален элемент", item);
            }
            return result;
        }

        // Индексатор с вызовом события при изменении
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException($"Индекс {index} находится вне границ коллекции (0..{Count - 1})");

                int i = 0;
                foreach (var item in this)
                {
                    if (i == index) return item;
                    i++;
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException($"Индекс {index} находится вне границ коллекции (0..{Count - 1})");

                int i = 0;
                DoublyNode<T> current = _list.head;
                while (current != null && i < index)
                {
                    current = current.Next;
                    i++;
                }

                if (current != null)
                {
                    current.Data = value;
                    OnCollectionReferenceChanged("Изменен элемент", value);
                }
            }
        }

        // Методы для вызова событий
        protected virtual void OnCollectionCountChanged(string changeType, object item)
        {
            CollectionCountChanged?.Invoke(this, new CollectionHandlerEventArgs(changeType, item));
        }

        protected virtual void OnCollectionReferenceChanged(string changeType, object item)
        {
            CollectionReferenceChanged?.Invoke(this, new CollectionHandlerEventArgs(changeType, item));
        }
    }
}
