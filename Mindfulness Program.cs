using System;
using System.Collections.Generic;
using System.Threading;

// Base class: Activity
abstract class Activity
{
    protected string Name;
    protected string Description;
    protected int Duration;

    public Activity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void StartActivity()
    {
        Console.WriteLine($"Starting {Name}...");
        Console.WriteLine(Description);
        Console.Write("Enter duration (seconds): ");
        Duration = int.Parse(Console.ReadLine());
        Console.WriteLine("Prepare to begin...");
        ShowAnimation();
        Execute();
        Console.WriteLine("Good job! Activity completed.");
        ShowAnimation();
    }

    protected void ShowAnimation()
    {
        string[] spinner = { "|", "/", "-", "\\" };
        for (int i = 0; i < 5; i++)
        {
            foreach (var symbol in spinner)
            {
                Console.Write($"\r{symbol}");
                Thread.Sleep(200);
            }
        }
        Console.WriteLine();
    }

    protected abstract void Execute();
}

// Breathing Activity
class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "Relax by following a breathing pattern.") { }

    protected override void Execute()
    {
        for (int i = 0; i < Duration / 4; i++)
        {
            Console.WriteLine("Breathe in... (4 sec)");
            Thread.Sleep(4000);
            Console.WriteLine("Breathe out... (4 sec)");
            Thread.Sleep(4000);
        }
    }
}

// Reflection Activity
class ReflectionActivity : Activity
{
    private List<string> Prompts = new List<string>
    {
        "Think of a time you felt truly happy.",
        "Remember a moment you overcame a challenge.",
        "Reflect on something you recently learned."
    };

    public ReflectionActivity() : base("Reflection Activity", "Think about meaningful moments in your life.") { }

    protected override void Execute()
    {
        Random rand = new Random();
        string prompt = Prompts[rand.Next(Prompts.Count)];
        Console.WriteLine(prompt);
        Thread.Sleep(Duration * 1000);
    }
}

// Listing Activity
class ListingActivity : Activity
{
    public ListingActivity() : base("Listing Activity", "List as many things as you can in a given topic.") { }

    protected override void Execute()
    {
        Console.WriteLine("Name things you are grateful for:");
        List<string> responses = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            responses.Add(Console.ReadLine());
        }
        Console.WriteLine($"You listed {responses.Count} items!");
    }
}

// Main Program
class Program
{
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Mindfulness App");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            Activity activity = choice switch
            {
                "1" => new BreathingActivity(),
                "2" => new ReflectionActivity(),
                "3" => new ListingActivity(),
                "4" => null,
                _ => null
            };

            if (activity == null) break;

            activity.StartActivity();
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}

