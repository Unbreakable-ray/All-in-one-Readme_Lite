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
    using System;
    using System.IO;
    using System.Threading;
using All_in_one_MD_Lite;

class Program
{

    
    static void Main(string[] args)




    {
       





        //------------------//start the engine//------------------//
        Console.OutputEncoding = Encoding.UTF8;
        //Dir//

        //get app location  
       // string appDir = AppDomain.CurrentDomain.BaseDirectory; //defult location
       string appDir = @"C:\Users\max\Desktop\steam_writing_assistant\sub-readme\App";

        //get files name on ../appDir/
        string path = Path.Combine(appDir, "..");
        string fullPath = Path.GetFullPath(path);

        string mergedFilePath = Path.Combine(appDir, "..", "..");

        string status = "no info";
        string status2 = "";
        string statusOldReadmeBak = "There isn't old \"Readme.md\" found.";
        //Console.WriteLine(path);
        //Console.WriteLine(fullPath);

        
        var info = "";
       ////////////////////////////////////////////////////////////////////////////
        
        
        //---------------------!!!MUST CHANGE!!!!!!----///
        String appVersion = " v1.1.0";



        ////////////////////////////////////////////////////////////
        
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
        Console.WriteLine("\nby @xMxrayx & @Unbreakable-ray\n\nLogs:");

        //start logs msg
        Console.WriteLine("[+][Strt]\tStart logs");
        Console.WriteLine("[+][info]\tLoad  the app directory\t(done)");

        //////////////////!/////////////////////////////////////////////////////////


        //string[] filesNames = Directory.GetFiles(path);


       
        // Assuming the file extensions are separated by spaces in the text file
        // Remove the double quotes from each extension
        string[] validExtensions = fileExtension.Split(' ').Select(x => x.Trim('"')).ToArray();
        // Print the values of the validExtensions array
        Console.WriteLine("[+][info]\tValid extensions are: " + string.Join(", ", validExtensions) + "\t(done)");
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

        string oldMergedFile = Path.Combine(mergedFilePath, "oldReadme.bak.md");

        string oldMergedFileRename;

        // Check if the merged file already exists
        if (File.Exists(mergedFile))
        {
            //check if old back exit
            if (File.Exists(oldMergedFile)) //found oldreadme.bak
            {
                // Find the next available number for the bak file
                int i = 1;
                while (File.Exists(Path.Combine(mergedFilePath, "oldReadme.bak" + i + ".md")))
                {
                    i++;

                }

                // Create a new name for file bak 
                oldMergedFileRename = Path.Combine(mergedFilePath, "oldReadme.bak" + Convert.ToString(i) + ".md");
                string statusConsoleoldMergedFilename = Convert.ToString("oldReadme.bak" + Convert.ToString(i) + ".md");

                // rename redme to file.bak+(i)
                File.Move(mergedFile, oldMergedFileRename);
                statusOldReadmeBak = Path.GetFullPath(oldMergedFileRename);
                Console.WriteLine("[+][info]\tAn \"oldredme.bak\" file found and rename it to \"" + statusConsoleoldMergedFilename+"\"\t(done)");
            }
            else
            {   // no old file  oldreadme.bak found
                
                File.Move(mergedFile, "oldReadme.bak.md");//rename

                statusOldReadmeBak = Path.GetFileName(oldMergedFile); // dir by string 
                mergedFile = Path.Combine(mergedFilePath, "Readme.md"); //for new readme file
                Console.WriteLine("[+][info]\tAn old redme file found and rename it to \"oldReadme.bak.md\"\t(done)");

            }
        }


        mergedFile = Path.Combine(mergedFilePath, "Readme.md");////////////////////////////////////////////////////////////////?wtf fix his maybe




        //--------------------------//auto exit//-------------------------------//




        //--------------------------//merge//-------------------------------//
        ConsoleKeyInfo key;
        //string variable;
        string variable2;
        try //do the work-------------------------------------------------------------------------------------------------------[file found]
        {
            string content;
            string headerText = File.ReadAllText(Path.Combine(appDir, "header.md"));
            string footerText = File.ReadAllText(Path.Combine(appDir, "footer.md"));
            string authorText = File.ReadAllText(Path.Combine(appDir, "author.md"));
            
            Console.WriteLine("[+][info]\tStart merging opration ........ \t");

















            //merge all files in one and add new line
            using (StreamWriter writer = new StreamWriter(mergedFile))
            {

                foreach (var file in orderedFiles) //Loop through the ordered files and append their content to the merged file
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        writer.Write(reader.ReadToEnd());
                        writer.WriteLine();// Add a new line after each file content

                    }
                }



                writer.Write(headerText);
                writer.WriteLine("\nAuto-merged at " + DateTime.Now.ToString("hh:mm:ss tt yyyy/MM/dd") + "\t" + (authorText) + "\n");
                writer.Write(footerText);
            }






            ////////////////////////////////////////////////////////replace 
            using (StreamReader reader = new StreamReader(mergedFile))
            {
                // Assign a new value to the content variable
                content = reader.ReadToEnd();
                content = content.Replace("./readme-data", "./sub-readme/readme-data");
                Console.WriteLine("[!][info]\tchange text from ./readme-data\" to ./sub-readme/readme-data");
            }

