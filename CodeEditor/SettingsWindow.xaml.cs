using System;
using System.Windows;

namespace CodeEditor
{
    public partial class SettingsWindow : Window
    {
        private readonly EditorSettings settings;

        public SettingsWindow(EditorSettings settings)
        {
            InitializeComponent();
            this.settings = settings;
            FontFamilyBox.Text = settings.FontFamily;
            FontSizeBox.Text = settings.FontSize.ToString();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            settings.FontFamily = string.IsNullOrWhiteSpace(FontFamilyBox.Text) ? "Consolas" : FontFamilyBox.Text;
            if (double.TryParse(FontSizeBox.Text, out double size))
                settings.FontSize = size;
            settings.Save();
            DialogResult = true;
        }
    }
}
