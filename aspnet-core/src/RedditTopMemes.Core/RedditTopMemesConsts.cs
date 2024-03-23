using RedditTopMemes.Debugging;

namespace RedditTopMemes
{
    public class RedditTopMemesConsts
    {
        public const string LocalizationSourceName = "RedditTopMemes";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "673ede840b474c259a945c890b8e5531";
    }
}
