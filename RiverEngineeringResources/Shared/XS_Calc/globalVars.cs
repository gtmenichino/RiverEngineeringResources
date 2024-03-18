using System.Data;
using System.Text;

namespace RiverEngineeringResources.Shared.XS_Calc
{
    static class globalVars
    {
        public static int selectedProfileReachID = 0;

        public static bool evaluatingCell = false;
        public static double storedValue = 0;
        public static string temp = "";
        public static SpreadsheetGear.CustomFunctions.IArguments argsTemp;
        public static bool okToModify = false;
        //public static MainForm myMainForm;

        public static bool boolMenuActive = false;

        public static int uniqueReachID = 1;

        public static List<string> GlobalStageStationList = new List<string>();
        public static DataTable GlobalXSList = new DataTable();

        public static bool firstRun = true;
        // Public dsProjectData As New dsProjectData

        public static void setupGlobalXSList()
        {
            GlobalXSList.TableName = "globalXSList";
            GlobalXSList.Columns.Add("Reach Name", typeof(string));

            GlobalXSList.Columns.Add("Reach ID", typeof(string));

            GlobalXSList.Columns.Add("Module Name", typeof(string));

            GlobalXSList.Columns.Add("Module ID", typeof(string));

            GlobalXSList.Columns.Add("Module Type", typeof(string));

            GlobalXSList.Columns.Add("Total", typeof(string));
        }

        public static string DumpDataTable(DataTable table)
        {
            string data = string.Empty;
            StringBuilder sb = new StringBuilder();

            if (table != null && table.Rows != null)
            {
                foreach (DataRow dataRow in table.Rows)
                {
                    foreach (var item in dataRow.ItemArray)
                    {
                        sb.Append(item);
                        sb.Append(',');
                    }

                    sb.AppendLine();
                }

                data = sb.ToString();
            }

            return data;
        }
    }
}
