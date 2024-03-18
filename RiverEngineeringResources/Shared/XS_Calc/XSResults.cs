//using System.Collections.Generic;
//using System.Data;
//using System.Globalization;
//using System.Linq;

//namespace RiverEngineeringResources.Shared.XS_Calc
//{
//    public class XSResults
//    {
//        public RasMapperLib.Profile LOBProfile;
//        public RasMapperLib.Profile ChanProfile;
//        public RasMapperLib.Profile ROBProfile;

//        public PartialResults Ch;
//        public PartialResults LOB;
//        public PartialResults ROB;
//        public PartialResults Full;

//        public class PartialResults
//        {
//            public double Q;
//            public double Vavg;
//            public double A;
//            public double R;
//            public double Wp;
//            public double Wtop;
//            public double Dmean;
//            public double n;
//            public double Tavg;
//            public double Unitstreampower;
//            public double Froude;

//            public double Shields1936;
//            public double LeopoldWolmanMiller1964;
//            public double Andrews;
//            public double Fischenich2001;
//            public double Rosgen;
//        }

//        public DataTable GetHydraulicsChannelCapacityDT()
//        {
//            DataTable dt = new DataTable("Hydraulics Calculations");
//            //dt.Columns.Add("WSE", typeof(System.Double));
//            dt.Columns.Add("Q cfs", typeof(System.Double));
//            dt.Columns.Add("V ft/s", typeof(System.Double));
//            dt.Columns.Add("A ft^2", typeof(System.Double));
//            dt.Columns.Add("R ft", typeof(System.Double));
//            dt.Columns.Add("Wp ft", typeof(System.Double));
//            dt.Columns.Add("Wtop ft", typeof(System.Double));
//            dt.Columns.Add("Dmean ft", typeof(System.Double));
//            dt.Columns.Add("n", typeof(System.Double));
//            dt.Columns.Add("Tavg lb/ft^2", typeof(System.Double));
//            dt.Columns.Add("Power lb/s", typeof(System.Double));
//            dt.Columns.Add("Froude", typeof(System.Double));
//            dt.Columns.Add("Shields in", typeof(System.Double));
//            dt.Columns.Add("LWM in", typeof(System.Double));
//            dt.Columns.Add("Andrews in", typeof(System.Double));
//            dt.Columns.Add("Fischenich in", typeof(System.Double));
//            dt.Columns.Add("Rosgen in", typeof(System.Double));

//            //dt.Clear();

//            dt.TableName = "Hydraulics at WSE"; //+ userWSE.ToString();

//                    //double maxelev = userWSE;
//                    //XSResults PrRes = new XSResults();
//                    // Dim ExRes As New XSResults(corridor, xs, False, maxelev, False)

//                    dt.Rows.Add(Ch.Q, Ch.Vavg, Ch.A, Ch.R, Ch.Wp, Ch.Wp, Ch.Wtop, Ch.Dmean, Ch.n, Ch.Tavg, Ch.Froude, Ch.Shields1936, Ch.LeopoldWolmanMiller1964, Ch.Andrews, Ch.Fischenich2001, Ch.Rosgen);

//                //dt.Print();
//            return dt;
//        }

//        public XSResults(RasMapperLib.Profile profile, double LOBstation, double ROBstation, double LOBn, Double ROBn, double ChanN, double slope, double wse)
//        {

//            //double newMinStationCorrection = Math.Round(Math.Abs((profile.Length()) / (double)2), 2);

//            //double LOBstation = xs.L5_sta + xs.MinStationCorrection + newMinStationCorrection;
//            //double LOBn = xs.OverbankN;
//            //double ROBstation = xs.R5_sta + xs.MinStationCorrection + newMinStationCorrection;
//            //double ROBn = xs.OverbankN;
//            //double ChanN = xs.ChannelN;

//            Ch = new PartialResults();
//            LOB = new PartialResults();
//            ROB = new PartialResults();
//            Full = new PartialResults();

//            LOBProfile = new RasMapperLib.Profile();
//            ChanProfile = new RasMapperLib.Profile();
//            ROBProfile = new RasMapperLib.Profile();

