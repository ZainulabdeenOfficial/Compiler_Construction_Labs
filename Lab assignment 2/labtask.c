#include <stdio.h>
#include <ctype.h>
#include <string.h>

#define MAX 20

int n;
char production[MAX][MAX];
char firstResult[MAX], followResult[MAX];
int firstCount, followCount;

void findFirst(char c);
void findFollow(char c);
int isPresent(char arr[], char c, int count);
int isTerminal(char c);

int main() {
    int i, j;

    printf("Enter the number of productions: ");
    scanf("%d", &n);

    printf("Enter the productions (like S=aBAh):\n");
    for (i = 0; i < n; i++)
        scanf("%s", production[i]);

    // ---- FIRST ----
    printf("\n----- FIRST Sets -----\n");
    for (i = 0; i < n; i++) {
        firstCount = 0;
        findFirst(production[i][0]);
        printf("FIRST(%c) = { ", production[i][0]);
        for (j = 0; j < firstCount; j++)
            printf("%c ", firstResult[j]);
        printf("}\n");
    }

    // ---- FOLLOW ----
    printf("\n----- FOLLOW Sets -----\n");
    for (i = 0; i < n; i++) {
        followCount = 0;
        findFollow(production[i][0]);
        printf("FOLLOW(%c) = { ", production[i][0]);
        for (j = 0; j < followCount; j++)
            printf("%c ", followResult[j]);
        printf("}\n");
    }

    return 0;
}

// ---------- FIRST ----------
void findFirst(char c) {
    int i, j;
    char subSymbol;

    if (!isupper(c)) {
        if (!isPresent(firstResult, c, firstCount))
            firstResult[firstCount++] = c;
        return;
    }

    for (i = 0; i < n; i++) {
        if (production[i][0] == c) {
            // ε production
            if (production[i][2] == '#') {
                if (!isPresent(firstResult, '#', firstCount))
                    firstResult[firstCount++] = '#';
            } else {
                for (j = 2; j < strlen(production[i]); j++) {
                    subSymbol = production[i][j];
                    findFirst(subSymbol);
                    if (!isupper(subSymbol))
                        break;
                }
            }
        }
    }
}

// ---------- FOLLOW ----------
void findFollow(char c) {
    int i, j, k;

    if (production[0][0] == c) {
        if (!isPresent(followResult, '$', followCount))
            followResult[followCount++] = '$';
    }

    for (i = 0; i < n; i++) {
        for (j = 2; j < strlen(production[i]); j++) {
            if (production[i][j] == c) {
                // next symbol exists
                if (production[i][j + 1] != '\0') {
                    firstCount = 0;
                    findFirst(production[i][j + 1]);
                    for (k = 0; k < firstCount; k++) {
                        if (firstResult[k] != '#' && !isPresent(followResult, firstResult[k], followCount))
                            followResult[followCount++] = firstResult[k];
                    }
                }

                // symbol is at end or followed by ε
                if (production[i][j + 1] == '\0' || production[i][j + 1] == '#') {
                    if (production[i][0] != c)
                        findFollow(production[i][0]);
                }
            }
        }
    }
}

// ---------- Helper ----------
int isPresent(char arr[], char c, int count) {
    for (int i = 0; i < count; i++)
        if (arr[i] == c)
            return 1;
    return 0;
}

int isTerminal(char c) {
    return !isupper(c);
}
