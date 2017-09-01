using System;
using System.Collections.Generic;

namespace FactoryMethod
{
    /// <summary>    
    /// Factory Method Design Pattern.
    /// Определяет интерфейс для создания объекта, позволяя дочерним классам решать какой класс создавать.
    /// Фабричный метод позволяет перенести ответственность по конкретизации реализации в дочерние классы.
    /// </summary>

    class MainApp
    {
        static void Main()
        {
            // constructors call Factory Method
            Car[] cars = new Car[2];
            cars[0] = new Truck();
            cars[1] = new Bus();

            foreach (Car car in cars)
            {
                Console.WriteLine("\n" + car.GetType().Name + "--");
                foreach (Part part in car.Parts)
                {
                    Console.WriteLine(" " + part.GetType().Name);
                }
            }
            Console.ReadKey();
        }
    }

    abstract class Part
    {

    }
    class TruckTransmission : Part
    {

    }
    class TruckEngine : Part
    {

    }
    class TruckSuspension : Part
    {

    }
    class BusTransmission : Part
    {

    }
    class BusEngine : Part
    {

    }
    class BusSuspension : Part
    {

    }
  
    abstract class Car
    {
        private List<Part> _parts = new List<Part>();
        //call abstract Factory method
        public Car()
        {
            this.CreateParts();
        }
        public List<Part> Parts
        {
            get { return _parts; }
        }
        // Factory Method
        public abstract void CreateParts();
    }

    class Truck : Car
    {
        // Factory Method implementation
        public override void CreateParts()
        {
            Parts.Add(new TruckTransmission());
            Parts.Add(new TruckEngine());
            Parts.Add(new TruckSuspension());
        }
    }
    class Bus : Car
    {
        // Factory Method implementation
        public override void CreateParts()
        {
            Parts.Add(new BusTransmission());
            Parts.Add(new BusEngine());
            Parts.Add(new BusSuspension());
        }
    }
}