ASM=nasm
ASMFLAGS=-f bin

all: bin/os-image.bin

bin/os-image.bin: src/boot.asm
	@mkdir -p $(dir $@)
	$(ASM) $(ASMFLAGS) $< -o $@

run: bin/os-image.bin
	qemu-system-x86_64 -drive format=raw,file=$< -nographic

clean:
	rm -f bin/os-image.bin
