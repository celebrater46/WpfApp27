using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfApp27.Views.Pages
{
    public partial class HelpContent : UserControl
    {
        public HelpContent()
        {
            InitializeComponent();
        }


        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            var p = new System.Diagnostics.Process();
            p.StartInfo.FileName = e.Uri.AbsoluteUri;
            p.StartInfo.UseShellExecute = true;
            p.Start();
        }
    }
}