using Verse;

namespace Bastyon
{
    [StaticConstructorOnStartup]
    public static class BastyonMain
    {
        static BastyonMain()
        {
            Log.Message("<color=white>[</color>" + "<color=#4494E3FF>Steve</color>" + "<color=white>]</color>" + 
                "<color=white>[</color>" + "<color=#4494E3FF>Steve's Animals</color>" + "<color=white>]</color>" + 
                "<color=#4494E3FF>Thank you for giving this mod a shot, have fun!</color>");
        }
    }
}
