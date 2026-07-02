using ShipyardExpansion.Scripts;

namespace HeadBonker
{
    internal class LadderCheck
    {
        public static bool CheckState()
        {
            return Plugin.SEinstalled && NANDLadder.animating;
        }
    }
}
