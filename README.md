# Open B OS Skeleton

This repository contains a minimal skeleton for an experimental operating system written mostly in C with a small assembly boot file. It now features stub modules for thirty additional systems such as memory management, scheduling, filesystems and networking. These placeholders accompany the terminal, text editor, web browser and GUI components and are meant for further expansion.

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
- Additional stub modules under `src/` provide placeholders for systems such as
  memory management, scheduling, drivers, networking, logging and more.
- `linker.ld` – Linker script placing sections at the 1 MB mark for a flat kernel.

This code is not a full operating system but a simple foundation for experimentation.
