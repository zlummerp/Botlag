namespace Botlag.Helpers
{
    public static class ComparisonHelpers
    {
        public static bool ListContainsCaseInsensitive(this IEnumerable<string> listOfStrings, string compareTo)
        {
            return listOfStrings.Any(x => x.Equals(compareTo, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
