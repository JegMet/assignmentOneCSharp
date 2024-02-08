using Microsoft.Win32;
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

namespace AssignmentOneCSharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        TagLib.File currentFile = null;

        bool isPlaying = false;

        public MainWindow()
        {
            InitializeComponent();

            // Subscribe to the event
            tag_editor.SaveRequested += TagEditor_SaveRequested;
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if(currentFile != null)
            {
                e.CanExecute = true;
            } 
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                myMediaPlayer.Play();
                isPlaying = true;
                now_playing_control.SetFile(currentFile);
                tag_editor.Visibility = Visibility.Collapsed;
                now_playing_control.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing media: {ex.Message}");
            }

        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (currentFile != null && isPlaying)
            {
                e.CanExecute = true;
            }
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Pause();
            isPlaying = false;
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (currentFile != null && isPlaying)
            {
                e.CanExecute = true;
            }
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            myMediaPlayer.Stop();
            now_playing_control.Visibility = Visibility.Collapsed;
            isPlaying = false;
        }

        private void Edit_Tags_Clicked(object sender, RoutedEventArgs e)
        {
            if (currentFile != null)
            {
                
                now_playing_control.Visibility = Visibility.Collapsed;
                tag_editor.Visibility = Visibility.Visible;
            } 
        }

        private void Open_File_Clicked(object sender, RoutedEventArgs e)
        {
           try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Mp3 files (*.mp3)|*.mp3";

                if (openFileDialog.ShowDialog() == true)
                {
                    string filename = openFileDialog.FileName;
                    fileNameBox.Text = filename;
                    currentFile = TagLib.File.Create(filename);
                    tag_editor.SetFile(currentFile);
                    myMediaPlayer.Source = new Uri(filename);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex.Message}");
            }
        }

        public void StopMediaPlayer()
        {
            myMediaPlayer.Stop();
            myMediaPlayer.Source = null;

            isPlaying = false;
        }


        //This needs to be async because we are awaiting the Task.Delay, need this or else media player doesnt close file in time and fails!!!
        public async void SaveFileTagsAsync()
        {
            try
            {
                if (currentFile == null) 
                    return;
                StopMediaPlayer();

                // need delay or else its too quick and fails
                await Task.Delay(100);

                currentFile.Save();

                myMediaPlayer.Source = new Uri(currentFile.Name);
                now_playing_control.SetFile(currentFile);
                tag_editor.Visibility = Visibility.Collapsed;
                now_playing_control.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tags: {ex.Message}");
            }

        }

        private void TagEditor_SaveRequested(object sender, EventArgs e)
        {
            // Call the method to stop the media player and save the file
            SaveFileTagsAsync();
        }

        private void Exit_Clicked(object sender, RoutedEventArgs e)
        {
            myMediaPlayer.Stop();
            myMediaPlayer.Source = null;
            Application.Current.Shutdown();
        }


    }
}