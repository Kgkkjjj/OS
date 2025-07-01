# Assembly OS

Assembly OS is a tiny operating system. A 16‑bit bootloader loads a C kernel which provides a simple command-line interface.

## Building

Install `nasm`, `gcc` and optionally `qemu-system-x86_64` then run:

```bash
make
```

This creates `bin/os-image.bin`, a bootable disk image containing the bootloader and kernel.

To fetch the upstream example repository used for updates run:

```bash
make update_repo
```

## Running with QEMU

```bash
make run
```

## Usage

At the prompt type `help` to list commands. `halt` stops the emulator. `update` prints an update message and reboots the machine. The update script pulls the repository `codex/build-an-os-in-assembly-with-cli-and-filesystem` into the `updates/` directory.

This project is a minimal starting point for experimenting with low‑level OS development in C.