//            LOBProfile.AddRange(profile.Where(x => x.X <= LOBstation - 0.1));
//            ChanProfile.AddRange(profile.Where(x => x.X >= LOBstation - 0.1 && x.X <= ROBstation + 0.1));
//            ROBProfile.AddRange(profile.Where(x => x.X >= ROBstation + 0.1));

//            if (ChanProfile.Count == 0)
//            {
//                ChanProfile.AddNew(LOBProfile.Last().X, LOBProfile.Last().Y);
//                ChanProfile.AddNew(ROBProfile.First().X, ROBProfile.First().Y);
//            }

//            RasMapperLib.Profile fischenichCritShearProfile = new RasMapperLib.Profile();
//            string fishShearDmobStr = " 0,0.0015 0.001,0.0025 0.002,0.004 0.003,0.0075 0.004,0.015 0.006,0.03 0.01,0.06 0.03,0.12 0.06,0.23 0.12,0.45 0.25,0.95 0.54,1.9 1.1,3.75 2.3,7.5 4.7,15 9.3,30 18.7,60 37.4,120";
//            fischenichCritShearProfile.AddRange(fishShearDmobStr);

//            //double slope = 0;
//            //slope = xs.DSslope;


//            List<RasMapperLib.Profile> profList = new List<RasMapperLib.Profile>();
//            //profList.AddRange({ ChanProfile; LOBProfile,ROBProfile});
//            profList.Add(ChanProfile);
//            profList.Add(LOBProfile);
//            profList.Add(ROBProfile);

//            int iterationCount = 0; // two is staring column
//            foreach (RasMapperLib.Profile prof in profList)
//            {
//                double currN = 0;
//                switch (iterationCount)
//                {
//                    case 0:
//                        {
//                            currN = ChanN;
//                            break;
//                        }

//                    case 1:
//                        {
//                            currN = LOBn;
//                            break;
//                        }

//                    case 2:
//                        {
//                            currN = ROBn;
//                            break;
//                        }
//                }

//                // extend the values up with a glass wall
//                if (prof.First().Y < Convert.ToSingle(wse))
//                    prof.Insert(0, new RasMapperLib.Point2D(prof.MinX(), ((float)wse)));

//                if (prof.Last().Y < Convert.ToSingle(wse))
//                    prof.AddNew(prof.MaxX(), ((float)wse));

//                RasMapperLib.ProfileHydraulicPropertiesTable hpt = new RasMapperLib.ProfileHydraulicPropertiesTable(prof, 0.01f, 0.02f, 0.2f, false);
//                RasMapperLib.ProfileHydraulicPropertiesTable.HydraulicProperties nearestHPHydrProps = hpt.HydrProp.OrderBy(x => Math.Abs(x.Elevation - wse)).FirstOrDefault();
//                RasMapperLib.ProfileHydraulicPropertiesTable.HydraulicProperties nearestHPGeom = hpt.GeomProp.OrderBy(x => Math.Abs(x.Elevation - wse)).FirstOrDefault();
//                // Dim extendedHP As RasMapperLib.ProfileHydraulicPropertiesTable.HydraulicProperties = nearestHP.MidPoint(nearestHP, RasMapperLib.ProfileHydraulicPropertiesTable.HydraulicProperties.AreaInterpolation.Linear)
//                double A = nearestHPGeom.Area; // nearestHP.Area 'PRArea(wse)
//                double WP = nearestHPGeom.WettedPerimeter; // PRWettedPerimeter(wse)

//                double MeanDepth = nearestHPGeom.Area / (double)nearestHPGeom.TopWidth;

//                double K = nearestHPGeom.Conveyance(((float)currN)); // nearestHP.Conveyance(currN)
//                double Q = ((1.49) / currN) * A * (Math.Pow((A / WP), (2 / (double)3))) * (Math.Pow(slope, 0.5));

//                if (double.IsNaN(Q))
//                    Q = 0;

//                double V = Q / A;

//                if (double.IsNaN(V))
//                    V = 0;

//                double Shear = 62.4 * (A / WP) * slope;
//                if (double.IsNaN(Shear))
//                    Shear = 0;
//                double UnitPower = (62.4 * Q) * slope / Math.Abs(nearestHPGeom.TopWidth);
//                if (double.IsNaN(UnitPower))
//                    UnitPower = 0;
//                double Froude = V / (Math.Pow((32.2 * nearestHPGeom.HydraulicDepth()), 0.5));
//                if (double.IsNaN(Froude))
//                    Froude = 0;


