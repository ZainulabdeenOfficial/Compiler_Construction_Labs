%{
#include <stdio.h>
%}

/* Definitions (optional) */
WS      [ \t]+
DIGIT   [0-9]

%%

{WS}                ; /* ignore whitespace */
"\n"                ; /* ignore newline */

"=="                { printf("The given operator [%s] is EQUALITY (==)\n", yytext); }
"!="                { printf("The given operator [%s] is NOT EQUAL (!=)\n", yytext); }
"<="                { printf("The given operator [%s] is LESS THAN OR EQUAL (<=)\n", yytext); }
">="                { printf("The given operator [%s] is GREATER THAN OR EQUAL (>=)\n", yytext); }
"&&"                { printf("The given operator [%s] is LOGICAL AND (&&)\n", yytext); }
"||"                { printf("The given operator [%s] is LOGICAL OR (||)\n", yytext); }

"+="                { printf("The given operator [%s] is ADDITION ASSIGN (+=)\n", yytext); }
"-="                { printf("The given operator [%s] is SUBTRACTION ASSIGN (-=)\n", yytext); }
"*="                { printf("The given operator [%s] is MULTIPLY ASSIGN (*=)\n", yytext); }
"/="                { printf("The given operator [%s] is DIVIDE ASSIGN (/=)\n", yytext); }
"%="                { printf("The given operator [%s] is MODULO ASSIGN (%%=)\n", yytext); }

"+"                 { printf("The given operator [%s] is ADDITION (+)\n", yytext); }
"-"                 { printf("The given operator [%s] is SUBTRACTION (-)\n", yytext); }
"*"                 { printf("The given operator [%s] is MULTIPLICATION (*)\n", yytext); }
"/"                 { printf("The given operator [%s] is DIVISION (/)\n", yytext); }
"%"                 { printf("The given operator [%s] is MODULO (%%)\n", yytext); }

"="                 { printf("The given operator [%s] is ASSIGNMENT (=)\n", yytext); }

"<"                 { printf("The given operator [%s] is LESS THAN (<)\n", yytext); }
">"                 { printf("The given operator [%s] is GREATER THAN (>)\n", yytext); }

"&"                 { printf("The given operator [%s] is BITWISE AND (&)\n", yytext); }
"|"                 { printf("The given operator [%s] is BITWISE OR (|)\n", yytext); }
"^"                 { printf("The given operator [%s] is BITWISE XOR (^)\n", yytext); }
"~"                 { printf("The given operator [%s] is BITWISE NOT (~)\n", yytext); }
"!"                 { printf("The given operator [%s] is LOGICAL NOT (!)\n", yytext); }

.                   { printf("The given character [%s] is an INVALID operator\n", yytext); }

%%

int main(int argc, char **argv)
{
    printf("Enter an operator to check whether it is in the valid operators OR Not\n");
    /* Read entire stdin; flex will tokenize what's typed */
    yylex();
    return 0;
}

int yywrap(void) { return 1; }
