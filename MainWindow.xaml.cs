using System;
using System.Windows;

namespace BayesianKT
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        BKTViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new BKTViewModel();
            this.DataContext = viewModel;
        }

        private void BKTrace(object sender, RoutedEventArgs e)
        {
            String input = m_InputData.Text;
            viewModel.ConstructResponseVector(input);

            viewModel.BKTrace();

            m_OutputData.Text = viewModel.PostOutputValues();
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            viewModel.Clear();
            m_OutputData.Text = "";

        }
    }
}
