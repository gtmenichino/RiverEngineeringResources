using System.Text.RegularExpressions;

namespace RiverEngineeringResources.Shared
{

    public class MyAssessmentResource
    {
        //public string? ShortTitle { get { return ShortText(Title); } }
        //public string? ShortAuthor { get { return ShortText(Author); } }
        //public string? ShortType { get { return ShortText(Type); } }
        //public int? ShortYear { get { return ShortNumber(Year); } }
        //public string? ShortWebsite { get { return ShortText(Website, 20); } }
        //public string? ShortCitation { get { return ShortText(Citation, 20); } }



        //public string? Title { get; set; }
        //public string? Author { get; set; }
        //public string? File_Format { get; set; }
        //public string? APA_Citation { get; set; }
        //public string? Link { get; set; }
        //public int? Year { get; set; }
        //public string? Tags { get; set; }
        //public string? Link_Owner { get; set; }
        //public string? Association { get; set; } = "";
        //public string? Description { get; set; }
        public string? NameClip
        {
            get
            {
                if (String.IsNullOrEmpty(Resource_Name))
                {
                    return "";
                }
                if (Resource_Name.Length > 20)
                {
                    string pattern = @"\(([^(){}]*)\)";
                    Regex regex = new Regex(pattern);
                    MatchCollection matches = regex.Matches(Resource_Name);
                    string snip = "";
                    foreach (Match match in matches)
                    {
                        // The actual content within the parentheses will be in match.Groups[1]
                        snip = match.Groups[1].Value;
                    }
                    
                    return Resource_Name.Substring(0, 20) + "..." + snip;
                }
                else
                {
                    return Resource_Name;
                }
            }
        }
        public string? DescriptionClip
        {
            get
            {
                if (String.IsNullOrEmpty(Introductory_Description))
                {
                    return "";
                }
                if (Introductory_Description.Length > 170)
                {
                    return Introductory_Description.Substring(0, 170) + "...";
                }
                else
                {
                    return Introductory_Description;
                }
            }
        }
        public int? TagsCount { get; set; }
        public string? LastTags { get; set; }




        public int? Catalog_Number { get; set; }
        public int? Match_Ranking { get; set; }
        public string? Resource_Name { get; set; }
        public string? Abbreviated_Citation { get; set; }
        public string? Perceived_Impact { get; set; }
        public string? Developer_Sector { get; set; }
        public string? Stream_Type_Requirements { get; set; }
        public string? Geographic_Applicability { get; set; }
        public string? Adaptable_To_Other_Locations { get; set; }
        public string? Agency_Or_Organization { get; set; }
        public int? Year_Of_Publication_Or_Latest_Update { get; set; }
        public string? Assessment_Class { get; set; }
        public string? Hydrology { get; set; }
        public string? Hydraulics { get; set; }
        public string? Geomorphology { get; set; }
        public string? Physicochemical { get; set; }
        public string? Biology { get; set; }
        public string? Biology_And_Stream_Habitat { get; set; }
        public string? Connectivity_Aquatic_Organism_Passage { get; set; }
        public string? Stream_Habitat { get; set; }
        public string? Riparian_Zone { get; set; }
        public string? Variability { get; set; }
        public string? Stream_Classification { get; set; }
        public string? Time_Required_Tasks_Only { get; set; }
        public string? Training_Needed { get; set; }
        public string? Location_Of_Assessment { get; set; }
        public string? Tier_Of_Analysis { get; set; }
        public string? Input_Data_Type { get; set; }
        public string? Output_Data_Type { get; set; }
        public string? Data_Availability { get; set; }
        public string? Reference_Required { get; set; }
        public string? Performance_Standards_or_Reference_Curves_Used { get; set; }
        public string? Programmatic_Applications { get; set; }
        public string? Region_of_development_application { get; set; }
        public string? State_of_development_application { get; set; }
        public string? USACE_Division_Use { get; set; }
        public string? USACE_District_Use { get; set; }
        public string? URL_For_Tool_Or_Data_Storage { get; set; }
        public string? URL_For_User_Guide { get; set; }
        public string? Notes { get; set; }
        public string? Long_Citation { get; set; }
        public string? Issues { get; set; }
        public string? Introductory_Description { get; set; }





        public List<AssessmentAffiliation> affiliations = new List<AssessmentAffiliation>();

    }

    public class AssessmentAffiliation
    {
        public string databaseName;
        public string classification;
        public string category;
        public string subcategory;

        public AssessmentAffiliation(string dbName)
        {
            databaseName = dbName;

            List<string> parts = databaseName.Split("_", StringSplitOptions.RemoveEmptyEntries).ToList();
            classification = parts.FirstOrDefault();
            category = parts[1];
            subcategory = parts.LastOrDefault();
        }
    }



}
