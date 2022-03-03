using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ConsoleApp1
{
    class studentas_names
    {

        public static void Main(string[] args)
        {
            string path = @"MyTest.txt";
            string arr;

            //to switch between add, update ,print  and finish 
            string c = "";

            while (c != "f")
            {
                Console.WriteLine("enter letter (a to add a student, u to update a student record by name, p to print list of students  \r\nand f to finish the program):");
                c = Console.ReadLine();

                switch (c)
                {
                    case "a":
                        arr = enter_student();

                        if (!File.Exists(path)) using (StreamWriter sw = File.CreateText(path)) write_to_file(path, arr, sw);
                        else using (StreamWriter sw = File.AppendText(path)) write_to_file(path, arr, sw);

                        break;

                    case "p":

                        if (file_dont_exist_prnt(path)) break;

                        string s = "";
                        using (StreamReader sr = File.OpenText(path)) while ((s = sr.ReadLine()) != null) Console.WriteLine(s);

                        break;

                    case "u":

                        if (file_dont_exist_prnt(path)) break;
                        arr = enter_student();
                        var values = arr.Split(',');

                        List<string> quotelist = File.ReadAllLines(path).ToList();

                        int query = quotelist.FindIndex(x => x.Contains(values[0]));
                        Console.WriteLine(query + "line");

                        if (query == -1)
                        {
                            Console.WriteLine(values[0] + " is not in the list");
                            break;
                        }

                        string pass_1 = "passed";
                        if (Convert.ToInt32(values[1]) < 60) pass_1 = "failed";
                        quotelist[query] = arr + "," + pass_1;
                        File.WriteAllLines(path, quotelist.ToArray());

                        break;
                    case "f":
                        break;
                    default:
                        Console.WriteLine("wrong letter, please enter again");
                        break;

                }
            }



        }

        public static string enter_student()
        {
            Console.WriteLine("Enter name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter grade:");
            string grade = Console.ReadLine();

            List<string> myValues = new List<string>() { name, grade };
            string csv = String.Join(",", myValues.Select(x => x.ToString()).ToArray());

            return csv;
        }

        public static void write_to_file(string path, string arr, StreamWriter sw)
        {
            var items = arr.Split(',');
            string pass_1 = "passed";
            if (Convert.ToInt32(items[1]) < 60) pass_1 = "failed";
            sw.Write(arr + "," + pass_1 + "\r\n");
        }

        public static bool file_dont_exist_prnt(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("student list is empty, please add students first");
                return true;
            }
            return false;
        }
    }
}
