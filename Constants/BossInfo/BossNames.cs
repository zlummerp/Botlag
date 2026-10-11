namespace Botlag.Constants.BossInfo
{
    public class BossNames
    {
        /*
         * Add new boss checklist:
         * Add boss to BossNames
         * Add boss to CheckBossNameText
         * Add boss + items to ItemLists
         * Add boss + items to ItemLists.SearchForBossItem
         * Add boss + items to ItemLists.CheckAllItems
         */
        public const string Cox = "CoX";

        public const string Tob = "ToB";

        public const string Toa = "ToA";

        public const string CG = "CG";

        public const string Global = "Global";

        public static string CheckBossNameText(string text)
        {
            if (text.Equals(Cox, StringComparison.OrdinalIgnoreCase))
            {
                return Cox;
            }
            if (text.Equals(Tob, StringComparison.OrdinalIgnoreCase))
            {
                return Tob;
            }
            if (text.Equals(Toa, StringComparison.OrdinalIgnoreCase))
            {
                return Toa;
            }
            if (text.Equals(CG, StringComparison.OrdinalIgnoreCase))
            {
                return CG;
            }
            if (text.Equals(Global, StringComparison.OrdinalIgnoreCase))
            {
                return Global;
            }
            return string.Empty;
        }
    }
}
