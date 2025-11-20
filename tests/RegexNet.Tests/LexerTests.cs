using RegexNet.Core.Lexing;

namespace RegexNet.Tests;

public class LexerTests
{
    public static TheoryData<string, Token[]> TokenizeCases => new()
    {
        {
            "a?b*c+",
            [
                Token.Literal(0,1,'a'),
                Token.QuestionMark(1),
                Token.Literal(2,1,'b'),
                Token.Star(3),
                Token.Literal(4,1,'c'),
                Token.Plus(5),
                Token.EndOfFile(6),
            ]
        },
    };

    [Theory]
    [MemberData(nameof(TokenizeCases))]
    public void Tokenize_Returns_ExpectedTokens(string input, Token[] expected)
    {
        var tokens = Lexer.Tokenize(input);
        Assert.Equal(expected, tokens);
    }
}