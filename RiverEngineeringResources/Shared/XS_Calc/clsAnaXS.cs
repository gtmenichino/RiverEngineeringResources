using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Xml.Serialization;
//using System.Windows.Forms.DataVisualization.Charting;
using System.Data;
using static System.Net.WebRequestMethods;

namespace RiverEngineeringResources.Shared.XS_Calc
{
    [Serializable()]
    public class clsAnaXS
    {
        public DataTable xsDataTable;
        public DataSet xsDataSet; // THIS HOUSES ALL THE RAW OUTPUT DATA FROM THE EXCEL SHEET - FOR WHEN NEEDING TO BE PLOTTED OR AFTER RECALCULATION
        public bool boolUpdatingDataTable = false;

        //public List<clsImageAsString> myColImageAsString = new List<clsImageAsString>();

        public clsAndrewsProperties myAndrewsProperties;
        public clsInnerBermProperties myInnerBermProperties;
        public clsStage1Properties myStage1Properties;
        public clsStage2Properties myStage2Properties;
        public clsStage3Properties myStage3Properties;
        public clsAdvancedXSProperties myAdvancedXSProperties;

        // Create a new empty workbook set.
        public SpreadsheetGear.IWorkbookSet myXSWorkbookSet = SpreadsheetGear.Factory.GetWorkbookSet();
        // Create a new workbook in the workbook set that loads the XS Designer Excel file
        public SpreadsheetGear.IWorkbook myXSWorkbook; //= myXSWorkbookSet.Workbooks.OpenFromMemory(My.Resources.XSAnalyzerV1y);


        private string m_Name;  // this will be set upon creation of the XS (in the feature class)
        private string m_Station;
        private string m_ID; // this will be set upon creation of the XS (in the feature class)
        private double m_LeftLimit;
        private double m_RightLimit;
        private double m_WSE;
        public string m_Left_3rd;
        public string m_Left_2nd;
        public string m_Left_Bank;
        public string m_Thalweg;
        public string m_Right_3rd;
        public string m_Right_2nd;
        public string m_Right_Bank;
        public string m_Right_IB;
        public string m_Left_IB;
        private double m_Slope;
        private int m_numStages;
        private string m_descXS;
        private string m_isInnerBerm;


        private string m_ShearMethod;
        private double m_ShieldsValue;
        private string m_VelocityPlotMethod;
        private string m_ShearPlotMethod;

        public double L9x, L8x, L7x, L6x, L5x, L4x, L3x, L2x, L1x, CLx, R1x, R2x, R3x, R4x, R5x, R6x, R7x, R8x, R9x;

        public double L9y, L8y, L7y, L6y, L5y, L4y, L3y, L2y, L1y, CLy, R1y, R2y, R3y, R4y, R5y, R6y, R7y, R8y, R9y;

        public double[] xPoints;

        public double[] yPoints;

        public string[] stgPoints = new string[20];

        private string pL9_XFormula, pL8_XFormula, pL7_XFormula, pL6_XFormula, pL5_XFormula, pL4_XFormula, pL3_XFormula, pL2_XFormula, pL1_XFormula, pCL_XFormula, pR1_XFormula, pR2_XFormula, pR3_XFormula, pR4_XFormula, pR5_XFormula, pR6_XFormula, pR7_XFormula, pR8_XFormula, pR9_XFormula;

        private string pL9_YFormula, pL8_YFormula, pL7_YFormula, pL6_YFormula, pL5_YFormula, pL4_YFormula, pL3_YFormula, pL2_YFormula, pL1_YFormula, pCL_YFormula, pR1_YFormula, pR2_YFormula, pR3_YFormula, pR4_YFormula, pR5_YFormula, pR6_YFormula, pR7_YFormula, pR8_YFormula, pR9_YFormula;

        private string pL9_Desc, pL8_Desc, pL7_Desc, pL6_Desc, pL5_Desc, pL4_Desc, pL3_Desc, pL2_Desc, pL1_Desc, pCL_Desc, pR1_Desc, pR2_Desc, pR3_Desc, pR4_Desc, pR5_Desc, pR6_Desc, pR7_Desc, pR8_Desc, pR9_Desc;

        private ReadOnlyAttribute attribute;


        public clsAnaXS(byte[] fileContent)
        {

           myXSWorkbookSet.Calculation = SpreadsheetGear.Calculation.Manual;

            myXSWorkbook = myXSWorkbookSet.Workbooks.OpenFromMemory(fileContent);

            //myXSWorkbook = myXSWorkbookSet.Workbooks.OpenFromMemory(My.Resources.XSAnalyzerV1y);
            xPoints = new[] { L9x, L8x, L7x, L6x, L5x, L4x, L3x, L2x, L1x, CLx, R1x, R2x, R3x, R4x, R5x, R6x, R7x, R8x, R9x };

            yPoints = new[] { L9y, L8y, L7y, L6y, L5y, L4y, L3y, L2y, L1y, CLy, R1y, R2y, R3y, R4y, R5y, R6y, R7y, R8y, R9y };



            xsDataTable = new DataTable();
            xsDataSet = new DataSet();

            myAndrewsProperties = new clsAndrewsProperties();
            myInnerBermProperties = new clsInnerBermProperties();
            myStage1Properties = new clsStage1Properties();
            myStage2Properties = new clsStage2Properties();
            myStage3Properties = new clsStage3Properties();
            myAdvancedXSProperties = new clsAdvancedXSProperties();
            createDT();

            initProperties();

            setSubcategories();

            setupModule.AddCustomFunctionsToWorkbookSet(myXSWorkbook.WorkbookSet);
        }

        public void initProperties()
        {
            m_Name = "-";
            m_Station = "0";
            m_ID = "-";

            m_Left_3rd = "";
            m_Left_2nd = "";
            m_Left_Bank = "";
            m_Left_IB = "";
            m_Thalweg = "";
            m_Right_IB = "";
            m_Right_Bank = "";
            m_Right_2nd = "";
            m_Right_3rd = "";
            m_ShearMethod = "Shields 1936";
            m_LeftLimit = -50;
            m_RightLimit = 50;
            m_Slope = 0.01;
            m_WSE = 4;
            m_ShieldsValue = 0.045;
            m_VelocityPlotMethod = "Stream Tubes";
            m_ShearPlotMethod = "Stream Tubes";
            m_numStages = 3;
            m_descXS = "Riffle";
            m_isInnerBerm = "No";
        }

        public void createDT()
        {
            xsDataTable.TableName = "xs_data";
            xsDataTable.Columns.Add("Station", typeof(double));
            // activeXSDataTable.Columns.Item("Station").ReadOnly = True

            xsDataTable.Columns.Add("Elevation", typeof(double));
            // activeXSDataTable.Columns.Item("Elevation").ReadOnly = True

            xsDataTable.Columns.Add("n Value", typeof(string));

            xsDataTable.Columns.Add("Stage", typeof(string));
        }

        public void setSubcategories()  // sets subcategories of property grid to the current XS
        {
            this.SubcategoryInnerBerm = myInnerBermProperties;
            this.SubcategoryStage1 = myStage1Properties;
            this.SubcategoryStage2 = myStage2Properties;
            this.SubcategoryStage3 = myStage3Properties;
            this.SubcategoryAdvancedXS = myAdvancedXSProperties;
        }


        public void updateAnaXStageStation()
        {
            // GET THE ALLOWED STAGE STATION LIST
            if (globalVars.GlobalStageStationList.Count > 0)
                globalVars.GlobalStageStationList.Clear();

            globalVars.GlobalStageStationList.Add("");

            foreach (DataRow dr in this.xsDataTable.Rows) // add all of the potential values to the list
            {
                string temp = dr["Station"].ToString();
                globalVars.GlobalStageStationList.Add(temp);
            }

            if (this.Left_3rd != "" && globalVars.GlobalStageStationList.Contains(this.Left_3rd))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Left_3rd));

