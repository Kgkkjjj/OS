#include "terminal.h"
#include <stddef.h>
#include <stdint.h>

static volatile char* video = (volatile char*)0xB8000;
static size_t pos = 0;

void terminal_print(const char* str) {
    for (const char* p = str; *p; ++p) {
        video[pos++] = *p;
        video[pos++] = 0x07;
    }
}

void terminal_init(void) {
    terminal_print("Open B OS - Terminal\n");
}

void terminal_update(void) {
    static int counter = 0;
    char buf[32];
    const char hex[] = "0123456789ABCDEF";
    buf[0] = 'C'; buf[1] = 't'; buf[2] = 'r'; buf[3] = ':'; buf[4] = ' ';
    int i = 5;
    int c = counter++;
    if (c == 0) {
        buf[i++] = '0';
    } else {
        char tmp[16];
        int j = 0;
        while (c > 0 && j < 16) {
            tmp[j++] = hex[c % 16];
            c /= 16;
        }
        while (j > 0) buf[i++] = tmp[--j];
    }
    buf[i++] = '\n';
    buf[i] = '\0';
    terminal_print(buf);
}
