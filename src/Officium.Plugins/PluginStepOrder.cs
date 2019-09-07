namespace Officium.Plugins
{
    public enum PluginStepOrder
    {
        AlwaysFirst      = -1999,
        BeforeAll       = 0,

        BeforeGet       = 110,
        BeforePost      = 111,
        BeforePut       = 112,
        BeforeDelete    = 113,

        OnGet           = 210,
        OnPost          = 211,
        OnPut           = 212,
        OnDelete        = 213,

        AfterGet        = 310,
        AfterPost       = 311,
        AfterPut        = 312,
        AfterDelete     = 313,
      
        AfterAll        = 999,
        AlwaysLast      = 1999
    }
}

