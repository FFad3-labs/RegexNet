using RegexNet.Core.Lexing;

namespace RegexNet.Tests;

public class LexerTests
{
    public static TheoryData<string, Token[]> TokenizeCases => new()
    {
        {
            "a",
            [
                Token.Literal(0),
                Token.EndOfFile(1),
            ]
        },
        {
            "a*",
            [
                Token.Literal(0),
                Token.Star(1),
                Token.EndOfFile(2),
            ]
        }
    };

    [Theory]
    [MemberData(nameof(TokenizeCases))]
    public void Tokenize_Returns_ExpectedTokens(string input, Token[] expected)
    {
        var tokens = Lexer.Tokenize(input);
        Assert.Equal(expected, tokens);
    }
}