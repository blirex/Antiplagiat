using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Antiplagiat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] fileSentences;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            fileSentences = File.ReadAllLines("data.txt");
            string inputSentence = inputBox.Text;
            int threshold = 3;
            double maxSimilarity = 0;
            string mostSimilarSentence = "";

            foreach (var sentence in fileSentences)
            {
                int distance = LevenshteinDistance.GetDistance(sentence, inputSentence);
                int maxLength = Math.Max(sentence.Length, inputSentence.Length);
                double similarity = 100 * (1.0 - (double)distance / maxLength);

                if (similarity > maxSimilarity)
                {
                    maxSimilarity = similarity;
                    mostSimilarSentence = sentence;
                }
            }

            if (maxSimilarity > threshold)
            {
                MessageBox.Show($"Самое большое сходство: {Math.Round(maxSimilarity, 2, MidpointRounding.AwayFromZero)}% с строкой: \"{mostSimilarSentence}\"");
            }
            else
            {
                MessageBox.Show("Слово не похоже ни на одно с тех что у меня есть");
            }

        }

    }

}