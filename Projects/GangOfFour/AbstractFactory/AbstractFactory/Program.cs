using System;

namespace AbstractFactory
{
    /// <summary>
    /// Abstract Factory Design Pattern.
    /// Интерфейс для создания групп связанных или зависящих объектов без определения конкретных классов
    /// </summary>

    internal class MainApp
    {

        public static void Main()
        {
            //Eurasia animal world
            ContinentFactory eurasia = new EurasiaFactory();
            AnimalWorld world = new AnimalWorld(eurasia);
            world.RunFoodChain();
            //Africa animal world
            ContinentFactory africa = new AfricaFactory();
            world = new AnimalWorld(africa);
            world.RunFoodChain();
            
            Console.ReadKey();
        }
    }

    internal abstract class ContinentFactory
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }

    internal class AfricaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Antelope();
        }

        public override Carnivore CreateCarnivore()
        {
            return new Lion();
        }
    }

    internal class EurasiaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Deer();
        }
        public override Carnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }

    internal abstract class Herbivore
    {
    }

    internal abstract class Carnivore
    {
        public abstract void Eat(Herbivore h);
    }

    #region Africa
    internal class Antelope : Herbivore
    {
    }

    internal class Lion : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            Console.WriteLine(this.GetType().Name +
              " ест " + h.GetType().Name);
        }
    }
    #endregion

    #region Eurasia
    internal class Deer : Herbivore
    {
    }

    internal class Wolf : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            Console.WriteLine(this.GetType().Name +
              " ест " + h.GetType().Name);
        }
    }
    #endregion

    internal class AnimalWorld
    {
        private Herbivore _herbivore;
        private Carnivore _carnivore;

        // Constructor
        public AnimalWorld(ContinentFactory factory)
        {
            _carnivore = factory.CreateCarnivore();
            _herbivore = factory.CreateHerbivore();
        }

        public void RunFoodChain()
        {
            _carnivore.Eat(_herbivore);
        }
    }
}