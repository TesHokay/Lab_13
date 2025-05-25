using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab_13;
using CarLibrary;

namespace TestCollection
{
    [TestClass]
    public class MyObservableCollectionTests
    {
        [TestMethod]
        public void Constructor_WithName_SetsCollectionName()
        {
            // Arrange
            string name = "Test Collection";

            // Act
            var collection = new MyObservableCollection<Car>(name);

            // Assert
            Assert.AreEqual(name, collection.CollectionName);
        }

        [TestMethod]
        public void Add_Item_RaisesCollectionCountChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<Car>("Test");
            var car = new PassengerCar();
            bool eventRaised = false;
            string changeType = "";
            object changedItem = null;

            collection.CollectionCountChanged += (sender, args) =>
            {
                eventRaised = true;
                changeType = args.ChangeType;
                changedItem = args.ChangedItem;
            };

            // Act
            collection.Add(car);

            // Assert
            Assert.IsTrue(eventRaised);
            Assert.AreEqual("Добавлен элемент", changeType);
            Assert.AreEqual(car, changedItem);
        }

        [TestMethod]
        public void Remove_Item_RaisesCollectionCountChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<Car>("Test");
            var car = new PassengerCar();
            collection.Add(car);

            bool eventRaised = false;
            string changeType = "";
            object changedItem = null;

            collection.CollectionCountChanged += (sender, args) =>
            {
                eventRaised = true;
                changeType = args.ChangeType;
                changedItem = args.ChangedItem;
            };

            // Act
            collection.Remove(car);

            // Assert
            Assert.IsTrue(eventRaised);
            Assert.AreEqual("Удален элемент", changeType);
            Assert.AreEqual(car, changedItem);
        }

        [TestMethod]
        public void Indexer_SetValue_RaisesCollectionReferenceChangedEvent()
        {
            // Arrange
            var collection = new MyObservableCollection<Car>("Test");
            var car1 = new PassengerCar();
            var car2 = new SUV();
            collection.Add(car1);

            bool eventRaised = false;
            string changeType = "";
            object changedItem = null;

            collection.CollectionReferenceChanged += (sender, args) =>
            {
                eventRaised = true;
                changeType = args.ChangeType;
                changedItem = args.ChangedItem;
            };

            // Act
            collection[0] = car2;

            // Assert
            Assert.IsTrue(eventRaised);
            Assert.AreEqual("Изменен элемент", changeType);
            Assert.AreEqual(car2, changedItem);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Indexer_Get_InvalidIndex_ThrowsException()
        {
            // Arrange
            var collection = new MyObservableCollection<Car>("Test");

            // Act
            var item = collection[0];
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Indexer_Set_InvalidIndex_ThrowsException()
        {
            // Arrange
            var collection = new MyObservableCollection<Car>("Test");
            var car = new PassengerCar();

            // Act
            collection[0] = car;
        }

        [TestMethod]
        public void Constructor_WithGenerator_CreatesCollectionWithItems()
        {
            // Arrange
            int count = 3;

            // Act
            var collection = new MyObservableCollection<Car>(count, () => new PassengerCar());

            // Assert
            Assert.AreEqual(count, collection.Count);
        }

        [TestMethod]
        public void Constructor_Copy_CreatesDeepCopy()
        {
            // Arrange
            var original = new MyObservableCollection<Car>("Original");
            original.Add(new PassengerCar("Toyota", 2020, "Red", 25000, 150, 1, 5, 180));

            // Act
            var copy = new MyObservableCollection<Car>(original);

            // Assert
            Assert.AreEqual(original.Count, copy.Count);
            Assert.AreNotSame(original[0], copy[0]);
        }

        [TestMethod]
        public void Events_NotSubscribed_DoesNotThrow()
        {
            // Arrange
            var collection = new MyObservableCollection<Car>("Test");
            var car = new PassengerCar();

            // Act & Assert (should not throw)
            collection.Add(car);
            collection.Remove(car);
            if (collection.Count > 0)
            {
                collection[0] = new SUV();
            }
        }

        [TestMethod]
        public void MultipleSubscribers_AllReceiveEvents()
        {
            // Arrange
            var collection = new MyObservableCollection<Car>("Test");
            var car = new PassengerCar();

            int event1Count = 0;
            int event2Count = 0;

            collection.CollectionCountChanged += (s, e) => event1Count++;
            collection.CollectionCountChanged += (s, e) => event2Count++;

            // Act
            collection.Add(car);

            // Assert
            Assert.AreEqual(1, event1Count);
            Assert.AreEqual(1, event2Count);
        }
    }
}