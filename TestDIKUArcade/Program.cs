namespace TestDIKUArcade;

using System;
using System.Linq;

internal class Program {

    private static void SafeClose(object sender, ConsoleCancelEventArgs args) {
        args.Cancel = true; // Don't end process.
        Console.Clear();
        Environment.Exit(0);
    }

    public static void Main(string[] args) {

        Console.CancelKeyPress += new ConsoleCancelEventHandler(SafeClose!);

        // Find all object which implement ITestable and sorts them by name.
        var testType = typeof(ITestable);
        var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();
        Func<Type, bool> isTestable = type => testType.IsAssignableFrom(type) && !type.IsInterface;
        var tests = types.Where(isTestable).ToList();
        tests.Sort((x, y) => x.Name.CompareTo(y.Name));

        
        for (int i = 0; i < tests.Count(); i++) {
            Console.WriteLine($"{i}: {tests[i].Name}");
        }

        Console.Write("Give the index of the test you want to run: ");
        var index = Convert.ToInt32(Console.ReadLine());
        
        // Creates an instance of the test.
        var instance = (ITestable) Activator.CreateInstance(tests[index]);
        instance.Help();
        instance.RunTest();
    }
}
