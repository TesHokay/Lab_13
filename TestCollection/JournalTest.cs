using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_13;
using System;
using System.IO;
using ColLab;
using CarLibrary;

namespace JournalTests
{
    [TestClass]
    public class JournalTest
    {
        [TestMethod]
        public void AddEntry_ShouldAddNewEntryToJournal()
        {
            // Arrange
            var journal = new Journal();
            string collectionName = "TestCollection";
            string changeType = "Add";
            object item = new object();

            // Act
            journal.AddEntry(collectionName, changeType, item);

            // Assert
            // Проверяем через вывод в консоль, так как нет публичного доступа к entries
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                journal.PrintEntries();
                string result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains(collectionName));
                Assert.IsTrue(result.Contains(changeType));
                Assert.IsTrue(result.Contains(item.ToString()));
            }
        }

        [TestMethod]
        public void AddEntry_MultipleEntries_ShouldStoreAllEntries()
        {
            // Arrange
            var journal = new Journal();
            string collectionName1 = "Collection1";
            string changeType1 = "Add";
            object item1 = "Item1";

            string collectionName2 = "Collection2";
            string changeType2 = "Remove";
            object item2 = "Item2";

            // Act
            journal.AddEntry(collectionName1, changeType1, item1);
            journal.AddEntry(collectionName2, changeType2, item2);

            // Assert
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                journal.PrintEntries();
                string result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains(collectionName1));
                Assert.IsTrue(result.Contains(changeType1));
                Assert.IsTrue(result.Contains(item1.ToString()));
                Assert.IsTrue(result.Contains(collectionName2));
                Assert.IsTrue(result.Contains(changeType2));
                Assert.IsTrue(result.Contains(item2.ToString()));
            }
        }

        [TestMethod]
        public void PrintEntries_EmptyJournal_ShouldPrintHeaderOnly()
        {
            // Arrange
            var journal = new Journal();

            // Act & Assert
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                journal.PrintEntries();
                string result = sw.ToString().Trim();
                Assert.AreEqual("Записи в журнале:", result);
            }
        }

        [TestMethod]
        public void JournalEntry_ShouldFormatCorrectly()
        {
            // Arrange
            var journal = new Journal();
            string collectionName = "TestCollection";
            string changeType = "Update";
            object item = new TestItem { Id = 1, Name = "Test" };

            // Act
            journal.AddEntry(collectionName, changeType, item);

            // Assert
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                journal.PrintEntries();
                string result = sw.ToString().Trim();
                Assert.IsTrue(result.Contains($"Коллекция: {collectionName}"));
                Assert.IsTrue(result.Contains($"Тип изменения: {changeType}"));
                Assert.IsTrue(result.Contains($"Данные: {item.ToString()}"));
            }
        }

        private class TestItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return $"TestItem: {Id}, {Name}";
            }
        }
    }
}