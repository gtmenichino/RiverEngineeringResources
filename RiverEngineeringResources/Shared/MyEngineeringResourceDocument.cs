using System.Text.Json.Serialization;

namespace RiverEngineeringResources.Shared
{
    public class MyEngineeringResourceDocument : MyEngineeringResource
    {
        public MyEngineeringResourceDocument()
        {
            Type = "document";
        }

        public void ComputeTags()
        {
            List<string> tagsList = new List<string>();

            if (!string.IsNullOrEmpty(KB_Hydrology) && KB_Hydrology != "-")
                tagsList.Add("Documents_Knowledge Base_Hydrology");

            if (!string.IsNullOrEmpty(KB_Hydraulics) && KB_Hydraulics != "-")
                tagsList.Add("Documents_Knowledge Base_Hydraulics");

            if (!string.IsNullOrEmpty(KB_Geomorphology) && KB_Geomorphology != "-")
                tagsList.Add("Documents_Knowledge Base_Geomorphology");

            if (!string.IsNullOrEmpty(KB_Physicochemical) && KB_Physicochemical != "-")
                tagsList.Add("Documents_Knowledge Base_Physicochemical");

            if (!string.IsNullOrEmpty(KB_Biological_Ecology) && KB_Biological_Ecology != "-")
                tagsList.Add("Documents_Knowledge Base_Biological Ecology");

            if (!string.IsNullOrEmpty(KB_Social) && KB_Social != "-")
                tagsList.Add("Documents_Knowledge Base_Social");

            if (!string.IsNullOrEmpty(E_Hydrology) && E_Hydrology != "-")
                tagsList.Add("Documents_Evaluation_Hydrology");

            if (!string.IsNullOrEmpty(E_Hydraulics) && E_Hydraulics != "-")
                tagsList.Add("Documents_Evaluation_Hydraulics");

            if (!string.IsNullOrEmpty(E_Sediment_Transport) && E_Sediment_Transport != "-")
                tagsList.Add("Documents_Evaluation_Sediment Transport");

            if (!string.IsNullOrEmpty(E_Geomorphology) && E_Geomorphology != "-")
                tagsList.Add("Documents_Evaluation_Geomorphology");

            if (!string.IsNullOrEmpty(E_Physicochemical) && E_Physicochemical != "-")
                tagsList.Add("Documents_Evaluation_Physicochemical");

            if (!string.IsNullOrEmpty(E_Biological_Ecology) && E_Biological_Ecology != "-")
                tagsList.Add("Documents_Evaluation_Biological Ecology");

            if (!string.IsNullOrEmpty(DT_Hydraulic_Geometry) && DT_Hydraulic_Geometry != "-")
                tagsList.Add("Documents_Design Theory_Hydraulic Geometry");

            if (!string.IsNullOrEmpty(DT_Natural_Channel_Design) && DT_Natural_Channel_Design != "-")
                tagsList.Add("Documents_Design Theory_Natural Channel Design");

            if (!string.IsNullOrEmpty(DT_Stable_ch_design_and_Stabilization) && DT_Stable_ch_design_and_Stabilization != "-")
                tagsList.Add("Documents_Design Theory_Stable ch design and Stabilization");

            if (!string.IsNullOrEmpty(DT_Threshold_channel_design) && DT_Threshold_channel_design != "-")
                tagsList.Add("Documents_Design Theory_Threshold channel design");

            if (!string.IsNullOrEmpty(DT_Alluvial_Channel_Design) && DT_Alluvial_Channel_Design != "-")
                tagsList.Add("Documents_Design Theory_Alluvial Channel Design");

            if (!string.IsNullOrEmpty(DT_Analytical_Design) && DT_Analytical_Design != "-")
                tagsList.Add("Documents_Design Theory_Analytical Design");

            if (!string.IsNullOrEmpty(AG_Aesthetics_Recreation_Education) && AG_Aesthetics_Recreation_Education != "-")
                tagsList.Add("Documents_Actions and Goals_Aesthetics Recreation Education");

            if (!string.IsNullOrEmpty(AG_Bank_Stabilization) && AG_Bank_Stabilization != "-")
                tagsList.Add("Documents_Actions and Goals_Bank Stabilization");

            if (!string.IsNullOrEmpty(AG_Channel_Reconfiguration) && AG_Channel_Reconfiguration != "-")
                tagsList.Add("Documents_Actions and Goals_Channel Reconfiguration");

            if (!string.IsNullOrEmpty(AG_Dam_removal_retrofit) && AG_Dam_removal_retrofit != "-")
                tagsList.Add("Documents_Actions and Goals_Dam removal retrofit");

            if (!string.IsNullOrEmpty(AG_Aquatic_organism_passage) && AG_Aquatic_organism_passage != "-")
                tagsList.Add("Documents_Actions and Goals_Aquatic organism passage");

            if (!string.IsNullOrEmpty(AG_Floodplain_Reconnection) && AG_Floodplain_Reconnection != "-")
                tagsList.Add("Documents_Actions and Goals_Floodplain Reconnection");

            if (!string.IsNullOrEmpty(AG_Flow_Modification) && AG_Flow_Modification != "-")
                tagsList.Add("Documents_Actions and Goals_Flow Modification");

            if (!string.IsNullOrEmpty(AG_Instream_Habitat_Improvement) && AG_Instream_Habitat_Improvement != "-")
                tagsList.Add("Documents_Actions and Goals_Instream Habitat Improvement");

            if (!string.IsNullOrEmpty(AG_Instream_Species_Management) && AG_Instream_Species_Management != "-")
                tagsList.Add("Documents_Actions and Goals_Instream Species Management");

            if (!string.IsNullOrEmpty(AG_Land_Acquisition) && AG_Land_Acquisition != "-")
                tagsList.Add("Documents_Actions and Goals_Land Acquisition");

            if (!string.IsNullOrEmpty(AG_Riparian_Management) && AG_Riparian_Management != "-")
                tagsList.Add("Documents_Actions and Goals_Riparian Management");

            if (!string.IsNullOrEmpty(AG_Stormwater_Management) && AG_Stormwater_Management != "-")
                tagsList.Add("Documents_Actions and Goals_Stormwater Management");

            if (!string.IsNullOrEmpty(AG_Water_Quality_Management) && AG_Water_Quality_Management != "-")
                tagsList.Add("Documents_Actions and Goals_Water Quality Management");

            if (!string.IsNullOrEmpty(AG_Flood_Risk_Management) && AG_Flood_Risk_Management != "-")
                tagsList.Add("Documents_Actions and Goals_Flood Risk Management");

            if (!string.IsNullOrEmpty(AG_Navigation_Channels) && AG_Navigation_Channels != "-")
                tagsList.Add("Documents_Actions and Goals_Navigation Channels");

            if (!string.IsNullOrEmpty(AG_Large_wood) && AG_Large_wood != "-")
                tagsList.Add("Documents_Actions and Goals_Large wood");

            if (!string.IsNullOrEmpty(AG_Grade_control) && AG_Grade_control != "-")
                tagsList.Add("Documents_Actions and Goals_Grade control");

            if (!string.IsNullOrEmpty(AG_River_training) && AG_River_training != "-")
                tagsList.Add("Documents_Actions and Goals_River training");

            if (!string.IsNullOrEmpty(AG_Reservoir_Operation) && AG_Reservoir_Operation != "-")
                tagsList.Add("Documents_Actions and Goals_Reservoir Operation");

            if (!string.IsNullOrEmpty(AG_Beaver_Dam_Analog) && AG_Beaver_Dam_Analog != "-")
                tagsList.Add("Documents_Actions and Goals_Beaver Dam Analog");

            if (!string.IsNullOrEmpty(AG_Stage_0_Condition) && AG_Stage_0_Condition != "-")
                tagsList.Add("Documents_Actions and Goals_Stage 0 Condition");

            if (!string.IsNullOrEmpty(AG_Legacy_Sed_Removal_Valley_Rest) && AG_Legacy_Sed_Removal_Valley_Rest != "-")
                tagsList.Add("Documents_Actions and Goals_Legacy Sed Removal Valley Rest");

            if (!string.IsNullOrEmpty(AG_Regenerative_Stormwater_Conv) && AG_Regenerative_Stormwater_Conv != "-")
                tagsList.Add("Documents_Actions and Goals_Regenerative Stormwater Conv");

            if (!string.IsNullOrEmpty(AG_In_stream_Structures) && AG_In_stream_Structures != "-")
                tagsList.Add("Documents_Actions and Goals_In stream Structures");

            Tags = string.Join(",", tagsList);
        }

