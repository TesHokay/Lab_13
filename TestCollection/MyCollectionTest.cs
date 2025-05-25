using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ColLab;
using CarLibrary;

namespace TestCollection
{
    [TestClass]
    public class MyCollectionTest
    {
        [TestMethod]
        public void Constructor_Default_CreatesEmptyCollection()
        {
            // Arrange & Act
            var collection = new MyCollection<Car>();

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Constructor_WithGenerator_CreatesCollectionWithSpecifiedLength()
        {
            // Arrange
            int length = 5;

            // Act
            var collection = new MyCollection<Car>(length, () => new PassengerCar());

            // Assert
            Assert.AreEqual(length, collection.Count);
        }

        [TestMethod]
        public void Add_Item_AddsToCollection()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new PassengerCar("Toyota", 2020, "Red", 25000, 150, 1, 5, 180);

            // Act
            collection.Add(car);

            // Assert
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(car, collection.First());
        }

        [TestMethod]
        public void Remove_ExistingItem_ReturnsTrueAndRemovesItem()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new PassengerCar();
            collection.Add(car);

            // Act
            bool result = collection.Remove(car);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Remove_NonExistingItem_ReturnsFalse()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new PassengerCar();
            var car2 = new SUV();
            collection.Add(car1);

            // Act
            bool result = collection.Remove(car2);

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Clear_RemovesAllItems()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            collection.Add(new PassengerCar());
            collection.Add(new SUV());

            // Act
            collection.Clear();

            // Assert
            Assert.AreEqual(0, collection.Count);
        }

        [TestMethod]
        public void Contains_ExistingItem_ReturnsTrue()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new PassengerCar();
            collection.Add(car);

            // Act & Assert
            Assert.IsTrue(collection.Contains(car));
        }

        [TestMethod]
        public void CopyTo_CopiesAllElementsToArray()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new PassengerCar();
            var car2 = new SUV();
            collection.Add(car1);
            collection.Add(car2);

            var array = new Car[2];

            // Act
            collection.CopyTo(array, 0);

            // Assert
            Assert.AreEqual(car1, array[0]);
            Assert.AreEqual(car2, array[1]);
        }

        [TestMethod]
        public void GetEnumerator_EnumeratesAllItems()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car1 = new PassengerCar();
            var car2 = new SUV();
            collection.Add(car1);
            collection.Add(car2);

            // Act
            int count = 0;
            foreach (var item in collection)
            {
                count++;
            }

            // Assert
            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void Peek_ReturnsTopItemWithoutRemoving()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            var car = new PassengerCar();
            collection.Push(car);

            // Act
            var peeked = collection.Peek();

            // Assert
            Assert.AreEqual(car, peeked);
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyCollection_ThrowsException()
        {
            // Arrange
            var collection = new MyCollection<Car>();

            // Act
            collection.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Peek_EmptyCollection_ThrowsException()
        {
            // Arrange
            var collection = new MyCollection<Car>();

            // Act
            collection.Peek();
        }

        [TestMethod]
        public void Clone_CreatesDeepCopy()
        {
            // Arrange
            var original = new MyCollection<Car>();
            original.Add(new PassengerCar("Toyota", 2020, "Red", 25000, 150, 1, 5, 180));

            // Act
            var clone = new MyCollection<Car>(original);

            // Assert
            Assert.AreEqual(original.Count, clone.Count);
            Assert.AreNotSame(original.First(), clone.First());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CopyTo_NullArray_ThrowsArgumentNullException()
        {
            // Arrange
            var collection = new MyCollection<Car>();

            // Act
            collection.CopyTo(null, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CopyTo_InvalidIndex_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            collection.Add(new PassengerCar());
            var array = new Car[1];

            // Act
            collection.CopyTo(array, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CopyTo_InsufficientSpace_ThrowsArgumentException()
        {
            // Arrange
            var collection = new MyCollection<Car>();
            collection.Add(new PassengerCar());
            collection.Add(new SUV());
            var array = new Car[1];

            // Act
            collection.CopyTo(array, 0);
        }
    }
}