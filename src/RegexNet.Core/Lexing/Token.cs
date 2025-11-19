namespace RegexNet.Core.Lexing;

public readonly record struct Token(TokenType Type, int Start, int Lenght)
{
    public static Token Literal(int position, int lenght = 1) => new(TokenType.Literal, position, lenght);
    public static Token Star(int position) => new(TokenType.Star, position, 1);
    public static Token EndOfFile(int position) => new(TokenType.EndOfInput, position, 0);
}