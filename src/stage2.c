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

void main(void)
{
    print("C kernel update loaded!\r\n");
    for (;;);
}
