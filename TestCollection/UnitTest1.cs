using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_13;
using System;

namespace JournalEntryTests
{
    [TestClass]
    public class JournalEntryTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializePropertiesCorrectly()
        {
            // Arrange
            string collectionName = "TestCollection";
            string changeType = "Add";
            object item = "TestItem";

            // Act
            var entry = new JournalEntry(collectionName, changeType, item);

            // Assert
            Assert.AreEqual(collectionName, entry.CollectionName);
            Assert.AreEqual(changeType, entry.ChangeType);
            Assert.AreEqual(item.ToString(), entry.ItemData);
        }

        [TestMethod]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            string collectionName = "MyCollection";
            string changeType = "Remove";
            object item = new TestItem { Id = 1, Name = "Item1" };
            var entry = new JournalEntry(collectionName, changeType, item);

            string expected = $"Коллекция: {collectionName}, Тип изменения: {changeType}, Данные: {item}";

            // Act
            string result = entry.ToString();

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void Constructor_WithEmptyStrings_ShouldHandleEmptyValues()
        {
            // Arrange
            string emptyCollection = "";
            string emptyChangeType = "";
            object item = "SomeItem";

            // Act
            var entry = new JournalEntry(emptyCollection, emptyChangeType, item);

            // Assert
            Assert.AreEqual(emptyCollection, entry.CollectionName);
            Assert.AreEqual(emptyChangeType, entry.ChangeType);
            Assert.AreEqual(item.ToString(), entry.ItemData);
        }

        [TestMethod]
        public void ItemData_ShouldUseToStringOfObject()
        {
            // Arrange
            var testItem = new TestItem { Id = 5, Name = "TestObject" };
            var entry = new JournalEntry("Col1", "Change", testItem);

            // Act & Assert
            Assert.AreEqual(testItem.ToString(), entry.ItemData);
        }

        private class TestItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return $"TestItem-{Id}: {Name}";
            }
        }
    }
}