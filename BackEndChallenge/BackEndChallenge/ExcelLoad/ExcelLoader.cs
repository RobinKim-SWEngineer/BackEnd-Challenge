using System;
using System.Data;
using System.Data.OleDb;

namespace BackEndChallenge.ExcelLoad
{
    public class ExcelLoader
    {
        public DataTable ConvertExcelToDataTable(string FileName)
        {
            DataTable ContentTable = new DataTable();
            OleDbConnection objConn = new OleDbConnection(
                @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");

            OleDbCommand oleDbCommand = new OleDbCommand();
            oleDbCommand.Connection = objConn;
            objConn.Open();

            oleDbCommand.CommandText = "SELECT * FROM [Sheet2$]";
            OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();

            addColumns(ContentTable);
            while(oleDbDataReader.Read() == true && oleDbDataReader.HasRows)
            {
                try
                {
                    if (oleDbDataReader[0].ToString().Trim() != string.Empty && oleDbDataReader[0].ToString().Trim() != " ")
                    {
                        DataRow row = ContentTable.NewRow();
                        row["index"] = oleDbDataReader[0].ToString().Trim();
                        row["first_name"] = oleDbDataReader[1].ToString().Trim();
                        row["last_name"] = oleDbDataReader[2].ToString().Trim();
                        row["language"] = oleDbDataReader[3].ToString().Trim();

                        ContentTable.Rows.Add(row);
                    }
                }
                catch (System.Exception exception)
                {
                    oleDbDataReader.Close();
                    objConn.Close();
                    Console.WriteLine("Went wrong : " + exception.Message);

                    return ContentTable;
                }
                
            }
            oleDbDataReader.Close();
            objConn.Close();

            // testDataTable(ContentTable);
            return ContentTable;
        }

        private void addColumns(DataTable dataTable)
        {
            dataTable.Columns.Add("index");
            dataTable.Columns.Add("first_name");
            dataTable.Columns.Add("last_name");
            dataTable.Columns.Add("language");
        }

        public void testDataTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (var item in row.ItemArray)
                {
                    Console.WriteLine(item);
                }
            }
        }
    }
}
