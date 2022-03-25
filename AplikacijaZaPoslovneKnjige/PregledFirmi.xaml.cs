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
    /// Interaction logic for PregledFirmi.xaml
    /// </summary>
    public partial class PregledFirmi : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public PregledFirmi()
        {
            InitializeComponent();
            Load();
        }

        public static DataGrid dataGrid;

        private void Load()
        {
            dataGridSveFirme.ItemsSource = gl.Firmas.ToList();
            dataGrid = dataGridSveFirme;
        }
        private void BtnIzmeniFirmu_Click(object sender, RoutedEventArgs e)
        {
            int id = (dataGridSveFirme.SelectedItem as Firma).IdFirma;
            string naziv = (dataGridSveFirme.SelectedItem as Firma).Naziv;
            string adresa = (dataGridSveFirme.SelectedItem as Firma).Adresa;
            string telefon = (dataGridSveFirme.SelectedItem as Firma).Telefon;
            
            IzmenaPodatakaFirme izmenaFirme = new IzmenaPodatakaFirme(id, naziv, adresa, telefon);
            izmenaFirme.ShowDialog();
        }
    }
}
