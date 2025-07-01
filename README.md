# Assembly OS

Assembly OS is a minimal operating system. The bootloader is written in 16‑bit x86 assembly and provides a simple command-line interface. An optional C kernel can be loaded from disk using the `update` command.

## Building

Ensure `nasm`, `gcc` and optionally `qemu-system-x86_64` are installed. Run:

```bash
make
```

This produces `bin/os-image.bin`, a bootable image.

The image includes a second stage written in C (`src/stage2.c`) that is built into the disk image. When you type `update` at the prompt, the bootloader loads this stage, displays an update message and then reboots the machine.

## Running with QEMU

With QEMU installed, boot the image using:

```bash
make run
```

## Usage

At the prompt you can type `help` to list available commands. `halt` stops the emulator and `update` loads a secondary stage from disk if present. Any unrecognised command prints an error message.

This project is a starting point for experimenting with low‑level OS development in pure assembly.
