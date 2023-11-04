﻿

namespace DalTest;
using DalApi;
using DO;
using System.Collections.Generic;

public static class Initialization
{
    private static IDependency? s_dalDependency; 
    private static IEngineer? s_dalEngineer; 
    private static ITask? s_dalTask;
    private static readonly Random s_rand = new();
    //Initialize the list of dependencys
    private static void createDependency()
    {
     for (int i = 0; i < 100; i++)
     {
      int DependentTask= s_rand.Next(1,251);
      int DependOnTask = s_rand.Next(1, DependentTask);
      Dependency newDependency = new(0, DependentTask, DependOnTask);
      s_dalDependency!.Create(newDependency);
     }
    }
    //Initialize the list of engineers
    private static void createEngineer()
    {
        string[] engineerNames =
        {
            "Ayala Levi",
            "Bracha Klein",
            "Gad Kohen",
            "Dan Kanner",
            "Hadar Fried",
            "Vered Moses",
            "Zisi Zander",
            "Chaya Dror",
            "Tova Levin",
            "Yehudit Gamliel",
            "Yael Solomon",
            "Mali Kanal",
            "Moshe Shif",
            "Hadas Barzel",
            "Tzipi Ziv",
            "Ruth Kepler",
            "Shaul Ben David",
            "David Solomon",
            "Chava Shor",
            "shira Avraham",
            "Avi Polack",
            "Mina Kaplan",
            "Benni Chen",
            "Sara Gefner",
            "Chaim Shitrit",
            "chana Montag",
            "Shoshi Miller",
            "Yosef Chefetz",
            "Tamar Frenckel",
            "Aharon Negev",
            "Tzvi Sason",
            "Akiva Sokol",
            "Zehev Segal",
            "Rachel Gefen",
            "Leah Mimran",
            "Ariel Green",
            "Pinchas Tzur",
            "Nechama Meller",
            "Dina Bagad",
            "Natan Regev"
        };
        foreach (string name in engineerNames)
        {
            int id, MINid= 200000000, MAXid= 400000000;
            do
                id = s_rand.Next(MINid, MAXid);
            while (s_dalEngineer!.Read(id) != null);
            double cost= s_rand.Next(14,30)*5;
            EngineerExperience level=(EngineerExperience)s_rand.Next((int)EngineerExperience.Novice,(int)EngineerExperience.Expert+1);
            string email = name.Replace(" ", "") + "@gmail.com";
            Engineer newEngineer = new(id, cost, name, email, level);
            s_dalEngineer!.Create(newEngineer);
        }
    }
    //Initialize the list of tasks
    private static void createTask()
    {
        string[] Deliverables =
        {
            "Project plan",
            "User manual",
            "Executable code module",
            "Design document",
            "Code listing",
            "software product"
        };
        List<Engineer> Engineers= s_dalEngineer!.ReadAll();
        for (int i = 0; i < 250; i++)
        {
            int index=s_rand.Next(0, Engineers.Count);
            int EngineerId= Engineers[index].Id;
            int moreDays= s_rand.Next(3, 31),moreMonths = s_rand.Next(0, 10);
            DateTime start = DateTime.Now.AddDays(moreDays);
            DateTime Deadline=start.AddMonths(4+ moreMonths).AddDays(moreDays);
            DateTime ForecastDate = Deadline.AddDays(-(moreDays + 10));
            EngineerExperience CompmlexityLevel = (EngineerExperience)s_rand.Next((int)EngineerExperience.Novice, (int)EngineerExperience.Expert + 1);
            index = s_rand.Next(0, Deliverables.Length);
            string myDeliverable = Deliverables[index];
            Task task = new(0, EngineerId, null, null, false, true, myDeliverable, null, start, ForecastDate, Deadline, null, CompmlexityLevel);
            s_dalTask!.Create(task);
        }
    }
    public static void Do(IEngineer? dalEngineer, IDependency? dalDependency, ITask? dalTask)
    {
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        createDependency();
        createEngineer();
        createTask();
    }
}