//                double shieldsParameter = 0.045;
//                double dMobShields = (Shear / ((1.65 * 62.4) * shieldsParameter)) * 12;
//                if (double.IsNaN(dMobShields))
//                    dMobShields = 0;
//                double dMobLWM = ((77.966 * (Math.Pow((62.4 * slope * (nearestHPGeom.HydraulicDepth())), 1.042)))) * 0.0393701;

//                double AndrewsSMR = 0;
//                double d50Bedmm = 150; // d50 mm from riffle pebble count
//                double d50Barmm = 16; // Bar sample d50 or subpavement d50 (use representative)
//                double PSRratio = d50Bedmm / d50Barmm;
//                double largestParticlemm = 240; // largest particle from bar sample or subpavement sample
//                double SMRratio = largestParticlemm / d50Bedmm;

//                double andrewCritShear = 0;

//                if (PSRratio < 7 && PSRratio > 3)
//                    andrewCritShear = 0.084 * (Math.Pow((d50Bedmm / d50Barmm), -0.872));
//                else if (SMRratio < 3 && SMRratio > 1.3)
//                    andrewCritShear = 0.0384 * (Math.Pow((largestParticlemm / d50Bedmm), -0.887));
//                else
//                    // invalid
//                    andrewCritShear = 0.045;

//                double dMobAndrews = ((slope * nearestHPGeom.HydraulicRadius()) / (1.65 * andrewCritShear)) * 12;
//                double dMobRosgen = ((152.02 * (Math.Pow((62.4 * slope * (nearestHPGeom.HydraulicDepth())), 0.7355)))) * 0.0393701;
//                if (double.IsNaN(dMobRosgen))
//                    dMobRosgen = 0;

//                double dMobFischenich = double.NaN;
//                if (!double.IsNaN(Shear))
//                    dMobFischenich = fischenichCritShearProfile.InterpolateY(Shear);

//                switch (iterationCount)
//                {
//                    case 0:
//                        {
//                            {
//                                var withBlock = Ch;
//                                withBlock.Q = Math.Round(Q, 2); // discharge
//                                withBlock.n = currN; // n composite
//                                withBlock.Vavg = Math.Round(V, 2); // avg velocity
//                                withBlock.Wp = Math.Round(nearestHPGeom.WettedPerimeter, 2); // wetted perimeter
//                                withBlock.A = Math.Round(nearestHPGeom.Area, 2);
//                                withBlock.R = Math.Round(nearestHPGeom.HydraulicRadius(), 2);
//                                withBlock.Wtop = Math.Round(nearestHPGeom.TopWidth, 2);
//                                withBlock.Dmean = Math.Round(nearestHPGeom.HydraulicDepth(), 2);
//                                withBlock.Tavg = Math.Round(Shear, 2);
//                                withBlock.Unitstreampower = Math.Round(UnitPower, 2);
//                                withBlock.Froude = Math.Round(Froude, 2);
//                                withBlock.Shields1936 = Math.Round(dMobShields * 25.4, 2);
//                                withBlock.LeopoldWolmanMiller1964 = Math.Round(dMobLWM * 25.4, 2);
//                                withBlock.Andrews = Math.Round(dMobAndrews * 25.4, 2);
//                                withBlock.Fischenich2001 = Math.Round(dMobFischenich * 25.4, 2);
//                                withBlock.Rosgen = Math.Round(dMobRosgen * 25.4, 2);
//                            }

//                            break;
//                        }

