using System.Collections.Immutable;
using RegexNet.Core.Lexing;

namespace RegexNet.Core.Parsing;

public sealed class Parser
{
    private int _position = 0;
    private readonly IReadOnlyList<Token> _tokens;

    public Parser(IReadOnlyList<Token> tokens)
        => _tokens = tokens;

    public static RegexNode Parse(IReadOnlyList<Token> tokens)
        => new Parser(tokens).Parse();

    private Token Current => _tokens[_position];

    private Token Consume(TokenType type)
    {
        if (Current.Type != type)
            throw new InvalidOperationException("Unexpected token type");

        return Current.Type == TokenType.EndOfInput ? Current : _tokens[_position++];
    }

    private RegexNode Parse()
        => ParseAlteration();

    private RegexNode ParseAlteration()
    {
        RegexNode left = ParseConcatenation();

        while (Current.Type == TokenType.Alter)
        {
            Consume(TokenType.Alter);
            var right = ParseConcatenation();
            left = new AlterationNode(left, right);
        }

        return left;
    }

    private RegexNode ParseConcatenation()
    {
        var nodes = new List<RegexNode>();

        while (Current.Type == TokenType.Literal)
            nodes.Add(ParseRepetition());

        return nodes.Count == 1 ? nodes[0] : new ConcatenationNode([..nodes]);
    }

    private RegexNode ParseRepetition()
    {
        var node = ParsePrimary();

        return Current.Type switch
        {
            TokenType.Star => ParseQuantifier(node, QuantifierType.ZeroOrMore),
            TokenType.Plus => ParseQuantifier(node, QuantifierType.OneOrMore),
            TokenType.QuestionMark => ParseQuantifier(node, QuantifierType.ZeroOrOne),
            _ => node
        };
    }

    private RegexNode ParseQuantifier(RegexNode inner, QuantifierType quantifierType)
    {
        Consume(Current.Type);

        return quantifierType switch
        {
            QuantifierType.ZeroOrMore => QuantifierNode.StarQuantifier(inner),
            QuantifierType.ZeroOrOne => QuantifierNode.QuestionMarkQuantifier(inner),
            QuantifierType.OneOrMore => QuantifierNode.PlusQuantifier(inner),
            _ => throw new ArgumentOutOfRangeException(nameof(quantifierType), quantifierType, null)
        };
    }

    private RegexNode ParsePrimary()
        => ParseLiteral();

    private RegexNode ParseLiteral() => new LiteralNode(Consume(TokenType.Literal).Value!.Value);
}