        //public string? KB_Hydrology { get; set; }
        //public string? KB_Hydraulics { get; set; }
        //public string? KB_Geomorphology { get; set; }
        //public string? KB_Physicochemical { get; set; }
        //public string? KB_Biological_Ecology { get; set; }
        //public string? KB_Social { get; set; }
        //public string? E_Hydrology { get; set; }
        //public string? E_Hydraulics { get; set; }
        //public string? E_Sediment_Transport { get; set; }
        //public string? E_Geomorphology { get; set; }
        //public string? E_Physicochemical { get; set; }
        //public string? E_Biological_Ecology { get; set; }
        //public string? DT_Hydraulic_Geometry { get; set; }
        //public string? DT_Natural_Channel_Design { get; set; }
        //public string? DT_Stable_ch_design_and_Stabilization { get; set; }
        //public string? DT_Threshold_channel_design { get; set; }
        //public string? DT_Alluvial_Channel_Design { get; set; }
        //public string? DT_Analytical_Design { get; set; }
        //public string? AG_Aesthetics_Recreation_Education { get; set; }
        //public string? AG_Bank_Stabilization { get; set; }
        //public string? AG_Channel_Reconfiguration { get; set; }
        //public string? AG_Dam_removal_retrofit { get; set; }
        //public string? AG_Aquatic_organism_passage { get; set; }
        //public string? AG_Floodplain_Reconnection { get; set; }
        //public string? AG_Flow_Modification { get; set; }
        //public string? AG_Instream_Habitat_Improvement { get; set; }
        //public string? AG_Instream_Species_Management { get; set; }
        //public string? AG_Land_Acquisition { get; set; }
        //public string? AG_Riparian_Management { get; set; }
        //public string? AG_Stormwater_Management { get; set; }
        //public string? AG_Water_Quality_Management { get; set; }
        //public string? AG_Flood_Risk_Management { get; set; }
        //public string? AG_Navigation_Channels { get; set; }
        //public string? AG_Large_wood { get; set; }
        //public string? AG_Grade_control { get; set; }
        //public string? AG_River_training { get; set; }
        //public string? AG_Reservoir_Operation { get; set; }
        //public string? AG_Beaver_Dam_Analog { get; set; }
        //public string? AG_Stage_0_Condition { get; set; }
        //public string? AG_Legacy_Sed_Removal_Valley_Rest { get; set; }
        //public string? AG_Regenerative_Stormwater_Conv { get; set; }
        //public string? AG_In_stream_Structures { get; set; }

