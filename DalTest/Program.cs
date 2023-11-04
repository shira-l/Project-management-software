using DO;
using Dal;
using DalApi;
using System;
using System.Threading.Tasks;
using System.Data;

namespace DalTest
{
    internal class Program
    {
        private static IDependency? s_dalDependency = new DependencyImplementation();
        private static IEngineer? s_dalEngineer  = new EngineerImplementation();
        private static ITask? s_dalTask = new TaskImplementation();
        static void Main()
        {
            Initialization.Do(s_dalEngineer , s_dalDependency, s_dalTask);
            menu();
        }
        public static void task()
        {
            Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
            int choice = Console.Read();
            switch (choice)
            {
                case 0:
                    menu();
                    break;
                case 1:
                    createTask();
                    break;
                case 2:
                    readTask();
                    break;
                case 3:
                    UpdateTask();
                    break;
                case 4:
                    readAllTask();
                    break;
                case 5:
                    deleteTask();
                    break;
            }
        }
        public static void createTask()
        {
            Console.WriteLine("Enter Engineer Id, Description, Alias, Deliverables, Remarks, Start date, Deadline date, CompmlexityLevel");
            int EngineerId;
            int.TryParse(Console.ReadLine()!, out EngineerId);
            string? Description = Console.ReadLine();
            string? Alias = Console.ReadLine();
            string? Deliverables = Console.ReadLine();
            string? Remarks =Console.ReadLine();
            DateTime start, Deadline;
            DateTime.TryParse(Console.ReadLine()!, out start);
            DateTime.TryParse(Console.ReadLine()!, out Deadline);
            EngineerExperience CompmlexityLevel;
            EngineerExperience.TryParse(Console.ReadLine()!, out CompmlexityLevel);
            DO.Task newTask = new(0, EngineerId, Description,Alias,false, true, Deliverables, Remarks, start, null, Deadline, null, CompmlexityLevel);
            s_dalTask!.Create(newTask);
        }
        public static void readTask()
        {
            int id;
            Console.WriteLine("Enter Task's id to read");
            int.TryParse(Console.ReadLine()!, out id);
            Console.WriteLine(s_dalTask!.Read(id));
        }
        public static void readAllTask()
        {
            s_dalTask!.ReadAll().ForEach(task => Console.WriteLine(task));
        }
        public static void deleteTask()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Task's id to delete");
                int.TryParse(Console.ReadLine()!, out id);
                s_dalTask!.Delete(id);
            }
            catch (Exception ex)
            { Console.WriteLine(ex); }
           
        }
        public static void UpdateTask()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Task's id to update");
                int.TryParse(Console.ReadLine()!, out id);
                DO.Task? previousTask= s_dalTask!.Read(id);
                Console.WriteLine(previousTask);
                int EngineerId;
                if (!int.TryParse(Console.ReadLine(), out EngineerId))
                    EngineerId = previousTask!.EngineerId;
                string? Description = Console.ReadLine();
                if (string.IsNullOrEmpty(Description))
                    Description = previousTask!.Description;
                string? Alias = Console.ReadLine();
                if (string.IsNullOrEmpty(Alias))
                    Alias = previousTask!.Alias;
                string? Deliverables = Console.ReadLine();
                if (string.IsNullOrEmpty(Deliverables))
                    Deliverables = previousTask!.Deliverables;
                string? Remarks = Console.ReadLine();
                if (string.IsNullOrEmpty(Remarks))
                    Remarks = previousTask!.Remarks;
                DateTime start, Deadline;
                DateTime.TryParse(Console.ReadLine()!, out start); 
                DateTime.TryParse(Console.ReadLine()!, out Deadline);
                EngineerExperience CompmlexityLevel;
                EngineerExperience.TryParse(Console.ReadLine()!, out CompmlexityLevel);
                DO.Task newTask = new(0, EngineerId, Description, Alias, false, true, Deliverables, Remarks, start, null, Deadline, null, CompmlexityLevel);
                s_dalTask!.Update(newTask);
            }
            catch(Exception ex)
            { Console.WriteLine(ex); };
        }

        public static void engineer()
        {
            Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
            int choice = Console.Read();
            switch (choice)
            {
                case 0:
                    menu();
                    break;
                case 1:
                    createEngineer();
                    break;
                case 2:
                    readEngineer();
                    break;
                case 3:
                    UpdateEngineer();
                    break;
                case 4:
                    readAllEngineer();
                    break;
                case 5:
                    deleteEngineer();
                    break;
            }

        }

        public static void createEngineer()
        {
            Console.WriteLine("Enter Engineer Id, cost, name, email, level");
            int Id;
            double cost;
            int.TryParse(Console.ReadLine()!, out Id);
            double.TryParse(Console.ReadLine()!, out cost);
            string? name = Console.ReadLine();
            string? email = Console.ReadLine();
            EngineerExperience level;
            EngineerExperience.TryParse(Console.ReadLine()!, out level);
            DO.Engineer newEngineer = new(Id, cost, name, email, level);
            s_dalEngineer!.Create(newEngineer);
        }
        public static void readEngineer()
        {
            int id;
            Console.WriteLine("Enter Engineer's id to read");
            int.TryParse (Console.ReadLine(), out id);
            Console.WriteLine(s_dalEngineer!.Read(id));
        }
        public static void UpdateEngineer()
        {
            int id;
            Console.WriteLine("Enter Engineer's id to update");
            int.TryParse(Console.ReadLine(), out id);
            DO.Engineer? previousEngineer = s_dalEngineer!.Read(id);
            Console.WriteLine(previousEngineer);
            double cost;
            if (!double.TryParse(Console.ReadLine(), out cost))
                cost = previousEngineer!.Cost;
            string? name = Console.ReadLine();
            if (string.IsNullOrEmpty(name))
                name = previousEngineer!.Name;
            string? email = Console.ReadLine();
            if (string.IsNullOrEmpty(email))
                email = previousEngineer!.Email;
            EngineerExperience level;
            EngineerExperience.TryParse(Console.ReadLine()!, out level);
            DO.Engineer newTask = new(id, cost,name,email,level);
            s_dalEngineer!.Update(newTask);
        }
        public static void readAllEngineer()
        {
            s_dalEngineer!.ReadAll().ForEach(engineer => Console.WriteLine(engineer));
        }
        public static void deleteEngineer()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Engineer's id to delete");
                int.TryParse(Console.ReadLine(), out id);
                s_dalEngineer!.Delete(id);
            }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }
        public static void dependency()
        {
            Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
            int choice = Console.Read();
            switch (choice)
            {
                case 0:
                    menu();
                    break;
                case 1:
                    createDependency();
                    break;
                case 2:
                    readDependency();
                    break;
                case 3:
                    UpdateDependency();
                    break;
                case 4:
                    readAllDependency();
                    break;
                case 5:
                    deleteDependency();
                    break;
            }
        }

        public static void createDependency()
        {
            Console.WriteLine("Enter Engineer Id, DependentTask, DependOnTask");
            int Id, DependentTask, DependOnTask;
            int.TryParse(Console.ReadLine()!, out Id);
            int.TryParse(Console.ReadLine()!, out DependentTask);
            int.TryParse(Console.ReadLine()!, out DependOnTask);
            DO.Dependency newDependency = new(Id, DependentTask, DependOnTask);
            s_dalDependency!.Create(newDependency);

        }
        public static void readDependency()
        {
            int id;
            Console.WriteLine("Enter Dependency's id to read");
            int.TryParse(Console.ReadLine()!, out id);
            Console.WriteLine(s_dalDependency!.Read(id));
        }
        public static void UpdateDependency()
        {
            int id;
            Console.WriteLine("Enter Dependency's id to read");
            int.TryParse(Console.ReadLine()!, out id);
            DO.Dependency? previousDependency = s_dalDependency!.Read(id);
            Console.WriteLine(previousDependency);
            int DependentTask, DependOnTask;
            if (!int.TryParse(Console.ReadLine(), out DependentTask))
                DependentTask = previousDependency!.DependentTask;
            if(int.TryParse(Console.ReadLine(), out DependOnTask))
                DependOnTask = previousDependency!.DependOnTask;
            DO.Dependency? UpdateDependency = new(id, DependentTask, DependOnTask);
            s_dalDependency!.Update(UpdateDependency);
        }
        public static void readAllDependency()
        {
            s_dalDependency!.ReadAll().ForEach(dependency => Console.WriteLine(dependency));
        }
        public static void deleteDependency()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Dependency's id to delete");
                int.TryParse(Console.ReadLine()!, out id);
                s_dalDependency!.Delete(id);
            }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }


        public static void menu()
        {
            while (true)
            {
                Console.WriteLine("Select an entity to test:\n1-task\n2-engineer\n3-dependency\n0-exit");
                int choice = Console.Read();
                switch (choice)
                {
                    case 0:
                        return;
                    case 1:
                        task();
                        break;
                    case 2:
                        engineer();
                        break;
                    case 3:
                        dependency();
                        break;
                    default:
                        break;
                }
            }
        }
    }   
}

         
