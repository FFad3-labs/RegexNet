using System.Globalization;
using System.Text;

namespace RegexNet.Core.Parsing.Visitors;

public class PrettyPrinter : IRegexVisitor<StringBuilder>
{
    private readonly StringBuilder _sb = new();
    private readonly Stack<string> _indents = new();
    private bool _isLast = true;

    private string Indent => string.Concat(_indents.Reverse());
    
    public static void PrintToConsole(RegexNode root)
     => Console.WriteLine(root.Accept(new PrettyPrinter()));

    public StringBuilder VisitLiteral(LiteralNode literalNode)
    {
        AppendNodeName(literalNode);
        return _sb;
    }

    public StringBuilder VisitConcatenation(ConcatenationNode concatenationNode)
    {
        AppendNodeName(concatenationNode);
        AppendChildren(concatenationNode.Parts.ToArray());
        return _sb;
    }

    public StringBuilder VisitAlternation(AlterationNode alterationNode)
    {
        AppendNodeName(alterationNode);
        AppendChildren(alterationNode.Left, alterationNode.Right);
        return _sb;
    }

    public StringBuilder VisitQuantifier(QuantifierNode quantifierNode)
    {
        AppendNodeName(quantifierNode);
        AppendChildren(quantifierNode.Inner);
        return _sb;
    }

    private void AppendChildren(params RegexNode[] children)
    {
        _indents.Push(_isLast ? "    " : "│   ");
        var isLast = _isLast;

        for (int i = 0; i < children.Length; i++)
        {
            _isLast = i == children.Length - 1;
            children[i].Accept(this);
        }

        _indents.Pop();
        _isLast = isLast;
    }

    private void AppendNodeName(RegexNode node)
    {
        _sb.Append(Indent);
        _sb.Append(_isLast ? "└── " : "├── ");
        _sb.AppendLine(GetLabel(node));
    }

    private static string GetLabel(RegexNode regexNode)
        => regexNode switch
        {
            AlterationNode _ => "alteration",
            ConcatenationNode concatenationNode => $"concatenation: {concatenationNode.Parts.Count}",
            LiteralNode literalNode => $"literal: '{literalNode.Value}'",
            QuantifierNode quantifierNode => quantifierNode.Type switch
            {
                QuantifierType.ZeroOrMore => "zeroOrMore",
                QuantifierType.OneOrMore => "OneOrMore",
                QuantifierType.ZeroOrOne => "ZeroOrOne",
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new ArgumentOutOfRangeException(nameof(regexNode))
        };
}