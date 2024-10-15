using System.Text.Json.Serialization;

namespace RiverEngineeringResources.Shared
{

    public class MyEngineeringResourceDataTool : MyEngineeringResource
    {
        public MyEngineeringResourceDataTool()
        {
            Type = "dataTool";
        }

        public void ComputeTags()
        {
            List<string> tagsList = new List<string>();

            if (!string.IsNullOrEmpty(DS_Climate) && DS_Climate != "-")
                tagsList.Add("Models and Tools_Data Sources_Climate");

            if (!string.IsNullOrEmpty(DS_Soils_and_Geology) && DS_Soils_and_Geology != "-")
                tagsList.Add("Models and Tools_Data Sources_Soils and Geology");

            if (!string.IsNullOrEmpty(DS_Hydrography_and_Floodplains) && DS_Hydrography_and_Floodplains != "-")
                tagsList.Add("Models and Tools_Data Sources_Hydrography and Floodplains");

            if (!string.IsNullOrEmpty(DS_Water_Quantity) && DS_Water_Quantity != "-")
                tagsList.Add("Models and Tools_Data Sources_Water Quantity");

            if (!string.IsNullOrEmpty(DS_Water_Quality) && DS_Water_Quality != "-")
                tagsList.Add("Models and Tools_Data Sources_Water Quality");

            if (!string.IsNullOrEmpty(DS_Topography_and_Bathymetry) && DS_Topography_and_Bathymetry != "-")
                tagsList.Add("Models and Tools_Data Sources_Topography and Bathymetry");

            if (!string.IsNullOrEmpty(DS_Vegetation) && DS_Vegetation != "-")
                tagsList.Add("Models and Tools_Data Sources_Vegetation");

            if (!string.IsNullOrEmpty(DS_Land_Use_and_Land_Cover) && DS_Land_Use_and_Land_Cover != "-")
                tagsList.Add("Models and Tools_Data Sources_Land Use and Land Cover");

            if (!string.IsNullOrEmpty(DS_Aerial_Photogrammetry) && DS_Aerial_Photogrammetry != "-")
                tagsList.Add("Models and Tools_Data Sources_Aerial Photogrammetry");

            if (!string.IsNullOrEmpty(HY_Stream_gages) && HY_Stream_gages != "-")
                tagsList.Add("Models and Tools_Hydrology_Stream gages");

            if (!string.IsNullOrEmpty(HY_Download_data) && HY_Download_data != "-")
                tagsList.Add("Models and Tools_Hydrology_Download data");

            if (!string.IsNullOrEmpty(HY_Visualize_time_series) && HY_Visualize_time_series != "-")
                tagsList.Add("Models and Tools_Hydrology_Visualize time series");

            if (!string.IsNullOrEmpty(HY_Compute_statistics) && HY_Compute_statistics != "-")
                tagsList.Add("Models and Tools_Hydrology_Compute statistics");

            if (!string.IsNullOrEmpty(HY_Ungaged_gage_transfer_regional_regressions) && HY_Ungaged_gage_transfer_regional_regressions != "-")
                tagsList.Add("Models and Tools_Hydrology_Ungaged gage transfer regional regressions");

            if (!string.IsNullOrEmpty(HY_Bankfull_Regional_Curves) && HY_Bankfull_Regional_Curves != "-")
                tagsList.Add("Models and Tools_Hydrology_Bankfull Regional Curves");

            if (!string.IsNullOrEmpty(HY_Flood_Frequency) && HY_Flood_Frequency != "-")
                tagsList.Add("Models and Tools_Hydrology_Flood Frequency");

            if (!string.IsNullOrEmpty(HY_Flow_Duration) && HY_Flow_Duration != "-")
                tagsList.Add("Models and Tools_Hydrology_Flow Duration");

            if (!string.IsNullOrEmpty(HY_Rainfall_Runoff_Analysis) && HY_Rainfall_Runoff_Analysis != "-")
                tagsList.Add("Models and Tools_Hydrology_Rainfall Runoff Analysis");

            if (!string.IsNullOrEmpty(HY_Flood_Routing) && HY_Flood_Routing != "-")
                tagsList.Add("Models and Tools_Hydrology_Flood Routing");

            if (!string.IsNullOrEmpty(HY_Peak_flows) && HY_Peak_flows != "-")
                tagsList.Add("Models and Tools_Hydrology_Peak flows");

            if (!string.IsNullOrEmpty(HS_Normal_depth_for_1D_steady_uniform_flow) && HS_Normal_depth_for_1D_steady_uniform_flow != "-")
                tagsList.Add("Models and Tools_Hydraulics_Normal depth for 1D steady uniform flow");

            if (!string.IsNullOrEmpty(HS_Critical_depth_for_1D_steady_uniform_flow) && HS_Critical_depth_for_1D_steady_uniform_flow != "-")
                tagsList.Add("Models and Tools_Hydraulics_Critical depth for 1D steady uniform flow");

            if (!string.IsNullOrEmpty(HS_1D_hydraulics) && HS_1D_hydraulics != "-")
                tagsList.Add("Models and Tools_Hydraulics_1D hydraulics");

            if (!string.IsNullOrEmpty(HS_2D_Hydraulics) && HS_2D_Hydraulics != "-")
                tagsList.Add("Models and Tools_Hydraulics_2D Hydraulics");

            if (!string.IsNullOrEmpty(HS_Computational_Fluid_Dynamics) && HS_Computational_Fluid_Dynamics != "-")
                tagsList.Add("Models and Tools_Hydraulics_Computational Fluid Dynamics");

            if (!string.IsNullOrEmpty(HS_Stormwater_and_Pipe_Flows) && HS_Stormwater_and_Pipe_Flows != "-")
                tagsList.Add("Models and Tools_Hydraulics_Stormwater and Pipe Flows");

            if (!string.IsNullOrEmpty(HS_Hydraulic_roughness) && HS_Hydraulic_roughness != "-")
                tagsList.Add("Models and Tools_Hydraulics_Hydraulic roughness");

            if (!string.IsNullOrEmpty(HS_Rating_curve) && HS_Rating_curve != "-")
                tagsList.Add("Models and Tools_Hydraulics_Rating curve");

            if (!string.IsNullOrEmpty(HS_Hydraulic_parameters) && HS_Hydraulic_parameters != "-")
                tagsList.Add("Models and Tools_Hydraulics_Hydraulic parameters");

            if (!string.IsNullOrEmpty(HS_Simple_backwater_profiles) && HS_Simple_backwater_profiles != "-")
                tagsList.Add("Models and Tools_Hydraulics_Simple backwater profiles");

            if (!string.IsNullOrEmpty(HS_SW_GW_Hydraulics) && HS_SW_GW_Hydraulics != "-")
                tagsList.Add("Models and Tools_Hydraulics_SW GW Hydraulics");

            if (!string.IsNullOrEmpty(S_Particle_size_analysis) && S_Particle_size_analysis != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Particle size analysis");

            if (!string.IsNullOrEmpty(S_Pebble_counts_and_bar_samples) && S_Pebble_counts_and_bar_samples != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Pebble counts and bar samples");

            if (!string.IsNullOrEmpty(S_Incipient_motion) && S_Incipient_motion != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Incipient motion");

            if (!string.IsNullOrEmpty(S_Sediment_competence) && S_Sediment_competence != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Sediment competence");

            if (!string.IsNullOrEmpty(S_Sediment_Supply_Yield) && S_Sediment_Supply_Yield != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Sediment Supply Yield");

            if (!string.IsNullOrEmpty(S_Sediment_Transport_Capacity) && S_Sediment_Transport_Capacity != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Sediment Transport Capacity");

            if (!string.IsNullOrEmpty(S_Effective_discharge) && S_Effective_discharge != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Effective discharge");

            if (!string.IsNullOrEmpty(S_Scour) && S_Scour != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Scour");

            if (!string.IsNullOrEmpty(S_Aerial_Photogrammetry) && S_Aerial_Photogrammetry != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Aerial Photogrammetry");

            if (!string.IsNullOrEmpty(S_Bed_and_Bank_Erosion) && S_Bed_and_Bank_Erosion != "-")
                tagsList.Add("Models and Tools_Sediment Transport_Bed and Bank Erosion");

            if (!string.IsNullOrEmpty(G_Hydraulic_geometry) && G_Hydraulic_geometry != "-")
                tagsList.Add("Models and Tools_Geomorphology_Hydraulic geometry");

            if (!string.IsNullOrEmpty(G_Planform_geometry) && G_Planform_geometry != "-")
                tagsList.Add("Models and Tools_Geomorphology_Planform geometry");

            if (!string.IsNullOrEmpty(G_Bed_evolution) && G_Bed_evolution != "-")
                tagsList.Add("Models and Tools_Geomorphology_Bed evolution");

            if (!string.IsNullOrEmpty(G_Channel_change_indices) && G_Channel_change_indices != "-")
                tagsList.Add("Models and Tools_Geomorphology_Channel change indices");

            if (!string.IsNullOrEmpty(G_Geomorphic_Features) && G_Geomorphic_Features != "-")
                tagsList.Add("Models and Tools_Geomorphology_Geomorphic Features");

            if (!string.IsNullOrEmpty(AG_Natural_Channel_Design) && AG_Natural_Channel_Design != "-")
                tagsList.Add("Models and Tools_Actions and Goals_Natural Channel Design");

            if (!string.IsNullOrEmpty(AG_Instream_Structures) && AG_Instream_Structures != "-")
                tagsList.Add("Models and Tools_Actions and Goals_Instream Structures");

            if (!string.IsNullOrEmpty(AG_Large_Wood) && AG_Large_Wood != "-")
                tagsList.Add("Models and Tools_Actions and Goals_Large Wood");

            if (!string.IsNullOrEmpty(AG_Stabilization) && AG_Stabilization != "-")
                tagsList.Add("Models and Tools_Actions and Goals_Stabilization");

            if (!string.IsNullOrEmpty(AG_Restoration_Planning_Site_Evaluation) && AG_Restoration_Planning_Site_Evaluation != "-")
                tagsList.Add("Models and Tools_Actions and Goals_Restoration Planning Site Evaluation");

            Tags = string.Join(",", tagsList);
        }

        //public string? DS_Climate { get; set; }
        //public string? DS_Soils_and_Geology { get; set; }
        //public string? DS_Hydrography_and_Floodplains { get; set; }
        //public string? DS_Water_Quantity { get; set; }
        //public string? DS_Water_Quality { get; set; }
        //public string? DS_Topography_and_Bathymetry { get; set; }
        //public string? DS_Vegetation { get; set; }
        //public string? DS_Land_Use_and_Land_Cover { get; set; }
        //public string? DS_Aerial_Photogrammetry { get; set; }
        //public string? HY_Stream_gages { get; set; }
        //public string? HY_Download_data { get; set; }
        //public string? HY_Visualize_time_series { get; set; }
        //public string? HY_Compute_statistics { get; set; }
        //public string? HY_Ungaged_gage_transfer_regional_regressions { get; set; }
        //public string? HY_Bankfull_Regional_Curves { get; set; }
        //public string? HY_Flood_Frequency { get; set; }
        //public string? HY_Flow_Duration { get; set; }
        //public string? HY_Rainfall_Runoff_Analysis { get; set; }
        //public string? HY_Flood_Routing { get; set; }
        //public string? HY_Peak_flows { get; set; }
        //public string? HS_Normal_depth_for_1D_steady_uniform_flow { get; set; }
        //public string? HS_Critical_depth_for_1D_steady_uniform_flow { get; set; }
        //public string? HS_1D_hydraulics { get; set; }
        //public string? HS_2D_Hydraulics { get; set; }
        //public string? HS_Computational_Fluid_Dynamics { get; set; }
        //public string? HS_Stormwater_and_Pipe_Flows { get; set; }
        //public string? HS_Hydraulic_roughness { get; set; }
        //public string? HS_Rating_curve { get; set; }
        //public string? HS_Hydraulic_parameters { get; set; }
        //public string? HS_Simple_backwater_profiles { get; set; }
        //public string? HS_SW_GW_Hydraulics { get; set; }
        //public string? S_Particle_size_analysis { get; set; }
        //public string? S_Pebble_counts_and_bar_samples { get; set; }
        //public string? S_Incipient_motion { get; set; }
        //public string? S_Sediment_competence { get; set; }
        //public string? S_Sediment_Supply_Yield { get; set; }
        //public string? S_Sediment_Transport_Capacity { get; set; }
        //public string? S_Effective_discharge { get; set; }
        //public string? S_Scour { get; set; }
        //public string? S_Aerial_Photogrammetry { get; set; }
        //public string? S_Bed_and_Bank_Erosion { get; set; }
        //public string? G_Hydraulic_geometry { get; set; }
        //public string? G_Planform_geometry { get; set; }
        //public string? G_Bed_evolution { get; set; }
        //public string? G_Channel_change_indices { get; set; }
        //public string? G_Geomorphic_Features { get; set; }
        //public string? AG_Natural_Channel_Design { get; set; }
        //public string? AG_Instream_Structures { get; set; }
        //public string? AG_Large_Wood { get; set; }
        //public string? AG_Stabilization { get; set; }
        //public string? AG_Restoration_Planning_Site_Evaluation { get; set; }

        [JsonPropertyName("DS_Climate")]
        public string? DS_Climate { get; set; }

        [JsonPropertyName("DS_Soils and Geology")]
        public string? DS_Soils_and_Geology { get; set; }

        [JsonPropertyName("DS_Hydrography and Floodplains")]
        public string? DS_Hydrography_and_Floodplains { get; set; }

        [JsonPropertyName("DS_Water Quantity")]
        public string? DS_Water_Quantity { get; set; }

        [JsonPropertyName("DS_Water Quality")]
        public string? DS_Water_Quality { get; set; }

        [JsonPropertyName("DS_Topography and Bathymetry")]
        public string? DS_Topography_and_Bathymetry { get; set; }

        [JsonPropertyName("DS_Vegetation")]
        public string? DS_Vegetation { get; set; }

        [JsonPropertyName("DS_Land Use and Land Cover")]
        public string? DS_Land_Use_and_Land_Cover { get; set; }

        [JsonPropertyName("DS_Aerial Photogrammetry")]
        public string? DS_Aerial_Photogrammetry { get; set; }

        [JsonPropertyName("HY_Stream gages")]
        public string? HY_Stream_gages { get; set; }

        [JsonPropertyName("HY_Download data")]
        public string? HY_Download_data { get; set; }

        [JsonPropertyName("HY_Visualize time series")]
        public string? HY_Visualize_time_series { get; set; }

        [JsonPropertyName("HY_Compute statistics")]
        public string? HY_Compute_statistics { get; set; }

        [JsonPropertyName("HY_Ungaged gage transfer regional regressions")]
        public string? HY_Ungaged_gage_transfer_regional_regressions { get; set; }

        [JsonPropertyName("HY_Bankfull Regional Curves")]
        public string? HY_Bankfull_Regional_Curves { get; set; }

        [JsonPropertyName("HY_Flood Frequency")]
        public string? HY_Flood_Frequency { get; set; }

        [JsonPropertyName("HY_Flow Duration")]
        public string? HY_Flow_Duration { get; set; }

        [JsonPropertyName("HY_Rainfall Runoff Analysis")]
        public string? HY_Rainfall_Runoff_Analysis { get; set; }

        [JsonPropertyName("HY_Flood Routing")]
        public string? HY_Flood_Routing { get; set; }

        [JsonPropertyName("HY_Peak flows")]
        public string? HY_Peak_flows { get; set; }

        [JsonPropertyName("HS_Normal depth for 1D steady uniform flow")]
        public string? HS_Normal_depth_for_1D_steady_uniform_flow { get; set; }

        [JsonPropertyName("HS_Critical depth for 1D steady uniform flow")]
        public string? HS_Critical_depth_for_1D_steady_uniform_flow { get; set; }

        [JsonPropertyName("HS_1D hydraulics")]
        public string? HS_1D_hydraulics { get; set; }

        [JsonPropertyName("HS_2D Hydraulics")]
        public string? HS_2D_Hydraulics { get; set; }

        [JsonPropertyName("HS_Computational Fluid Dynamics")]
        public string? HS_Computational_Fluid_Dynamics { get; set; }

        [JsonPropertyName("HS_Stormwater and Pipe Flows")]
        public string? HS_Stormwater_and_Pipe_Flows { get; set; }

        [JsonPropertyName("HS_Hydraulic roughness")]
        public string? HS_Hydraulic_roughness { get; set; }

        [JsonPropertyName("HS_Rating curve")]
        public string? HS_Rating_curve { get; set; }

        [JsonPropertyName("HS_Hydraulic parameters")]
        public string? HS_Hydraulic_parameters { get; set; }

        [JsonPropertyName("HS_Simple backwater profiles")]
        public string? HS_Simple_backwater_profiles { get; set; }

        [JsonPropertyName("HS_SW GW Hydraulics")]
        public string? HS_SW_GW_Hydraulics { get; set; }

        [JsonPropertyName("S_Particle size analysis")]
        public string? S_Particle_size_analysis { get; set; }

        [JsonPropertyName("S_Pebble counts and bar samples")]
        public string? S_Pebble_counts_and_bar_samples { get; set; }

        [JsonPropertyName("S_Incipient motion")]
        public string? S_Incipient_motion { get; set; }

        [JsonPropertyName("S_Sediment competence")]
        public string? S_Sediment_competence { get; set; }

        [JsonPropertyName("S_Sediment Supply Yield")]
        public string? S_Sediment_Supply_Yield { get; set; }

        [JsonPropertyName("S_Sediment Transport Capacity")]
        public string? S_Sediment_Transport_Capacity { get; set; }

        [JsonPropertyName("S_Effective discharge")]
        public string? S_Effective_discharge { get; set; }

        [JsonPropertyName("S_Scour")]
        public string? S_Scour { get; set; }

        [JsonPropertyName("S_Aerial Photogrammetry")]
        public string? S_Aerial_Photogrammetry { get; set; }

        [JsonPropertyName("S_Bed and Bank Erosion")]
        public string? S_Bed_and_Bank_Erosion { get; set; }

        [JsonPropertyName("G_Hydraulic geometry")]
        public string? G_Hydraulic_geometry { get; set; }

        [JsonPropertyName("G_Planform geometry")]
        public string? G_Planform_geometry { get; set; }

        [JsonPropertyName("G_Bed evolution")]
        public string? G_Bed_evolution { get; set; }

        [JsonPropertyName("G_Channel change indices")]
        public string? G_Channel_change_indices { get; set; }

        [JsonPropertyName("G_Geomorphic Features")]
        public string? G_Geomorphic_Features { get; set; }

        [JsonPropertyName("AG_Natural Channel Design")]
        public string? AG_Natural_Channel_Design { get; set; }

        [JsonPropertyName("AG_Instream Structures")]
        public string? AG_Instream_Structures { get; set; }

        [JsonPropertyName("AG_Large Wood")]
        public string? AG_Large_Wood { get; set; }

        [JsonPropertyName("AG_Stabilization")]
        public string? AG_Stabilization { get; set; }

        [JsonPropertyName("AG_Restoration Planning Site Evaluation")]
        public string? AG_Restoration_Planning_Site_Evaluation { get; set; }
    }
}
