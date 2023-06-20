using System;
using System.IO;
using System.Linq;
using System.Globalization;
using static System.IO.File;
using System.Text.RegularExpressions;
using System.Reflection.PortableExecutable;
using System.Threading;
using System.Text;

class Program
{   
    static void Main(string[] args)
    {
        //---------------------!!!MUST CHANGE!!!!!!----///
        String appVersion = " v1";

        //------------------//start the engine//------------------//
        Console.OutputEncoding = Encoding.UTF8;

       
        // string systemVersion = Convert.ToString(Environment.OSVersion);

        OperatingSystem os = Environment.OSVersion;
        string arch = Environment.Is64BitOperatingSystem ? "64-bit" : "86-bit";





        //wolcome msg
        Console.WriteLine("Welcome,in \"all-in-one MD\" files Auto-merge software \t [version:" + appVersion+"]");
        Console.WriteLine("System: " + os +"\tArch: "+ arch);

        Console.WriteLine("\nby @xMxrayx & @Unbreakable-ray\n\n\n\nLogs:");
       



        //get app location  
        // string appDir = AppDomain.CurrentDomain.BaseDirectory; //defult location
        string appDir = @"C:\Users\max\Desktop\steam_writing_assistant\ss";


        Console.WriteLine("[+][info]\tLoad  the app directory\t(done)");



        //get files name on ../appDir/notepad
        string path = Path.Combine(appDir, "notebad");
        string[] filesNames = Directory.GetFiles(path);



        //-------------------//sort//----------------------//


        //sort [number] by LINQ and Regex
        Console.WriteLine("[+][info]\tOrganize files names   \t(done)");
        var orderedFiles = filesNames.OrderBy(f =>
        {
            // Try to match the pattern [number] in the file name
            var match = Regex.Match(Path.GetFileName(f), @"\[(\d+)\]");
            // If matched, return the number, otherwise return int.MaxValue
            return match.Success ? int.Parse(match.Groups[1].Value) : int.MaxValue;
        });






        //-----------------//  create  [auto-merge] file on (../appDir)        //-----------------//

        
        string mergedFile = Path.Combine(appDir, "..", "Readme.md");
        



        // Check if the merged file already exists
        if (File.Exists(mergedFile))
        {
            // Find the next available number for the merged file
            int i = 1;
            while (File.Exists(Path.Combine(appDir, "..", "Readme" + i + ".md")))
            {
                i++;
            }
            // Create a new file with the number
            mergedFile = Path.Combine(appDir, "..", "Readme" + Convert.ToString(i) + ".md");
        }




        //--------------------------//merge//-------------------------------//
        //ingerdian


        //string header = Path.Combine(appDir, "header.md");
        //string footer = Path.Combine(appDir, "footer.md");
        // authorText = File.ReadAllText(Path.Combine(appDir, "author.md"));

     
            
                    try //do the work
                            {

                                string headerText = File.ReadAllText(Path.Combine(appDir, "header.md"));
                                string footerText = File.ReadAllText(Path.Combine(appDir, "footer.md"));
                                string authorText = File.ReadAllText(Path.Combine(appDir, "author.md"));


                                    //lets coock
                                    using (StreamWriter writer = new StreamWriter(mergedFile))
                                    {
                                        // Loop through the ordered files and append their content to the merged file
                                        foreach (var file in orderedFiles)
                                        {
                                            using (StreamReader reader = new StreamReader(file))
                                            {
                                                writer.Write(reader.ReadToEnd());
                                                // Add a new line after each file content
                                                writer.WriteLine();
                                            }
                                        }



                                        writer.Write(headerText);
                                        writer.WriteLine("\nEdited at " + DateTime.Now.ToString("hh:mm:ss tt yyyy/MM/dd") + "\t" + (authorText) + "\n");
                                        writer.Write(footerText);

                                    }
                            }


        

                   
                    catch (FileNotFoundException)  //if we dont find a file
                    {
                        // Ignore the error and continue
                        Console.WriteLine("[!][Warn]\tSome files are missing skipping them\t(DONE)[!]");
                    }



        //--------------------------// good bye //---------------------------//

        
        //Console.WriteLine("( •ॢ◡-ॢ)_o");



        // Print the merged file name
        Console.WriteLine("\n\n\n\nThe Auto-merged file location is: " + mergedFile);
        Console.WriteLine("Completed at:\t" + DateTime.Now.ToString("hh:mm:ss tt") + "\t" + DateTime.Now.ToString("yyyy/MM/dd"));
        
        
        
        
        //press any key to escape
        Console.WriteLine("\nPress any key to Exit");
        Console.ReadKey();
        Console.WriteLine("\nOk have a good day <3 \t\t\t\t\t\t\t\t ^_^  <3");


      






        //exit
        Thread.Sleep(1000);
        Environment.Exit(0);
        
    }


}
