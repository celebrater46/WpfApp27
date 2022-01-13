using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp27.Views.Pages
{
    public partial class MatchingContent : UserControl
    {
        public MatchingContent()
        {
            InitializeComponent();
        }

        private void RunButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.IsEnabled = false;
            ExecuteMatching(InTxt.Text, RegPtn.Text, GetRegexOptions());
            this.IsEnabled = true;
        }

        private RegexOptions GetRegexOptions()
        {
            var opt = RegexOptions.None;
            
            if (ROptIgnoreCase.IsChecked == true)
            {
                opt |= RegexOptions.IgnoreCase;
            }

            if (ROptMultiLine.IsChecked == true)
            {
                opt |= RegexOptions.Multiline;
            }

            if (ROptSingleLine.IsChecked == true)
            {
                opt |= RegexOptions.Singleline;
            }

            return opt;
        }

        private void ExecuteMatching(string input, string regPattern, RegexOptions regOpt)
        {
            OutTxt.Clear();

            if (IsAnyNullOrEmpties(input, regPattern))
            {
                MessageBox.Show("Not entered items exist.");
                return;
            }

            try
            {
                var reg = new Regex(regPattern, regOpt);
                MatchCollection matches = reg.Matches(input);

                if (matches.Count == 0)
                {
                    OutTxt.Text = "No match.";
                    return;
                }

                var sb = new StringBuilder();
                sb.Append($"{matches.Count} matches.\n");
                foreach (Match m in matches)
                {
                    sb.Append($"Value -> {m.Value}\n");
                    sb.Append("[Group]\n");
                    foreach (string gName in reg.GetGroupNames())
                    {
                        sb.Append($"{gName} -> {m.Groups[gName].Value}\n");
                    }

                    sb.Append("-----------------------------\n");
                }

                OutTxt.Text = sb.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool IsAnyNullOrEmpties(params string[] values)
        {
            foreach (string value in values)
            {
                if (string.IsNullOrEmpty(value))
                {
                    return true;
                }
            }

            return false;
        }
    }
}