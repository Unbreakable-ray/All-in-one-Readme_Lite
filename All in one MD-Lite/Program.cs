//@xmaxrayx //@unbreakable-ray

    using System;
    using System.IO;
    using System.Linq;
    using System.Globalization;
    using static System.IO.File;
    using System.Text.RegularExpressions;
    using System.Reflection.PortableExecutable;
    using System.Threading;
    using System.Text;
    using System.Reflection.Metadata;
    using static System.Net.Mime.MediaTypeNames;
    using System.Diagnostics;
using System.Reflection;





class Program
{
    static void Main(string[] args)




    {


        //------------------//start the engine//------------------//
        Console.OutputEncoding = Encoding.UTF8;
        //Dir//

        //get app location  
        string appDir = AppDomain.CurrentDomain.BaseDirectory; //defult location
        //string appDir = @"C:\Users\max\Desktop\steam_writing_assistant\sub-readme\App";

        //get files name on ../appDir/
        string path = Path.Combine(appDir, "..");
        string fullPath = Path.GetFullPath(path);

        string mergedFilePath = Path.Combine(appDir, "..", "..");

        string status = "no info";
        string status2 = "";
        //Console.WriteLine(path);
        //Console.WriteLine(fullPath);


        var info ="";
        //---------------------!!!MUST CHANGE!!!!!!----///
        String appVersion = " v1";




        
        // system version
        OperatingSystem os = Environment.OSVersion;
        string arch = Environment.Is64BitOperatingSystem ? "64-bit" : "86-bit";


        //---------------load file extantion sttings----------------//


        string fileExtensionPath;
        string fileExtension;
        try
        {
            fileExtensionPath = Path.Combine(appDir + "\\file-extensions.txt");
            fileExtension = System.IO.File.ReadAllText(fileExtensionPath);
        }
        catch (FileNotFoundException)
        {
            fileExtension = ".txt .md .text .mmd";


        }
        




 














        //---------------welcome msg----------------//

        // Console.WriteLine(fileExtension);


        //wolcome msg
        Console.WriteLine("\nWelcome,in \"All-in-one Lite\" .MD files Auto-merge software \t [version:" + appVersion + "]");
        Console.WriteLine("System: " + os + "\tArch: " + arch);
        Console.WriteLine("\nby @xMxrayx & @Unbreakable-ray\n\n\n\nLogs:");

        //start logs msg
        Console.WriteLine("[+][Strt]\tStart logs");
        Console.WriteLine("[+][info]\tLoad  the app directory\t(done)");

        //////////////////!/////////////////////////////////////////////////////////


        //string[] filesNames = Directory.GetFiles(path);


       
        // Assuming the file extensions are separated by spaces in the text file
        // Remove the double quotes from each extension
        string[] validExtensions = fileExtension.Split(' ').Select(x => x.Trim('"')).ToArray();
        // Print the values of the validExtensions array
        Console.WriteLine("[+][info]\tValid extensions: " + string.Join(", ", validExtensions));
        string[] filesNames = Directory.EnumerateFiles(path) // Enumerate all files
                    .Where(file => validExtensions.Contains(Path.GetExtension(file))) // Filter by extension
                    .ToArray(); // Convert to array




        //////////////////////////////////////////////////////////////////////////


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


        string mergedFile = Path.Combine(mergedFilePath, "Readme.md");




        // Check if the merged file already exists
        if (File.Exists(mergedFile))
        {
            // Find the next available number for the merged file
            int i = 1;
            while (File.Exists(Path.Combine(mergedFilePath, "Readme" + i + ".md")))
            {
                i++;
            }
            // Create a new file with the number
            mergedFile = Path.Combine(mergedFilePath, "Readme" + Convert.ToString(i) + ".md");
        }




        //--------------------------//merge//-------------------------------//
        ConsoleKeyInfo key; // Declare a ConsoleKeyInfo variable

        try //do the work-------------------------------------------------------------------------------------------------------[file found]
        {
            string content;
            string headerText = File.ReadAllText(Path.Combine(appDir, "header.md"));
            string footerText = File.ReadAllText(Path.Combine(appDir, "footer.md"));
            string authorText = File.ReadAllText(Path.Combine(appDir, "author.md"));

            Console.WriteLine("[+][info]\tStart merging opration ........ \t");

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
                writer.WriteLine("\nUpdated at " + DateTime.Now.ToString("hh:mm:ss tt yyyy/MM/dd") + "\t" + (authorText) + "\n");
                writer.Write(footerText);
            }

            //replace
            using (StreamReader reader = new StreamReader(mergedFile))
            {
                // Assign a new value to the content variable
                content = reader.ReadToEnd();
                content = content.Replace("./readme-data", "./sub-readme/readme-data");

            }

            using (StreamWriter writer = new StreamWriter(mergedFile))
            {
                writer.Write(content);

            }
            status = "Complated";
            status2 = "\n\n\n\nThe Auto-merged file location is: " + (Path.GetFullPath(mergedFile));

            ////good bye msg
            Console.WriteLine("[+][END]\tDone");
            Console.WriteLine("\n\n" + status + " at:\t" + DateTime.Now.ToString("hh:mm:ss tt") + "\t" + DateTime.Now.ToString("yyyy/MM/dd"));
            Console.WriteLine(status2);
            Console.WriteLine("\nPress \"O\" or \"R\" key to Open the file Or Press any key to Exit.\nWaiting for key.....");



            key = Console.ReadKey(true); //exit or open
            if (key.KeyChar == 'o' || key.KeyChar == 'O' || key.KeyChar == 'r' || key.KeyChar == 'R')
            {
                
                Console.WriteLine("\nOpenning ....."); //open file
                Thread.Sleep(1000);
                Process.Start(new ProcessStartInfo(mergedFile) { UseShellExecute = true });
                Thread.Sleep(1000);
                Environment.Exit(0);


            }
            else                                        //exit
            {
                Console.WriteLine("\nOk have a good day <3 \t\t\t\t\t\t\t\t ^_^  <3");
                Console.WriteLine("\nExiting...");

                //exit
                Thread.Sleep(1000);
                Environment.Exit(0);

            }




        }





