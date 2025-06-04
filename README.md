# Open B OS Skeleton

This repository contains a minimal skeleton for an experimental operating system written mostly in C with a small assembly boot file. The goal is to provide a starting point for future development of a terminal, text editor, web browser and GUI environment.

## Building

```
make
```

This requires `gcc`, `nasm` and `ld`. Running `make` produces a `kernel.bin` that can be booted with QEMU:

```
make run
```

## Files

- `src/boot.asm` – Multiboot entry point that sets up a stack and jumps to `kernel_main`.
- `src/kernel.c` – Initializes subsystems and enters the main loop.
- `src/terminal.c` – Simple video memory output demonstrating a terminal placeholder.
- `src/text_editor.c` – Stub for future text editor functionality.
- `src/web_browser.c` – Stub for future web browser functionality.
- `src/gui.c` – Stub for GUI initialization.
- `linker.ld` – Linker script placing sections at the 1 MB mark for a flat kernel.

This code is not a full operating system but a simple foundation for experimentation.
