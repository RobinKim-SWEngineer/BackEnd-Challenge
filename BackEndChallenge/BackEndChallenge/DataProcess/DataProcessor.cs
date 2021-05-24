using System.Collections.Generic;
using System.Data;

namespace BackEndChallenge.DataProcess
{
    public class DataProcessor
    {
        public static int GetNumberOfMentees(DataTable dataTable) => dataTable.Rows.Count;

        public static List<string> GetMemteesLanguages(DataTable dataTable)
        {
            string userLanguage = "";
            List<string> languageList = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                userLanguage = row["language"].ToString();

                if (!languageList.Contains(userLanguage))
                {
                    languageList.Add(userLanguage);
                }
            }
            return languageList;
        }

        public static int GetAverageNameLength(DataTable dataTable)
        {
            List<string> namesMerged = new List<string>();
            string namesConcat = "";

            foreach (DataRow row in dataTable.Rows)
            {
                namesMerged.Add(row["first_name"].ToString());
                namesMerged.Add(row["last_name"].ToString());
            }
            namesConcat = string.Concat(namesMerged);

            return namesConcat.Length / GetNumberOfMentees(dataTable);
        }

        public static string GetMenteeLongestName(DataTable dataTable)
        {
            List<string> fullNameList = GetFullNameList(dataTable);
            string longestName = "";
            int nameLengthCounter = 0;

            foreach (string name in fullNameList)
            {
                if (name.Length > nameLengthCounter)
                {
                    nameLengthCounter = name.Length;
                    longestName = name;
                }
            }
            return longestName;
        }

        public static string GetMenteeShortestName(DataTable dataTable)
        {
            List<string> fullNameList = GetFullNameList(dataTable);
            string shortestName = "";
            int nameLengthCounter = fullNameList[0].Length;

            foreach (string name in fullNameList)
            {
                if (name.Length < nameLengthCounter)
                {
                    nameLengthCounter = name.Length;
                    shortestName = name;
                }
            }
            return shortestName;
        }

        private static List<string> GetFullNameList(DataTable dataTable)
        {
            List<string> fullNameList = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                fullNameList.Add(row["first_name"].ToString() + " " + row["last_name"].ToString());
            }

            return fullNameList;
        }
    }
}
