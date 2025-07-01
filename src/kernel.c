#include <stdint.h>

static void print(const char *s)
{
    while (*s) {
        __asm__ volatile (
            "movb %[c], %%al\n"
            "movb $0x0E, %%ah\n"
            "int $0x10"
            :
            : [c] "r"(*s)
            : "ax"
        );
        s++;
    }
}

static void read_line(char *buf, uint16_t max)
{
    uint16_t i = 0;
    for (;;) {
        char c;
        __asm__ volatile (
            "int $0x16"
            : "=a"(c)
            : "a"(0x0000)
        );
        if (c == '\r')
            break;
        if (i < max - 1) {
            buf[i++] = c;
            __asm__ volatile (
                "movb %[c], %%al\n"
                "movb $0x0E, %%ah\n"
                "int $0x10"
                :
                : [c] "r"(c)
                : "ax"
            );
        }
    }
    buf[i] = '\0';
    /* new line */
    __asm__ volatile ("movb $0x0D, %al; movb $0x0E, %ah; int $0x10");
    __asm__ volatile ("movb $0x0A, %al; movb $0x0E, %ah; int $0x10");
}

static int str_eq(const char *a, const char *b)
{
    while (*a && *b) {
        if (*a != *b)
            return 0;
        a++; b++;
    }
    return *a == *b;
}

static void halt(void)
{
    __asm__ volatile ("cli; hlt");
}

static void reboot(void)
{
    __asm__ volatile ("int $0x19");
}

void main(void)
{
    char buf[64];
    print("Welcome to Assembly OS!\r\n");
    for (;;) {
        read_line(buf, sizeof(buf));
        if (str_eq(buf, "help")) {
            print("Available commands: help, halt, update\r\n");
        } else if (str_eq(buf, "halt")) {
            halt();
        } else if (str_eq(buf, "update")) {
            print("Updating your system...\r\n");
            reboot();
        } else {
            print("Unknown command\r\n");
        }
    }
}
