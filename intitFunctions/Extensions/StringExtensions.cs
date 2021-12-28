namespace intitFunctions;

public static class StringExtensions
{
    public static Stream AsStream(this string input, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return new MemoryStream(encoding.GetBytes(input));
    }
}