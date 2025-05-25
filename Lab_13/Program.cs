using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarLibrary;

namespace Lab_13
{
    internal class Program
    {
        static void Main()
        {
            // 1. Создаем две коллекции
            var collection1 = new MyObservableCollection<Car>("Коллекция 1");
            var collection2 = new MyObservableCollection<Car>("Коллекция 2");

            // 2. Создаем журналы
            var journal1 = new Journal();
            var journal2 = new Journal();

            // 3. Подписываем журналы на события
            // Журнал 1 подписан на все события коллекции 1
            collection1.CollectionCountChanged += (sender, args) =>
                journal1.AddEntry(collection1.CollectionName, args.ChangeType, args.ChangedItem);
            collection1.CollectionReferenceChanged += (sender, args) =>
                journal1.AddEntry(collection1.CollectionName, args.ChangeType, args.ChangedItem);

            // Журнал 2 подписан на события изменения ссылок обеих коллекций
            collection1.CollectionReferenceChanged += (sender, args) =>
                journal2.AddEntry(collection1.CollectionName, args.ChangeType, args.ChangedItem);
            collection2.CollectionReferenceChanged += (sender, args) =>
                journal2.AddEntry(collection2.CollectionName, args.ChangeType, args.ChangedItem);

            // 4. Добавляем элементы в коллекции
            Console.WriteLine("Добавляем элементы в коллекции...");
            collection1.Add(new PassengerCar("Toyota", 2020, "Red", 25000, 150, 1, 5, 180));
            collection1.Add(new SUV("Land Rover", 2019, "Black", 45000, 210, 2, 5, 200, "Yes", "Горный"));
            collection2.Add(new Truck("Volvo", 2018, "Blue", 60000, 180, 3, 2, 120, 2000));

            // 5. Удаляем элементы
            Console.WriteLine("Удаляем элементы из коллекций...");
            if (collection1.Count > 0)
            {
                collection1.Remove(collection1[0]);
            }

            // 6. Изменяем элементы
            Console.WriteLine("Изменяем элементы в коллекциях...");
            if (collection1.Count > 0)
            {
                collection1[0] = new PassengerCar("Honda", 2021, "White", 28000, 160, 4, 5, 190);
            }
            if (collection2.Count > 0)
            {
                collection2[0] = new Truck("MAN", 2020, "Green", 65000, 190, 5, 2, 130, 2500);
            }

            // 7. Выводим журналы
            Console.WriteLine("\nЖурнал 1:");
            journal1.PrintEntries();

            Console.WriteLine("\nЖурнал 2:");
            journal2.PrintEntries();
        }
    }
}
