using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace RiverEngineeringResources.Shared.XS_Calc
{


    public class clsShearOptions : System.ComponentModel.StringConverter
    {

        private static readonly IList myList = new ReadOnlyCollection<string>(
            new[] {
            "Shields 1936",
            "Leo, Wol, Mill 1964",
            "Andrews 1983",
            "Rosgen CO",
            "Fischenich 2001",
            "USFWS 2008"
            }
        );

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(myList);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // True makes the combobox select only. 
            // False allows free text entry.
            return true;
        }

    }

    public class clsShearVelPlotOptions : System.ComponentModel.StringConverter
    {

        private static readonly IList myList = new ReadOnlyCollection<string>(
            new[] {
                    "Off",
                    "Stream Tubes",
                    "Zone Averaged"
            }
        );

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(myList);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // True makes the combobox select only. 
            // False allows free text entry.
            return true;
        }


    }

    public class clsYesNoOptions : System.ComponentModel.StringConverter
    {
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(myList);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // True makes the combobox select only. 
            // False allows free text entry.
            return true;
        }

        private static readonly IList myList = new ReadOnlyCollection<string>(
            new[] {
                    "Yes",
                    "No"
            }
        );




    }

    public class clsTrueFalseOptions : System.ComponentModel.StringConverter
    {
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(myList);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // True makes the combobox select only. 
            // False allows free text entry.
            return true;
        }

        private static readonly IList myList = new ReadOnlyCollection<string>(
            new[] {
                    "true",
                    "false"
            }
        );
    }


    public class clsNumStageOptions : System.ComponentModel.StringConverter
    {
        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(myList);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // True makes the combobox select only. 
            // False allows free text entry.
            return true;
        }

        private static readonly IList myList = new ReadOnlyCollection<string>(
            new[] {
                    "1",
                    "2",
                    "3"
            }
        );
    }



    public class clsAndrewsProperties
    {
        private double m_Largest_D;
        private double m_D50_Bed;
        private double m_D50_Bar;

        public clsAndrewsProperties()
        {
            m_Largest_D = 200;
            m_D50_Bed = 150;
            m_D50_Bar = 16;
        }

        public override string ToString()
        {
            return string.Empty;
        }

        [Category("D50_Bed")]
        [Description("D50 Bed Material (D50 from riffle pebble count)")]
        [DisplayName("D50 Bed (mm)")]
        public double D50_Bed
        {
            get
            {
                return m_D50_Bed;
            }
            set
            {
                m_D50_Bed = value;
            }
        }

        [Category("D50_Bar")]
        [Description("Bar Sample D50 Or Subpavement D50 (Use Representative)")]
        [DisplayName("D50 Bar (mm)")]
        public double D50_Bar
        {
            get
            {
                return m_D50_Bar;
            }
            set
            {
                m_D50_Bar = value;
            }
        }

        [Category("Largest_D")]
        [Description("Largest Particle from Bar Sample or Subpavement Sample")]
        [DisplayName("D100 Bar (mm)")]
        public double Largest_D
        {
            get
            {
                return m_Largest_D;
            }
            set
            {
                m_Largest_D = value;
            }
        }
    }


    [Serializable()]
    public class clsInnerBermProperties
    {
        private double m_WIB;
        private double m_DmaxIB;

        public clsInnerBermProperties()
        {
            m_WIB = 1;
            m_DmaxIB = 0.5;
        }

        public override string ToString()
        {
            return string.Empty;
        }

        [Category("Wib")]
        [Description("Identify Inner Berm Width")]
        [DisplayName("Width-IB")]
        public double WIB
        {
            get
            {
                return m_WIB;
            }
            set
            {
                m_WIB = value;
            }
        }

        [Category("DmaxIB")]
        [Description("Identify Maximum Depth Inner Berm")]
        [DisplayName("Dmax IB")]
        public double DmaxIB
        {
            get
            {
                return m_DmaxIB;
            }
            set
            {
                m_DmaxIB = value;
            }
        }
    }

    [Serializable()]
    public class clsStage1Properties
    {
        public override string ToString()
        {
            return string.Empty;
        }



        private double m_Width;
        private double m_Depth;
        private double m_mbanks;
        private double m_ybed;
        private double m_thalweg_shift;
        private double m_Mtieout_L;
        private double m_Mtieout_R;
        private double m_Roundness;



        public clsStage1Properties()
        {
            m_Width = 10;
            m_Depth = 2;
            m_mbanks = 2;
            m_ybed = 0.5;
            m_thalweg_shift = 0;
            m_Mtieout_L = 3;
            m_Mtieout_R = 3;
            m_Roundness = 0;
        }
        [Category("Width")]
        [Description("Stage Width")]
        [DisplayName("Width")]
        public double Width
        {
            get
            {
                return m_Width;
            }
            set
            {
                m_Width = value;
            }
        }


        [Category("Depth")]
        [Description("Stage Width")]
        [DisplayName("Depth")]
        public double Depth
        {
            get
            {
                return m_Depth;
            }
            set
            {
                m_Depth = value;
            }
        }


        [Category("mbanks")]
        [Description("Stage Width")]
        [DisplayName("mbanks")]
        public double mbanks
        {
            get
            {
                return m_mbanks;
            }
            set
            {
                m_mbanks = value;
            }
        }


        [Category("ybed")]
        [Description("Stage Width")]
        [DisplayName("ybed")]
        public double ybed
        {
            get
            {
                return m_ybed;
            }
            set
            {
                m_ybed = value;
            }
        }


        [Category("thalweg_shift")]
        [Description("Stage Width")]
        [DisplayName("thalweg_shift")]
        public double thalweg_shift
        {
            get
            {
                return m_thalweg_shift;
            }
            set
            {
                m_thalweg_shift = value;
            }
        }


        [Category("Mtieout_L")]
        [Description("Stage Width")]
        [DisplayName("Mtieout-L")]
        public double Mtieout_L
        {
            get
            {
                return m_Mtieout_L;
            }
            set
            {
                m_Mtieout_L = value;
            }
        }


        [Category("Mtieout_R")]
        [Description("Stage Width")]
        [DisplayName("Mtieout-R")]
        public double Mtieout_R
        {
            get
            {
                return m_Mtieout_R;
            }
            set
            {
                m_Mtieout_R = value;
            }
        }


        [Category("Roundness")]
        [Description("Stage Width")]
        [DisplayName("Roundness")]
        public double Roundness
        {
            get
            {
                return m_Roundness;
            }
            set
            {
                m_Roundness = value;
            }
        }
    }

    [Serializable()]
    public class clsStage2Properties
    {
        public override string ToString()
        {
            return string.Empty;
        }


        private double m_D2stg;
        private double m_Mbanks_2stg;
        private double m_Wbench_L;
        private double m_Wbench_R;
        private double m_Ybench_L;
        private double m_Ybench_R;
        private double m_Mtieout_2stg_L;
        private double m_Mtieout_2stg_R;



        public clsStage2Properties()
        {
            m_D2stg = 2;
            m_Mbanks_2stg = 2;
            m_Wbench_L = 10;
            m_Wbench_R = 10;
            m_Ybench_L = 0.2;
            m_Ybench_R = 0.2;
            m_Mtieout_2stg_L = 3;
            m_Mtieout_2stg_R = 3;
        }

        [Category("D2stg")]
        [Description("Stage Width")]
        [DisplayName("D2stg")]
        public double D2stg
        {
            get
            {
                return m_D2stg;
            }
            set
            {
                m_D2stg = value;
            }
        }


        [Category("Mbanks_2stg")]
        [Description("Stage Width")]
        [DisplayName("Mbanks-2stg")]
        public double Mbanks_2stg
        {
            get
            {
                return m_Mbanks_2stg;
            }
            set
            {
                m_Mbanks_2stg = value;
            }
        }


        [Category("Wbench_L")]
        [Description("Stage Width")]
        [DisplayName("Wbench-L")]
        public double Wbench_L
        {
            get
            {
                return m_Wbench_L;
            }
            set
            {
                m_Wbench_L = value;
            }
        }


        [Category("Wbench_R")]
        [Description("Stage Width")]
        [DisplayName("Wbench-R")]
        public double Wbench_R
        {
            get
            {
                return m_Wbench_R;
            }
            set
            {
                m_Wbench_R = value;
            }
        }


        [Category("Ybench_L")]
        [Description("Stage Width")]
        [DisplayName("ΔYbench-L")]
        public double Ybench_L
        {
            get
            {
                return m_Ybench_L;
            }
            set
            {
                m_Ybench_L = value;
            }
        }


        [Category("Ybench_R")]
        [Description("Stage Width")]
        [DisplayName("ΔYbench-R")]
        public double Ybench_R
        {
            get
            {
                return m_Ybench_R;
            }
            set
            {
                m_Ybench_R = value;
            }
        }


        [Category("Mtieout_2stg_L")]
        [Description("Stage Width")]
        [DisplayName("Mtieout-L")]
        public double Mtieout_2stg_L
        {
            get
            {
                return m_Mtieout_2stg_L;
            }
            set
            {
                m_Mtieout_2stg_L = value;
            }
        }


        [Category("Mtieout_2stg_R")]
        [Description("Stage Width")]
        [DisplayName("Mtieout-R")]
        public double Mtieout_2stg_R
        {
            get
            {
                return m_Mtieout_2stg_R;
            }
            set
            {
                m_Mtieout_2stg_R = value;
            }
        }
    }

    [Serializable()]
    public class clsStage3Properties
    {
        public override string ToString()
        {
            return string.Empty;
        }


        private double m_D3stg;
        private double m_Mbanks_3stg;
        private double m_W3rdstage_L;
        private double m_W3rdstage_R;
        private double m_y3rdStage_L;
        private double m_y3rdStage_R;
        private double m_Mtieout_3stg_L;
        private double m_Mtieout_3stg_R;



        public clsStage3Properties()
        {
            m_D3stg = 0.5;
            m_Mbanks_3stg = 2;
            m_W3rdstage_L = 10;
            m_W3rdstage_R = 10;
            m_y3rdStage_L = 0.2;
            m_y3rdStage_R = 0.2;
            m_Mtieout_3stg_L = 3;
            m_Mtieout_3stg_R = 3;
        }



        [Category("D3stg")]
        [Description("Stage Width")]
        [DisplayName("D3stg")]
        public double D3stg
        {
            get
            {
                return m_D3stg;
            }
            set
            {
                m_D3stg = value;
            }
        }


        [Category("Mbanks_3stg")]
        [Description("Stage Width")]
        [DisplayName("Mbanks-3stg")]
        public double Mbanks_3stg
        {
            get
            {
                return m_Mbanks_3stg;
            }
            set
            {
                m_Mbanks_3stg = value;
            }
        }


        [Category("W3rdstage_L")]
        [Description("Stage Width")]
        [DisplayName("W3rdstage-L")]
        public double W3rdstage_L
        {
            get
            {
                return m_W3rdstage_L;
            }
            set
            {
                m_W3rdstage_L = value;
            }
        }


        [Category("W3rdstage_R")]
        [Description("Stage Width")]
        [DisplayName("W3rdstage-R")]
        public double W3rdstage_R
        {
            get
            {
                return m_W3rdstage_R;
            }
            set
            {
                m_W3rdstage_R = value;
            }
        }


        [Category("y3rdStage_L")]
        [Description("Stage Width")]
        [DisplayName("y3rdStage-L")]
        public double y3rdStage_L
        {
            get
            {
                return m_y3rdStage_L;
            }
            set
            {
                m_y3rdStage_L = value;
            }
        }


        [Category("y3rdStage_R")]
        [Description("Stage Width")]
        [DisplayName("y3rdStage-R")]
        public double y3rdStage_R
        {
            get
            {
                return m_y3rdStage_R;
            }
            set
            {
                m_y3rdStage_R = value;
            }
        }


        [Category("Mtieout_3stg_L")]
        [Description("Stage Width")]
        [DisplayName("Mtieout-L")]
        public double Mtieout_3stg_L
        {
            get
            {
                return m_Mtieout_3stg_L;
            }
            set
            {
                m_Mtieout_3stg_L = value;
            }
        }


        [Category("Mtieout_3stg_R")]
        [Description("Stage Width")]
        [DisplayName("Mtieout-R")]
        public double Mtieout_3stg_R
        {
            get
            {
                return m_Mtieout_3stg_R;
            }
            set
            {
                m_Mtieout_3stg_R = value;
            }
        }
    }


    [Serializable()]
    public class clsAdvancedXSProperties
    {
        public override string ToString()
        {
            return string.Empty;
        }


        private double m_Left_BKF_Height_Multiplier;
        private double m_Right_BKF_Height_Multiplier;
        private double m_Left_Mbanks_BKF_Multiplier;
        private double m_Right_Mbanks_BKF_Multiplier;
        private double m_Left_BKF_Bottom_Slope_Multiplier;
        private double m_Right_BKF_Bottom_Slope_Multiplier;
        private double m_Y_datum;
        private double m_X_datum;


        public clsAdvancedXSProperties()
        {
            m_Left_BKF_Height_Multiplier = 1;
            m_Right_BKF_Height_Multiplier = 1;
            m_Left_Mbanks_BKF_Multiplier = 1;
            m_Right_Mbanks_BKF_Multiplier = 1;
            m_Left_BKF_Bottom_Slope_Multiplier = 1;
            m_Right_BKF_Bottom_Slope_Multiplier = 1;
            m_Y_datum = 0;
            m_X_datum = 0;
        }



        [Category("Left_BKF_Height_Multiplier")]
        [Description("Stage Width")]
        [DisplayName("Left_BKF_Height_Multiplier")]
        public double Left_BKF_Height_Multiplier
        {
            get
            {
                return m_Left_BKF_Height_Multiplier;
            }
            set
            {
                m_Left_BKF_Height_Multiplier = value;
            }
        }


        [Category("Right_BKF_Height_Multiplier")]
        [Description("Stage Width")]
        [DisplayName("Right_BKF_Height_Multiplier")]
        public double Right_BKF_Height_Multiplier
        {
            get
            {
                return m_Right_BKF_Height_Multiplier;
            }
            set
            {
                m_Right_BKF_Height_Multiplier = value;
            }
        }


        [Category("Left_Mbanks_BKF_Multipler")]
        [Description("Stage Width")]
        [DisplayName("Left_Mbanks_BKF_Multipler")]
        public double Left_Mbanks_BKF_Multiplier
        {
            get
            {
                return m_Left_Mbanks_BKF_Multiplier;
            }
            set
            {
                m_Left_Mbanks_BKF_Multiplier = value;
            }
        }


        [Category("Right_Mbanks_BKF_Multiplier")]
        [Description("Stage Width")]
        [DisplayName("Right_Mbanks_BKF_Multiplier")]
        public double Right_Mbanks_BKF_Multiplier
        {
            get
            {
                return m_Right_Mbanks_BKF_Multiplier;
            }
            set
            {
                m_Right_Mbanks_BKF_Multiplier = value;
            }
        }


        [Category("Left_BKF_Bottom_Slope_Multiplier")]
        [Description("Stage Width")]
        [DisplayName("Left_BKF_Bottom_Slope_Multiplier")]
        public double Left_BKF_Bottom_Slope_Multiplier
        {
            get
            {
                return m_Left_BKF_Bottom_Slope_Multiplier;
            }
            set
            {
                m_Left_BKF_Bottom_Slope_Multiplier = value;
            }
        }


        [Category("Right_BKF_Bottom_Slope_Multiplier")]
        [Description("Stage Width")]
        [DisplayName("Right_BKF_Bottom_Slope_Multiplier")]
        public double Right_BKF_Bottom_Slope_Multiplier
        {
            get
            {
                return m_Right_BKF_Bottom_Slope_Multiplier;
            }
            set
            {
                m_Right_BKF_Bottom_Slope_Multiplier = value;
            }
        }


        [Category("Y_datum")]
        [Description("Stage Width")]
        [DisplayName("Y_datum")]
        public double Y_datum
        {
            get
            {
                return m_Y_datum;
            }
            set
            {
                m_Y_datum = value;
            }
        }


        [Category("X_datum")]
        [Description("Stage Width")]
        [DisplayName("X_datum")]
        public double X_datum
        {
            get
            {
                return m_X_datum;
            }
            set
            {
                m_X_datum = value;
            }
        }
    }



}