//                    case 1:
//                        {
//                            {
//                                var withBlock = LOB;
//                                withBlock.Q = Math.Round(Q, 2); // discharge
//                                withBlock.n = currN; // n composite
//                                withBlock.Vavg = Math.Round(V, 2); // avg velocity
//                                withBlock.Wp = Math.Round(nearestHPGeom.WettedPerimeter, 2); // wetted perimeter
//                                withBlock.A = Math.Round(nearestHPGeom.Area, 2);
//                                withBlock.R = Math.Round(nearestHPGeom.HydraulicRadius(), 2);
//                                withBlock.Wtop = Math.Round(nearestHPGeom.TopWidth, 2);
//                                withBlock.Dmean = Math.Round(nearestHPGeom.HydraulicDepth(), 2);
//                                withBlock.Tavg = Math.Round(Shear, 2);
//                                withBlock.Unitstreampower = Math.Round(UnitPower, 2);
//                                withBlock.Froude = Math.Round(Froude, 2);
//                                withBlock.Shields1936 = Math.Round(dMobShields * 25.4, 2);
//                                withBlock.LeopoldWolmanMiller1964 = Math.Round(dMobLWM * 25.4, 2);
//                                withBlock.Andrews = Math.Round(dMobAndrews * 25.4, 2);
//                                withBlock.Fischenich2001 = Math.Round(dMobFischenich * 25.4, 2);
//                                withBlock.Rosgen = Math.Round(dMobRosgen * 25.4, 2);
//                            }

//                            break;
//                        }

//                    case 2:
//                        {
//                            {
//                                var withBlock = ROB;
//                                withBlock.Q = Math.Round(Q, 2); // discharge
//                                withBlock.n = currN; // n composite
//                                withBlock.Vavg = Math.Round(V, 2); // avg velocity
//                                withBlock.Wp = Math.Round(nearestHPGeom.WettedPerimeter, 2); // wetted perimeter
//                                withBlock.A = Math.Round(nearestHPGeom.Area, 2);
//                                withBlock.R = Math.Round(nearestHPGeom.HydraulicRadius(), 2);
//                                withBlock.Wtop = Math.Round(nearestHPGeom.TopWidth, 2);
//                                withBlock.Dmean = Math.Round(nearestHPGeom.HydraulicDepth(), 2);
//                                withBlock.Tavg = Math.Round(Shear, 2);
//                                withBlock.Unitstreampower = Math.Round(UnitPower, 2);
//                                withBlock.Froude = Math.Round(Froude, 2);
//                                withBlock.Shields1936 = Math.Round(dMobShields * 25.4, 2);
//                                withBlock.LeopoldWolmanMiller1964 = Math.Round(dMobLWM * 25.4, 2);
//                                withBlock.Andrews = Math.Round(dMobAndrews * 25.4, 2);
//                                withBlock.Fischenich2001 = Math.Round(dMobFischenich * 25.4, 2);
//                                withBlock.Rosgen = Math.Round(dMobRosgen * 25.4, 2);
//                            }

//                            break;
//                        }
//                }

//                if (double.IsNaN(Ch.Q))
//                    Ch.Q = 0;
//                if (double.IsNaN(LOB.Q))
//                    Ch.Q = 0;
//                if (double.IsNaN(ROB.Q))
//                    Ch.Q = 0;


//                // hydraulicsResultsDT.Rows(qrow).Item(iterationCount) = Math.Round(Q, 2) 'discharge
//                // hydraulicsResultsDT.Rows(nRow).Item(iterationCount) = currN 'n composite
//                // hydraulicsResultsDT.Rows(vRow).Item(iterationCount) = Math.Round(V, 2) 'avg velocity
//                // hydraulicsResultsDT.Rows(wpRow).Item(iterationCount) = Math.Round(nearestHP.WettedPerimeter, 2) 'wetted perimeter
//                // hydraulicsResultsDT.Rows(areaRow).Item(iterationCount) = Math.Round(nearestHP.Area, 2)
//                // hydraulicsResultsDT.Rows(hydradiusRow).Item(iterationCount) = Math.Round(nearestHP.HydraulicRadius, 2)
//                // hydraulicsResultsDT.Rows(wtopRow).Item(iterationCount) = Math.Round(nearestHP.TopWidth, 2)
//                // hydraulicsResultsDT.Rows(dMeanRow).Item(iterationCount) = Math.Round(nearestHP.HydraulicDepth, 2)
//                // hydraulicsResultsDT.Rows(tAvgRow).Item(iterationCount) = Math.Round(Shear, 2)
//                // hydraulicsResultsDT.Rows(streamPowerRow).Item(iterationCount) = Math.Round(UnitPower, 2)
//                // hydraulicsResultsDT.Rows(froudeRow).Item(iterationCount) = Math.Round(Froude, 2)

