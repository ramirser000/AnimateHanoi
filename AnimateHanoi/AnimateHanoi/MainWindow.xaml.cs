using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;


namespace AnimateHanoi
{
    public partial class MainWindow : Window
    {
        private Hanoi hanoi;
        private bool stop = true;
        private Thread thread;

        public ListBox output;
        public Label expectedMoves;
        public MainWindow()
        {
            InitializeComponent();
            output = labelOutput;
            expectedMoves = labelMoves;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (stop)
            {
                ListBoxItem item = (ListBoxItem)comboBoxNumDiscs.SelectedItem;
                hanoi = new Hanoi(drawingCanvas, this, int.Parse((String)item.Content));
                stop = false;
                thread = new System.Threading.Thread(ThreadFunction);
                thread.Name = "UpdateThread";
                thread.Start();
                btnStart.Content = "Abort";
                
               
            }
            else
            {
                drawingCanvas.Children.Clear();
                stop = true;
                btnStart.Content = "Start";
                labelMoves.Content = "";
            }
        }

        private void ThreadFunction()
        {

            while (!stop)
            {
                update();
                draw();
                Thread.Sleep(10);
            }

        }

        private void draw()
        {
            try
            {
                hanoi.draw();
            }
            catch(Exception)
            {
                thread.Abort();
            }
            
        }

        private void update()
        {
            try
            {
                hanoi.update();
            }
            catch (Exception)
            {
                thread.Abort();
            }
        }

    }
}
