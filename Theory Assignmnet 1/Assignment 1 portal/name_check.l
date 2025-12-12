%{
#include <stdio.h>
#include <string.h>
%}

%option noyywrap

%%
[Z][a][i][n]   { return 1; }
.|\n                                                    { return 0; }
%%

int main()
{
    char input[100];
    printf("Enter a name to check whether it is a valid name OR Not: ");
    fgets(input, sizeof(input), stdin);
    // Remove newline if present
    input[strcspn(input, "\n")] = 0;

    YY_BUFFER_STATE buffer = yy_scan_string(input);
    int result = yylex();

    if (result == 1)
        printf("The given name [%s] is a VALID name\n", input);
    else
        printf("The given input [%s] is INVALID\n", input);

    yy_delete_buffer(buffer);
    return 0;
}
