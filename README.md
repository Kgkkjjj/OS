# Assembly OS

Assembly OS is a minimal operating system written entirely in 16‑bit x86 assembly. It provides a simple command-line interface with a couple of built‑in commands.

## Building

Ensure `nasm` and optionally `qemu-system-x86_64` are installed. Run:

```bash
make
```

This produces `bin/os-image.bin`, a bootable image.

The image includes a second stage (`src/stage2.asm`) that is loaded when you run the `update` command inside the OS.

## Running with QEMU

With QEMU installed, boot the image using:

```bash
make run
```

## Usage

At the prompt you can type `help` to list available commands. `halt` stops the emulator and `update` loads a secondary stage from disk if present. Any unrecognised command prints an error message.

This project is a starting point for experimenting with low‑level OS development in pure assembly.
