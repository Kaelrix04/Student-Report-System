using System;

class StudentResultsSystem
{
    // Dynamic lists to support any number of students
    static System.Collections.Generic.List<string> names = new System.Collections.Generic.List<string>();
    static System.Collections.Generic.List<string> ids = new System.Collections.Generic.List<string>();
    static System.Collections.Generic.List<string> programmes = new System.Collections.Generic.List<string>();
    static System.Collections.Generic.List<string> levels = new System.Collections.Generic.List<string>();
    static System.Collections.Generic.List<double[]> scores = new System.Collections.Generic.List<double[]>();
    static bool dataEntered = false;

    static string[] courses = {
        "Programming with C#",
        "Database Systems",
        "Computer Networks",
        "Web Development",
        "Mathematics for Computing"
    };

    static void Main(string[] args)
    {
        int choice;

        do
        {
            Console.WriteLine("\n========== STUDENT RESULTS PROCESSING SYSTEM ==========");
            Console.WriteLine("1. Enter Student Results");
            Console.WriteLine("2. View Student Report");
            Console.WriteLine("3. View Class Summary");
            Console.WriteLine("4. Exit");
            Console.Write("\nChoose an option: ");

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.Write("Invalid option. Please enter 1, 2, 3, or 4: ");
            }

            switch (choice)
            {
                case 1:
                    EnterStudentResults();
                    break;
                case 2:
                    ViewStudentReport();
                    break;
                case 3:
                    ViewClassSummary();
                    break;
                case 4:
                    Console.WriteLine("\nThank you for using the Student Results Processing System.");
                    break;
            }

        } while (choice != 4);
    }

    static void EnterStudentResults()
    {
        int numStudents;
        Console.Write("\nHow many students do you want to enter results for? ");
        while (!int.TryParse(Console.ReadLine(), out numStudents) || numStudents < 1)
        {
            Console.Write("Please enter a valid number (at least 1): ");
        }

        for (int i = 0; i < numStudents; i++)
        {
            Console.WriteLine($"\n------ Enter Details for Student {i + 1} ------");

            Console.Write("Enter full name: ");
            names.Add(Console.ReadLine());

            Console.Write("Enter student ID: ");
            ids.Add(Console.ReadLine());

            Console.Write("Enter programme: ");
            programmes.Add(Console.ReadLine());

            Console.Write("Enter level: ");
            levels.Add(Console.ReadLine());

            Console.WriteLine();

            double[] studentScores = new double[5];
            for (int j = 0; j < 5; j++)
            {
                double score;
                do
                {
                    Console.Write($"Enter score for {courses[j]}: ");
                    while (!double.TryParse(Console.ReadLine(), out score))
                    {
                        Console.WriteLine("Invalid score. Score must be between 0 and 100.");
                        Console.Write($"Enter score for {courses[j]}: ");
                    }

                    if (score < 0 || score > 100)
                        Console.WriteLine("Invalid score. Score must be between 0 and 100.");

                } while (score < 0 || score > 100);

                studentScores[j] = score;
            }
            scores.Add(studentScores);
        }

        dataEntered = true;
        Console.WriteLine($"\n{numStudents} student(s) entered successfully!");
    }

    static double GetAverage(int index)
    {
        double total = 0;
        foreach (double s in scores[index])
            total += s;
        return total / 5;
    }

    static void ViewStudentReport()
    {
        if (!dataEntered)
        {
            Console.WriteLine("\nNo data found. Please enter student results first (Option 1).");
            return;
        }

        Console.WriteLine("\n================= STUDENT RESULTS REPORT =================");

        for (int i = 0; i < names.Count; i++)
        {
            double total = 0;
            foreach (double s in scores[i]) total += s;
            double average = total / 5;
            string grade = GetGrade(average);
            string status = average >= 50 ? "Passed" : "Failed";

            Console.WriteLine($"\nStudent Name : {names[i]}");
            Console.WriteLine($"Student ID   : {ids[i]}");
            Console.WriteLine($"Programme    : {programmes[i]}");
            Console.WriteLine($"Level        : {levels[i]}");
            Console.WriteLine();

            for (int j = 0; j < 5; j++)
                Console.WriteLine($"  {courses[j],-30}: {scores[i][j]}");

            Console.WriteLine();
            Console.WriteLine($"  Total Score  : {total}");
            Console.WriteLine($"  Average Score: {average:F1}");
            Console.WriteLine($"  Grade        : {grade}");
            Console.WriteLine($"  Status       : {status}");
            Console.WriteLine(new string('-', 55));
        }
    }

    static void ViewClassSummary()
    {
        if (!dataEntered)
        {
            Console.WriteLine("\nNo data found. Please enter student results first (Option 1).");
            return;
        }

        int bestIndex = 0;
        int lowestIndex = 0;
        double classTotal = 0;

        for (int i = 0; i < names.Count; i++)
        {
            double avg = GetAverage(i);
            classTotal += avg;

            if (avg > GetAverage(bestIndex))
                bestIndex = i;

            if (avg < GetAverage(lowestIndex))
                lowestIndex = i;
        }

        double classAverage = classTotal / names.Count;

        Console.WriteLine("\n================= CLASS SUMMARY =================");

        Console.WriteLine($"\n  Total Students : {names.Count}");
        Console.WriteLine($"  Class Average  : {classAverage:F1}  ({GetGrade(classAverage)})");

        Console.WriteLine($"\n  Best Student");
        Console.WriteLine($"  Name    : {names[bestIndex]}");
        Console.WriteLine($"  ID      : {ids[bestIndex]}");
        Console.WriteLine($"  Average : {GetAverage(bestIndex):F1}  ({GetGrade(GetAverage(bestIndex))})");

        Console.WriteLine($"\n  Lowest Average Student");
        Console.WriteLine($"  Name    : {names[lowestIndex]}");
        Console.WriteLine($"  ID      : {ids[lowestIndex]}");
        Console.WriteLine($"  Average : {GetAverage(lowestIndex):F1}  ({GetGrade(GetAverage(lowestIndex))})");

        Console.WriteLine(new string('-', 50));
    }

    static string GetGrade(double average)
    {
        if (average >= 80) return "A";
        if (average >= 70) return "B";
        if (average >= 60) return "C";
        if (average >= 50) return "D";
        return "F";
    }
}