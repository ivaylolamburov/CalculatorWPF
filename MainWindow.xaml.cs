using System.Windows;
using System.Windows.Controls;

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Calculation = 0;
            Action = "none";
            IsPercented = false;
        }

        public double Calculation { get; set; }
        public string Action { get; set; }

        public string RContent
        {
            get { return result.Content.ToString(); }
        }

        public bool IsPercented { get; set; }

        private void ClickedC(object sender, RoutedEventArgs e)
        {
            if (RContent.Length == 1 || (RContent.StartsWith('-') && RContent.Length == 2))
                result.Content = 0;
            else
                result.Content = $"{RContent[..(RContent.Length - 1)]}";

            IsPercented = false;
        }

        private void ClickedAC(object sender, RoutedEventArgs e)
        {
            result.Content = "0";
            Action = "none";
            Calculation = 0;
            IsPercented = false;
        }

        private void ClickedEquals(object sender, RoutedEventArgs e)
        {
            double num = Double.Parse(RContent);

            result.Content = Compute(Calculation, num, Action).ToString();
            Calculation = 0;
        }

        private void ClickedPoint(object sender, RoutedEventArgs e)
        {
            if (!RContent.Contains('.'))
                result.Content = $"{result.Content}.";
            else if (RContent.EndsWith('.'))
                result.Content = RContent[..(RContent.Length - 1)];
        }

        private void ClickedPercent(object sender, RoutedEventArgs e)
        {
            if (!IsPercented)
            {
                IsPercented = true;
                result.Content = $"{Double.Parse(RContent) / 100}";
            }
            else
            {
                result.Content = $"{Double.Parse(RContent) * 100}";
                IsPercented = false;
            }
        }

        private void ClickedOpposite(object sender, RoutedEventArgs e)
        {
            if (!RContent.Contains('-'))
                result.Content = $"-{result.Content}";
            else if (RContent.StartsWith('-'))
                result.Content = RContent[1..];
        }

        private void ClickedAction(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            double num = Double.Parse(RContent);

            if (Calculation == 0)
                Calculation = num;
            else
            {
                Calculation = Compute(Calculation, num, Action);
                result.Content = Calculation;
            }

            Action = button.Content.ToString();
            IsPercented = false;
        }

        private void ClickedNumber(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (RContent == "-0")
                result.Content = $"-{button.Content}";
            else if (RContent == "0" || RContent == Calculation.ToString())
                result.Content = button.Content;
            else
                result.Content = $"{RContent}{button.Content}";
        }

        private static double Compute(double num1, double num2, string action)
        {
            double calculation = 0;

            switch (action)
            {
                case "+":
                    calculation = num1 + num2;
                    break;

                case "-":
                    calculation = num1 - num2;
                    break;

                case "x":
                    calculation = Math.Round(num1 * num2, 3);
                    break;

                case ":":
                    calculation = Math.Round(num1 / num2, 3);
                    break;
            }

            return calculation;
        }
    }
}