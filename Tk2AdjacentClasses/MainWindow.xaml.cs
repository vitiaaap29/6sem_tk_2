using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LineryBinaryCode;

namespace Tk2AdjacentClasses
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<NkCode> generatorsNkCodes;

        public MainWindow()
        {
            InitializeComponent();
            initGeneratorsNkCodes();
        }

        private void initGeneratorsNkCodes()
        {
            generatorsNkCodes = new List<NkCode>();
            NkCode temp = new ParityCheckNkCode();
            generatorsNkCodes.Add(temp);
            temp = new RepetitionNkCode();
            generatorsNkCodes.Add(temp);
            temp = new SquareNkCode();
            generatorsNkCodes.Add(temp);
            temp = new TriangularNkCode();
            generatorsNkCodes.Add(temp);
        }

        private void GetNkCodeButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder result = new StringBuilder();
            if (messageTextBox.Text != "")
            {
                try
                {
                    BitNumber bitNumber = new BitNumber(messageTextBox.Text);
                    foreach (NkCode code in generatorsNkCodes)
                    {
                        BitNumber b = code.Generate(bitNumber);
                        result.Append(code.GetType().Name + ": " + b + " \r\n");
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Сообщение не соответствует формату: только нули и единицы. Или " + ee.Message);
                }
                finally
                {
                    resultLabel.Content = result.ToString();
                }
            }
            else
            {
                MessageBox.Show("Пустое сообщение нельзя кодировать");
            }
        }

        private void decodeNkButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder result = new StringBuilder();
            if (nkTextBox.Text != "")
            {
                try
                {
                    //в цикле падает
                    BitNumber bitNumber = new BitNumber(nkTextBox.Text);
                    foreach (NkCode code in generatorsNkCodes)
                    {
                        BitNumber b = code.Decode(bitNumber);
                        result.Append(code.GetType().Name + ": " + b + " \r\n");
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show("Сообщение не соответствует формату: только нули и единицы. Или " + ee.Message);
                }
                finally
                {
                    resultDeCodeLabel.Content = result.ToString();
                }
            }
            else
            {
                MessageBox.Show("Пустое сообщение нельзя кодировать");
            }
        }
    }
}