            using (StreamWriter writer = new StreamWriter(mergedFile))
            {
                writer.Write(content);

            }


            ///////////////////////////////////////////////////////////add tree
            ///



            /*
            // Create a StreamReader object to read from a file
            using (StreamReader sr = new StreamReader(mergedFile))
            {
                string input = sr.ReadToEnd();
                  sr.Close();

                // Create a StreamWriter object to write to another file
                using (StreamWriter sw = new StreamWriter(mergedFile, true))
                {       

                    
                    // Read a line of text from the input file
                     
                    
                    // Check if the line starts with "#"
                    if (input.StartsWith("#"))
                    {
                        // Remove "#" and save it as a string variable
                        string variable = input.TrimStart('#');
                       
                        sw.WriteLine("({0})[#{0}]", variable);
                    }
                }
            }

            */























                        
                        using (var sr = new StreamReader(mergedFile))
                        {
                            // Read the stream line by line
                            string line;
                            // Declare and initialize a list to store the variables
                            List<string> variables = new List<string>();
                            
                            while ((line = sr.ReadLine()) != null)
                            {
                                // Check if the line starts with "#"
                                if (line.StartsWith("#"))
                                {
                                       string  var = line.TrimStart('#').TrimStart(); //remove all space bettwen #->litters
                                     //    var = var.TrimStart(' ').TrimStart();
                                        //  var = var.Replace(@"\s+", "");
                                        //var = line.Substring(1);// Remove the "#" and save it as a string variable
                                             var = var.Trim();
                        ///manbalte var2

                        string var2 = line;
                        var2 = var2.Trim(); //remove space from start and end
                        
                        var2 = var2.Replace("#", "");//replace # with space
                        var2 = var2.Trim();
                        var2 = var2.ToLower();
                        var2 =var2.Insert(0,"#");// add
                        // var2 = var2.Replace(@"\s+", "-");
                        var2 = var2.Replace(" ", "-");



                        variables.Add($"[-{var}]({var2})\n");






                                }
                            }

                            // Close the stream reader
                            sr.Close();

                            // Use the StreamWriter constructor with the append parameter set to true
                            using (StreamWriter writer = new StreamWriter(mergedFile, true))
                            {
                                // Loop through the list and write each variable to the file
                                    foreach (string var in variables)
                                    {
                                        writer.WriteLine(var);
                                    }
                                // Close the stream writer
                                writer.Close();
                            }
                        }
                        




                        //////////////////////////////////////////////write the readme.md








                        status = "Complated";
                status2 = "\n\nThe Auto-merged file location is: " + (Path.GetFullPath(mergedFile));

                ////good bye msg
                Console.WriteLine("[+][END]\tDone");
                Console.WriteLine("\n\n" + status + " at:\t" + DateTime.Now.ToString("hh:mm:ss tt") + "\t" + DateTime.Now.ToString("yyyy/MM/dd"));
                Console.WriteLine(status2);
                Console.WriteLine("\nAn old readme file was found and changed it's name:\t" + (statusOldReadmeBak));
                Console.WriteLine("\nDone please chose:");
                Console.WriteLine("- Press \"O\" or \"R\" key to Open the file Or Press any key to Exit.");
                Console.WriteLine("- Press \"E\" to open file directory.");
                Console.WriteLine("- Press \"Any key else\" to Exit.");
                Console.WriteLine("\nWaiting for key....... \nthe App will exit after 10 seconds..........");


                //--------------------------//auto close//-------------------------------//

                Timer timer = new Timer(CloseProgram, null, 10000, Timeout.Infinite);

                static void CloseProgram(object? state)
                {
                    Environment.Exit(0);
                }





                key = Console.ReadKey(true); //exit or open
                if (key.KeyChar == 'o' || key.KeyChar == 'O' || key.KeyChar == 'r' || key.KeyChar == 'R')
                {

                    Console.WriteLine("\nOpenning ....."); //open file
                    Thread.Sleep(1000);
                    Process.Start(new ProcessStartInfo(mergedFile) { UseShellExecute = true });
                    Thread.Sleep(1000);
                    Environment.Exit(0);


                }

                if (key.KeyChar == 'e' || key.KeyChar == 'E')
                {

                    Process.Start("explorer.exe", mergedFilePath);
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
            Console.WriteLine("\n\n\nPress \"R\" key to Restart Or Press any key to Exit");
            Console.WriteLine("\nWaiting for key.....");



            key = Console.ReadKey(true);
            if (key.KeyChar == 'r' || key.KeyChar == 'R')
            {
                //info = AppDomain.CurrentDomain.BaseDirectory; //app dir
                //info = Environment.GetCommandLineArgs()[0];
                //info = Assembly.GetExecutingAssembly().Location;

                //info = Process.GetCurrentProcess().MainModule.FileName;
                info = Environment.ProcessPath;

                Console.WriteLine("\nRestarting .....\n"); //show restart msg


                Thread.Sleep(500);
                Console.WriteLine(info);

                Process.Start(new ProcessStartInfo(info));
                //Process.Start(new ProcessStartInfo(Environment.ProcessPath));

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