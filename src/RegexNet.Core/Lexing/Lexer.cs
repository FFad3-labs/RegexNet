using System.Diagnostics;

namespace RegexNet.Core.Lexing;

public class Lexer
{
    private readonly string _input;
    private int _pos;
    private readonly List<Token> _tokens = new();

    public Lexer(string input)
    {
        _input = input;
        _pos = 0;
    }

    private bool EndOfInput => _pos >= _input.Length;

    public static IReadOnlyList<Token> Tokenize(string pattern)
    {
        var lexer = new Lexer(pattern);
        lexer.Tokenize();
        return lexer._tokens;
    }

    private void Tokenize()
    {
        while (!EndOfInput)
            ScanToken();

        _tokens.Add(Token.EndOfFile(_input.Length));
    }

    private void ScanToken()
    {
        var start = _pos;
        var c = Advance();

        var token = c switch
        {
            //'\\' => ReadEscape(start),
            '|' => Token.Alter(start),
            '*' => Token.Star(start),
            '+' => Token.Plus(start),
            '?' => Token.QuestionMark(start),
            _ => Token.Literal(start, 1, c)
        };

        _tokens.Add(token);
    }

    private Token ReadEscape(int start)
    {
        var c = Advance();

        var token = c switch
        {
            '\\' => Token.Literal(start, 2, '\\'),
            '(' => Token.Literal(start, 2, '('),
            ')' => Token.Literal(start, 2, ')'),
            _ => throw new Exception($"Unknown escape '\\{c}' at position {start}")
        };

        return token;
    }

    private char Advance()
    {
        if (EndOfInput)
            throw new Exception("Unexpected end of pattern.");

        return _input[_pos++];
    }
}