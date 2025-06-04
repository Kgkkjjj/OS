; boot.asm - Multiboot compliant bootloader header and entry.
; Only used to provide entry point for the kernel.

; Multiboot header
MBALIGN equ 1<<0
MEMINFO equ 1<<1
FLAGS equ MBALIGN | MEMINFO
MAGIC equ 0x1BADB002
CHECKSUM equ -(MAGIC + FLAGS)

section .multiboot
    align 4
    dd MAGIC
    dd FLAGS
    dd CHECKSUM

extern kernel_main

section .text
    global _start
_start:
    ; Set up stack
    mov esp, stack_top
    call kernel_main
.hang:
    hlt
    jmp .hang

section .bss
    resb 16384
stack_top:
