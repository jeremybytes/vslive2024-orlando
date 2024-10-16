using CreditCard.Library;
using System.Windows;
using System.Windows.Controls;

namespace CreditCard.TesterApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TestButton_Click(object sender, RoutedEventArgs e)
    {
        string testNumber = CreditCardTextBox.Text;
        bool result = LuhnCheck.PassesLuhnCheck(testNumber);
        OutputTextBlock.Text = result.ToString();
    }

    private void CreditCardTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
        OutputTextBlock.Text = "";
    }
}