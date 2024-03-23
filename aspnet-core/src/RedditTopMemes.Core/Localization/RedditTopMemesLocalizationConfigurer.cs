using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace RedditTopMemes.Localization
{
    public static class RedditTopMemesLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(RedditTopMemesConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(RedditTopMemesLocalizationConfigurer).GetAssembly(),
                        "RedditTopMemes.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
