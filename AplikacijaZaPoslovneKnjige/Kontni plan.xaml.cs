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

namespace AplikacijaZaPoslovneKnjige
{
    /// <summary>
    /// Interaction logic for Kontni_plan.xaml
    /// </summary>
    public partial class Kontni_plan : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public Kontni_plan()
        {
            InitializeComponent();
            Load();
        }
        public static DataGrid dataGrid;

        private void Load()
        {
            dataGridKreiranjeKonta.ItemsSource = gl.KontniPlans.ToList();
            dataGrid = dataGridKreiranjeKonta;
        }
        private void BtnUnesiKonto_Click(object sender, RoutedEventArgs e)
        {
            InsertUKontniPLan unosuUKontniPLan = new InsertUKontniPLan();
            unosuUKontniPLan.ShowDialog();
        }
        private void BtnIzmeniKonto_Click(object sender, RoutedEventArgs e)
        {
            int id = (dataGridKreiranjeKonta.SelectedItem as KontniPlan).Id;
            string identifikacioni = (dataGridKreiranjeKonta.SelectedItem as KontniPlan).IdentifikacioniBroj;
            IzmenaKontaUKontnomPlanu izemnaKonta = new IzmenaKontaUKontnomPlanu(id, identifikacioni);
            izemnaKonta.ShowDialog();
        }
    }
}
