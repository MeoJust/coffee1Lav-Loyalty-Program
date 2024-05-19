using System.Windows.Input;
using System.Windows;


namespace adminWPF.core
{
    internal class DragableWindow
    {
        public void DragTheWindow(object sender, MouseButtonEventArgs e, Window window) {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                window.DragMove();
            }
        }
    }
}