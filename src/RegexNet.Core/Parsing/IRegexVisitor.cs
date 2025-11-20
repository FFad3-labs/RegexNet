namespace RegexNet.Core.Parsing;

public interface IRegexVisitor<out T>
{
    T VisitLiteral(LiteralNode literalNode);
    T VisitConcatenation(ConcatenationNode concatenationNode);
    T VisitAlternation(AlterationNode alterationNode);
    T VisitQuantifier(QuantifierNode quantifierNode);
}