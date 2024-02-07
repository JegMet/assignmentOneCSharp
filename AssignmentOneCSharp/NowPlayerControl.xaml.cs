using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AssignmentOneCSharp
{
    /// <summary>
    /// Interaction logic for NowPlayerControl.xaml
    /// </summary>
    public partial class NowPlayerControl : UserControl
    {

        TagLib.File _currentFile = null;
        public NowPlayerControl()
        {
            InitializeComponent();
        }

        public void SetFile(TagLib.File file)
        {
            _currentFile = file;
            Title.Content = "Title: " + _currentFile.Tag.Title;
            Artist.Content = "Artist: " + _currentFile.Tag.FirstPerformer;
            Album.Content = "Album: " + _currentFile.Tag.Album;
            Year.Content = "Year: " + _currentFile.Tag.Year;
            duration.Content = "Duration: " + _currentFile.Properties.Duration;
        }
    }
}
