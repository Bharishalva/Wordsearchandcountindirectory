using countofwords;
using Microsoft.Extensions.Options;
using PowerArgs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace countofwords
{
    class WordSearchClass
    {   /// <summary>
        ///Searching the key word using LINQ Method
        /// </summary>
        /// <param name="FilePathList"> file path list</param>
        /// <param name="keySearch">keyword for search</param>
        public void SearchWordUsingLinq(List<String> FilePathList, String keySearch)

        {
            try
            {
                var OutputList = new List<String>();
                int TotalCount = 0;
                var watch = new System.Diagnostics.Stopwatch();
                var tasks = new List<Task>();
                watch.Start();
                var xCount = 0;
                foreach (var x in FilePathList)
                {
                    var c = File.ReadAllText(x).Split(' ');
                    xCount = c.Count(c => c.Equals(keySearch, StringComparison.OrdinalIgnoreCase));
                    if (xCount > 0)                                            //finding if the word is present in the file
                    {
                        OutputList.Add(Path.GetFileName(x));

                    }
                    TotalCount += xCount;
                }       

                watch.Stop();
                //stop execution timer
                Console.WriteLine("\nLinq Search\n");
                if (TotalCount > 0)
                {
                    Console.WriteLine($"\nWord Is Found in These Files:");
                    foreach (var x in OutputList)
                        Console.WriteLine($"\n{x}");
                }
                else
                {
                    Console.WriteLine("\nNot Found");
                }
                Console.WriteLine($"\nTotal Count: { TotalCount}\nExecution Time : {(watch.ElapsedMilliseconds) / 1000}Sec {watch.ElapsedMilliseconds % 1000}Ms");
            }
            catch
            {
                Console.WriteLine("\nError");
            }

        }
        /// <summary>
        /// Search using Async LINQ
        /// </summary>
        /// <param name="FilePathList">File list</param>
        /// <param name="keySearch">search word</param>
        public void SearchWordUsingAsyncLinq(List<String> FilePathList, String keySearch)

        {
            try
            {
                var OutputList = new List<String>();
                int TotalCount = 0;
                var watch = new System.Diagnostics.Stopwatch();
                var tasks = new List<Task>();
                watch.Start();
                var xCount = 0;
                foreach (var x in FilePathList)
                {

                   tasks.Add(Task.Run(async () =>
                  {

                      
                      var cx = await File.ReadAllTextAsync(x);
                      var c = cx.Split(' ');
                      xCount = c.Count(c => c.Equals(keySearch, StringComparison.OrdinalIgnoreCase));
                      if (xCount > 0)                                            
                      {
                          OutputList.Add(Path.GetFileName(x));
                          
                      }
                      TotalCount += xCount;
                     

                  }));


                }
                

                Task.WhenAll(tasks).Wait();

                watch.Stop();
                //stop execution timer
                Console.WriteLine("\nLinq Search\n");
                if (TotalCount > 0)
                {
                    Console.WriteLine($"\nWord Is Found in These Files:");
                    foreach (var x in OutputList)
                        Console.WriteLine($"\n{x}");
                }
                else
                {
                    Console.WriteLine("\nNot Found");
                }
                Console.WriteLine($"\nTotal Count: { TotalCount}\nExecution Time : {(watch.ElapsedMilliseconds) / 1000}Sec {watch.ElapsedMilliseconds % 1000}Ms");
            }
            catch
            {
                Console.WriteLine("\nError");
            }

        }
        /// <summary>
        ///Searching the key word using REGEX Method
        /// </summary>
        /// <param name="FilePathList"> file path list</param>
        /// <param name="keySearch">key word for search</param>
        public void SearchWordUsingRegex(List<String> FilePathList, String keySearch)
        {
            try
            {
                var watch = new System.Diagnostics.Stopwatch();                                                                         //start execution timer
                var OutputList = new List<String>();
                var TotalCount = 0;
                var count = 0;
                string pattern = @"\b"+keySearch+@"\b";
                watch.Start();


                foreach (var x in FilePathList)
                {
                   
                    var inputSentence =  File.ReadAllText(x);
                    count = Regex.Matches(inputSentence, pattern, (RegexOptions)StringComparison.OrdinalIgnoreCase).Count;
                    if (count > 0)
                    {
                        OutputList.Add(Path.GetFileName(x));
                    }
                    TotalCount += count;
                }

                watch.Stop();                                                                            //stop execution timer
                Console.WriteLine("\nRegex Search\n");
                if (TotalCount > 0)
                {
                    Console.WriteLine($"\nWord Is Found in These Files:");
                    foreach (var x in OutputList)
                        Console.WriteLine($"\n{x}");
                }
                else
                {
                    Console.WriteLine("\nNot Found");
                }
                Console.WriteLine($"\nTotal Count: { TotalCount}\nExecution Time : {(watch.ElapsedMilliseconds) / 1000}Sec {watch.ElapsedMilliseconds % 1000}Ms");
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }
        /// <summary>
        ///Searching the key word using Async REGEX Method
        /// </summary>
        /// <param name="FilePathList"> file path list</param>
        /// <param name="keySearch">key word for search</param>
        public void SearchWordUsingAsyncRegex(List<String> FilePathList, String keySearch)
        {
            try
            {
                var watch = new System.Diagnostics.Stopwatch();                                                                         //start execution timer
                var OutputList = new List<String>();
                var TotalCount = 0;
                var count = 0;
                string pattern = @"\b" + keySearch + @"\b";
                var tasks = new List<Task>();
                watch.Start();


                foreach (var x in FilePathList)
                {

                   tasks.Add( Task.Run(async () =>
                   {
                       var inputSentence = await File.ReadAllTextAsync(x);
                       count = Regex.Matches(inputSentence, pattern, (RegexOptions)StringComparison.OrdinalIgnoreCase).Count;
                       if (count > 0)
                       {
                           OutputList.Add(Path.GetFileName(x));
                       }
                       TotalCount += count;
                   }));


                   
                }
                Task.WhenAll(tasks).Wait();
                watch.Stop();                                                                            //stop execution timer
                Console.WriteLine("\nRegex Search\n");
                if (TotalCount > 0)
                {
                    Console.WriteLine($"\nWord Is Found in These Files:");
                    foreach (var x in OutputList)
                        Console.WriteLine($"\n{x}");
                }
                else
                {
                    Console.WriteLine("\nNot Found");
                }
                Console.WriteLine($"\nTotal Count: { TotalCount}\nExecution Time : {(watch.ElapsedMilliseconds) / 1000}Sec {watch.ElapsedMilliseconds % 1000}Ms");
            }
            catch
            {
                Console.WriteLine("Error");
            }

        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Console.WriteLine("****Word Search In All The Files In The Directory****");
                String keySearch;
                var watch = new System.Diagnostics.Stopwatch();
                var FilePathList = new List<String>();
                var filePaths = Directory.GetFiles(@"C:\Users\LENOVO\source\repos\ConsoleApp3\ConsoleApp3\New folder\1\", "*.txt");
                foreach (var path in filePaths)
                {
                    FilePathList.Add(path);                                                                        //add all the file paths to the list

                }
                 WordSearchClass WordSearch= new WordSearchClass();
               

                int Choice;
            
                while (true)
                {
                    Console.WriteLine("\nEnter The Word to be searched\n");
                    keySearch = Console.ReadLine();
                    Console.WriteLine("\nPress 1 To Search a Word in file Using Linq Method\nPress 2 To Search a Word in file Using Regex Method\nPress 3 To Search a Word in file Using Async Linq Method\n Press 4 To Search a Word in file Using Async Regex Method\n Press 5 To Search a Word in file Using ALL Method\nPress 6 to Exit\nEnter Your Choice\n\n");
                    Choice = int.Parse(Console.ReadLine());
                    switch (Choice)
                    {
                        case 1:
                            WordSearch.SearchWordUsingLinq(FilePathList, keySearch);
                            break;
                        case 2:
                            WordSearch.SearchWordUsingRegex(FilePathList, keySearch);
                            break;
                        case 3:
                            WordSearch.SearchWordUsingAsyncLinq(FilePathList, keySearch);
                            break;
                        case 4:
                            WordSearch.SearchWordUsingAsyncRegex(FilePathList, keySearch);
                            break;
                        case 5:
                            WordSearch.SearchWordUsingLinq(FilePathList, keySearch);
                            WordSearch.SearchWordUsingRegex(FilePathList, keySearch);
                            WordSearch.SearchWordUsingAsyncLinq(FilePathList, keySearch);
                            WordSearch.SearchWordUsingAsyncRegex(FilePathList, keySearch);
                            
                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("invalid");
                            break;
                    }


                }



            }
            catch
            {
                Console.WriteLine("Error");
            }



        }
    }
}
