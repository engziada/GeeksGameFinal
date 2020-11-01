using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DragAndDropApp
{
    public partial class DragAndDropControls
    {
        private double FirstXPos, FirstYPos;
        private object MovingObject;

        private Point dz_TopLeftPoint;
        private Point dz_BottomLeftPoint;
        private Point dz_BottomRightPoint;
        private Point dz_TopRightPoint;

        private DispatcherTimer timer;

        private Storyboard dropdownStoryboard;
        private Storyboard slideY_Storyboard;
        private Storyboard slideX_Storyboard;
        private Storyboard fade_Storyboard;
        private Storyboard width_Storyboard;
        private Storyboard height_Storyboard;

        private bool isPaused = false;
        private bool isStarted = false;
        private int milliseconds = 0;
        private int total = 0;
        private int score = 0;
        private int seconds = 0;
        

        public DragAndDropControls()
        {
            InitializeComponent();
            timer = new DispatcherTimer(TimeSpan.FromMilliseconds(1), DispatcherPriority.Background,timerCallback, Dispatcher.CurrentDispatcher);
        }

        private void timerCallback(object sender, EventArgs e)
        {
            if (milliseconds == 99)
            {
                milliseconds = 0;
                seconds++;

                lbl_MilliSeconds.Content = milliseconds;
                lbl_Seconds.Content = seconds;
            }
            else
            {
                milliseconds++;
                lbl_MilliSeconds.Content = milliseconds;
            }


        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            //Point position = ElementBeingDragged.TranslatePoint(new Point(0, 0), UIElementRelativeTo);
            //top left
            dz_TopLeftPoint = dz_China.TranslatePoint(new Point(dz_China.ActualWidth, dz_China.ActualHeight), DesigningCanvas);
                //dz_China.TransformToAncestor(grid_Main).Transform(new Point(dz_China.ActualWidth, dz_China.ActualHeight));
            //bottom left
            dz_BottomLeftPoint =dz_Japan.TranslatePoint(new Point(dz_Japan.ActualWidth, 0), DesigningCanvas);
                //dz_Japan.TransformToAncestor(grid_Main).Transform(new Point(dz_Japan.ActualWidth, 0));
            //top right
            dz_TopRightPoint =dz_Korea.TranslatePoint(new Point(0, dz_Korea.ActualHeight), DesigningCanvas);
                //dz_Korea.TransformToAncestor(grid_Main).Transform(new Point(0, dz_Korea.ActualHeight));
            //bottom right
            dz_BottomRightPoint = dz_Thailand.TranslatePoint(new Point(0,0), DesigningCanvas);
                //dz_Thailand.TransformToAncestor(grid_Main).Transform(new Point(0, 0));

        }


        #region -- Buttons Events --

        private void btn_Start_Click(object sender, RoutedEventArgs e)
        {
            Button selButton = (Button) sender;
            if (!isStarted)
            {
                DropNewFace();
                timer.Start();
                isStarted = true;
                selButton.Content = "Stop";
                selButton.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                DesigningCanvas?.Children?.Clear();

                timer?.Stop();

                dropdownStoryboard?.Stop();
                fade_Storyboard?.Stop();
                height_Storyboard?.Stop();
                slideX_Storyboard?.Stop();
                slideY_Storyboard?.Stop();
                width_Storyboard?.Stop();

                isStarted = false;
                isPaused = false;
                MovingObject = false;

                milliseconds = 0;
                lbl_MilliSeconds.Content = milliseconds;
                seconds = 0;
                lbl_Seconds.Content = seconds;
                total = 0;
                lbl_Total.Content = total;
                score = 0;
                lbl_Score.Content = score;

                selButton.Content = "Start";
                selButton.Foreground = new SolidColorBrush(Colors.DarkBlue);

            }
        }

        private void btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
#endregion

        #region -- Mouse Events --
        new void PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (MovingObject != null)
            {
                Control targetControl = (Control)sender;
                StartDropdown(targetControl,e.GetPosition(DesigningCanvas).Y,2);
            }
            MovingObject = null;
        }

        private new void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PauseDropdown();

            FirstXPos = e.GetPosition(sender as Control).X;
            FirstYPos = e.GetPosition(sender as Control).Y;
            MovingObject = sender;

        }
        
        private new void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && MovingObject!=null)
            {
                FrameworkElement selElement = MovingObject as FrameworkElement;
                FrameworkElement selParent = selElement?.Parent as FrameworkElement;

                if (selElement != null)
                {
                    selElement.SetValue(Canvas.LeftProperty, e.GetPosition(selParent).X - FirstXPos);
                    selElement.SetValue(Canvas.TopProperty, e.GetPosition(selParent).Y - FirstYPos);
                }

                double distance2Japan = Point.Subtract(e.GetPosition(DesigningCanvas), dz_BottomLeftPoint).Length;
                double distance2Korea = Point.Subtract(e.GetPosition(DesigningCanvas), dz_TopRightPoint).Length;
                double distance2Thailand = Point.Subtract(e.GetPosition(DesigningCanvas), dz_BottomRightPoint).Length;
                double distance2China = Point.Subtract(e.GetPosition(DesigningCanvas), dz_TopLeftPoint).Length;

#if DEBUG
                dz_Japan.Content = distance2Japan;
                dz_Korea.Content = distance2Korea;
                dz_Thailand.Content = distance2Thailand;
                dz_China.Content = distance2China;
#endif

                if (distance2China <= 200.0)
                {
                    dropdownStoryboard.Stop(this);
                    Drop2Box(e.GetPosition(DesigningCanvas).X,e.GetPosition(DesigningCanvas).Y,dz_TopLeftPoint.X, dz_TopLeftPoint.Y, 1);
                    if (selElement?.Tag?.ToString() == "Chinese") score++;
                    MovingObject = null;
                    isPaused = false;


                }
                else if (distance2Japan <= 200.0)
                {
                    dropdownStoryboard.Stop(this);
                    Drop2Box(e.GetPosition(DesigningCanvas).X, e.GetPosition(DesigningCanvas).Y, dz_BottomLeftPoint.X, dz_BottomLeftPoint.Y, 1);
                    if (selElement?.Tag?.ToString() == "Japanese") score++;
                    MovingObject = null;
                    isPaused = false;

                }
                else if (distance2Korea <= 200.0)
                {
                    dropdownStoryboard.Stop(this);
                    Drop2Box(e.GetPosition(DesigningCanvas).X, e.GetPosition(DesigningCanvas).Y, dz_TopRightPoint.X, dz_TopRightPoint.Y, 1);
                    if (selElement?.Tag?.ToString() == "Korean") score++;
                    MovingObject = null;
                    isPaused = false;

                }
                else if (distance2Thailand <= 200.0)
                {
                    dropdownStoryboard.Stop(this);
                    Drop2Box(e.GetPosition(DesigningCanvas).X, e.GetPosition(DesigningCanvas).Y, dz_BottomRightPoint.X, dz_BottomRightPoint.Y, 1);
                    if (selElement?.Tag?.ToString() == "Thai") score++;
                    MovingObject = null;
                    isPaused = false;

                }



            }
        }
        #endregion

        #region -- Movement --

        private void DropNewFace()
        {
            try
            {
                MovingObject = null;
                isPaused = false;
                isStarted = true;
                total++;
                lbl_Total.Content = total;

                string imagePath = getRandomImagePath(out string nationality);

                Frame newFace = new Frame
                {
                    Name = "faceFrame_" + DateTime.Now.Ticks,
                    Background =
                        new ImageBrush(new BitmapImage(new Uri(imagePath.Replace("\\", "/"), UriKind.Absolute))),
                    Width = 150,
                    Height = 150,
                    Cursor = Cursors.Hand,
                    Tag = nationality

                };

                newFace.SetValue(Canvas.LeftProperty, (DesigningCanvas.ActualWidth - newFace.Width) / 2);
                newFace.SetValue(Canvas.TopProperty, 0.0);
                newFace.PreviewMouseLeftButtonDown += this.MouseLeftButtonDown;
                newFace.PreviewMouseLeftButtonUp += this.PreviewMouseLeftButtonUp;


                DesigningCanvas.Children.Add(newFace);
                this.RegisterName(newFace.Name, newFace);

                isPaused = false;
                StartDropdown(newFace, 0, 3);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error in [Drop New Face]", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void dropdownStoryboard_Completed(object sender, EventArgs e)
        {
            if (!isPaused && isStarted)
            {
                DropNewFace();
            }
        }

        private void Drop2Box(double oldX, double oldY,double newX, double newY, int seconds)
        {
            if (MovingObject == null) return;
            Control _targetControl = (Control) MovingObject;

            Duration dropdownDuration = new Duration(TimeSpan.FromSeconds(seconds));
            
            slideY_Storyboard = new Storyboard();
            DoubleAnimation slideY_Animation = new DoubleAnimation(oldY, newY, dropdownDuration);
            Storyboard.SetTargetName(slideY_Animation, _targetControl.Name);
            Storyboard.SetTargetProperty(slideY_Animation, new PropertyPath(Canvas.TopProperty));
            slideY_Storyboard.Children.Add(slideY_Animation);

            slideX_Storyboard = new Storyboard();
            DoubleAnimation slideX_Animation = new DoubleAnimation(oldX, newX, dropdownDuration);
            Storyboard.SetTargetName(slideX_Animation, _targetControl.Name);
            Storyboard.SetTargetProperty(slideX_Animation, new PropertyPath(Canvas.LeftProperty));
            slideX_Storyboard.Children.Add(slideX_Animation);

            fade_Storyboard = new Storyboard();
            DoubleAnimation fade_Animation = new DoubleAnimation(_targetControl.Opacity, 0.0, dropdownDuration);
            Storyboard.SetTargetName(fade_Animation, _targetControl.Name);
            Storyboard.SetTargetProperty(fade_Animation, new PropertyPath("Opacity"));
            fade_Storyboard.Children.Add(fade_Animation);

            width_Storyboard = new Storyboard();
            DoubleAnimation width_Animation =
                new DoubleAnimation(_targetControl.Width, _targetControl.Width * 0.2, dropdownDuration);
            Storyboard.SetTargetName(width_Animation, _targetControl.Name);
            Storyboard.SetTargetProperty(width_Animation, new PropertyPath("Width"));
            width_Storyboard.Children.Add(width_Animation);

            height_Storyboard = new Storyboard();
            DoubleAnimation height_Animation =
                new DoubleAnimation(_targetControl.Height, _targetControl.Height * 0.2, dropdownDuration);
            Storyboard.SetTargetName(height_Animation, _targetControl.Name);
            Storyboard.SetTargetProperty(height_Animation, new PropertyPath("Height"));
            height_Storyboard.Children.Add(height_Animation);

            fade_Storyboard.Completed += Fade_Storyboard_Completed;

            slideX_Storyboard.Begin(DesigningCanvas, false);
            slideY_Storyboard.Begin(DesigningCanvas, false);
            fade_Storyboard.Begin(DesigningCanvas, false);
            width_Storyboard.Begin(DesigningCanvas, false);
            height_Storyboard.Begin(DesigningCanvas, false);

        }

        private void Fade_Storyboard_Completed(object sender, EventArgs e)
        {
            lbl_Score.Content = score;
            DropNewFace();
        }

        private void PauseDropdown()
        {
            isPaused = true;
            dropdownStoryboard.Stop(DesigningCanvas);
            dropdownStoryboard.Completed -= dropdownStoryboard_Completed;

        }

        private void StartDropdown(Control targetControl, double oldY, int seconds)
        {
            isPaused = false;
            Duration dropdownDuration = new Duration(TimeSpan.FromSeconds(seconds));
            dropdownStoryboard = new Storyboard();
            DoubleAnimation slideY_Animation = new DoubleAnimation(oldY, DesigningCanvas.ActualHeight, dropdownDuration);
            Storyboard.SetTargetName(slideY_Animation, targetControl.Name);
            Storyboard.SetTargetProperty(slideY_Animation, new PropertyPath(Canvas.TopProperty));
            dropdownStoryboard.Children.Add(slideY_Animation);
            dropdownStoryboard.Completed += dropdownStoryboard_Completed;
            dropdownStoryboard.Begin(DesigningCanvas, true);


        }
        #endregion
        private string getRandomImagePath(out string Nationality)
        {
            try
            {
                string imagesDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, "Images");
                string[] nationalityDirectories = Directory.GetDirectories(imagesDirectory);
                Random rnd = new Random();
                int nationalityIndex = rnd.Next(0, 3);
                string selectedNationalityDirectory = nationalityDirectories[nationalityIndex];
                string[] faceFiles = Directory.GetFiles(selectedNationalityDirectory);
                int faceIndex = rnd.Next(0, faceFiles.Length - 1);
                string selectedFaceImagePath = faceFiles[faceIndex];
                Nationality = selectedNationalityDirectory?.Substring(selectedNationalityDirectory.LastIndexOf(Path.DirectorySeparatorChar)+1);
                return selectedFaceImagePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Error in [Get Random Image]", MessageBoxButton.OK, MessageBoxImage.Error);
                Nationality = "";
                return "";
            }
        }
    }

}
