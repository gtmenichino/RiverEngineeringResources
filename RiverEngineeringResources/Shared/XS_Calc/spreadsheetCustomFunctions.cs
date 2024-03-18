namespace RiverEngineeringResources.Shared.XS_Calc
{
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
    //using SpreadsheetGear.Windows.Forms;
    using SpreadsheetGear.CustomFunctions;
    using System;
    using System.Diagnostics;

    public class LinInterp : Function
    {
        // Set to the one and only instance of LinInterp.
        public static readonly LinInterp linInterpResult = new LinInterp();

        // Singleton class - so make the constructor private.
        private LinInterp()
            : base("LININTERP", Volatility.Invariant, SpreadsheetGear.CustomFunctions.ValueType.Variant)
        {
            // Constructor logic, if any
        } //linInterp

        public override void Evaluate(IArguments arguments, IValue result)
        {
            ////result.Number = 1;
            //result.Error = SpreadsheetGear.ValueError.Value;
            //return;

            // Check if argument x range or y range has an error
            if (arguments.GetArrayValue(1, 0, 0).IsError || arguments.GetArrayValue(2, 0, 0).IsError)
            {
                // Skip this and set the cell to #n/a
                //Console.WriteLine("value is error");
                return;
            }

            // Need to set up rounding
            int numDecimalsToRound = 5;

            double xVal = arguments.GetArrayValue(0, 0, 0).Number;

            // Rounding = none (xVal remains unchanged)

            try
            {
                //Console.WriteLine("working");
                arguments.GetArrayDimensions(1, out int xRowCount, out int xColCount);
                double[] xNumbers = new double[Math.Max(xRowCount, xColCount)];
                int i = 0;
                int validXCount = 0;
                for (int row = 0; row < xRowCount; row++)
                {
                    bool validRow = true;
                    for (int col = 0; col < xColCount; col++)
                    {
                        IValue value = arguments.GetArrayValue(1, row, col);
                        if (value.Type == SpreadsheetGear.CustomFunctions.ValueType.Number)
                        {
                            //Console.WriteLine($"x'{row}' val: _'{value.Number.ToString()}'_");
                            xNumbers[i++] = value.Number;
                        }
                        else
                        {
                            validRow = false;
                            break;
                        }
                    }
                    if (validRow == true)
                    {
                        validXCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                //Console.WriteLine($"valid x count: _'{validXCount.ToString()}'_");
                Array.Resize(ref xNumbers, validXCount);

                //Console.WriteLine($"length: _'{xNumbers.Length.ToString()}'_");
                double xMin = xNumbers[0];
                double xMax = xNumbers[validXCount - 1];

                arguments.GetArrayDimensions(2, out int yRowCount, out int yColCount);
                double[] yNumbers = new double[Math.Max(yRowCount, yColCount)];
                i = 0;
                int validYCount = 0;
                for (int row = 0; row < yRowCount; row++)
                {
                    bool validRow = true;
                    for (int col = 0; col < yColCount; col++)
                    {
                        IValue value = arguments.GetArrayValue(2, row, col);
                        if (value.Type == SpreadsheetGear.CustomFunctions.ValueType.Number)
                        {
                            //Console.WriteLine($"y val: _'{value.Number.ToString()}'_");
                            yNumbers[i++] = value.Number;
                        }
                        else
                        {
                            validRow = false;
                            break;
                        }
                    }
                    if (validRow == true)
                    {
                        validYCount++;
                    }
                    else
                    {
                        break;
                    }
                }

                //Console.WriteLine($"valid y count: _'{validYCount.ToString()}'_");
                Array.Resize(ref yNumbers, validYCount);
                //Console.WriteLine($"length: _'{yNumbers.Length.ToString()}'_");

                //if(validXCount != validYCount){
                //    Console.WriteLine("")
                //    return
                //}

                double yAtMinX = yNumbers[0];
                double yAtMaxX = yNumbers[validYCount - 1];

                int low = 0;
                int high = i - 1;

                // Binary search sorted range
                int med;
                do
                {
                    med = (low + high) / 2;
                    if (xNumbers[med] < xVal)
                        low = med;
                    else
                        high = med;
                }
                while (Math.Abs(high - low) > 1);

                double xBelow = xNumbers[low], xAbove = xNumbers[high];
                double yBelow = yNumbers[low], yAbove = yNumbers[high];

                double yVal = yBelow + (xVal - xBelow) * (yAbove - yBelow) / (xAbove - xBelow);

                arguments.ClearError();

                //Console.WriteLine($"yAtMinX: _'{yAtMinX.ToString()}'_");
                //Console.WriteLine($"yAtMaxX: _'{yAtMaxX.ToString()}'_");
               // Console.WriteLine($"yval: _'{yVal.ToString()}'_");


                //Console.WriteLine($"xVal: _'{xVal.ToString()}'_");
                //Console.WriteLine($"xMin: _'{xMin.ToString()}'_");
                //Console.WriteLine($"xMax: _'{xMax.ToString()}'_");

                // Handling min and max values
                if (xVal <= xMin)
                    result.Number = yAtMinX;
                else if (xVal >= xMax)
                    result.Number = yAtMaxX;
                else
                    result.Number = yVal;

                //result.Error = SpreadsheetGear.ValueError.None;

                //Console.WriteLine($"result: _'{result.Number}'_");
                //Console.WriteLine($"Unable to parse '{result.Number.ToString()}'");

                //try
                //{
                //    double result123 = double.Parse(result.Number.ToString());
                //    //Console.WriteLine(result);
                //}
                //catch (FormatException)
                //{
                //    Console.WriteLine($"Unable to parse '{result.Number.ToString()}'");
                //}
            }

           
            catch (Exception ex)
            {
                //Console.WriteLine("ERROR");
                //Debug.WriteLine($"Error in {arguments.CurrentWorksheet.Name} at row {arguments.CurrentRow}, col {arguments.CurrentColumn}: {ex.Message}");
            }
        }
    }

}
