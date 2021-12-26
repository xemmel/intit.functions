namespace intitFunctions;

public interface IRegexHandler
{
    IEnumerable<RegexMatchModel> GetMatches(string input, string pattern);
}
