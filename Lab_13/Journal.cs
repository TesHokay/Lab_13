using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_13
{
    internal class Journal
    {
        private List<JournalEntry> entries = new List<JournalEntry>();

        public void AddEntry(string collectionName, string changeType, object item)
        {
            entries.Add(new JournalEntry(collectionName, changeType, item));
        }

        public void PrintEntries()
        {
            Console.WriteLine("Записи в журнале:");
            foreach (var entry in entries)
            {
                Console.WriteLine(entry);
            }
        }
    }
}
