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
using System.Windows.Shapes;

namespace apiConsole
{
	/// <summary>
	/// Interaction logic for diagramWindow.xaml
	/// </summary>
	public partial class diagramWindow : Window
	{

		private List<Messurment> data;
		private double totalDistance;
		public diagramWindow(List<Messurment> data, double totalDistance)
		{
			InitializeComponent();
			this.data = data;
			this.totalDistance = totalDistance;
			Loaded += Betoltes;
		}

		private void Betoltes(object sender, RoutedEventArgs e) 
		{
			if (data.Count < 2)
			{
				MessageBox.Show("Nincs elég adat a diagramhoz!");
				return;
			}

			double margin = 30;
			double width = canvas.ActualWidth - 2 * margin;
			double height = canvas.ActualHeight - 2 * margin;
			double maxTime = data.Max(m => m.Time);
			double maxSpeed = data.Max(m => m.Speed);

			//első pont kezdőpozíciója
			Point prev = new Point(margin, margin + height - (data[0].Speed / maxSpeed) * height);

			//minden szomszédos pont összekötése
			for (int i = 1; i < data.Count; i++)
			{
				double x = margin + (data[i].Time / maxTime) * width;
				double y = margin + height - (data[i].Speed / maxSpeed) * height;

				Line line = new Line()
				{
					X1 = prev.X,
					Y1 = prev.Y,
					X2 = x,
					Y2 = y,
					Stroke = Brushes.SteelBlue,
					StrokeThickness = 2
				};

				canvas.Children.Add(line);
				prev = new Point(x, y);
			}

			//X tengely
			Line xAxis = new Line()
			{
				X1 = margin,
				Y1 = margin + height,
				X2 = margin + width + 5,
				Y2 = margin + height,
				Stroke = Brushes.Black,
				StrokeThickness = 3
			};

			//Y tengely
			Line yAxis = new Line()
			{
				X1 = margin,
				Y1 = margin,
				X2 = margin,
				Y2 = margin + height + 5,
				Stroke = Brushes.Black,
				StrokeThickness = 3
			};

			canvas.Children.Add(xAxis);
			canvas.Children.Add(yAxis);

			txtTotal.Text = $"Teljes megtett út: {totalDistance} m";
		}

	}
}
