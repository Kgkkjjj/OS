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

static void reboot(void)
{
    __asm__ volatile ("int $0x19");
}

void main(void)
{
    print("Updating your system...\r\n");
    reboot();
    for (;;);
}