//                // sedimentResultsDT.Rows(shieldsRow).Item(iterationCount) = Math.Round(dMobShields * 25.4, 2)
//                // sedimentResultsDT.Rows(lwmRow).Item(iterationCount) = Math.Round(dMobLWM * 25.4, 2)
//                // sedimentResultsDT.Rows(andrewsRow).Item(iterationCount) = Math.Round(dMobAndrews * 25.4, 2)
//                // sedimentResultsDT.Rows(fischenichRow).Item(iterationCount) = Math.Round(dMobFischenich * 25.4, 2)
//                // sedimentResultsDT.Rows(rosgenRow).Item(iterationCount) = Math.Round(dMobRosgen * 25.4, 2)

//                iterationCount += 1;
//            }

//            // TOTAL CALCS

//            // Dim totalArea As Double = hydraulicsResultsDT.Rows(areaRow).Item(chanCol) + hydraulicsResultsDT.Rows(areaRow).Item(LOBcol) + hydraulicsResultsDT.Rows(areaRow).Item(ROBcol)
//            // Dim totalWp As Double = hydraulicsResultsDT.Rows(wpRow).Item(chanCol) + hydraulicsResultsDT.Rows(wpRow).Item(LOBcol) + hydraulicsResultsDT.Rows(wpRow).Item(ROBcol)
//            // Dim totalR As Double = totalArea / totalWp
//            // Dim totalWpN As Double = (hydraulicsResultsDT.Rows(wpRow).Item(chanCol) * hydraulicsResultsDT.Rows(nRow).Item(chanCol) ^ 1.5) + (hydraulicsResultsDT.Rows(wpRow).Item(LOBcol) * hydraulicsResultsDT.Rows(nRow).Item(LOBcol) ^ 1.5) + (hydraulicsResultsDT.Rows(wpRow).Item(ROBcol) * hydraulicsResultsDT.Rows(nRow).Item(ROBcol) ^ 1.5)
//            // Dim totalNcomponents As Double = (totalWpN / totalWp) ^ (2 / 3)

//            Full.A = Ch.A + LOB.A + ROB.A;
//            Full.Wp = Ch.Wp + LOB.Wp + ROB.Wp;
//            Full.R = Full.A / Full.Wp;
//            Full.n = ((Ch.Wp * Math.Pow(Ch.n, 1.5)) + (LOB.Wp * Math.Pow(LOB.n, 1.5)) + (LOB.Wp * Math.Pow(LOB.n, 1.5))) / Full.Wp;

//            Full.Q = Ch.Q + LOB.Q + ROB.Q;
//            Full.Vavg = Full.Q / Full.A;

//            RasMapperLib.ProfileHydraulicPropertiesTable fullhpt = new RasMapperLib.ProfileHydraulicPropertiesTable(profile, 0.01f, 0.02f, 0.2f, false);
//            RasMapperLib.ProfileHydraulicPropertiesTable.HydraulicProperties nearestFullHP = fullhpt.HydrProp.OrderBy(x => Math.Abs(x.Elevation - wse)).FirstOrDefault();

//            Full.Froude = Full.Vavg / (Math.Pow((32.2 * nearestFullHP.HydraulicDepth()), 0.5));
//            Full.Tavg = 62.4 * nearestFullHP.HydraulicRadius() * slope;
//            Full.Unitstreampower = (62.4 * Full.Q) * slope / Math.Abs(nearestFullHP.TopWidth);

//            Full.Q = Math.Round(Full.Q, 2); // discharge
//            Full.n = Math.Round(Full.n, 3); // n composite
//            Full.Vavg = Math.Round(Full.Vavg, 2); // avg velocity
//            Full.Wp = Math.Round(Full.Wp, 2); // wetted perimeter
//            Full.A = Math.Round(Full.A, 2);
//            Full.R = Math.Round(nearestFullHP.HydraulicRadius(), 2);
//            Full.Wtop = Math.Round(nearestFullHP.TopWidth, 2);
//            Full.Dmean = Math.Round(nearestFullHP.HydraulicDepth(), 2);
//            Full.Tavg = Math.Round(Full.Tavg, 2);
//            Full.Unitstreampower = Math.Round(Full.Unitstreampower, 2);
//            Full.Froude = Math.Round(Full.Froude, 2);

