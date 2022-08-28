using System;
using System.Windows.Forms;

namespace ImageCrvLib.Viewers
{
    public partial class ProgressForm : Form
    {
        public ProgressForm()
        {
            InitializeComponent();
        }

        public void SetProgress(int prog)
        {
            prog = prog > progressBar1.Maximum ? progressBar1.Maximum : prog;
            prog = prog < progressBar1.Minimum ? progressBar1.Minimum : prog;
            progressBar1.Value = prog;
            progressBar1.Value = Math.Abs(prog - 1);
            progressBar1.Refresh();
            lblPercent.Text = prog.ToString() + "%";
            lblPercent.Refresh();

        }

        public void ShowProgress(bool show)
        {
            if (show)
            {
                Show();
                return;
            }
            Hide();
        }

        public void setText(string text)
        {
            lblInfo.Text = text;
            lblInfo.Refresh();
        }

        private void ProgressForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
