namespace RiverEngineeringResources.Shared
{

    public class MyEngineeringResource
    {
        //public string? ShortTitle { get { return ShortText(Title); } }
        //public string? ShortAuthor { get { return ShortText(Author); } }
        //public string? ShortType { get { return ShortText(Type); } }
        //public int? ShortYear { get { return ShortNumber(Year); } }
        //public string? ShortWebsite { get { return ShortText(Website, 20); } }
        //public string? ShortCitation { get { return ShortText(Citation, 20); } }
        public int? Year_Int
        {
            get
            {
                if (string.IsNullOrEmpty(Year))
                {
                    return 0;
                }

                if (int.TryParse(Year, out var result))
                {
                    return result;
                }

                // If the parsing fails, return 0
                return 0;
            }
        }

        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? File_Format { get; set; }
        public string? APA_Citation { get; set; }
        public string? Link { get; set; }
        public string? Year { get; set; }
        public string? Tags { get; set; }
        public string? Link_Owner { get; set; }
        public string? Association { get; set; } = "";
        public string? Description { get; set; }
        public string? DescriptionClip
        {
            get
            {
                if (String.IsNullOrEmpty(Description))
                {
                    return "";
                }
                if (Description.Length > 170)
                {
                    return Description.Substring(0, 170) + "...";
                }
                else
                {
                    return Description;
                }
            }
        }
        public int? TagsCount { get; set; }
        public string? LastTags { get; set; }


        public List<EngineeringAffiliation> affiliations = new List<EngineeringAffiliation>();


        // Method to call ComputeTags in the inherited class
        public void CallComputeTags()
        {
            if (Type == "document" && this is MyEngineeringResourceDocument documentResource)
            {
                documentResource.ComputeTags();
            }
            else if (Type == "dataTool" && this is MyEngineeringResourceDataTool dataToolResource)
            {
                dataToolResource.ComputeTags();
            }
            else
            {
                throw new InvalidOperationException("ComputeTags method not available in this class.");
            }
        }

    }

    public class EngineeringAffiliation
    {
        public string databaseName;
        public string classification;
        public string category;
        public string subcategory;

        public EngineeringAffiliation(string dbName)
        {
            databaseName = dbName;

            List<string> parts = databaseName.Split("_", StringSplitOptions.RemoveEmptyEntries).ToList();
            classification = parts.FirstOrDefault();
            category = parts[1];
            subcategory = parts.LastOrDefault();
        }
    }



}
