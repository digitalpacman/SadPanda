using System;
using System.Collections.Generic;
using System.Linq;
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

namespace SadPanda.Starcraft.UnitCompare.UI
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		public Window1()
		{
			InitializeComponent();
		}

		private static string resultFormat = "Dies in {0} hits";

		private class Unit
		{
			public int HitPoints;
			public int CurrentHitPoints;
			public int Damage;
			public int Armor;
			public int AttackSpeed;
			public int DamagePerSecond;
			public int DamagePerUpgrade;
			public int ArmorPerUpgrade = 1;
		}

		private void fightButton_Click(object sender, RoutedEventArgs e)
		{
			Unit unit1 = new Unit();
			int.TryParse(unit1HitPointsTextBox.Text, out unit1.HitPoints);
			int.TryParse(unit1DamageTextBox.Text, out unit1.Damage);
			int.TryParse(unit1ArmorTextBox.Text, out unit1.Armor);
			int.TryParse(unit1SpeedTextBox.Text, out unit1.AttackSpeed);
			int.TryParse(unit1UpgradeDamageTextBox.Text, out unit1.DamagePerUpgrade);

			Unit unit2 = new Unit();
			int.TryParse(unit2HitPointsTextBox.Text, out unit2.HitPoints);
			int.TryParse(unit2DamageTextBox.Text, out unit2.Damage);
			int.TryParse(unit2ArmorTextBox.Text, out unit2.Armor);
			int.TryParse(unit2SpeedTextBox.Text, out unit2.AttackSpeed);
			int.TryParse(unit2UpgradeDamageTextBox.Text, out unit2.DamagePerUpgrade);
			

		}
	}
}
