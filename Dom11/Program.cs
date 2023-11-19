using System;
using System.Linq;

interface IEmployee
{
    string Position { get; set; }
    string LastName { get; set; }
    string FirstName { get; set; }
    DateTime HireDate { get; set; }
    decimal Salary { get; set; }
    string Gender { get; set; }

    string ToString();
}

struct Employee : IEmployee
{
    public string Position { get; set; }
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public DateTime HireDate { get; set; }
    public decimal Salary { get; set; }
    public string Gender { get; set; }

    public override string ToString()
    {
        return $"{LastName}, {FirstName}\nPosition: {Position}\nHire Date: {HireDate:d}\nSalary: {Salary:C}\nGender: {Gender}\n";
    }
}

class EmployeeManager
{
    private Employee[] employees;

    public EmployeeManager(int size)
    {
        employees = new Employee[size];
    }

    public void FillEmployees()
    {
        for (int i = 0; i < employees.Length; i++)
        {
            Console.WriteLine($"Enter details for employee {i + 1}:");

            employees[i] = new Employee();

            Console.Write("Position: ");
            employees[i].Position = Console.ReadLine();

            Console.Write("Last Name: ");
            employees[i].LastName = Console.ReadLine();

            Console.Write("First Name: ");
            employees[i].FirstName = Console.ReadLine();

            Console.Write("Hire Date (yyyy-MM-dd): ");
            employees[i].HireDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Salary: ");
            employees[i].Salary = decimal.Parse(Console.ReadLine());

            Console.Write("Gender (Male/Female): ");
            employees[i].Gender = Console.ReadLine();
        }
    }

    public void DisplayAllEmployees()
    {
        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }
    }

    public void DisplayEmployeesByPosition(string position)
    {
        var employeesByPosition = employees.Where(e => e.Position.Equals(position, StringComparison.OrdinalIgnoreCase));

        foreach (var employee in employeesByPosition)
        {
            Console.WriteLine(employee);
        }
    }

    public void DisplayHighSalaryManagers()
    {
        var clerksAverageSalary = employees.Where(e => e.Position.Equals("Clerk", StringComparison.OrdinalIgnoreCase)).Average(e => e.Salary);
        var highSalaryManagers = employees.Where(e => e.Position.Equals("Manager", StringComparison.OrdinalIgnoreCase) && e.Salary > clerksAverageSalary)
                                          .OrderBy(e => e.LastName);

        foreach (var manager in highSalaryManagers)
        {
            Console.WriteLine(manager);
        }
    }

    public void DisplayEmployeesHiredAfterDate(DateTime date)
    {
        var employeesAfterDate = employees.Where(e => e.HireDate > date).OrderBy(e => e.LastName);

        foreach (var employee in employeesAfterDate)
        {
            Console.WriteLine(employee);
        }
    }

    public void DisplayEmployeesByGender(string gender)
    {
        var filteredEmployees = employees;

        if (!string.IsNullOrEmpty(gender))
        {
            filteredEmployees = employees.Where(e => e.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase)).ToArray();
        }

        foreach (var employee in filteredEmployees)
        {
            Console.WriteLine(employee);
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter the number of employees: ");
        int size = int.Parse(Console.ReadLine());

        EmployeeManager employeeManager = new EmployeeManager(size);

        employeeManager.FillEmployees();

        Console.WriteLine("\nAll Employees:");
        employeeManager.DisplayAllEmployees();

        Console.Write("\nEnter position to filter employees: ");
        string positionFilter = Console.ReadLine();
        employeeManager.DisplayEmployeesByPosition(positionFilter);

        Console.WriteLine("\nManagers with salary higher than average salary of clerks:");
        employeeManager.DisplayHighSalaryManagers();

        Console.Write("\nEnter date to filter employees hired after (yyyy-MM-dd): ");
        DateTime dateFilter = DateTime.Parse(Console.ReadLine());
        employeeManager.DisplayEmployeesHiredAfterDate(dateFilter);

        Console.Write("\nEnter gender to filter employees (leave blank for all): ");
        string genderFilter = Console.ReadLine();
        employeeManager.DisplayEmployeesByGender(genderFilter);
    }
}
