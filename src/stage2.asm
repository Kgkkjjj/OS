BITS 16
ORG 0x1000

start:
    mov si, msg
    call print_string

.halt:
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

msg db "Update loaded!",13,10,0

; pad to 512 bytes
    times 510-($-$$) db 0
    dw 0xAA55
