BITS 16
ORG 0x7C00

start:
    xor ax, ax
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7C00

    mov [boot_drive], dl

    mov bx, kernel_addr
    mov ah, 0x02
    mov al, kernel_sectors
    mov ch, 0
    mov dh, 0
    mov cl, 2
    mov dl, [boot_drive]
    int 0x13

    jmp kernel_addr

boot_drive db 0
kernel_addr equ 0x1000
kernel_sectors equ 8

times 510-($-$$) db 0
DW 0xAA55
