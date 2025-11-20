using RegexNet.Core.Lexing;
using RegexNet.Core.Parsing;
using RegexNet.Core.Parsing.Visitors;


var tokens = Lexer.Tokenize("1*2?3+|345|678");
var root = Parser.Parse(tokens);
PrettyPrinter.PrintToConsole(root);