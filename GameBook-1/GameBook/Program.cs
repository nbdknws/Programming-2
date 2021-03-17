using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBook
{
    class Program
    {

        static void Main(string[] args)
        {
       

            // Uncomment to load all strings...
            // ... locate the file LoneWolf-FlightFromTheDark.txt in your project (through Explorer) 
            //     so you know where it comes from!
            string[] lines = System.IO.File.ReadAllLines("LoneWolf-FlightFromTheDark.txt");

            // Uncomment to instantiate new book
            Book book = new Book();

            // Uncomment to initialize 'pages' object field with dynamic array
            book.pages = new List<Page>();

            // Uncomment to add new page as the last entry of "book.pages"
            Page page = new Page();
            page.options = new List<Option>();
            

            // Now its time to read the book the lines!
            foreach (string line in lines)
            {
                if (int.TryParse(line.Trim(), out int pageNum))
                {
                    // we have a new page!
                    book.pages.Add(page);
                    page = new Page();
                    page.options = new List<Option>();
                    page.pageNum = pageNum;
                }
                else
                if (line.ToLower().IndexOf("turn to") >= 0)
                {
                    // we hit some option
                    Option option = new Option();
                    option.text = line;
                    option.pageNum = 0;
                    
                    string pageNumStr = Regex.Match(line, @"\d+").Value; // TODO: rozparsovat cislo za "turn to"
                    if (pageNumStr.Length > 0)
                    {
                        option.pageNum = int.Parse(pageNumStr);
                    }
                    page.options.Add(option);
                }
                else
                {
                    // page text
                    page.text += "\n" + line;
                }

            }
            book.pages.Add(page);


            int currPage = 1; // our current page
            while (true) {
                // 1) output page
                Console.WriteLine(currPage);
                Console.WriteLine(book.pages[currPage].text);

                // 2) present options
                if (book.pages[currPage].options.Count == 0)
                {
                    break;
                }
                else
                {
                    //print options on the screen
                    int optNum = 1;
                    foreach (Option opt in book.pages[currPage].options)
                    {
                        Console.WriteLine(optNum.ToString() + " >>> " + opt.text );
                        optNum += 1;
                    
                    }
           

                }

                // 3) read user's input and change "currPage" according to the option

                Console.Write("Choose Option: ");
                currPage = book.pages[currPage].options[int.Parse(Console.ReadLine()) - 1].pageNum;
                Console.Clear();
            }

            Console.WriteLine("--/ The End /--");
            Console.ReadLine();
        }

        // NOTE THAT YOU HAVE TO KEEP 'static' KEYWORD IN ORDER TO BE ABLE TO REFERENCE IT
        // FROM THE 'main' METHOD
        static Book LoadBook()
        {
            // In the end, you might want to move the "book loading" code into a custom function!

            return null;
        }

        static void PlayBook(Book book)
        {
            // In the end, you might want to move the "book playing" code into a custom function!
        }


    }
}
