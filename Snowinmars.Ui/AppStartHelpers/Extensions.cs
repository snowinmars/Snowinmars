namespace Snowinmars.Ui.AppStartHelpers
{
    internal static class Extensions
    {
        internal static bool NeedToBeTrimed(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return false;
            }

            return str.StartsWith(" ") || str.EndsWith(" ");
        }
    }
}