        [JsonPropertyName("KB_Hydrology")]
        public string? KB_Hydrology { get; set; }

        [JsonPropertyName("KB_Hydraulics")]
        public string? KB_Hydraulics { get; set; }

        [JsonPropertyName("KB_Geomorphology")]
        public string? KB_Geomorphology { get; set; }

        [JsonPropertyName("KB_Physicochemical")]
        public string? KB_Physicochemical { get; set; }

        [JsonPropertyName("KB_Biological Ecology")]
        public string? KB_Biological_Ecology { get; set; }

        [JsonPropertyName("KB_Social")]
        public string? KB_Social { get; set; }

        [JsonPropertyName("E_Hydrology")]
        public string? E_Hydrology { get; set; }

        [JsonPropertyName("E_Hydraulics")]
        public string? E_Hydraulics { get; set; }

        [JsonPropertyName("E_Sediment Transport")]
        public string? E_Sediment_Transport { get; set; }

        [JsonPropertyName("E_Geomorphology")]
        public string? E_Geomorphology { get; set; }

        [JsonPropertyName("E_Physicochemical")]
        public string? E_Physicochemical { get; set; }

        [JsonPropertyName("E_Biological Ecology")]
        public string? E_Biological_Ecology { get; set; }

        [JsonPropertyName("DT_Hydraulic Geometry")]
        public string? DT_Hydraulic_Geometry { get; set; }

