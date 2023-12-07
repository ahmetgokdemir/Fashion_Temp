namespace Project.Services.Category.OptionPatternSettings
{
    public interface IOptionSettings
    {        
        // appsetting.json
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
