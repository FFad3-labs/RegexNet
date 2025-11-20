namespace RegexNet.Core.Lexing;

public readonly record struct Token(TokenType Type, int Start, int Lenght, char? Value = null)
{
    public static Token Literal(int position, int lenght, char value) =>
        new(TokenType.Literal, position, lenght, value);

    public static Token Star(int position) => new(TokenType.Star, position, 1);
    public static Token Plus(int position) => new(TokenType.Plus, position, 1);
    public static Token QuestionMark(int position) => new(TokenType.QuestionMark, position, 1);
    public static Token Alter(int position) => new(TokenType.Alter, position, 1);

    public static Token EndOfFile(int position) => new(TokenType.EndOfInput, position, 0);
}