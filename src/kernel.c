#include "kernel.h"
#include "terminal.h"
#include "web_browser.h"
#include "text_editor.h"
#include "gui.h"
#include "memory_manager.h"
#include "scheduler.h"
#include "filesystem.h"
#include "vfs.h"
#include "pci.h"
#include "keyboard_driver.h"
#include "mouse_driver.h"
#include "display_driver.h"
#include "timer.h"
#include "interrupts.h"
#include "power_manager.h"
#include "audio_driver.h"
#include "network_stack.h"
#include "tcp_stack.h"
#include "udp_stack.h"
#include "ip_stack.h"
#include "shell.h"
#include "syscalls.h"
#include "security_manager.h"
#include "package_manager.h"
#include "environment_manager.h"
#include "config_manager.h"
#include "device_manager.h"
#include "user_manager.h"
#include "permissions.h"
#include "process_manager.h"
#include "service_manager.h"
#include "logger.h"
#include "bootloader.h"
#include "debug_tools.h"

void kernel_main(void) {
    terminal_init();
    text_editor_init();
    web_browser_init();
    gui_init();
    memory_manager_init();
    scheduler_init();
    filesystem_init();
    vfs_init();
    pci_init();
    keyboard_driver_init();
    mouse_driver_init();
    display_driver_init();
    timer_init();
    interrupts_init();
    power_manager_init();
    audio_driver_init();
    network_stack_init();
    tcp_stack_init();
    udp_stack_init();
    ip_stack_init();
    shell_init();
    syscalls_init();
    security_manager_init();
    package_manager_init();
    environment_manager_init();
    config_manager_init();
    device_manager_init();
    user_manager_init();
    permissions_init();
    process_manager_init();
    service_manager_init();
    logger_init();
    bootloader_init();
    debug_tools_init();
    while (1) {
        terminal_update();
    }
}
