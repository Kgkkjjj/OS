BITS 16
ORG 0x7C00

start:
    mov ax, 0x07C0
    mov ds, ax
    mov es, ax
    mov ss, ax
    mov sp, 0x7C00

    mov si, welcome_msg
    call print_string

main_loop:
    call read_line
    mov si, cmd_buffer
    mov di, help_cmd
    call str_cmp
    jc .do_help
    mov di, halt_cmd
    mov si, cmd_buffer
    call str_cmp
    jc .do_halt
    mov si, unknown_msg
    call print_string
    jmp main_loop

.do_help:
    mov si, help_msg
    call print_string
    jmp main_loop

.do_halt:
    cli
    hlt

print_string:
.next:
    lodsb
    cmp al, 0
    je .done
    mov ah, 0x0E
    int 0x10
    jmp .next
.done:
    ret

read_line:
    mov di, cmd_buffer
.loop:
    mov ah, 0x00
    int 0x16
    cmp al, 0x0D
    je .end
    mov [di], al
    inc di
    mov ah, 0x0E
    int 0x10
    jmp .loop
.end:
    mov byte [di], 0
    mov ah, 0x0E
    mov al, 0x0D
    int 0x10
    mov al, 0x0A
    int 0x10
    ret

str_cmp:
.loop_cmp:
    lodsb
    mov bl, [di]
    cmp al, bl
    jne .ne
    cmp al, 0
    je .eq
    inc di
    jmp .loop_cmp
.ne:
    clc
    ret
.eq:
    stc
    ret

welcome_msg db "Welcome to Assembly OS!", 13, 10, 0
help_msg db "Available commands: help, halt", 13, 10, 0
unknown_msg db "Unknown command", 13, 10, 0
help_cmd db "help",0
halt_cmd db "halt",0
cmd_buffer times 64 db 0

times 510-($-$$) db 0
DW 0xAA55
