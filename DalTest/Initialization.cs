

namespace DalTest;
using DalApi;
using DO;
public static class Initialization
{
    private static IDependency? s_dalDependency; 
    private static IEngineer? s_dalEngineer; 
    private static ITask? s_dalTask;
    private static readonly Random s_rand = new();
    private static void createDependency()
    {

    }
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
            Engineer newEngineer = new(id, cost, name, null);
            s_dalEngineer!.Create(newEngineer);
            //צריך לבדוק לגבי level
        }
    }
}
