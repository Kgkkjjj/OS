using System.Windows;

namespace CodeEditor
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewFile_Click(object sender, RoutedEventArgs e) => MessageBox.Show("New File tool not implemented.");
        private void OpenFile_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Open File tool not implemented.");
        private void SaveFile_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Save File tool not implemented.");
        private void SaveAs_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Save As tool not implemented.");
        private void Build_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Build tool not implemented.");
        private void Run_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Run tool not implemented.");
        private void Debug_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Debug tool not implemented.");
        private void FormatCode_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Format Code tool not implemented.");
        private void Search_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Search tool not implemented.");
        private void Replace_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Replace tool not implemented.");
        private void GitCommit_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Git Commit tool not implemented.");
        private void GitPush_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Git Push tool not implemented.");
        private void AddReference_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Add Reference tool not implemented.");
        private void ManageNuGet_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Manage NuGet tool not implemented.");
        private void Settings_Click(object sender, RoutedEventArgs e) => MessageBox.Show("Settings tool not implemented.");
    }
}
