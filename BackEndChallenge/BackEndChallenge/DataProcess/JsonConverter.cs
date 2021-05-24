using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;

namespace BackEndChallenge.DataProcess
{
    public class JsonConverter
    {
        public static string ChangeToJson(DataTable dataTable)
        {
            DataSet dataSet = new DataSet();

            dataSet.Tables.Add(GetAllMenteesTable(dataTable));
            dataSet.Tables.Add(GetTotalMenteeTable(DataProcessor.GetNumberOfMentees(dataTable)));
            dataSet.Tables.Add(GetLanguagesTable(DataProcessor.GetMemteesLanguages(dataTable)));
            dataSet.Tables.Add(GetAvgNameTable(DataProcessor.GetAverageNameLength(dataTable)));
            dataSet.Tables.Add(GetLongestNameMenteeTable(DataProcessor.GetMenteeLongestName(dataTable)));
            dataSet.Tables.Add(GetShortestMenteeTable(DataProcessor.GetMenteeShortestName(dataTable)));

            dataSet.AcceptChanges();

            return JsonConvert.SerializeObject(dataSet, Formatting.Indented);
        }

        private static DataTable GetAllMenteesTable(DataTable dataTable)
        {
            DataTable menteesTable = new DataTable("ALL MENTEES");
            DataColumn indexColumn = new DataColumn("Index", typeof(int));
            DataColumn nameColumn = new DataColumn("Name");
            indexColumn.AutoIncrement = true;

            menteesTable.Columns.Add(indexColumn);
            menteesTable.Columns.Add(nameColumn);

            foreach (DataRow row in dataTable.Rows)
            {
                DataRow newRow = menteesTable.NewRow();
                newRow["Name"] = row["first_name"].ToString() + " " + row["last_name"].ToString();
                menteesTable.Rows.Add(newRow);
            }
            return menteesTable;
        }

        private static DataTable GetTotalMenteeTable(int totalMentees)
        {
            DataTable numberTable = new DataTable("NUMBER OF MENTEES");
            DataColumn numberOfMenteesColumn = new DataColumn("Total mentees");
            numberTable.Columns.Add(numberOfMenteesColumn);

            DataRow newRow = numberTable.NewRow();
            newRow["Total mentees"] = totalMentees.ToString();
            numberTable.Rows.Add(newRow);

            return numberTable;
        }

        private static DataTable GetLanguagesTable(List<string> languages)
        {
            DataTable languageTable = new DataTable("ALL LANGUAGES");
            DataColumn indexColumn = new DataColumn("Index");
            indexColumn.AutoIncrement = true;
            DataColumn languageColumn = new DataColumn("Language");
            languageTable.Columns.Add(indexColumn);
            languageTable.Columns.Add(languageColumn);

            foreach (string language in languages)
            {
                DataRow newRow = languageTable.NewRow();
                newRow["Language"] = language;
                languageTable.Rows.Add(newRow);
            }

            return languageTable;
        }

        private static DataTable GetAvgNameTable(int avgName)
        {
            DataTable avgLengthNameTable = new DataTable("AVG NAME LENGTH");
            DataColumn avgNameLengthColumn = new DataColumn("Average name length");
            avgLengthNameTable.Columns.Add(avgNameLengthColumn);

            DataRow newRow = avgLengthNameTable.NewRow();
            newRow["Average name length"] = avgName.ToString();
            avgLengthNameTable.Rows.Add(newRow);

            return avgLengthNameTable;
        }

        private static DataTable GetLongestNameMenteeTable(string menteeName)
        {
            DataTable longestNameTable = new DataTable("LONGEST NAME");
            DataColumn longestNameMenteeColumn = new DataColumn("Longest name length");
            longestNameTable.Columns.Add(longestNameMenteeColumn);

            DataRow newRow = longestNameTable.NewRow();
            newRow["Longest name length"] = menteeName;
            longestNameTable.Rows.Add(newRow);

            return longestNameTable;
        }

        private static DataTable GetShortestMenteeTable(string menteeName)
        {
            DataTable shortestNameTable = new DataTable("SHORTEST NAME");
            DataColumn shortestNameMenteeColumn = new DataColumn("Shortest name length");
            shortestNameTable.Columns.Add(shortestNameMenteeColumn);

            DataRow newRow = shortestNameTable.NewRow();
            newRow["Shortest name length"] = menteeName;
            shortestNameTable.Rows.Add(newRow);

            return shortestNameTable;
        }
    }
}
