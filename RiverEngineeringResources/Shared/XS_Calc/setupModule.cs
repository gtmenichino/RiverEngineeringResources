
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
using System.Data;

namespace RiverEngineeringResources.Shared.XS_Calc
{
    static class setupModule
    {
        //public static void AddCustomFunctions(SpreadsheetGear.Windows.Forms.WorkbookView theWBV)
        //{
        //    // Call Module1.addCustomFunctions()
        //    // Sub DoSomeThreadSafeWork(ByVal workbookSet As IWorkbookSet)
        //    // Interrupt background calculation if necessary and acquire an 
        //    // exclusive lock on the workbook set. 
        //    if (myMainForm == null)
        //    {
        //        // MsgBox("nothing")
        //        // mainForm = New mainForm() ' SOMETHING WEIRD WITH DEFAULT INSTANCES NOT BEING CREATED
        //        var frm2 = new MainForm();
        //        theWBV.ActiveWorkbook.WorkbookSet.GetLock();
        //    }
        //    else
        //    {
        //        // MsgBox("something")

        //        theWBV.ActiveWorkbook.WorkbookSet.GetLock();

        //        try
        //        {
        //            // Do some work...
        //            // MsgBox("locked")

        //            theWBV.ActiveWorkbook.WorkbookSet.Add(LinInterp.linInterpResult); // add the custom functions to the workbook set
        //        }

        //        finally
        //        {
        //            // Release the lock on the workbook set and start background 
        //            // calculation if appropriate. 

        //            theWBV.ActiveWorkbook.WorkbookSet.ReleaseLock();
        //        }
        //    }
        //}

        public static void AddCustomFunctionsToWorkbookSet(SpreadsheetGear.IWorkbookSet theWBset)
        {
            // Call Module1.addCustomFunctions()
            // Sub DoSomeThreadSafeWork(ByVal workbookSet As IWorkbookSet)
            // Interrupt background calculation if necessary and acquire an 
            // exclusive lock on the workbook set. 


            //if (myMainForm == null)
            //    // MsgBox("nothing")
            //    // mainForm = New mainForm() ' SOMETHING WEIRD WITH DEFAULT INSTANCES NOT BEING CREATED

            //    theWBset.GetLock();
            //else
            //{
            // MsgBox("something")

            theWBset.GetLock();

            try
            {
                // Do some work...
                // MsgBox("locked")

                theWBset.Add(LinInterp.linInterpResult); // add the custom functions to the workbook set
            }

            finally
            {
                // Release the lock on the workbook set and start background 
                // calculation if appropriate. 

                theWBset.ReleaseLock();
            }
            //}
        }

        public static string datasetToStringAsXml(DataSet ds)
        {
            StringWriter sw = new StringWriter();
            ds.WriteXml(sw, XmlWriteMode.WriteSchema);
            string s = sw.ToString();
            return s;
        }

        public static string datatableToStringAsXml(DataTable dt)
        {
            StringWriter sw = new StringWriter();
            dt.WriteXml(sw, XmlWriteMode.WriteSchema);
            string s = sw.ToString();
            return s;
        }
    }
}
