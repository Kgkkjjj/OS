ASM=nasm
ASMFLAGS=-f bin
CC=gcc
STAGE2_CFLAGS=-m16 -ffreestanding -fno-pic -fno-asynchronous-unwind-tables -Os
LD=ld
LDFLAGS_STAGE2=-N -m elf_i386 -Ttext 0x1000

all: bin/os-image.bin

bin/os-image.bin: bin/boot.bin bin/stage2.bin
	@mkdir -p $(dir $@)
	cat $^ > $@

bin/boot.bin: src/boot.asm
	@mkdir -p $(dir $@)
	$(ASM) $(ASMFLAGS) $< -o $@

bin/stage2.bin: src/stage2.c
	@mkdir -p $(dir $@)
	$(CC) $(STAGE2_CFLAGS) -c $< -o bin/stage2.o
	$(LD) $(LDFLAGS_STAGE2) bin/stage2.o -o bin/stage2.elf
	objcopy -O binary bin/stage2.elf $@
	truncate -s 510 $@
	printf '\x55\xAA' >> $@

run: bin/os-image.bin
	qemu-system-x86_64 -drive format=raw,file=$< -nographic

clean:
	rm -f bin/os-image.bin bin/boot.bin bin/stage2.bin bin/stage2.o bin/stage2.elf
