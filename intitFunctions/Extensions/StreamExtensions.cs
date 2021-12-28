namespace intitFunctions;

public static class StreamExtesions
{
    public static Task<string> AsStringAsync(this Stream stream, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        StreamReader streamReader = new StreamReader(stream);
        return streamReader.ReadToEndAsync();
    }
}