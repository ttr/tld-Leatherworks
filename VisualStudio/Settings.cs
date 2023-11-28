using ModSettings;

namespace Leatherworks
{
    internal class Settings : JsonModSettings
    {
        internal static Settings instance = new Settings();


        [Section("Spawn Chances")]

        [Name("Treebark spawn chance")]
        [Description("Choose How Often Treebark Should Spawn. By default 12%")]
        [Slider(1f, 100f, 12, NumberFormat = "{0:#}")]
        public float treebarkChance = 12f;

        [Section("Leather Yield")]

        [Name("Rabbit leather yield")]
        [Description("Choose How Much Scraped Leather You Get From A Rabbit Pelt. By default 1")]
        [Slider(1, 5, 1, NumberFormat = "{0:#}")]
        public float rabbitYield = 1;

        [Name("Deer and wolf leather yield")]
        [Description("Choose How Much Scraped Leather You Get From A Deer Hide and Wolf Pelt. By default 3")]
        [Slider(1, 5, 3, NumberFormat = "{0:#}")]
        public float wolfdeerYield = 3;

        [Name("Bear and moose leather yield")]
        [Description("Choose How Much Scraped Leather You Get From A Moose and Bear Hide. By default 5")]
        [Slider(1, 5, 5, NumberFormat = "{0:#}")]
        public float moosebearYield = 5;

        [Section("Preferences")]

        [Name("Able to Scrape Fresh Hide?")]
        [Description("Enable The Ability To Scrape Fresh Hide (Makes It A Bit Harder). By Default True")]
        public bool noCured = true;

        [Name("Able to Use Stone To Grind Flour?")]
        [Description("Enable The Ability To Grind Flour Using A Stone. By Default True")]
        public bool noGrind = true;

        //  [Name("Bark amount for making flour")]
        //  [Description("Choose How Much Fried Tree Bark Is Needed For Making Flour. By default 35")]
        //  [Slider(25f, 60f, 351, NumberFormat = "{0:#}")]
        //  public float flourAmount = 35f;
    }
}