            if (this.Left_2nd != "" && globalVars.GlobalStageStationList.Contains(this.Left_2nd))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Left_2nd));

            if (this.Left_Bank != "" && globalVars.GlobalStageStationList.Contains(this.Left_Bank))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Left_Bank));

            if (this.Left_IB != "" && globalVars.GlobalStageStationList.Contains(this.Left_IB))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Left_IB));

            if (this.Thalweg != "" && globalVars.GlobalStageStationList.Contains(this.Thalweg))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Thalweg));

            if (this.Right_3rd != "" && globalVars.GlobalStageStationList.Contains(this.Right_3rd))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Right_3rd));

            if (this.Right_2nd != "" && globalVars.GlobalStageStationList.Contains(this.Right_2nd))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Right_2nd));

            if (this.Right_Bank != "" && globalVars.GlobalStageStationList.Contains(this.Right_Bank))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Right_Bank));

            if (this.Right_IB != "" && globalVars.GlobalStageStationList.Contains(this.Right_IB))
                globalVars.GlobalStageStationList.RemoveAt(globalVars.GlobalStageStationList.IndexOf(this.Right_IB));
        }


        public void Calculate_Full() // full updates the XS DATA
        {


            // xsDataSet.Clear()
            // Dim newtable As New DataTable

            myXSWorkbook.WorkbookSet.GetLock();

            try
            {
                // Update the stage station list
                updateAnaXStageStation();

                // Do some work...
                // MsgBox("locked")
                // dataSet = myXSWorkbook.GetDataSet("Threshold", SpreadsheetGear.Data.GetDataFlags.None)


                // Insert the DataTable into the template worksheet range. The InsertCells
                // flag will cause the formatted range to be adjusted for the inserted data.
                int numRow = 0;

                numRow = xsDataTable.Rows.Count;

                DataTable dummyDT = new DataTable();
                dummyDT.TableName = "xs_data_complete";
                // dummyDT = myproperties.xsAnalyzerDataTable.Clone
                dummyDT.Columns.Add("Station", typeof(string));
                dummyDT.Columns.Add("Elevation", typeof(string));
                dummyDT.Columns.Add("n value", typeof(string));

                foreach (DataRow row in xsDataTable.Rows)
                {
                    DataRow newRow = dummyDT.NewRow();
                    newRow["Station"] = row["Station"];
                    newRow["Elevation"] = row["Elevation"];
                    newRow["n Value"] = "0.1";//row["n Value"];
                    dummyDT.Rows.Add(newRow);
                }

                // add blank rows for the rest of the rows
                for (int i = dummyDT.Rows.Count - 1; i <= 101 - 1; i += 1)
                {
                    DataRow blankRow;
                    blankRow = dummyDT.NewRow();
                    blankRow[0] = "=na()";
                    blankRow[1] = "=na()";
                    blankRow[2] = "=na()";
                    dummyDT.Rows.Add(blankRow);
                }

                myXSWorkbook.Worksheets["Data"].Range["A2:C101"].CopyFromDataTable(dummyDT, SpreadsheetGear.Data.SetDataFlags.NoColumnHeaders);

                myXSWorkbook.Worksheets["Data"].Range["E3"].Value = LeftLimit;
                myXSWorkbook.Worksheets["Data"].Range["F3"].Value = RightLimit;
                myXSWorkbook.Worksheets["Data"].Range["E7"].Value = Slope;
                myXSWorkbook.Worksheets["Data"].Range["E10"].Value = WSE;

                myXSWorkbook.Worksheets["Data"].Range["H3"].Value = Left_3rd;
                myXSWorkbook.Worksheets["Data"].Range["H4"].Value = Left_2nd;
                myXSWorkbook.Worksheets["Data"].Range["H5"].Value = "10"; //Left_Bank
                myXSWorkbook.Worksheets["Data"].Range["H6"].Value = Left_IB;
                myXSWorkbook.Worksheets["Data"].Range["H7"].Value = "20"; //Thalweg
                myXSWorkbook.Worksheets["Data"].Range["H8"].Value = Right_IB;
                myXSWorkbook.Worksheets["Data"].Range["H9"].Value = "30"; //Right_Bank
                myXSWorkbook.Worksheets["Data"].Range["H10"].Value = Right_2nd;
                myXSWorkbook.Worksheets["Data"].Range["H11"].Value = Right_3rd;

                myXSWorkbook.Worksheets["Data"].Range["I14"].Value = ShearMethod;

                myXSWorkbook.Worksheets["Data"].Range["I22"].Value = ShieldsValue;
                myXSWorkbook.Worksheets["Data"].Range["I19"].Value = myAndrewsProperties.D50_Bed;
                myXSWorkbook.Worksheets["Data"].Range["J19"].Value = myAndrewsProperties.D50_Bar;
                myXSWorkbook.Worksheets["Data"].Range["K19"].Value = myAndrewsProperties.Largest_D;

                myXSWorkbookSet.Calculate();

                xsDataSet = myXSWorkbook.GetDataSet("xs_data,n_label_data,n_line_data,stage_label_data,stage_line_data,stage_points_data,limit_data,wse_data,hydraulics_data", SpreadsheetGear.Data.GetDataFlags.FormattedText);  // THIS WILL BRING IN JUST STRINGS

                double doub;

                string[] hydraulics_dataArray = new[] { "velocity station", "velocity value", "shear station", "shear value", "D50 Station", "D50 value" };

                foreach (string val in hydraulics_dataArray)
                    ChangeFieldType(xsDataSet, "hydraulics_data", val, typeof(string));// CONVERT STRINGS TO DOUBLES AND SET NULLS TO ""


                // Console.Write("*****************1*************************")
                // Console.Write(DumpDataTable(xsDataset.Tables("xs_data")))

                ChangeFieldType(xsDataSet, "xs_data", "Stage", typeof(string)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                                                                                // ChangeFieldType(xsDataset, "xs_data", "Elevation", doub.GetType) 'CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS

                ChangeFieldType(xsDataSet, "limit_data", "L Station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "limit_data", "L Elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "limit_data", "R Station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "limit_data", "R Elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS

                ChangeFieldType(xsDataSet, "wse_data", "Top Station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "wse_data", "Bottom Elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "wse_data", "Top Elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS

                ChangeFieldType(xsDataSet, "n_label_data", "n label station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "n_label_data", "n label elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS

                ChangeFieldType(xsDataSet, "n_line_data", "n station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "n_line_data", "n elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS

                ChangeFieldType(xsDataSet, "stage_label_data", "stage label station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "stage_label_data", "stage label elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "stage_line_data", "stage station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "stage_line_data", "stage elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "stage_points_data", "stage points station", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS
                ChangeFieldType(xsDataSet, "stage_points_data", "stage points elevation", typeof(double)); // CONVERT STRINGS TO DOUBLES AND GET RID OF NULLS


                // update the XS dataTable with the stage station values
                foreach (DataRow dr in xsDataSet.Tables["xs_data"].Rows)
                {
                    boolUpdatingDataTable = true;

                    int rowIndex = xsDataSet.Tables["xs_data"].Rows.IndexOf(dr);
                    xsDataTable.Rows[rowIndex]["Stage"] = dr["Stage"];
                    //xsDataTable.Rows.Item(xsDataSet.Tables("xs_data").Rows.IndexOf(dr)).Item("Stage") = dr("Stage");
                    boolUpdatingDataTable = false;
                }
            }

            catch (Exception ex)
            {
                // Show the exception's message.
                //MessageBox.Show(ex.Message);
                //await JSRuntime.InvokeVoidAsync("alert", ex.Message);
                var ojoj = "ss";
            }

            // Show the stack trace, which is a list of methods
            // that are currently executing.
            // MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)

            finally
            {
                // ' Release the lock on the workbook set and start background 
                // ' calculation if appropriate. 
                myXSWorkbook.WorkbookSet.ReleaseLock();
            }
        }

        public void PlotXSonSelectedGraph()//Chart targetChart)
        {
            //targetChart.Invalidate();

            //try
            //{
            //    // PLOTTING CODE
            //    targetChart.Series("Ground Data").Points.DataBind(xsDataTable.DefaultView, "Station", "Elevation", null/* TODO Change to default(_) if this is not a reference type */);


            //    targetChart.Series("Left Limit").Points.DataBind(xsDataSet.Tables("limit_data").DefaultView, "L Station", "L Elevation", null/* TODO Change to default(_) if this is not a reference type */);
            //    targetChart.Series("Right Limit").Points.DataBind(xsDataSet.Tables("limit_data").DefaultView, "R Station", "R Elevation", null/* TODO Change to default(_) if this is not a reference type */);

            //    targetChart.Series("WSE").Points.DataBind(xsDataSet.Tables("wse_data").DefaultView, "Top Station", "Top Elevation,Bottom Elevation", null/* TODO Change to default(_) if this is not a reference type */);

            //    targetChart.Series("n value divides").Points.DataBind(xsDataSet.Tables("n_line_data").DefaultView, "n station", "n elevation", null/* TODO Change to default(_) if this is not a reference type */);
            //    targetChart.Series("n values").Points.DataBind(xsDataSet.Tables("n_label_data").DefaultView, "n label station", "n label elevation", "Label=n label");

            //    targetChart.Series("stage divides").Points.DataBind(xsDataSet.Tables("stage_line_data").DefaultView, "stage station", "stage elevation", null/* TODO Change to default(_) if this is not a reference type */);
            //    targetChart.Series("stage values").Points.DataBind(xsDataSet.Tables("stage_label_data").DefaultView, "stage label station", "stage label elevation", "Label=stage label");

            //    targetChart.Series("Stage Points").Points.DataBind(xsDataSet.Tables("stage_points_data").DefaultView, "stage points station", "stage points elevation", "Label=stage points label");


            //    targetChart.Series("Velocity").Points.DataBind(xsDataSet.Tables("hydraulics_data").DefaultView, "velocity station", "velocity value", null/* TODO Change to default(_) if this is not a reference type */);
            //    targetChart.Series("Shear Stress").Points.DataBind(xsDataSet.Tables("hydraulics_data").DefaultView, "shear station", "shear value", null/* TODO Change to default(_) if this is not a reference type */);
            //    targetChart.Series("D50 Mobilized").Points.DataBind(xsDataSet.Tables("hydraulics_data").DefaultView, "D50 station", "D50 value", null/* TODO Change to default(_) if this is not a reference type */);
            //}

            //// Console.Write("*****************2*************************")
            //// Console.Write(DumpDataTable(xsDataset.Tables("xs_data")))
            //catch (Exception ex)
            //{
            //    Interaction.MsgBox(ex.Message);
            //}


            //bool readyForChart = false;  // figure out if ready for chart

            //if (targetChart.Series("Ground Data").Points.Count < 3 | xsDataTable.Rows.Count < 3)
            //    readyForChart = false;
            //else
            //{
            //    readyForChart = true;

            //    // looks to be ok... run it through a bunch of checks
            //    // For i = 0 To 2
            //    // With DataGridView1.Rows.Item(i)
            //    if (readyForChart == true)
            //    {
            //        for (var j = 0; j <= xsDataTable.Rows.Count - 2; j++)
            //        {
            //            if (xsDataTable.Rows.Item(j).Item("Station").ToString == "" | xsDataTable.Rows.Item(j).Item("Elevation").ToString == "")
            //            {
            //                // got some blanks in there                        
            //                // readyForChart = True
            //                readyForChart = false;
            //                break;
            //            }
            //            else
            //                readyForChart = true;
            //        }
            //    }

            //    if (readyForChart == true)
            //    {
            //        if (xsDataTable.Rows.Item(0).Item("Station") == xsDataTable.Rows.Item(1).Item("Station") | xsDataTable.Rows.Item(1).Item("Station") == xsDataTable.Rows.Item(2).Item("Station") | xsDataTable.Rows.Item(0).Item("Station") == xsDataTable.Rows.Item(2).Item("Station"))
            //            readyForChart = false;
            //    }


            //    // test If the station data Is in ascending station order
            //    if (readyForChart == true)
            //    {
            //        for (var j = 0; j <= xsDataTable.Rows.Count - 2; j++)
            //        {
            //            if (xsDataTable.Rows.Item(j).Item("Station") <= xsDataTable.Rows.Item(j + 1).Item("Station"))
            //                // ascending Or equal order                        
            //                readyForChart = true;
            //            else
            //            {
            //                readyForChart = false;

            //                break;
            //            }
            //        }
            //    }

            //    // Exit For


            //    // End With
            //    // Next


            //    if (readyForChart == true)
            //    {
            //        targetChart.Visible = true;

            //        targetChart.ChartAreas("ChartArea1").AxisX.IsStartedFromZero = false;
            //        targetChart.ChartAreas("ChartArea1").AxisY.IsStartedFromZero = false;
            //        targetChart.ChartAreas("ChartArea1").AxisY2.IsStartedFromZero = true;

            //        targetChart.ChartAreas("ChartArea1").AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //        targetChart.ChartAreas("ChartArea1").AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //        targetChart.ChartAreas("ChartArea1").AxisY2.IntervalAutoMode = IntervalAutoMode.VariableCount;
            //        targetChart.ChartAreas("ChartArea1").AxisY2.MajorGrid = targetChart.ChartAreas("ChartArea1").AxisY.MajorGrid;


            //        if (!IsDBNull(xsDataTable.Compute("Min([Station])", null/* TODO Change to default(_) if this is not a reference type */)) && !IsDBNull(xsDataTable.Compute("Max([Station])", null/* TODO Change to default(_) if this is not a reference type */)) && !IsDBNull(xsDataTable.Compute("Min([Elevation])", null/* TODO Change to default(_) if this is not a reference type */)))
            //        {
            //            targetChart.ChartAreas("ChartArea1").AxisX.Minimum = xsDataTable.Compute("Min([Station])", null/* TODO Change to default(_) if this is not a reference type */); // "nothing" will filter the white space out
            //            targetChart.ChartAreas("ChartArea1").AxisX.Maximum = xsDataTable.Compute("Max([Station])", null/* TODO Change to default(_) if this is not a reference type */); // "nothing" will filter the white space out

            //            targetChart.ChartAreas("ChartArea1").AxisY.Minimum = Math.Floor(xsDataTable.Compute("Min([Elevation])", null/* TODO Change to default(_) if this is not a reference type */)); // "nothing" will filter the white space out
            //            targetChart.ChartAreas("ChartArea1").AxisY2.Minimum = 0; // "nothing" will filter the white space out
            //        }


            //        targetChart.ChartAreas("ChartArea1").AxisX.RoundAxisValues();
            //        targetChart.ChartAreas("ChartArea1").AxisY.RoundAxisValues();
            //        targetChart.ChartAreas("ChartArea1").AxisY2.RoundAxisValues();
            //    }
            //    else
            //    {
            //    }
            //}


            //targetChart.Update();
        }

        private void ChangeFieldType(DataSet dataset, string tableName, string colName, Type newType) // As DataSet  'THIS IS PROBABLY WAY INEFFICIENT
        {
            DataSet newDataSet = dataset.Clone();

            // PrintRows(newDataSet)
            newDataSet.Tables[tableName].Columns[colName].DataType = newType;

            foreach (DataRow row in dataset.Tables[tableName].Rows)
            {
                if (row[colName] == "")
                {
                }
                else
                    // count it
                    newDataSet.Tables[tableName].ImportRow(row);// copies over each row... row by row
            }

            dataset.Tables.Remove(tableName);

            dataset.Tables.Add(newDataSet.Tables[tableName].Copy());

            newDataSet.Dispose();
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

        private void ReplaceNA(DataSet dataset, string sheetName, string columnName) // PROBABLY INEFFICIENT
        {
            foreach (DataRow row in dataset.Tables[sheetName].Rows)
            {
                if ((row[columnName].ToString() == "#N/A"))
                    row.SetField(columnName, "");
            }
        }

        private void PrintRows(DataSet dataSet)
        {
            // For each table in the DataSet, print the values of each row.
            //DataTable thisTable;
            //foreach (var thisTable in dataSet.Tables)
            //{
            //    // For each row, print the values of each column.
            //    DataRow row;
            //    foreach (var row in thisTable.Rows)
            //    {
            //        DataColumn column;
            //        foreach (var column in thisTable.Columns)
            //            Console.WriteLine(row(column));
            //    }
            //}

            foreach (DataTable table in dataSet.Tables)
            {
                // For each row, print the values of each column.
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                        Console.WriteLine(row[column]);
                }
            }
        }

        public void loadBaseNvalues()
        {
            xsDataTable.Rows[0]["n Value"] = 0.1;  // THE GAPS WILLL BE FILLED IN WITH BLANK STRINGS LATER
            xsDataTable.Rows[2]["n Value"] = 0.06;
            xsDataTable.Rows[4]["n Value"] = 0.035;
            xsDataTable.Rows[14]["n Value"] = 0.06;
            xsDataTable.Rows[16]["n Value"] = 0.1;
        }

        [Category(" Setup")]
        [Description("Unique Name of the XS")]
        [DisplayName("XS Name")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }



        [Category(" Setup")]
        [Description("Type of the Module")]
        [DisplayName("XS ID")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string ID
        {
            get
            {
                return m_ID;
            }
            set
            {
                m_ID = value;
            }
        }

        [Category(" Setup")]
        [Description("Longitudinal Station of the XS")]
        [DisplayName("XS Station")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Station
        {
            get
            {
                return m_Station;
            }
            set
            {
                m_Station = value;
            }
        }


        [Category("Hydraulics")]
        [Description("Identify the Top Limit of analysis")]
        [ReadOnly(false)]
        [Browsable(true)]
        public double WSE
        {
            get
            {
                return m_WSE;
            }
            set
            {
                m_WSE = value;
            }
        }

        [Category("Hydraulics")]
        [Description("Identify the channel slope")]
        [ReadOnly(false)]
        [Browsable(true)]
        public double Slope
        {
            get
            {
                return m_Slope;
            }
            set
            {
                m_Slope = value;
            }
        }

        [Category("Flow Limits")]
        [Description("Identify the Left Limit of analysis")]
        [DisplayName("Left Limit")]
        [ReadOnly(false)]
        [Browsable(true)]
        public double LeftLimit
        {
            get
            {
                return m_LeftLimit;
            }
            set
            {
                m_LeftLimit = value;
            }
        }

        [Category("Flow Limits")]
        [Description("Identify the Right Limit of analysis")]
        [DisplayName("Right Limit")]
        [ReadOnly(false)]
        [Browsable(true)]
        public double RightLimit
        {
            get
            {
                return m_RightLimit;
            }
            set
            {
                m_RightLimit = value;
            }
        }


        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Left 3rd")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Left_3rd
        {
            get
            {
                return m_Left_3rd;
            }
            set
            {
                m_Left_3rd = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Left 2nd")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Left_2nd
        {
            get
            {
                return m_Left_2nd;
            }
            set
            {
                m_Left_2nd = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Left Bank*")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Left_Bank
        {
            get
            {
                return m_Left_Bank;
            }
            set
            {
                m_Left_Bank = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Left IB")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Left_IB
        {
            get
            {
                return m_Left_IB;
            }
            set
            {
                m_Left_IB = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Thalweg*")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Thalweg
        {
            get
            {
                return m_Thalweg;
            }
            set
            {
                m_Thalweg = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Right IB")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Right_IB
        {
            get
            {
                return m_Right_IB;
            }
            set
            {
                m_Right_IB = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Right Bank*")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Right_Bank
        {
            get
            {
                return m_Right_Bank;
            }
            set
            {
                m_Right_Bank = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Right 2nd")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Right_2nd
        {
            get
            {
                return m_Right_2nd;
            }
            set
            {
                m_Right_2nd = value;
            }
        }

        [TypeConverter(typeof(clsStageOptions))]
        [Category("Stages")]
        [Description("Select the station location from the list")]
        [DisplayName("Right 3rd")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string Right_3rd
        {
            get
            {
                return m_Right_3rd;
            }
            set
            {
                m_Right_3rd = value;
            }
        }


        // XS DESIGN VARIABLES

        [TypeConverter(typeof(clsNumStageOptions))]
        [Category("XS Design Variables")]
        [Description("Identify number of stages, not including Inner Berm")]
        [DisplayName("Number of Stages")]
        [ReadOnly(false)]
        [Browsable(false)]
        public int numStages
        {
            get
            {
                return m_numStages;
            }
            set
            {
                m_numStages = value;
            }
        }

        [TypeConverter(typeof(clsShearOptions))]
        [Category("Plot Options")]
        [Description("Select D50 Mobilized equation to plot")]
        [DisplayName("Shear-Threshold")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string ShearMethod
        {
            get
            {
                return m_ShearMethod;
            }
            set
            {
                m_ShearMethod = value;
            }
        }


        [TypeConverter(typeof(clsShearVelPlotOptions))]
        [Category("Plot Options")]
        [Description("Select Velocity Options to Plot")]
        [DisplayName("Velocity Calcs")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string VelocityPlotMethod
        {
            get
            {
                return m_VelocityPlotMethod;
            }
            set
            {
                m_VelocityPlotMethod = value;
            }
        }

        [TypeConverter(typeof(clsShearVelPlotOptions))]
        [Category("Plot Options")]
        [Description("Select Shear Options to Plot")]
        [DisplayName("Shear Calcs")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string ShearPlotMethod
        {
            get
            {
                return m_ShearPlotMethod;
            }
            set
            {
                m_ShearPlotMethod = value;
            }
        }


        [Category("Shear-Threshold Calcs")]
        [Description("Andrews Equation Variables")]
        [DisplayName("Andrews 1983")]
        [ReadOnly(false)]
        [Browsable(true)]
        public clsAndrewsProperties Subcategory1 { get; set; }


        [Category("Shear-Threshold Calcs")]
        [Description("Identify Shield's Parameter Value: Typically 0.045 to 0.06")]
        [DisplayName("Shields Parameter")]
        [ReadOnly(false)]
        [Browsable(true)]
        public double ShieldsValue
        {
            get
            {
                return m_ShieldsValue;
            }
            set
            {
                m_ShieldsValue = value;
            }
        }

        [Category("XS Design Variables")]
        [Description("Description of current XS")]
        [DisplayName("XS Description")]
        [ReadOnly(false)]
        [Browsable(true)]
        public string descXS
        {
            get
            {
                return m_descXS;
            }
            set
            {
                m_descXS = value;
            }
        }


        [TypeConverter(typeof(clsYesNoOptions))]
        [Category("XS Design Variables")]
        [Description("Is there an Inner Berm?")]
        [DisplayName("Inner Berm")]
        [ReadOnly(false)]
        [Browsable(false)]
        public string isInnerBerm
        {
            get
            {
                return m_isInnerBerm;
            }
            set
            {
                m_isInnerBerm = value;
            }
        }


        [Category("XS Design Variables")]
        [Description("Inner Berm")]
        [DisplayName("Inner Berm")]
        [ReadOnly(false)]
        [Browsable(false)]
        public clsInnerBermProperties SubcategoryInnerBerm { get; set; }

        [Category("XS Design Variables")]
        [Description("Stage 1")]
        [DisplayName("Stage 1")]
        [ReadOnly(false)]
        [Browsable(false)]
        public clsStage1Properties SubcategoryStage1 { get; set; }

        [Category("XS Design Variables")]
        [Description("Stage 2")]
        [DisplayName("Stage 2")]
        [ReadOnly(false)]
        [Browsable(false)]
        public clsStage2Properties SubcategoryStage2 { get; set; }

        [Category("XS Design Variables")]
        [Description("Stage 3")]
        [DisplayName("Stage 3")]
        [ReadOnly(false)]
        [Browsable(false)]
        public clsStage3Properties SubcategoryStage3 { get; set; }

        [Category("XS Design Variables")]
        [Description("Advanced Parameters")]
        [DisplayName("Advanced")]
        [ReadOnly(false)]
        [Browsable(false)]
        public clsAdvancedXSProperties SubcategoryAdvancedXS { get; set; }

        public void ChangeIsBrowsableTo(bool @bool, string PD)
        {
            PropertyDescriptor oDescriptor;
            BrowsableAttribute attribute;
            // Dim isBrowsable As FieldInfo
            oDescriptor = TypeDescriptor.GetProperties(typeof(clsAnaXS))["test"];
            // MsgBox(theProperty.Name)

            // attribute = CType(theProperty.Attributes(GetType(BrowsableAttribute)), BrowsableAttribute)
            // isBrowsable = attribute.[GetType]().GetField("browsable", BindingFlags.Public Or BindingFlags.Instance)
            // isBrowsable.SetValue(attribute, bool)

            System.ComponentModel.ReadOnlyAttribute oReadOnlyAttribute = (System.ComponentModel.ReadOnlyAttribute)oDescriptor.Attributes[typeof(System.ComponentModel.ReadOnlyAttribute)];

            // if we have a reference to the attribute
            if ((oReadOnlyAttribute != null))
            {
                System.Reflection.FieldInfo oField = oReadOnlyAttribute.GetType().GetField("isReadOnly", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.IgnoreCase);

                // set the value to read only
                oField.SetValue(oReadOnlyAttribute, @bool);
            } // if we have a reference to the attribute
        }

        public void initVars()
        {
            if (isInnerBerm == "Yes")
            {

                // RIFFLE WITH IB
                pL9_XFormula = "W3sbkL";
                pL8_XFormula = "W3rdstageL";
                pL7_XFormula = "W2sbkL";
                pL6_XFormula = "WbenchL";
                pL5_XFormula = "-((Mbanks*Left_Mbanks_BKF_Multiplier)*(Ybanks*Left_BKF_Height_Multiplier))";
                pL4_XFormula = "-0.5*(0.5Wbed)";
                pL3_XFormula = "-0.25*(0.5Wbed)";
                pL2_XFormula = "-0.25*(0.5Wbed)";
                pL1_XFormula = "-.001";
                pCL_XFormula = "0 + x_datum";
                pR1_XFormula = "-.001";
                pR2_XFormula = "0.25*(0.5Wbed)";
                pR3_XFormula = "0.25*(0.5Wbed)";
                pR4_XFormula = "0.5*(0.5Wbed)";
                pR5_XFormula = "(Mbanks)*(Ybanks)";
                pR6_XFormula = "WbenchL";
                pR7_XFormula = "W2sbkR";
                pR8_XFormula = "W3rdstageR";
                pR9_XFormula = "W3sbkR";

                pL9_YFormula = "D3stg-Ybench-L";
                pL8_YFormula = "dY3rdstage-L";
                pL7_YFormula = "D2stg-Ybench-L";
                pL6_YFormula = "dYbench-L";
                pL5_YFormula = "Ybanks";
                pL4_YFormula = "(DMAX_BKF * Left_BKF_Bottom_Slope_Multiplier) - ((tempDmaxib + temphalfDibPrevH + 0.001)) - (dYbanks * Left_BKF_Bottom_Slope_Multiplier) + L3y";
                pL3_YFormula = "0.25*Ybed";
                pL2_YFormula = "0.25*Ybed";
                pL1_YFormula = "0.001";
                pCL_YFormula = "0 + y_datum";
                pR1_YFormula = "0.001";
                pR2_YFormula = "0.25*Ybed";
                pR3_YFormula = "0.25*Ybed";
                pR4_YFormula = "(DMAX_BKF * Right_BKF_Bottom_Slope_Multiplier) - ((tempDmaxib + temphalfDibPrevH + 0.001)) - (dYbanks * Right_BKF_Bottom_Slope_Multiplier) + R3y";
                pR5_YFormula = "(Ybanks * Left_BKF_Height_Multiplier)";
                pR6_YFormula = "dYbench-R";
                pR7_YFormula = "D2stg-Ybench-R";
                pR8_YFormula = "dY3rdstage-R";
                pR9_YFormula = "D3stg-Ybench-R";

                pL9_Desc = "3rd Stage Top Bank";
                pL8_Desc = "End of 3rd stage bench";
                pL7_Desc = "3rd Stage";
                pL6_Desc = "End of bankfull/design bench";
                pL5_Desc = "Bankfull/Design";
                pL4_Desc = "Bottom of Bank";
                pL3_Desc = "Top Innerberm (Point Bar)";
                pL2_Desc = "Baseflow";
                pL1_Desc = "CL ThalwegShift Left";
                pCL_Desc = "Centerline";
                pR1_Desc = "CL ThalwegShift Right";
                pR2_Desc = "Baseflow";
                pR3_Desc = "Top Innerberm (Point Bar)";
                pR4_Desc = "Bottom of Bank";
                pR5_Desc = "Bankfull/Design";
                pR6_Desc = "End of bankfull/design bench";
                pR7_Desc = "3rd Stage";
                pR8_Desc = "End of 3rd stage bench";
                pR9_Desc = "3rd Stage Top Bank";
            }
            else if (isInnerBerm == "No")
            {

                // RIFFLE WITHOUT IB
                pL9_XFormula = "W3sbkL";
                pL8_XFormula = "W3rdstageL";
                pL7_XFormula = "W2sbkL";
                pL6_XFormula = "WbenchL";
                pL5_XFormula = "-(Mbanks)*(Ybanks)";
                pL4_XFormula = "-0.5*(0.5Wbed)";
                pL3_XFormula = "-0.25*(0.5Wbed)";
                pL2_XFormula = "-0.25*(0.5Wbed)";
                pL1_XFormula = "-.001";
                pCL_XFormula = "-RThalwegShift";
                pR1_XFormula = "-.001";
                pR2_XFormula = "0.25*(0.5Wbed)";
                pR3_XFormula = "0.25*(0.5Wbed)";
                pR4_XFormula = "0.5*(0.5Wbed)";
                pR5_XFormula = "(Mbanks)*(Ybanks)";
                pR6_XFormula = "WbenchL";
                pR7_XFormula = "W2sbkR";
                pR8_XFormula = "W3rdstageR";
                pR9_XFormula = "W3sbkR";

                pL9_YFormula = "D3stg-Ybench-L";
                pL8_YFormula = "dY3rdstage-L";
                pL7_YFormula = "D2stg-Ybench-L";
                pL6_YFormula = "dYbench-L";
                pL5_YFormula = "Ybanks";
                pL4_YFormula = "0.5*Ybed";
                pL3_YFormula = "0.25*Ybed";
                pL2_YFormula = "0.25*Ybed";
                pL1_YFormula = "0.001";
                pCL_YFormula = "0";
                pR1_YFormula = "0.001";
                pR2_YFormula = "0.25*Ybed";
                pR3_YFormula = "0.25*Ybed";
                pR4_YFormula = "0.5*Ybed";
                pR5_YFormula = "Ybanks";
                pR6_YFormula = "dYbench-R";
                pR7_YFormula = "D2stg-Ybench-R";
                pR8_YFormula = "dY3rdstage-R";
                pR9_YFormula = "D3stg-Ybench-R";

                pL9_Desc = "3rd Stage Top Bank";
                pL8_Desc = "End of 3rd stage bench";
                pL7_Desc = "3rd Stage";
                pL6_Desc = "End of bankfull/design bench";
                pL5_Desc = "Bankfull/Design";
                pL4_Desc = "Bottom of Bank";
                pL3_Desc = "Top Innerberm (Point Bar)";
                pL2_Desc = "Baseflow";
                pL1_Desc = "CL ThalwegShift Left";
                pCL_Desc = "Centerline";
                pR1_Desc = "CL ThalwegShift Right";
                pR2_Desc = "Baseflow";
                pR3_Desc = "Top Innerberm (Point Bar)";
                pR4_Desc = "Bottom of Bank";
                pR5_Desc = "Bankfull/Design";
                pR6_Desc = "End of bankfull/design bench";
                pR7_Desc = "3rd Stage";
                pR8_Desc = "End of 3rd stage bench";
                pR9_Desc = "3rd Stage Top Bank";
            }
            else
            {
            }
        }


        public void CalculatePoints()
        {
            double dYbanks;
            double Wbed;
            double halfWbed;
            double Sbed_transverse;
            double Sib_trns, PtBrSlp;
            double W1;
            double W2;
            double Y1;
            double Y2;
            double Abkf;
            double Dmean;
            double WDRatio;
            double Ybedtotal;
            double YibArea;
            double YibMeanDepth;

            double W2s_bk_L;
            double W2s_bk_R;
            double D2s_bk_L;
            double D2s_bk_R;
            double S2stg_bench_L;
            double S2stg_bench_R;

            double W3s_bk_L;
            double W3s_bk_R;
            double D3s_bk_L;
            double D3s_bk_R;
            double S3stg_bench_L;
            double S3stg_bench_R;

            // call method to init vars (descriptions/formulas)
            initVars();

            if (isInnerBerm == "Yes")
            {

                // set up calculated variables
                YibArea = (2 * ((0.5 * SubcategoryInnerBerm.WIB) * SubcategoryInnerBerm.DmaxIB));
                Ybedtotal = SubcategoryStage1.ybed + SubcategoryInnerBerm.DmaxIB;
                YibMeanDepth = SubcategoryInnerBerm.DmaxIB + SubcategoryStage1.ybed;
                YibMeanDepth = YibArea / SubcategoryStage1.Width;

                dYbanks = SubcategoryStage1.Depth - SubcategoryStage1.ybed - YibMeanDepth;
                Y2 = dYbanks;
                Y1 = SubcategoryStage1.ybed;
                W2 = SubcategoryStage1.mbanks * dYbanks;
                W1 = (SubcategoryStage1.Width * 0.5) - W2;

                Abkf = (2 * (W1 * Y2) + (W1 * Y1) + (W2 * Y2)) + YibArea;
                Dmean = Abkf / SubcategoryStage1.Width;
                WDRatio = SubcategoryStage1.Width / Dmean;


                Sib_trns = SubcategoryInnerBerm.DmaxIB / (double)(0.5 * SubcategoryInnerBerm.WIB);
                Wbed = SubcategoryStage1.Width - (2 * (SubcategoryStage1.mbanks * dYbanks));
                halfWbed = 0.5 * Wbed;
                Sbed_transverse = (SubcategoryStage1.ybed - SubcategoryInnerBerm.DmaxIB) / (double)(halfWbed - (0.5 * SubcategoryInnerBerm.WIB));

                S2stg_bench_R = SubcategoryStage2.Ybench_R / (double)SubcategoryStage2.Wbench_R;
                S2stg_bench_L = SubcategoryStage2.Ybench_L / (double)SubcategoryStage2.Wbench_L;
                D2s_bk_R = SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_R;
                D2s_bk_L = SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_R;
                W2s_bk_R = SubcategoryStage2.Mbanks_2stg * D2s_bk_R;
                W2s_bk_L = SubcategoryStage2.Mbanks_2stg * D2s_bk_L;

                D3s_bk_R = SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_R;
                D3s_bk_L = SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_L;
                W3s_bk_R = SubcategoryStage3.Mbanks_3stg * D3s_bk_R;
                W3s_bk_L = SubcategoryStage3.Mbanks_3stg * D3s_bk_L;
                S3stg_bench_R = SubcategoryStage3.y3rdStage_R / (double)SubcategoryStage3.W3rdstage_R;
                S3stg_bench_L = SubcategoryStage3.y3rdStage_L / (double)SubcategoryStage3.W3rdstage_L;


                // SETUP X VALUES FOR POINTS
                CLx = SubcategoryStage1.thalweg_shift + SubcategoryAdvancedXS.X_datum;

                // LEFT SIDE
                L1x = -0.001 + CLx;
                L2x = (-0.25 * SubcategoryInnerBerm.WIB) + L1x;
                L3x = (-0.25 * SubcategoryInnerBerm.WIB) + L2x;


                if (SubcategoryStage1.thalweg_shift > 0)
                    L4x = ((0.5 * -Wbed) - ((-0.25 * SubcategoryInnerBerm.WIB) + (-0.25 * SubcategoryInnerBerm.WIB)) - SubcategoryStage1.thalweg_shift) + L3x;
                else if (SubcategoryStage1.thalweg_shift < 0)
                    L4x = ((0.5 * -Wbed) - ((-0.25 * SubcategoryInnerBerm.WIB) + (-0.25 * SubcategoryInnerBerm.WIB)) - SubcategoryStage1.thalweg_shift) + L3x;
                else
                    L4x = ((0.5 * -Wbed) - ((-0.25 * SubcategoryInnerBerm.WIB) + (-0.25 * SubcategoryInnerBerm.WIB))) + L3x;


                L5x = -((SubcategoryStage1.mbanks * SubcategoryAdvancedXS.Left_Mbanks_BKF_Multiplier) * (dYbanks * SubcategoryAdvancedXS.Left_BKF_Height_Multiplier)) + L4x;
                // L5x = (-(Mbanks * dYbanks)) + L4x

                L6x = -SubcategoryStage2.Wbench_L + L5x;
                L7x = -W2s_bk_L + L6x;
                L8x = -SubcategoryStage3.W3rdstage_L + L7x;
                L9x = -W3s_bk_L + L8x;

                // RIGHT SIDE
                R1x = 0.001 + CLx;
                R2x = (0.25 * SubcategoryInnerBerm.WIB) + R1x;
                R3x = (0.25 * SubcategoryInnerBerm.WIB) + R2x;

                if (SubcategoryStage1.thalweg_shift > 0)
                    R4x = ((0.5 * Wbed) - ((0.25 * SubcategoryInnerBerm.WIB) + (0.25 * SubcategoryInnerBerm.WIB)) - SubcategoryStage1.thalweg_shift) + R3x;
                else if (SubcategoryStage1.thalweg_shift < 0)
                    R4x = ((0.5 * Wbed) - ((0.25 * SubcategoryInnerBerm.WIB) + (0.25 * SubcategoryInnerBerm.WIB)) - SubcategoryStage1.thalweg_shift) + R3x;
                else
                    R4x = ((0.5 * Wbed) - ((0.25 * SubcategoryInnerBerm.WIB) + (0.25 * SubcategoryInnerBerm.WIB))) + R3x;

                // R5x = (Mbanks * dYbanks) + R4x
                R5x = ((SubcategoryStage1.mbanks * SubcategoryAdvancedXS.Right_Mbanks_BKF_Multiplier) * (dYbanks * SubcategoryAdvancedXS.Right_BKF_Height_Multiplier)) + R4x;

                R6x = SubcategoryStage2.Wbench_R + R5x;
                R7x = W2s_bk_R + R6x;
                R8x = SubcategoryStage3.W3rdstage_R + R7x;
                R9x = W3s_bk_R + R8x;

                // SETUP Y VALUES FOR POINTS - left side
                CLy = 0 + SubcategoryAdvancedXS.Y_datum;

                double temphalfDibPrevH;
                temphalfDibPrevH = (SubcategoryInnerBerm.DmaxIB / (double)2) - (0.001 + 0);
                double tempDmaxib;
                tempDmaxib = SubcategoryInnerBerm.DmaxIB - temphalfDibPrevH;

                L1y = 0.001 + CLy;
                L2y = temphalfDibPrevH + L1y;
                L3y = tempDmaxib + L2y;

                // L4y = DMAX_BKF - ((tempDmaxib + temphalfDibPrevH + 0.001)) - dYbanks + L3y
                L4y = (SubcategoryStage1.Depth * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) - ((tempDmaxib + temphalfDibPrevH + 0.001)) - (dYbanks * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + L3y;

                // L5y = dYbanks + L4y
                L5y = (dYbanks * SubcategoryAdvancedXS.Left_BKF_Height_Multiplier) + L4y;

                L6y = SubcategoryStage2.Ybench_L + L5y;
                L7y = (SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_L) + L6y;
                L8y = SubcategoryStage3.y3rdStage_L + L7y;
                L9y = (SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_L) + L8y;

                // RIGHT SIDE
                R1y = 0.001 + CLy;
                R2y = temphalfDibPrevH + R1y;
                R3y = tempDmaxib + R2y;

                // R4y = DMAX_BKF - ((tempDmaxib + temphalfDibPrevH + 0.001)) - dYbanks + R3y
                R4y = (SubcategoryStage1.Depth * SubcategoryAdvancedXS.Right_BKF_Bottom_Slope_Multiplier) - ((tempDmaxib + temphalfDibPrevH + 0.001)) - (dYbanks * SubcategoryAdvancedXS.Right_BKF_Bottom_Slope_Multiplier) + R3y;

                // R5y = dYbanks + R4y
                R5y = (dYbanks * SubcategoryAdvancedXS.Right_BKF_Height_Multiplier) + R4y;

                R6y = SubcategoryStage2.Ybench_R + R5y;
                R7y = (SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_R) + R6y;
                R8y = SubcategoryStage3.y3rdStage_R + R7y;
                R9y = (SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_R) + R8y;

                if (numStages == 1)
                {
                    L9x = L5x; L8x = L5x; L7x = L5x; L6x = L5x;
                    L9y = L5y; L8y = L5y; L7y = L5y; L6y = L5y;
                    R9x = R5x; R8x = R5x; R7x = R5x; R6x = R5x;
                    R9y = R5y; R8y = R5y; R7y = R5y; R6y = R5y;
                }
                else if (numStages == 2)
                {
                    L9x = L7x; L8x = L7x;
                    L9y = L7y; L8y = L7y;
                    R9x = R7x; R8x = R7x;
                    R9y = R7y; R8y = R7y;
                }
                else
                {
                }
            }
            else if (isInnerBerm == "No")
            {

                // set up calculated variables
                SubcategoryInnerBerm.DmaxIB = 0;
                SubcategoryInnerBerm.WIB = 0;

                // YibArea = (2 * ((0.5 * SubcategoryInnerBerm.WIB) * DmaxIB))
                // Ybedtotal = Ybed + DmaxIB
                // YibMeanDepth = DmaxIB + Ybed
                // YibMeanDepth = YibArea / SubcategoryStage1.Width

                dYbanks = SubcategoryStage1.Depth - SubcategoryStage1.ybed; // - YibMeanDepth
                Y2 = dYbanks;
                Y1 = SubcategoryStage1.ybed;
                W2 = SubcategoryStage1.mbanks * dYbanks;
                W1 = (SubcategoryStage1.Width * 0.5) - W2;

                Abkf = (2 * (W1 * Y2) + (W1 * Y1) + (W2 * Y2)); // + YibArea
                Dmean = Abkf / SubcategoryStage1.Width;
                WDRatio = SubcategoryStage1.Width / Dmean;

                // Sib_trns = DmaxIB / (0.5 * SubcategoryInnerBerm.WIB)
                Wbed = SubcategoryStage1.Width - (2 * (SubcategoryStage1.mbanks * dYbanks));
                halfWbed = 0.5 * Wbed;
                // Sbed_transverse = (Ybed - DmaxIB) / (halfWbed - (0.5 * SubcategoryInnerBerm.WIB))
                Sbed_transverse = (SubcategoryStage1.ybed - SubcategoryInnerBerm.DmaxIB) / (double)(halfWbed - (0.5 * SubcategoryInnerBerm.WIB));

                S2stg_bench_R = SubcategoryStage2.Ybench_R / (double)SubcategoryStage2.Wbench_R;
                S2stg_bench_L = SubcategoryStage2.Ybench_L / (double)SubcategoryStage2.Wbench_L;
                D2s_bk_R = SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_R;
                D2s_bk_L = SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_R;
                W2s_bk_R = SubcategoryStage2.Mbanks_2stg * D2s_bk_R;
                W2s_bk_L = SubcategoryStage2.Mbanks_2stg * D2s_bk_L;

                S3stg_bench_R = SubcategoryStage2.Ybench_R / (double)SubcategoryStage2.Wbench_R;
                S3stg_bench_L = SubcategoryStage2.Ybench_L / (double)SubcategoryStage2.Wbench_L;
                D3s_bk_R = SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_R;
                D3s_bk_L = SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_L;
                W3s_bk_R = SubcategoryStage3.Mbanks_3stg * D3s_bk_R;
                W3s_bk_L = SubcategoryStage3.Mbanks_3stg * D3s_bk_L;


                // SETUP X VALUES FOR POINTS
                CLx = SubcategoryStage1.thalweg_shift + SubcategoryAdvancedXS.X_datum;

                // LEFT SIDE
                L1x = -0.001 + CLx;
                L2x = (-0.25 + (-0.25 * SubcategoryStage1.Roundness)) * (L1x + CLx + (0.5 * Wbed)) + L1x;
                L3x = -0.25 * (CLx + (0.5 * Wbed)) + L2x;


                if (SubcategoryStage1.thalweg_shift > 0)
                    L4x = ((-0.25 + (-0.25 * Math.Abs(SubcategoryStage1.Roundness - 1))) * ((0.5 * Wbed) + SubcategoryStage1.thalweg_shift)) + L3x;
                else if (SubcategoryStage1.thalweg_shift < 0)
                    L4x = ((-0.25 + (-0.25 * Math.Abs(SubcategoryStage1.Roundness - 1))) * ((0.5 * Wbed) + SubcategoryStage1.thalweg_shift)) + L3x;
                else
                    L4x = ((-0.25 + (-0.25 * Math.Abs(SubcategoryStage1.Roundness - 1))) * ((0.5 * Wbed))) + L3x;

                L5x = -((SubcategoryStage1.mbanks * SubcategoryAdvancedXS.Left_Mbanks_BKF_Multiplier) * (dYbanks * SubcategoryAdvancedXS.Left_BKF_Height_Multiplier)) + L4x;
                // L5x = (-(Mbanks * dYbanks)) + L4x

                L6x = -SubcategoryStage2.Wbench_L + L5x;
                L7x = -W2s_bk_L + L6x;
                L8x = -SubcategoryStage3.W3rdstage_L + L7x;
                L9x = -W3s_bk_L + L8x;

                // RIGHT SIDE
                R1x = 0.001 + CLx;
                R2x = (0.25 + (0.25 * SubcategoryStage1.Roundness)) * ((0.5 * Wbed) - L1x + SubcategoryStage1.thalweg_shift) + R1x;
                R3x = 0.25 * ((0.5 * Wbed) - SubcategoryStage1.thalweg_shift) + R2x;

                if (SubcategoryStage1.thalweg_shift > 0)
                    R4x = ((0.25 + (0.25 * Math.Abs(SubcategoryStage1.Roundness - 1))) * ((0.5 * Wbed) - SubcategoryStage1.thalweg_shift)) + R3x;
                else if (SubcategoryStage1.thalweg_shift < 0)
                    R4x = ((0.25 + (0.25 * Math.Abs(SubcategoryStage1.Roundness - 1))) * ((0.5 * Wbed) - SubcategoryStage1.thalweg_shift)) + R3x;
                else
                    R4x = ((0.25 + (0.25 * Math.Abs(SubcategoryStage1.Roundness - 1))) * ((0.5 * Wbed))) + R3x;

                // R5x = (Mbanks * dYbanks) + R4x
                R5x = ((SubcategoryStage1.mbanks * SubcategoryAdvancedXS.Right_Mbanks_BKF_Multiplier) * (dYbanks * SubcategoryAdvancedXS.Right_BKF_Height_Multiplier)) + R4x;

                R6x = SubcategoryStage2.Wbench_R + R5x;
                R7x = W2s_bk_R + R6x;
                R8x = SubcategoryStage3.W3rdstage_R + R7x;
                R9x = W3s_bk_R + R8x;

                // SETUP Y VALUES FOR POINTS - left side
                CLy = 0 + SubcategoryAdvancedXS.Y_datum;

                L1y = (0.001 * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + CLy;
                L2y = (0.25 * SubcategoryStage1.ybed * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + L1y;
                L3y = (0.25 * SubcategoryStage1.ybed * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + L2y;

                // L4y = SubcategoryStage1.Depth - ((tempDmaxib + temphalfDibPrevH + 0.001)) - dYbanks + L3y
                L4y = (0.5 * SubcategoryStage1.ybed * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + L3y;

                // L5y = dYbanks + L4y
                L5y = (dYbanks * SubcategoryAdvancedXS.Left_BKF_Height_Multiplier) + L4y;

                L6y = SubcategoryStage2.Ybench_L + L5y;
                L7y = (SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_L) + L6y;
                L8y = SubcategoryStage3.y3rdStage_L + L7y;
                L9y = (SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_L) + L8y;

                // RIGHT SIDE
                R1y = (0.001 * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + CLy;
                R2y = (0.25 * SubcategoryStage1.ybed * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + R1y;
                R3y = (0.25 * SubcategoryStage1.ybed * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + R2y;

                // R4y = SubcategoryStage1.Depth - ((tempDmaxib + temphalfDibPrevH + 0.001)) - dYbanks + R3y
                R4y = (0.5 * SubcategoryStage1.ybed * SubcategoryAdvancedXS.Left_BKF_Bottom_Slope_Multiplier) + R3y;

                // R5y = dYbanks + R4y
                R5y = (dYbanks * SubcategoryAdvancedXS.Right_BKF_Height_Multiplier) + R4y;

                R6y = SubcategoryStage2.Ybench_R + R5y;
                R7y = (SubcategoryStage2.D2stg - SubcategoryStage2.Ybench_R) + R6y;
                R8y = SubcategoryStage3.y3rdStage_R + R7y;
                R9y = (SubcategoryStage3.D3stg - SubcategoryStage3.y3rdStage_R) + R8y;

                if (numStages == 1)
                {
                    L9x = L5x; L8x = L5x; L7x = L5x; L6x = L5x;
                    L9y = L5y; L8y = L5y; L7y = L5y; L6y = L5y;
                    R9x = R5x; R8x = R5x; R7x = R5x; R6x = R5x;
                    R9y = R5y; R8y = R5y; R7y = R5y; R6y = R5y;
                }
                else if (numStages == 2)
                {
                    L9x = L7x; L8x = L7x;
                    L9y = L7y; L8y = L7y;
                    R9x = R7x; R8x = R7x;
                    R9y = R7y; R8y = R7y;
                }
                else if (numStages == 0)
                {
                    L9x = L5x; L8x = L5x; L7x = L5x; L6x = L5x;
                    L9y = L5y; L8y = L5y; L7y = L5y; L6y = L5y;
                    R9x = R5x; R8x = R5x; R7x = R5x; R6x = R5x;
                    R9y = R5y; R8y = R5y; R7y = R5y; R6y = R5y;
                }
                else
                {
                }
            }
            else
            {
            }

            assignPointsToArray();

            writeXYZData();
        }

        public void assignPointsToArray()
        {
            xPoints = new[] { L9x, L8x, L7x, L6x, L5x, L4x, L3x, L2x, L1x, CLx, R1x, R2x, R3x, R4x, R5x, R6x, R7x, R8x, R9x };

            for (int i = 0; i <= xPoints.Length - 1; i++)
                xPoints[i] = Math.Round(xPoints[i], 2);

            yPoints = new[] { L9y, L8y, L7y, L6y, L5y, L4y, L3y, L2y, L1y, CLy, R1y, R2y, R3y, R4y, R5y, R6y, R7y, R8y, R9y };

            for (int i = 0; i <= xPoints.Length - 1; i++)
                yPoints[i] = Math.Round(yPoints[i], 2);

            if (isInnerBerm == "No")
            {
                if (numStages == 3)
                    stgPoints = new[] { "Left 3rd Stage", "", "Left 2nd Stage", "", "Left Bankfull", "", "", "", "", "Thalweg", "", "", "", "", "Right Bankfull", "", "Right 2nd Stage", "", "Right 3rd Stage" };
                else if (numStages == 2)
                    stgPoints = new[] { "", "", "Left 2nd Stage", "", "Left Bankfull", "", "", "", "", "Thalweg", "", "", "", "", "Right Bankfull", "", "Right 2nd Stage", "", "" };
                else if (numStages == 1)
                    stgPoints = new[] { "", "", "", "", "Left Bankfull", "", "", "", "", "Thalweg", "", "", "", "", "Right Bankfull", "", "", "", "" };
                else
                {
                }
            }
            else if (numStages == 3)
                stgPoints = new[] { "Left 3rd Stage", "", "Left 2nd Stage", "", "Left Bankfull", "", "", "Left Inner Berm", "", "Thalweg", "", "Right Inner Berm", "", "", "Right Bankfull", "", "Right 2nd Stage", "", "Right 3rd Stage" };
            else if (numStages == 2)
                stgPoints = new[] { "", "", "Left 2nd Stage", "", "Left Bankfull", "", "", "Left Inner Berm", "", "Thalweg", "", "Right Inner Berm", "", "", "Right Bankfull", "", "Right 2nd Stage", "", "" };
            else if (numStages == 1)
                stgPoints = new[] { "", "", "", "", "Left Bankfull", "", "", "", "", "Thalweg", "", "", "", "", "Right Bankfull", "", "", "", "" };
            else
            {
            }
        }

        public void writeXYZData()
        {
            int dataPoints = 19;
            string[] tempStoreNValues = new string[dataPoints - 1 + 1];
            int i = 0;
            foreach (DataRow row in xsDataTable.Rows)
            {
                if (row["n Value"] == DBNull.Value)
                    tempStoreNValues[i] = "";
                else if (row["n Value"].ToString() != "" | row["n Value"].ToString() == "")
                    tempStoreNValues[i] = row["n Value"].ToString();
                else
                {
                }
                i = i + 1;
            }

            xsDataTable.Clear();


            int count = 0;
            foreach (double item in xPoints)
            {
                DataRow newRow = xsDataTable.NewRow();
                newRow["Station"] = System.Convert.ToString(item);
                newRow["Elevation"] = System.Convert.ToString(yPoints[count]);
                newRow["n Value"] = tempStoreNValues[count];
                newRow["Stage"] = stgPoints[count];
                xsDataTable.Rows.Add(newRow);
                count = count + 1;
            }

            loadBaseNvalues();

            Console.WriteLine("Writing XS Data Table");
        }


        public void Load()//string fileName)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(clsXSDesignerModule));
            //clsXSDesignerModule retVal;
            //TextReader reader;
            //bool fileNotFound;

            //try
            //{
            //    reader = new StreamReader(fileName);
            //}
            //catch (FileNotFoundException ex)
            //{
            //    // Take the defaults
            //    fileNotFound = true;
            //}

            //if (fileNotFound)
            //{
            //}
            //else
            //{
            //    // Read it from the file
            //    retVal = serializer.Deserialize(reader);
            //    reader.Close();
            //}

            //return retVal;
        }

        public void Save()//string saveLocation)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(clsXSDesignerModule));
            //TextWriter writer = new StreamWriter(saveLocation);
            //serializer.Serialize(writer, this);
            //writer.Close();
        }
    }
}
