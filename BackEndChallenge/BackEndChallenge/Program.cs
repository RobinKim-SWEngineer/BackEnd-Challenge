using System;
using System.Data;
using System.IO;
using BackEndChallenge.DataProcess;
using BackEndChallenge.ExcelLoad;

namespace BackEndChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter excel file path: ");
            string filePath = Console.ReadLine();

            if (File.Exists(filePath))
            {
                ExcelLoader excelLoader = new ExcelLoader();
                DataTable menteeTable = excelLoader.ConvertExcelToDataTable(filePath);
                string jsonFormat = JsonConverter.ChangeToJson(menteeTable);

                Console.WriteLine(jsonFormat);
                Console.WriteLine("Enter to finish the console");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Please check file path and try again");
                Console.ReadLine();
            }
        }
    }
}
