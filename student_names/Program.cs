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
            file new_file = new file();
            new_file.path = @"student_name_list.txt";

            student student = new student();
            
            string c = "";

            while (c != "f")
            {
                Console.WriteLine("enter letter (a to add a student, u to update a student record by name, p to print list of students  \r\nand f to finish the program):");
                c = Console.ReadLine();

                switch (c)
                {
                    case "a"://add student name
                        new_file.addName(student);
                        break;

                    case "p"://print students list
                        if (new_file.file_not_exist()) break;
                        new_file.printList();
                        break;

                    case "u"://update student grade
                        if (new_file.file_not_exist()) break;
                        new_file.upadateName(student);
                        break;

                    case "f"://end the program
                        break;

                    default:
                        Console.WriteLine("wrong letter, please enter again");
                        break;

                }
            }
        }

       
    }
    class student
    {
        public string name; 
        public int grade;
        public string pass;

        public student()
        {
            name = "ahmed";
            grade = 100;
            pass = "passed";
        }
        public void readName()
        {
            Console.WriteLine("Enter name:");
            name = Console.ReadLine();
            Console.WriteLine("Enter grade:");
            grade = int.Parse(Console.ReadLine());
        }

        
        public string ispass()
        {
            if (grade < 60) pass = "failed";
            else pass = "passed";
            return pass;
        }
        public string[] returnName()
        {
            pass = ispass();
            string grade_str=grade.ToString();
            string[] arr = { name, grade_str, pass };
            return arr;

        }

    }
    class file
    {
        
        public string path;
        
        public bool file_not_exist()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("student list is empty, please add students first");
                return true;
            }
            return false;
        }

        public string printName(student s)
        {
            return String.Join(",", s.returnName().Select(x => x.ToString()));   
        }

        public void addName(student s)
        {
            if (!File.Exists(path)) using (StreamWriter sw = File.CreateText(path)) writeToFile(s, sw);
            else using (StreamWriter sw = File.AppendText(path)) writeToFile(s, sw);
        }
        
        public void writeToFile(student s, StreamWriter sw)
        {
            s.readName();
            sw.Write(printName(s) + "\r\n");
        }

        public List<string> readFile()
        {
            return File.ReadAllLines(path).ToList();
        }

        public void printList()
        {
             List<string> list = readFile();
             for (int i = 0; i < list.Count; i++)
             Console.WriteLine(list[i]);
        }
        public void upadateName(student s)
        {
            s.readName();
            var item = nameExist(s);
            if (item.exist) 
            {
                item.quotelist[item.quary] = printName(s);
                File.WriteAllLines(path, item.quotelist.ToArray());
            }
            else Console.WriteLine(s.name + " is not in the list");

        }

        public (bool exist,int quary,List<string> quotelist) nameExist(student s)
        {
            List<string> quotelist = readFile();
            int query = quotelist.FindIndex(x => x.ToString().Split(",").Contains(s.name));
        
            if (query == -1) return (false, query, quotelist);

            return (true, query, quotelist);
        }
    } 
        
}

