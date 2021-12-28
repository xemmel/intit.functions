namespace intitFunctions.Tests;

public class DummyStreamTests
{

    [Theory]
    [InlineData("Morten la Cour", "Norten la Cour")]
    public async Task ReadStreamAsync(string input, string expected)
    {
        var stream = input.AsStream();
        var dummyStream = new DummyStream(stream);
        var result = await dummyStream.AsStringAsync();
        Assert.Equal(expected,result);
    }
}