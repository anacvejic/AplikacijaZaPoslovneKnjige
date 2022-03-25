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
    /// Interaction logic for KreiranjeKontnogOkvira.xaml
    /// </summary>
    public partial class KreiranjeKontnogOkvira : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public KreiranjeKontnogOkvira()
        {
            InitializeComponent();
            Load();
        }
        public static DataGrid dataGrid;
        private void Load()
        {
            dataGridKreiranjeGrupeKonta.ItemsSource = gl.GrupaKontas.ToList();
            dataGrid = dataGridKreiranjeGrupeKonta;
        }
        private void BtnUnesiGrupu_Click(object sender, RoutedEventArgs e)
        {
            string grupa = (dataGridKreiranjeGrupeKonta.SelectedItem as GrupaKonta).Grupa;
            string klasa = (dataGridKreiranjeGrupeKonta.SelectedItem as GrupaKonta).Klasa;
            string nazivGrupe = (dataGridKreiranjeGrupeKonta.SelectedItem as GrupaKonta).NazivGrupa;
            Kreiranje_kontnog_okvira novi = new Kreiranje_kontnog_okvira(grupa, klasa, nazivGrupe);
            novi.ShowDialog();
        }

        private void BtnIzmeniGrupu_Click(object sender, RoutedEventArgs e)
        {
            string grupa = (dataGridKreiranjeGrupeKonta.SelectedItem as GrupaKonta).Grupa;
            string klasa = (dataGridKreiranjeGrupeKonta.SelectedItem as GrupaKonta).Klasa;
            string nazivGrupe = (dataGridKreiranjeGrupeKonta.SelectedItem as GrupaKonta).NazivGrupa;
            IzmenaGrupeKonta izmena = new IzmenaGrupeKonta(grupa, klasa, nazivGrupe);
            izmena.ShowDialog();
        }

        private void BtnObrisiGrupu_Click(object sender, RoutedEventArgs e)
        {
            string grupa = (dataGridKreiranjeGrupeKonta.SelectedItem as GrupaKonta).Grupa;
            if(gl.Kontas.Any(n=>n.Grupa.Equals(grupa)))
            {
                MessageBox.Show("Podaci postoje u tabeli konto i nije ih moguće obrisati!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                GrupaKonta brisi = (from f in gl.GrupaKontas
                                    where f.Grupa.Equals(grupa)
                                    select f).Single();
                MessageBoxResult rez = MessageBox.Show("Da li ste sigururni da želite da obrišete grupu konta?", "Brisanje", MessageBoxButton.YesNo);
                if (rez == MessageBoxResult.Yes)
                {
                    try
                    {
                        gl.GrupaKontas.DeleteOnSubmit(brisi);
                        gl.SubmitChanges();
                        MessageBox.Show("Uspešno ste obrisali grupu konta iz kontnog okvira!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        Load();
                    }
                    catch (Exception ex) { MessageBox.Show(ex.Message); }
                }
            }
        }
    }
}
