using System.Windows;

namespace adminWPF.core
{
    internal class CloseApp
    {
        public void Close()
        {
            Application.Current.Shutdown();
        }
    }
}
