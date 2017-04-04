using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            var path = PathBox.Text;
            var file = new FileInfo(path);
            using (var stream = file.OpenRead())
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                var array = memoryStream.ToArray();
                var photoAsString = System.Convert.ToBase64String(array);
                ResultBox.Text = photoAsString;
            }
        }

        private void PathBox_Drop(object sender, DragEventArgs e)
        {
            var obj = e.Data.GetData(DataFormats.FileDrop);

            PathBox.Text = (obj as string[])[0];
        }

        private void PathBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
}
