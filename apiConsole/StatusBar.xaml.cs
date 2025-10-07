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

namespace apiConsole
{
	/// <summary>
	/// Interaction logic for StatusBar.xaml
	/// </summary>
	public partial class StatusBar : UserControl
	{
		public StatusBar()
		{
			InitializeComponent();
		}

		public void SetCount(int count) 
		{
			txtCount.Text = $"Adatok száma: {count}";
		}

		public void SetSaved(bool saved)
		{
			txtStatus.Text = saved ? "Mentve" : "Nincs mentve";
			txtStatus.Foreground = saved ? Brushes.Green : Brushes.Red;
		}
	}
}