//            // hydraulicsResultsDT.Rows(0).Item(xsAvgCol) = Math.Round(totalQ, 2) 'discharge
//            // hydraulicsResultsDT.Rows(1).Item(xsAvgCol) = totalNcomponents 'n composite
//            // hydraulicsResultsDT.Rows(2).Item(xsAvgCol) = Math.Round(avgV, 2) 'avg velocity
//            // hydraulicsResultsDT.Rows(3).Item(xsAvgCol) = Math.Round(totalWp, 2) 'wetted perimeter
//            // hydraulicsResultsDT.Rows(4).Item(xsAvgCol) = Math.Round(totalArea, 2)
//            // hydraulicsResultsDT.Rows(5).Item(xsAvgCol) = Math.Round(nearestFullHP.HydraulicRadius, 2)
//            // hydraulicsResultsDT.Rows(6).Item(xsAvgCol) = Math.Round(nearestFullHP.TopWidth, 2)
//            // hydraulicsResultsDT.Rows(7).Item(xsAvgCol) = Math.Round(nearestFullHP.HydraulicDepth, 2)
//            // hydraulicsResultsDT.Rows(8).Item(xsAvgCol) = Math.Round(avgShear, 2)
//            // hydraulicsResultsDT.Rows(9).Item(xsAvgCol) = Math.Round(avgUnitPower, 2)
//            // hydraulicsResultsDT.Rows(10).Item(xsAvgCol) = Math.Round(avgFroude, 2)

//            double avgShieldsParameter = 0.045;
//            Full.Shields1936 = (Full.Tavg / ((1.65 * 62.4) * avgShieldsParameter)) * 12;
//            Full.LeopoldWolmanMiller1964 = ((77.966 * (Math.Pow((62.4 * slope * (nearestFullHP.HydraulicDepth())), 1.042)))) * 0.0393701;

//            double avgAndrewsSMR = 0;
//            double avgd50Bedmm = 150; // d50 mm from riffle pebble count
//            double avgd50Barmm = 16; // Bar sample d50 or subpavement d50 (use representative)
//            double avgPSRratio = avgd50Bedmm / avgd50Barmm;
//            double avglargestParticlemm = 240; // largest particle from bar sample or subpavement sample
//            double avgSMRratio = avglargestParticlemm / avgd50Bedmm;

//            double avgAndrewCritShear = 0;
//            if (avgPSRratio < 7 && avgPSRratio > 3)
//                avgAndrewCritShear = 0.084 * (Math.Pow((avgd50Bedmm / avgd50Barmm), -0.872));
//            else if (avgSMRratio < 3 && avgSMRratio > 1.3)
//                avgAndrewCritShear = 0.0384 * (Math.Pow((avglargestParticlemm / avgd50Bedmm), -0.887));
//            else
//                // invalid
//                avgAndrewCritShear = 0.045;

//            Full.Andrews = ((slope * nearestFullHP.HydraulicRadius()) / (1.65 * avgAndrewCritShear)) * 12;
//            Full.Rosgen = ((152.02 * (Math.Pow((62.4 * slope * (nearestFullHP.HydraulicDepth())), 0.7355)))) * 0.0393701;

//            Full.Fischenich2001 = double.NaN;
//            if (!double.IsNaN(Full.Tavg))
//                Full.Fischenich2001 = fischenichCritShearProfile.InterpolateY(Full.Tavg);

//            // Convert to inches
//            Full.Shields1936 = Math.Round(Full.Shields1936 * 25.4, 2);
//            Full.LeopoldWolmanMiller1964 = Math.Round(Full.LeopoldWolmanMiller1964 * 25.4, 2);
//            Full.Andrews = Math.Round(Full.Andrews * 25.4, 2);
//            Full.Fischenich2001 = Math.Round(Full.Fischenich2001 * 25.4, 2);
//            Full.Rosgen = Math.Round(Full.Rosgen * 25.4, 2);
//        }
//    }
//}
