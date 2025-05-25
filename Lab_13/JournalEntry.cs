using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_13
{
    public class JournalEntry
    {
        public string CollectionName { get; set; }
        public string ChangeType { get; set; }
        public string ItemData { get; set; }

        public JournalEntry(string collectionName, string changeType, object item)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ItemData = item.ToString();
        }

        public override string ToString()
        {
            return $"Коллекция: {CollectionName}, Тип изменения: {ChangeType}, Данные: {ItemData}";
        }
    }
}
