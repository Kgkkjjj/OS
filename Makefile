CC=gcc
LD=ld
CFLAGS=-std=c99 -ffreestanding -O2 -Wall -Wextra -Iinclude
LDFLAGS=-T linker.ld -nostdlib

SRCS=$(wildcard src/*.c)
OBJS=boot.o $(SRCS:src/%.c=%.o)

all: kernel.bin

boot.o: src/boot.asm
	nasm -f elf32 $< -o $@

%.o: src/%.c
	$(CC) $(CFLAGS) -m32 -c $< -o $@

kernel.bin: $(OBJS)
	$(LD) -m elf_i386 $(LDFLAGS) -o $@ $(OBJS)

clean:
	rm -f $(OBJS) kernel.bin

run: kernel.bin
	qemu-system-x86_64 -display none -serial mon:stdio -kernel kernel.bin
