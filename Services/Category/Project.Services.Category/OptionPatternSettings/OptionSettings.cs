﻿namespace Project.Services.Category.OptionPatternSettings
{
    public class OptionSettings : IOptionSettings
    {
        public string ProductCollectionName { get; set; }
        public string CategoryCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
