ASM=nasm
ASMFLAGS=-f bin
CC=gcc
CFLAGS16=-m16 -ffreestanding -fno-pic -fno-asynchronous-unwind-tables -Os
LD=ld
LDFLAGS16=-N -e main -m elf_i386 -Ttext 0x1000

all: bin/os-image.bin

bin/os-image.bin: bin/boot.bin bin/kernel.bin
	@mkdir -p $(dir $@)
	cat $^ > $@

bin/boot.bin: src/boot.asm
	@mkdir -p $(dir $@)
	$(ASM) $(ASMFLAGS) $< -o $@

bin/kernel.bin: src/kernel.c
	@mkdir -p $(dir $@)
	$(CC) $(CFLAGS16) -c $< -o bin/kernel.o
	$(LD) $(LDFLAGS16) bin/kernel.o -o bin/kernel.elf
	objcopy -O binary bin/kernel.elf $@
	truncate -s 4096 $@

run: bin/os-image.bin
	qemu-system-x86_64 -drive format=raw,file=$< -nographic

update_repo:
	./scripts/pull_updates.sh

clean:
	rm -f bin/os-image.bin bin/boot.bin bin/kernel.bin bin/kernel.o bin/kernel.elf
