namespace intitFunctions;

public class RegexHandler : IRegexHandler
{

    public IEnumerable<RegexMatchModel> GetMatches(string input, string pattern)
    {
        var matches = Regex.Matches(input: input, pattern: pattern);
        foreach(Match match in matches)
        {
            var regexMatch = new RegexMatchModel
            {
                Value = match.Value,
                StartPos = match.Index
                
            };
            yield return regexMatch;
        }
    }
}