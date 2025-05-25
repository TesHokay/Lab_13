using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarLibrary;
using ColLab;

namespace TestCollection
{
    [TestClass]
    public class DoublyLinkedListTest
    {
        [TestMethod]
        public void Add_ShouldIncreaseCount()
        {
            // Arrange
            var list = new DoublyLinkedList<Car>();
            var car = new Car("Toyota", 2020, "Red", 25000, 180, 1500);

            // Act
            list.Add(car);

            // Assert
            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void RemoveAllFromElement_ShouldRemoveAllFromSpecifiedElement()
        {
            // Arrange
            var list = new DoublyLinkedList<Car>();
            var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            var car2 = new Car("BMW", 2019, "Black", 45000, 210, 1700);
            var car3 = new Car("Audi", 2021, "White", 35000, 190, 1600);

            list.Add(car1);
            list.Add(car2);
            list.Add(car3);

            // Act
            list.RemoveAllFromElement(car2);

            // Assert
            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(car1, list.head.Data);
        }

        [TestMethod]
        public void RemoveAllFromElement_WhenFirstElement_ShouldClearList()
        {
            // Arrange
            var list = new DoublyLinkedList<Car>();
            var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            var car2 = new Car("BMW", 2019, "Black", 45000, 210, 1700);

            list.Add(car1);
            list.Add(car2);

            // Act
            list.RemoveAllFromElement(car1);

            // Assert
            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }

        [TestMethod]
        public void InsertAtOddPositions_ShouldInsertCorrectly()
        {
            // Arrange
            var list = new DoublyLinkedList<Car>();
            var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            var car2 = new Car("BMW", 2019, "Black", 45000, 210, 1700);
            var newCars = new Car[] {
                new Car("Audi", 2021, "White", 35000, 190, 1600)
            };

            list.Add(car1);
            list.Add(car2);

            // Act
            list.InsertAtOddPositions(newCars);

            // Assert
            Assert.AreEqual(3, list.Count);
            Assert.AreEqual(car1, list.head.Data);
            Assert.AreEqual(newCars[0], list.head.Next.Data);
            Assert.AreEqual(car2, list.tail.Data);
        }

        [TestMethod]
        public void Clear_ShouldEmptyList()
        {
            // Arrange
            var list = new DoublyLinkedList<Car>();
            list.Add(new Car("Toyota", 2020, "Red", 25000, 180, 1500));

            // Act
            list.Clear();

            // Assert
            Assert.AreEqual(0, list.Count);
            Assert.IsNull(list.head);
            Assert.IsNull(list.tail);
        }

        [TestMethod]
        public void Clone_ShouldCreateDeepCopy()
        {
            // Arrange
            var list = new DoublyLinkedList<Car>();
            var car = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            list.Add(car);

            // Act
            var clonedList = (DoublyLinkedList<Car>)list.Clone();

            // Assert
            Assert.AreEqual(list.Count, clonedList.Count);
            Assert.AreNotSame(list.head, clonedList.head);
            Assert.AreNotSame(list.head.Data, clonedList.head.Data);
        }

        [TestMethod]
        public void GetEnumerator_ShouldIterateThroughAllElements()
        {
            // Arrange
            var list = new DoublyLinkedList<Car>();
            var car1 = new Car("Toyota", 2020, "Red", 25000, 180, 1500);
            var car2 = new Car("BMW", 2019, "Black", 45000, 210, 1700);
            list.Add(car1);
            list.Add(car2);

            // Act & Assert
            int count = 0;
            foreach (var car in list)
            {
                count++;
                Assert.IsTrue(car.Equals(car1) || car.Equals(car2));
            }
            Assert.AreEqual(2, count);
        }
    }
}
