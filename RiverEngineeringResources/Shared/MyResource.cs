namespace RiverEngineeringResources.Shared
{

    public class MyResource
    {
        //public string? ShortTitle { get { return ShortText(Title); } }
        //public string? ShortAuthor { get { return ShortText(Author); } }
        //public string? ShortType { get { return ShortText(Type); } }
        //public int? ShortYear { get { return ShortNumber(Year); } }
        //public string? ShortWebsite { get { return ShortText(Website, 20); } }
        //public string? ShortCitation { get { return ShortText(Citation, 20); } }



        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? File_Format { get; set; }
        public string? APA_Citation { get; set; }
        public string? Link { get; set; }
        public int? Year { get; set; }
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


        public List<affiliation> affiliations = new List<affiliation>();

    }

    public class affiliation
    {
        public string databaseName;
        public string classification;
        public string category;
        public string subcategory;

        public affiliation(string dbName)
        {
            databaseName = dbName;

            List<string> parts = databaseName.Split("_", StringSplitOptions.RemoveEmptyEntries).ToList();
            classification = parts.FirstOrDefault();
            category = parts[1];
            subcategory = parts.LastOrDefault();
        }
    }



}
