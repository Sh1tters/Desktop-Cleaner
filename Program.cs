using System;
using System.IO;
using System.Threading;

namespace Desktop_Cleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Cleaning up...");
            int amount = 0;
        

        string env = @"%USERPROFILE%\Downloads";
            string path = Environment.ExpandEnvironmentVariables(env);
            Console.WriteLine("PATH: "+path);
            Thread.Sleep(300);
            System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(path);
            foreach (FileInfo file in di.EnumerateFiles())
            {

                amount++;
                Console.WriteLine("Removed "+file.Name);
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.EnumerateDirectories())
            {
                dir.Delete(true);
            }
            Console.WriteLine("Removed " + amount + " in total");
            Thread.Sleep(400);
            Console.WriteLine("Want to clean up desktop? (yes/no)");
            string res = Console.ReadLine();
            if (res.StartsWith("yes"))
            {
                string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Console.WriteLine("FOUND DESKTOP: " + desktop);
                Thread.Sleep(1000);
                Console.WriteLine("Moving files from " + desktop + "(desktop) to " + documents + "\\DesktopRecovery");
                
                if (!Directory.Exists(documents + "\\DesktopRecovery"))
                {
                    Directory.CreateDirectory(documents + "\\DesktopRecovery");
                    goto startProcess;
                }
                if (Directory.Exists(documents + "\\DesktopRecovery"))
                {
                    goto startProcess;
                }
    
            startProcess:
                {

                    int x = 0;
                    System.IO.DirectoryInfo did = new System.IO.DirectoryInfo(desktop);
                    foreach(FileInfo file in did.EnumerateFiles())
                    {
                        x = x + 1;
                        Console.WriteLine("Moving " + file.Name + " to " + documents + "\\DesktopRecovery");
                        string ta = documents + "\\DesktopRecovery";
                        file.MoveTo(ta + "\\" + file.Name);   
                    }
                    Console.WriteLine("Moved {0} in total", x);
                }
            } 
    }
    }
}
