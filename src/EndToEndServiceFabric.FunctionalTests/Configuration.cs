namespace EndToEndServiceFabric.FunctionalTests
{
    public static class Configuration
    {
        public static string Current
        {
            get
            {
#if DEBUG
                var mode = "Debug";
#else
                var mode = "Release";
#endif
                return mode;
            }
        }
    }
}