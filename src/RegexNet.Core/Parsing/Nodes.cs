namespace RegexNet.Core.Parsing;

public abstract record RegexNode
{
    public abstract T Accept<T>(IRegexVisitor<T> visitor);
};

public record LiteralNode(char Value) : RegexNode
{
    public override T Accept<T>(IRegexVisitor<T> visitor)
        => visitor.VisitLiteral(this);
}

public record ConcatenationNode(IReadOnlyCollection<RegexNode> Parts) : RegexNode
{
    public override T Accept<T>(IRegexVisitor<T> visitor)
        => visitor.VisitConcatenation(this);

    public virtual bool Equals(ConcatenationNode? other)
    {
        // Need to override default comparison bcs of the Collections Equality - by Ref
        if (other is null)
            return false;

        return ReferenceEquals(this, other) || Parts.SequenceEqual(other.Parts);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();

        foreach (var part in Parts)
            hash.Add(part);

        return hash.ToHashCode();
    }
}

public record AlterationNode(RegexNode Left, RegexNode Right) : RegexNode
{
    public override T Accept<T>(IRegexVisitor<T> visitor)
        => visitor.VisitAlternation(this);
}

public record QuantifierNode(RegexNode Inner, QuantifierType Type) : RegexNode
{
    public static QuantifierNode StarQuantifier(RegexNode inner) =>
        new QuantifierNode(inner, QuantifierType.ZeroOrMore);

    public static QuantifierNode PlusQuantifier(RegexNode inner) =>
        new QuantifierNode(inner, QuantifierType.OneOrMore);

    public static QuantifierNode QuestionMarkQuantifier(RegexNode inner) =>
        new QuantifierNode(inner, QuantifierType.ZeroOrOne);

    public override T Accept<T>(IRegexVisitor<T> visitor)
        => visitor.VisitQuantifier(this);
}

public enum QuantifierType
{
    ZeroOrMore, // *
    OneOrMore, // +
    ZeroOrOne, // ?
}