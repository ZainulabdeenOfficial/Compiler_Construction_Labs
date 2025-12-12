using System;
using System.Collections.Generic;

enum TokenType
{
    PLUS, MINUS, MUL, DIV,
    LPAREN, RPAREN,
    NUMBER, ID,
    EOF
}

class Token
{
    public TokenType Type;
    public string Value;

    public Token(TokenType t, string v)
    {
        Type = t;
        Value = v;
    }
}

class Lexer
{
    private string text;
    private int pos;

    public Lexer(string input)
    {
        text = input;
        pos = 0;
    }

    private char CurrentChar()
    {
        if (pos >= text.Length)
            return '\0';
        return text[pos];
    }

    private void Advance()
    {
        pos++;
    }

    public Token GetNextToken()
    {
        while (char.IsWhiteSpace(CurrentChar()))
            Advance();

        char ch = CurrentChar();

        // NUMBER
        if (char.IsDigit(ch))
        {
            string num = "";
            while (char.IsDigit(CurrentChar()))
            {
                num += CurrentChar();
                Advance();
            }
            return new Token(TokenType.NUMBER, num);
        }

        // IDENTIFIER
        if (char.IsLetter(ch))
        {
            string id = "";
            while (char.IsLetterOrDigit(CurrentChar()))
            {
                id += CurrentChar();
                Advance();
            }
            return new Token(TokenType.ID, id);
        }

        // OPERATORS + SYMBOLS
        if (ch == '+') { Advance(); return new Token(TokenType.PLUS, "+"); }
        if (ch == '-') { Advance(); return new Token(TokenType.MINUS, "-"); }
        if (ch == '*') { Advance(); return new Token(TokenType.MUL, "*"); }
        if (ch == '/') { Advance(); return new Token(TokenType.DIV, "/"); }
        if (ch == '(') { Advance(); return new Token(TokenType.LPAREN, "("); }
        if (ch == ')') { Advance(); return new Token(TokenType.RPAREN, ")"); }

        // END OF INPUT
        if (ch == '\0')
            return new Token(TokenType.EOF, "");

        throw new Exception($"Invalid character: {ch}");
    }
}

class Parser
{
    private Lexer lexer;
    private Token current;

    public Parser(Lexer l)
    {
        lexer = l;
        current = lexer.GetNextToken();
    }

    private void Eat(TokenType type)
    {
        if (current.Type == type)
            current = lexer.GetNextToken();
        else
            throw new Exception($"Expected {type}, got {current.Type}");
    }

    // === Grammar ===
    // E → T { (+ | -) T }
    // T → F { (* | /) F }
    // F → NUMBER | ID | (E)

    public int ParseExpr(Dictionary<string, int> vars)
    {
        return E(vars);
    }

    private int E(Dictionary<string, int> vars)
    {
        int value = T(vars);

        while (current.Type == TokenType.PLUS || current.Type == TokenType.MINUS)
        {
            if (current.Type == TokenType.PLUS)
            {
                Eat(TokenType.PLUS);
                value += T(vars);
            }
            else
            {
                Eat(TokenType.MINUS);
                value -= T(vars);
            }
        }

        return value;
    }

    private int T(Dictionary<string, int> vars)
    {
        int value = F(vars);

        while (current.Type == TokenType.MUL || current.Type == TokenType.DIV)
        {
            if (current.Type == TokenType.MUL)
            {
                Eat(TokenType.MUL);
                value *= F(vars);
            }
            else
            {
                Eat(TokenType.DIV);
                int divisor = F(vars);
                if (divisor == 0)
                    throw new Exception("Division by zero");
                value /= divisor;
            }
        }

        return value;
    }

    private int F(Dictionary<string, int> vars)
    {
        if (current.Type == TokenType.NUMBER)
        {
            int v = int.Parse(current.Value);
            Eat(TokenType.NUMBER);
            return v;
        }

        if (current.Type == TokenType.ID)
        {
            string name = current.Value;

            if (!vars.ContainsKey(name))
                throw new Exception($"Undefined variable: {name}");

            Eat(TokenType.ID);
            return vars[name];
        }

        if (current.Type == TokenType.LPAREN)
        {
            Eat(TokenType.LPAREN);
            int v = E(vars);
            Eat(TokenType.RPAREN);
            return v;
        }

        throw new Exception($"Unexpected token: {current.Type}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter expression:");
        string input = Console.ReadLine();

        // Predefined variables
        var vars = new Dictionary<string, int>()
        {
            {"a", 10},
            {"b", 5},
            {"c", 3}
        };

        try
        {
            Lexer lexer = new Lexer(input);
            Parser parser = new Parser(lexer);

            int result = parser.ParseExpr(vars);

            Console.WriteLine("\nResult = " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("\nError: " + ex.Message);
        }
    }
}