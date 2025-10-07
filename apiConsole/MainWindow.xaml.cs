using Microsoft.Win32;
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

namespace apiConsole
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Messurment> messurments = new List<Messurment>();
        public MainWindow()
        {
            InitializeComponent();
            UpdateStatusBar();
        }

        private void UpdateStatusBar()
        {
            statusBar.SetCount(messurments.Count);
            statusBar.SetSaved(true);
		}

		private void Refresh()
        {
            listBox.Items.Clear();

            //rendezés
            foreach (var item in messurments.OrderBy(x => x.Time))
            {
                listBox.Items.Add(item.ToString());
                
            }
            UpdateStatusBar()
			;
        }
		private void btnAdd_Click(object sender, RoutedEventArgs e)
		{
            if (!double.TryParse(txtTime.Text, out double t) ||
                (!double.TryParse(txtSpeed.Text, out double s)) ||
                t < 0 || s < 0) 
            {
                MessageBox.Show(":(");
                return;
            }

            messurments.Add(new Messurment(t,s));
            Refresh();
            txtTime.Clear();
            txtSpeed.Clear();
		}

		private void mentesClick(object sender, RoutedEventArgs e)
		{
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Szövegfájl (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter(saveFileDialog.FileName)) 
                { 
                    foreach (var m  in messurments)
                    {
                        sw.WriteLine(m.ToFileString());
                    }
                    MessageBox.Show(":D");
                }
            }
		}

		private void betoltesClick(object sender, RoutedEventArgs e)
		{
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Szövegfájl (*.txt)|*.txt";
			if (openFileDialog.ShowDialog() == true)
			{
				messurments.Clear();
                var lines = File.ReadAllLines(openFileDialog.FileName);
				foreach (var item in lines)
				{
                    var parts = item.Split(";");
                    if(parts.Length == 2 &&
                        double.TryParse(parts[0], out double t) &&
						double.TryParse(parts[1], out double s)) 
                    {
                        messurments.Add(new Messurment(t, s));
                    }
				}
                Refresh();
                MessageBox.Show(":D");
			}
		}

		private void kilepesClick(object sender, RoutedEventArgs e)
		{
            this.Close();
		}

		private void deleteClick(object sender, RoutedEventArgs e)
		{
			if (listBox.SelectedIndex == -1)
			{
				MessageBox.Show("Please select an item to remove.");
                return;
            }

            var sorted = messurments.OrderBy(x => x.Time).ToList();
            var selected = sorted[listBox.SelectedIndex];
            messurments.Remove(selected);
            Refresh();
			
		}

		private void diagramClick(object sender, RoutedEventArgs e)
		{
            if (messurments.Count < 2) 
            {
                MessageBox.Show("Min 2 data >:(");
            }

			var sorted = messurments.OrderBy(x => x.Time).ToList();

            double total = 0;
            for(int i = 0; i < sorted.Count -1; i++)
            {
                double dt = sorted[i + 1].Time - sorted[i].Time;
                double avg = sorted[i].Speed + sorted[i + 1].Speed / 2;
                total += dt * avg;
            }
			new diagramWindow(messurments,total).ShowDialog();
            
		}
	}
}