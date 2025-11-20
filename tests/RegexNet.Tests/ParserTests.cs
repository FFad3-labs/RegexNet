using RegexNet.Core.Lexing;
using RegexNet.Core.Parsing;

namespace RegexNet.Tests;

public class ParserTests
{
    public static TheoryData<Token[], RegexNode> TokenizeCases => new()
    {
        {
            [
                Token.Literal(0, 1, 'a'),
                Token.Star(1),
                Token.Literal(2, 1, 'b'),
                Token.QuestionMark(3),
                Token.EndOfFile(4),
            ],
            new ConcatenationNode(
                [
                    QuantifierNode.StarQuantifier(new LiteralNode('a')),
                    QuantifierNode.QuestionMarkQuantifier(new LiteralNode('b')),
                ]
            )
        }
    };

    [Theory]
    [MemberData(nameof(TokenizeCases))]
    public void Parse_Returns_ExpectedRegexNodes(Token[] tokens, RegexNode expected)
    {
        RegexNode actual = Parser.Parse(tokens);
        Assert.Equal(expected, actual);
    }
}