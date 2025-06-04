#include "terminal.h"
#include <stddef.h>
#include <stdint.h>

static volatile char* video = (volatile char*)0xB8000;
static size_t pos = 0;

void terminal_init(void) {
    const char* msg = "Open B OS - Terminal\n";
    for (const char* p = msg; *p; ++p) {
        video[pos++] = *p;
        video[pos++] = 0x07;
    }
}

void terminal_update(void) {
    /* Placeholder for terminal logic */
}