        catch (FileNotFoundException)  //if we dont find a file ----------------------------------------------------------------------------[no file]
        {
            // Ignore the error and continueh
            Console.WriteLine("[!][Warn]\tSome files are missing abroting\t(!)");
            status = "[!] Abroted";
            status2 = "[!] Make sure you have header.md, footer.md author.md in App Dirouctry";

            Console.WriteLine("[!][EROR]\tMerging Opration is not complaited (!)");
            Console.WriteLine("[!][info]\tEnd of logs");
            Console.WriteLine("\n\n" + status + " at:\t" + DateTime.Now.ToString("hh:mm:ss tt") + "\t" + DateTime.Now.ToString("yyyy/MM/dd"));
            Console.WriteLine(status2);
            Console.WriteLine("\n\n\nPress \"R\" key to Restart Or Press any key to Exit\nWaiting for key.....");
           


            key = Console.ReadKey(true);
              if (key.KeyChar == 'r' || key.KeyChar == 'R')
                {
                //info = AppDomain.CurrentDomain.BaseDirectory; //app dir
                //info = Environment.GetCommandLineArgs()[0];
                //info = Assembly.GetExecutingAssembly().Location;

                info = Process.GetCurrentProcess().MainModule.FileName;


                Console.WriteLine("\nRestarting .....\n"); //show restart msg


                Thread.Sleep(500);
                Console.WriteLine(info);
                
                Process.Start(new ProcessStartInfo(info));
                

            }
            else
            {   
                Console.WriteLine("\nExiting..."); 

                //exit
                Thread.Sleep(1000);
                Environment.Exit(0);

            }

        }



        //--------------------------// good bye //---------------------------//


        //Console.WriteLine("( •ॢ◡-ॢ)_o");


        /*
        // show merged file location
        Console.WriteLine(status + " at:\t" + DateTime.Now.ToString("hh:mm:ss tt") + "\t" + DateTime.Now.ToString("yyyy/MM/dd"));




      
        Console.WriteLine(status2);
        Console.WriteLine("\nPress any key to Exit");
 
        Console.WriteLine("\nOk have a good day <3 \t\t\t\t\t\t\t\t ^_^  <3");
        // Console.ReadKey();

        
      
        */
        }


    }

//@xMaxray @unbreakable-ray