        [JsonPropertyName("DT_Natural Channel Design")]
        public string? DT_Natural_Channel_Design { get; set; }

        [JsonPropertyName("DT_Stable ch design and Stabilization")]
        public string? DT_Stable_ch_design_and_Stabilization { get; set; }

        [JsonPropertyName("DT_Threshold channel design")]
        public string? DT_Threshold_channel_design { get; set; }

        [JsonPropertyName("DT_Alluvial Channel Design")]
        public string? DT_Alluvial_Channel_Design { get; set; }

        [JsonPropertyName("DT_Analytical Design")]
        public string? DT_Analytical_Design { get; set; }

        [JsonPropertyName("AG_Aesthetics Recreation Education")]
        public string? AG_Aesthetics_Recreation_Education { get; set; }

        [JsonPropertyName("AG_Bank Stabilization")]
        public string? AG_Bank_Stabilization { get; set; }

        [JsonPropertyName("AG_Channel Reconfiguration")]
        public string? AG_Channel_Reconfiguration { get; set; }

        [JsonPropertyName("AG_Dam removal retrofit")]
        public string? AG_Dam_removal_retrofit { get; set; }

        [JsonPropertyName("AG_Aquatic organism passage")]
        public string? AG_Aquatic_organism_passage { get; set; }

        [JsonPropertyName("AG_Floodplain Reconnection")]
        public string? AG_Floodplain_Reconnection { get; set; }

        [JsonPropertyName("AG_Flow Modification")]
        public string? AG_Flow_Modification { get; set; }

        [JsonPropertyName("AG_Instream Habitat Improvement")]
        public string? AG_Instream_Habitat_Improvement { get; set; }

        [JsonPropertyName("AG_Instream Species Management")]
        public string? AG_Instream_Species_Management { get; set; }

        [JsonPropertyName("AG_Land Acquisition")]
        public string? AG_Land_Acquisition { get; set; }

        [JsonPropertyName("AG_Riparian Management")]
        public string? AG_Riparian_Management { get; set; }

        [JsonPropertyName("AG_Stormwater Management")]
        public string? AG_Stormwater_Management { get; set; }

        [JsonPropertyName("AG_Water Quality Management")]
        public string? AG_Water_Quality_Management { get; set; }

        [JsonPropertyName("AG_Flood Risk Management")]
        public string? AG_Flood_Risk_Management { get; set; }

        [JsonPropertyName("AG_Navigation Channels")]
        public string? AG_Navigation_Channels { get; set; }

        [JsonPropertyName("AG_Large wood")]
        public string? AG_Large_wood { get; set; }

        [JsonPropertyName("AG_Grade control")]
        public string? AG_Grade_control { get; set; }

        [JsonPropertyName("AG_River training")]
        public string? AG_River_training { get; set; }

        [JsonPropertyName("AG_Reservoir Operation")]
        public string? AG_Reservoir_Operation { get; set; }

        [JsonPropertyName("AG_Beaver Dam Analog")]
        public string? AG_Beaver_Dam_Analog { get; set; }

        [JsonPropertyName("AG_Stage 0 Condition")]
        public string? AG_Stage_0_Condition { get; set; }

        [JsonPropertyName("AG_Legacy Sed Removal Valley Rest")]
        public string? AG_Legacy_Sed_Removal_Valley_Rest { get; set; }

        [JsonPropertyName("AG_Regenerative Stormwater Conv")]
        public string? AG_Regenerative_Stormwater_Conv { get; set; }

        [JsonPropertyName("AG_In stream Structures")]
        public string? AG_In_stream_Structures { get; set; }
    }
}
