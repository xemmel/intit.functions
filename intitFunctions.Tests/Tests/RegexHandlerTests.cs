namespace intitFunctions.Tests;

public class RegexHandlerTests
{
    private readonly IRegexHandler _regexHandler;

    public RegexHandlerTests()
    {
        _regexHandler = DITestFactory.GetService<IRegexHandler>();
    }

    [Theory]
    [InlineData("Morten la Cour\r\n Clara la Cour","la",3,"la",7)]
    [InlineData("Morten la Cour\r\n Clara la Cour","(?<= )la",2,"la",7)]
    [InlineData(@"
        <root>
            <id>18</id>
            <name>Morten</name>
            <id>19</id>
        </root>
        ","(?<=<id>).*?(?=</id>)",2,"18",34)]

    //[InlineData("Morten la Cour\r\n Clara la Cour","kurt",0)]

    public void GetMatches(
            string input, 
            string pattern, 
            int expectedCount, 
            string? firstMatch = null,
            int? firstPos = null)
    {
        var result = _regexHandler
                        .GetMatches(input: input,pattern: pattern)
                        .ToArray();
        Assert.NotNull(result);
        Assert.Equal(expectedCount,result.Count());
        if (firstMatch != null)
        {
            Assert.Equal(firstMatch,result.First().Value);
            Assert.Equal(firstPos,result.First().StartPos);
        }
    }
}