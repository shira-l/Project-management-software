using BlApi;
using BO;
using DalApi;
using DalTest;

namespace BlTest
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                Initialization.Do();

            }
            catch (BlDoesNotExistException ex)
            { Console.WriteLine(ex); }
            catch (BlAlreadyExistsException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }

        //public static void task()
        //{
        //    Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
        //    int choice;
        //    int.TryParse(Console.ReadLine()!, out choice);
        //    switch (choice)
        //    {
        //        case 0:
        //            menu();
        //            break;
        //        case 1:
        //            createTask();
        //            break;
        //        case 2:
        //            readTask();
        //            break;
        //        case 3:
        //            UpdateTask();
        //            break;
        //        case 4:
        //            readAllTask();
        //            break;
        //        case 5:
        //            deleteTask();
        //            break;
        //        default:
        //            throw new NullReferenceException("There is no such choice");
        //    }
        //}

        //public static void createTask()
        //{
        //    Console.WriteLine("Enter Engineer Id, Description, Alias, Milestone, Status ,Deliverables, Remarks, createAt date,StartDate,,ScheduleDate,DeadlineDate,ForecastDate date,Deadline date,ComplateDate ,CompmlexityLevel,pendingTasks");
        //    int EngineerId;
        //    int.TryParse(Console.ReadLine()!, out EngineerId);
        //    string? Description = Console.ReadLine();
        //    string? Alias = Console.ReadLine();
        //    MilestoneInTask milestone = Console.ReadLine();
        //    string? Deliverables = Console.ReadLine();
        //    string? Remarks = Console.ReadLine();
        //    DateTime StartDate, createAt, Deadline;
        //    while (!DateTime.TryParse(Console.ReadLine()!, out StartDate))
        //    { Console.WriteLine("enter start Date"); }
        //    while (!DateTime.TryParse(Console.ReadLine()!, out createAt))
        //    { Console.WriteLine("enter createAt Date"); }
        //    DateTime? ScheduleDate = TryParseNullableDateTime((DateTime?)null);
        //    DateTime? ForecastDate = TryParseNullableDateTime((DateTime?)null);
        //    while (!DateTime.TryParse(Console.ReadLine()!, out Deadline))
        //    { Console.WriteLine("enter DateTime Date"); }
        //    EngineerExperience CompmlexityLevel;
        //    EngineerExperience.TryParse(Console.ReadLine()!, out CompmlexityLevel);
        //    IEnumerable<TaskInList> taskInLists = new List<TaskInList>();

        //    BO.Task newTask = new(0, EngineerId, Description, Alias, milestone, null , Deliverables, Remarks, StartDate , createAt, ScheduleDate, ForecastDate, Deadline, null, CompmlexityLevel, taskInLists);
        //    s_dal!.Task.Create(newTask);
        //    task();
        //}

        //public static void engineer()
        //{
        //    Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
        //    int choice;
        //    int.TryParse(Console.ReadLine()!, out choice);
        //    switch (choice)
        //    {
        //        case 0:
        //            menu();
        //            break;
        //        case 1:
        //            createEngineer();
        //            break;
        //        case 2:
        //            readEngineer();
        //            break;
        //        case 3:
        //            UpdateEngineer();
        //            break;
        //        case 4:
        //            readAllEngineer();
        //            break;
        //        case 5:
        //            deleteEngineer();
        //            break;
        //        default:
        //            throw new NullReferenceException("There is no such choice");
        //    }

        //}
        //public static void menu()
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("Select an entity to test:\n1-task\n2-engineer\n3-dependency\n0-exit");
        //        int choice;
        //        int.TryParse(Console.ReadLine()!, out choice);
        //        switch (choice)
        //        {
        //            case 0:
        //                return;
        //            case 1:
        //                task();
        //                break;
        //            case 2:
        //                engineer();
        //                break;
        //            //case 3:
        //            //    milestone();
        //            //    break;
        //        }
        //    }
        //}
    }
}