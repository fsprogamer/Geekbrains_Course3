using System;

public class ClassA
{
    public virtual void ClassMethod()
    {
        Console.Write("ClassA");
    }
}
public class ClassB : ClassA
{
    public override void ClassMethod()
    {
        Console.Write("ClassB");
    }
}

class Program
{
    static void Main(string[] args)
    {
        //ClassA object4 = new ClassA();
        //object4.ClassMethod();

        //ClassB object1 = new ClassA();
        //object1.ClassMethod();

        //ClassB object2 = new ClassB();
        //object2.ClassMethod();

        //ClassA object3 = new ClassB();
        //object3.ClassMethod();
    }
}

