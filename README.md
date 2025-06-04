# Open B OS Skeleton

This repository contains a tiny educational kernel written mostly in C with a small assembly boot file. It demonstrates a very small terminal, text editor, web browser and GUI. Each component simply prints a short message to the screen when the kernel boots.

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
- `src/terminal.c` – Minimal VGA text terminal with a counter updated in the main loop.
- `src/text_editor.c` – Prints a message indicating the text editor is ready.
- `src/web_browser.c` – Prints a message indicating the web browser loaded.
- `src/gui.c` – Prints a message indicating the GUI is initialized.
- `linker.ld` – Linker script placing sections at the 1 MB mark for a flat kernel.

This code is not a full operating system but a simple foundation for experimentation.
