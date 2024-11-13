class Program()
{
    static void Main(string[] args)
    {
        Hello hello = new Hello();
        hello.PrintHello();


        // instance of a manager
        Manager manager = new Manager("James", "Bond", 007, "R&D");

        manager.AddTask();
    }
}
