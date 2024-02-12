using DO;
using Dal;
using DalApi;
using System;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlTypes;

namespace DalTest
{
    internal class Program
    {
        //private readonly static IDal? s_dal = new DalList();stage 2
        //static readonly IDal s_dal = new DalXml(); //stage 3
        static readonly IDal s_dal = Factory.Get; //stage 4

        static void Main()
        {
            try
            {
                //Initialization.Do(s_dal); //stage 2
                Initialization.Do(); //stage 4
                menu();
            }
            catch(DalIsNotExistException ex)
            { Console.WriteLine(ex); }
            catch (DalAlreadyExistsException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); }


        }

        /// <summary>
        /// Task function to add, read, delete and update a task.
        /// </summary>
        public static void task()
        {
            Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
            int choice;
            int.TryParse(Console.ReadLine()!, out choice);
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
                default:
                    throw new NullReferenceException("There is no such choice");
            }
        }

        /// <summary>
        ///Creates new Task object in DAL
        ///</summary>
        public static void createTask()
        {
            Console.WriteLine("Enter Engineer Id, Description, Alias, Deliverables, Remarks, createAt date,ForecastDate date, Deadline date, CompmlexityLevel");
            int EngineerId;
            int.TryParse(Console.ReadLine()!, out EngineerId);
            string? Description = Console.ReadLine();
            string? Alias = Console.ReadLine();
            string? Deliverables = Console.ReadLine();
            string? Remarks =Console.ReadLine();
            DateTime createAt, Deadline;
            while(!DateTime.TryParse(Console.ReadLine()!, out createAt))
            { Console.WriteLine("enter createAt Date"); }
            DateTime? ScheduleDate = TryParseNullableDateTime((DateTime?)null);
            DateTime? ForecastDate = TryParseNullableDateTime((DateTime?)null);
            while (!DateTime.TryParse(Console.ReadLine()!, out Deadline))
            { Console.WriteLine("enter DateTime Date"); }
            EngineerExperience CompmlexityLevel;
            EngineerExperience.TryParse(Console.ReadLine()!, out CompmlexityLevel);
            DO.Task newTask = new(0, EngineerId, Description,Alias, true, Deliverables, Remarks, createAt, ScheduleDate, ForecastDate, Deadline, null, CompmlexityLevel);
            s_dal!.Task.Create(newTask);
            task();
        }

        /// <summary>
        /// Reads Task object by its ID
        /// </summary>
       
        public static void readTask()
        {
            int id;
            Console.WriteLine("Enter Task's id to read");
            int.TryParse(Console.ReadLine()!, out id);
            Console.WriteLine(s_dal!.Task.Read(id));
            task();
        }

        /// <summary>
        /// Reads all Task objects
        /// </summary>
        public static void readAllTask(Func<DO.Task?, bool>? filter = null)
        {
            s_dal!.Task.ReadAll(filter).ToList().ForEach(task => Console.WriteLine(task));
            task();
        }

        /// <summary>
        /// Deletes an object by its Id
        /// </summary>
        public static void deleteTask()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Task's id to delete");
                int.TryParse(Console.ReadLine()!, out id);
                s_dal!.Task.Delete(id);
                task();
            }
            catch(DalIsNotExistException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); }
           
        }
        /// <summary>
        /// Updates Task object
        /// </summary>
        public static void UpdateTask()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Task's id to update");
                int.TryParse(Console.ReadLine()!, out id);
                DO.Task? previousTask= s_dal!.Task.Read(id);
                if (previousTask == null)
                    throw new DalIsNotExistException($"task with ID={id} is not exists");
                Console.WriteLine("the task tou want to update: "+previousTask);
                Console.WriteLine("Enter Engineer Id, Description, Alias, Deliverables, Remarks, createAt date,ForecastDate date, Deadline date, CompmlexityLevel");
                int ID = previousTask.Id;
                int EngineerId;
                if (!int.TryParse(Console.ReadLine(), out EngineerId))
                    EngineerId = previousTask!.EngineerId;
                string? Description, Alias, Deliverables, Remarks;
                Description = stringIsNullOrEmpty(previousTask!.Description);
                Alias = stringIsNullOrEmpty(previousTask!.Alias);
                Deliverables = stringIsNullOrEmpty(previousTask!.Deliverables);
                Remarks = stringIsNullOrEmpty(previousTask!.Remarks);
                DateTime? createAt = TryParseNullableDateTime(previousTask!.DeadlineDate);
                DateTime? ScheduleDate = TryParseNullableDateTime(previousTask!.ScheduleDate);
                DateTime? ForecastDate = TryParseNullableDateTime(previousTask!.ForecastDate);
                DateTime? Deadline = TryParseNullableDateTime(previousTask!.DeadlineDate);
                EngineerExperience? CompmlexityLevel = TryParseNullableEngineerExperience(previousTask!.CompmlexityLevel);
                DO.Task newTask = new(ID, EngineerId, Description, Alias, true, Deliverables, Remarks, createAt, ScheduleDate, ForecastDate, Deadline, null, CompmlexityLevel);
                s_dal!.Task.Update(newTask);
                task();
            }
            catch(DalIsNotExistException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); };
        }
        /// <summary>
        ///Engineer function to add, read, delete and update an engineer. 
        /// </summary>
        public static void engineer()
        {
            Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
            int choice;
            int.TryParse(Console.ReadLine()!, out choice);
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
                default:
                    throw new NullReferenceException("There is no such choice");
            }

        }

        /// <summary>
        /// Creates new Engineer object in DAL
        /// </summary>
        public static void createEngineer()
        {
            try
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
                s_dal!.Engineer.Create(newEngineer);
                engineer();
            }
            catch (DalAlreadyExistsException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }

        /// <summary>
        /// Reads Engineer object by its ID 
        /// </summary>
        public static void readEngineer()
        {
            int id;
            Console.WriteLine("Enter Engineer's id to read");
            int.TryParse (Console.ReadLine(), out id);
            Console.WriteLine(s_dal!.Engineer.Read(id));
            engineer();
        }

        /// <summary>
        /// Updates Engineer object
        /// </summary>
        public static void UpdateEngineer()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Engineer's id to update");
                int.TryParse(Console.ReadLine(), out id);
                DO.Engineer? previousEngineer = s_dal!.Engineer.Read(id);
                if (previousEngineer == null)
                    throw new DalIsNotExistException($"engineer with ID={id} is not exists");
                Console.WriteLine("the engineer you want to update: "+previousEngineer);
                Console.WriteLine("Enter Engineer Id, cost, name, email, level");
                double cost;
                if (!double.TryParse(Console.ReadLine(), out cost))
                    cost = previousEngineer!.Cost;
                string? name ,email;
                name = stringIsNullOrEmpty(previousEngineer!.Name);
                email = stringIsNullOrEmpty(previousEngineer!.Email);
                EngineerExperience? level = TryParseNullableEngineerExperience(previousEngineer!.Level);
                DO.Engineer newTask = new(id, cost, name, email, level);
                s_dal!.Engineer.Update(newTask);
                engineer();
            }
            catch (DalIsNotExistException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }

        /// <summary>
        /// Reads all Engineer objects
        /// </summary>
        public static void readAllEngineer(Func<Engineer?, bool>? filter = null)
        {
            s_dal!.Engineer.ReadAll(filter).ToList().ForEach(engineer => Console.WriteLine(engineer));
            engineer();
        }

        /// <summary>
        /// Deletes an object by its Id
        /// </summary>
        public static void deleteEngineer()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Engineer's id to delete");
                int.TryParse(Console.ReadLine(), out id);
                s_dal!.Engineer.Delete(id);
                engineer();
            }
            catch (DalIsNotExistException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); }
        }

        /// <summary>
        /// Dependency function to add, read, delete and update dependencies
        /// </summary>
        public static void dependency()
        {
            Console.WriteLine("Select the desired action:\n1-create\n2-read\n3-update\n4-read all\n5-delete\n0-exit");
            int choice;
            int.TryParse(Console.ReadLine()!, out choice);
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
                default:
                    throw new NullReferenceException("There is no such choice");
            }
        }

        /// <summary>
        /// Creates new Dependency object in DAL
        /// </summary>
        public static void createDependency()
        {
            Console.WriteLine("DependentTask, DependOnTask");
            int DependentTask, DependOnTask;
            int.TryParse(Console.ReadLine()!, out DependentTask);
            int.TryParse(Console.ReadLine()!, out DependOnTask);
            DO.Dependency newDependency = new(0, DependentTask, DependOnTask);
            s_dal!.Dependency.Create(newDependency);
            dependency();
        }

        /// <summary>
        /// Reads Dependency object by its ID 
        /// </summary>
        public static void readDependency()
        {
            int id;
            Console.WriteLine("Enter Dependency's id to read");
            int.TryParse(Console.ReadLine()!, out id);
            Console.WriteLine(s_dal!.Dependency.Read(id));
            dependency();
        }

        /// <summary>
        /// Updates Dependency object
        /// </summary>
        public static void UpdateDependency()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Dependency's id to update");
                int.TryParse(Console.ReadLine()!, out id);
                DO.Dependency? previousDependency = s_dal!.Dependency.Read(id);
                if( previousDependency == null )
                    throw new DalIsNotExistException($"dependency with ID={id} is not exists");
                Console.WriteLine("the dependency you want to update: "+previousDependency);
                Console.WriteLine("Enter DependentTask, DependOnTask");
                int DependentTask, DependOnTask;
                if (!int.TryParse(Console.ReadLine(), out DependentTask))
                    DependentTask = previousDependency!.DependentTask;
                if (!int.TryParse(Console.ReadLine(), out DependOnTask))
                    DependOnTask = previousDependency!.DependOnTask;
                DO.Dependency? UpdateDependency = new(id, DependentTask, DependOnTask);
                s_dal!.Dependency.Update(UpdateDependency);
                dependency();
            }
            catch (DalIsNotExistException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex) 
            { Console.WriteLine(ex); }
           
        }

        /// <summary>
        /// Reads all Dependency objects
        /// </summary>
        public static void readAllDependency(Func<Dependency?, bool>? filter = null)
        {
            s_dal!.Dependency.ReadAll(filter).ToList().ForEach(dependency => Console.WriteLine(dependency));
            dependency();
        }

        /// <summary>
        /// Deletes an object by its Id
        /// </summary>
        public static void deleteDependency()
        {
            try
            {
                int id;
                Console.WriteLine("Enter Dependency's id to delete");
                int.TryParse(Console.ReadLine()!, out id);
                s_dal!.Dependency.Delete(id);
                dependency();
            
        }
            catch (DalIsNotExistException ex)
            { Console.WriteLine(ex); }
            catch (Exception ex)
            { Console.WriteLine(ex); }
}
        /// <summary>
        /// --------------------------------------------------------------
        /// Initialize the date in the input entered by the user or in the
        ///value received by the function if no value is entered
        ///--------------------------------------------------------------
        /// </summary>
        /// <param name="previous">the previous date</param>
        public static DateTime? TryParseNullableDateTime(DateTime? previous)
        {
            DateTime value;
            return DateTime.TryParse(Console.ReadLine(), out value) ? value : previous;
        }

        /// <summary>
        ///--------------------------------------------------------------
        ///Initialize the level in the input entered by the user or in the
        ///value received by the function if no value is entered
        ///--------------------------------------------------------------
        /// </summary>
        /// <param name="previous">the previous Engineer Experience</param>


        public static EngineerExperience? TryParseNullableEngineerExperience(EngineerExperience? previous)
        {
            EngineerExperience value;
            return EngineerExperience.TryParse(Console.ReadLine(), out value) ? value : previous;
        }

        /// <summary>
        ///--------------------------------------------------------------
        /// Initialize the string in the input entered by the user or in the
        /// value received by the function if no value is entered
        ///--------------------------------------------------------------
        /// </summary>
        /// <param name="previous">the previous string value</param>
        public static string? stringIsNullOrEmpty(string? previous)
        {
            string? value= Console.ReadLine();
            return string.IsNullOrEmpty(value) ? previous : value;
        }

        /// <summary>
        /// Main function to select an engineer, task or dependency
        /// </summary>
        public static void menu()
        {
            while (true)
            {
                Console.WriteLine("Select an entity to test:\n1-task\n2-engineer\n3-dependency\n0-exit");
                int choice;
                int.TryParse(Console.ReadLine()!, out choice);
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
                        throw new IsNotChoiceException("There is no such choice");
                }
            }
        }
    }   
}

         
