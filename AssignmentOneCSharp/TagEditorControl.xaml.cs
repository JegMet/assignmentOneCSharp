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
    /// Interaction logic for TagEditorControl.xaml
    /// </summary>
    public partial class TagEditorControl : UserControl
    {

        TagLib.File _currentFile = null;

        MediaElement _myMediaPlayer = null;

        // Define a delegate for your event handler
        public delegate void SaveRequestedEventHandler(object sender, EventArgs e);

        // Define the event based on the delegate
        public event SaveRequestedEventHandler SaveRequested;

        public TagEditorControl()
        {
            InitializeComponent();

        }

        public void SetFile(TagLib.File file)
        {
            _currentFile = file;

        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {

            if(_currentFile == null)
            {
                return;
            }

            if(Title.Text != "")
                _currentFile.Tag.Title = Title.Text;
            if(Album.Text != "")
                _currentFile.Tag.Album = Album.Text;
            if (Year.Text != "")
                _currentFile.Tag.Year = Convert.ToUInt32(Year.Text);
            if (Artist.Text != "")
                _currentFile.Tag.Performers = new string[] { Artist.Text };


            // Raise the event to notify that save is requested
            SaveRequested?.Invoke(this, EventArgs.Empty);


        }


    }
}
