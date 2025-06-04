#include "kernel.h"
#include "terminal.h"
#include "web_browser.h"
#include "text_editor.h"
#include "gui.h"

void kernel_main(void) {
    terminal_init();
    text_editor_init();
    web_browser_init();
    gui_init();
    while (1) {
        terminal_update();
    }
}
