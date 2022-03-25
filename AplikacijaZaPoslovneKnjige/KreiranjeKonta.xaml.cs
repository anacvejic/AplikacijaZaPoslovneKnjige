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
    /// Interaction logic for KreiranjeKonta.xaml
    /// </summary>
    public partial class KreiranjeKonta : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public KreiranjeKonta()
        {
            InitializeComponent();
            Load();
        }
        public static DataGrid dataGrid;

        private void Load()
        {
            dataGridKreiranjeKonta.ItemsSource = gl.Kontas.ToList();
            dataGrid = dataGridKreiranjeKonta;
        }

        private void BtnUnesiKonto_Click(object sender, RoutedEventArgs e)
        {
            InsertNovogKonta noviKonto = new InsertNovogKonta();
            noviKonto.ShowDialog();
        }

        private void BtnIzmeniKonto_Click(object sender, RoutedEventArgs e)
        {
            string sifraKonta = (dataGridKreiranjeKonta.SelectedItem as Konta).SifraKonta;
            string opisKonta = (dataGridKreiranjeKonta.SelectedItem as Konta).OpisKonta;
            IzmeniKontoUKontnomOkviru izmeniKonto = new IzmeniKontoUKontnomOkviru(sifraKonta, opisKonta);
            izmeniKonto.ShowDialog();
        }

        private void BtnObrisiKonto_Click(object sender, RoutedEventArgs e)
        {
            string sifraKonta = (dataGridKreiranjeKonta.SelectedItem as Konta).SifraKonta;
            if (gl.KontniPlans.Any(k => k.SifraKonta == sifraKonta) || gl.StavkaNalogas.Any(n=>n.Konto == sifraKonta))
            { 
                MessageBox.Show("Ne može se obrisati šifra konta jer postoji u kontnom planu!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                Load();
            }
            else
            {
                //Konta brisanje = gl.Kontas.Single(k => k.SifraKonta == sifraKonta);
                Konta brisanje = (from f in gl.Kontas
                                  where f.SifraKonta.Equals(sifraKonta)
                                  select f).Single();
                MessageBoxResult rez = MessageBox.Show("Da li ste sigururni da želite da obrišete konto?", "Brisanje", MessageBoxButton.YesNo);
                if (rez == MessageBoxResult.Yes)
                {
                    try
                    {
                        gl.Kontas.DeleteOnSubmit(brisanje);
                        gl.SubmitChanges();
                        MessageBox.Show("Uspešno ste obrisali konto iz kontnog okvira!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
    }
}
