namespace NetQuestion
{

    //using System;
    //using System.Reflection;

    //class Sample
    //{
    //    private string _x = "No change me!";
    //    public override string ToString()
    //    {
    //        return _x;
    //    }
    //}
    //class Program
    //{
    //    static void Main()
    //    {
    //        var sample = new Sample();
    //        typeof(Sample).GetField("_x", BindingFlags.NonPublic | BindingFlags.Instance)
    //               .SetValue(sample, "I change you...");
    //        Console.Write(sample);
    //        Console.ReadKey();
    //    }
    //}

    //1.
    //class A<T>
    //{
    //    public static int Value;
    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        A<int>.Value = 5;
    //        A<Int32>.Value = 10;
    //        A<uint>.Value = 15;
    //        Console.WriteLine(A<int>.Value);
    //    }
    //}

    //2.
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Int32 v = 5;
    //        Object o = v;
    //        v = 123;

    //        Console.WriteLine(v + "," + o);
    //    }
    //}

    //3.
    //class A
    //{
    //    public int i;
    //    public Func<int> Func()
    //    {
    //        return () => i + 5; 
    //    }
    //}
    //class Prog
    //{
    //    static void Main(string[] args)
    //    {
    //        A a = new A();
    //        a.i = 1;
    //        Func<int> f = a.Func();
    //        a.i = 10;

    //        Console.WriteLine(f());
    //    }
    //}

    //4.
    //class Prog
    //{
    //    static void Main(string[] args)
    //    {
    //        var array = new int[] { 7, 13, 15, 21, 30, 50 };

    //        foreach (var a in array)
    //            ThreadPool.QueueUserWorkItem(callback => Console.WriteLine(a));
    //        Thread.Sleep(2000);
    //    }
    //}

    //5.
    //public class A
    //{
    //    public A()
    //    {
    //        Console.WriteLine('A');
    //    }
    //}

    //public class B : A
    //{
    //    public B()
    //    {
    //        Console.WriteLine('B');
    //    }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        new B();
    //    }
    //}

    //6.
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var s1 = new string(new[] { 't', 'e', 's', 't' });
    //        var s2 = new string(new[] { 't', 'e', 's', 't' });

    //        var o1 = (object)s1;
    //        var o2 = (object)s2;

    //        Console.WriteLine(s1 == s2);
    //        Console.WriteLine(Equals(s1, s2));

    //        Console.WriteLine(o1 == o2);
    //        Console.WriteLine(Equals(o1, o2));

    //    }
    //}
    //7.
    //public class A
    //{
    //    public string Name { get; set; }
    //    public int Age { get; set; }
    //}

    //struct B
    //{
    //    public A A { get; set; }
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        var b = new B();
    //        b.A = new A();

    //        b.A.Name = "My Name";
    //        b.A.Age = 25;

    //        M(b);

    //        Console.WriteLine(b.A.Name);
    //        Console.WriteLine(b.A.Age);
    //    }

    //    static void M(B b)
    //    {
    //        b.A.Name = "Another Name";
    //        b.A.Age = 35;
    //    }
    //}

    //8.
    //class A
    //{
    //    public int a;
    //}
    //class B : A
    //{
    //    public int b;
    //}

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        A a1 = new A();
    //        B b1 = new B();

    //        a1 = b1;

    //        //b1 = a1;

    //        List<A> lb = new List<B>();
    //    }
    //}

    //9.
    //class A
    //{

    //}
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        using (A a = new A())
    //        {

    //        } 
    //    }
    //}

    //10.
    //class A
    //{

    //}

    //struct B : A
    //{

    //}

    //11.
    //namespace Tests
    //{
    //    class Program
    //    {
    //        public class A { }

    //        private class B : A { }

    //        static void Main(string[] args)
    //        {

    //        }
    //    }
    //}

    //11.
    //namespace Tests
    //{
    //    class Program
    //    {
    //        public class A
    //        {
    //            public virtual bool M1() { return false; }
    //        }

    //        private class B : A
    //        {
    //            public override bool M1() { return true; }
    //        }

    //        static void Main(string[] args)
    //        {
    //            A b = new B();
    //            b.M1();
    //        }
    //    }
    //}
}
