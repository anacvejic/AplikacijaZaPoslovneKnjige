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
    /// Interaction logic for IzmeniKontoUKontnomOkviru.xaml
    /// </summary>
    public partial class IzmeniKontoUKontnomOkviru : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        string sifraKonta;
        string opis;
        public IzmeniKontoUKontnomOkviru(string sifra,  string opisKonta)
        {
            InitializeComponent();
            sifraKonta = sifra;
            opis = opisKonta;
            textBoxOpisKonta.Text = opis;
        }
        private void FillSifraKonta()
        {
            var sifra = from f in gl.GrupaKontas
                        select new { f.Grupa };
            cmbGrupaKonta.ItemsSource = sifra;
            cmbGrupaKonta.SelectedValuePath = "Grupa";
            cmbGrupaKonta.DisplayMemberPath = "Grupa";
        }

        private void BtnUnesiIzmene_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBoxOpisKonta.Text) && cmbGrupaKonta.SelectedIndex > -1)
            {
                if(!int.TryParse(textBoxOpisKonta.Text, out int _))
                {
                    Konta izmena = (from k in gl.Kontas
                                    where k.SifraKonta.Equals(sifraKonta)
                                    select k).Single();
                    izmena.OpisKonta = textBoxOpisKonta.Text;
                    izmena.Grupa = cmbGrupaKonta.Text;
                    try
                    {
                        gl.SubmitChanges();
                        MessageBox.Show("Podaci su uspešno izemnjeni u bazi!");
                        KreiranjeKonta.dataGrid.ItemsSource = gl.Kontas.ToList();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Opis konta mora se satojati iz karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxOpisKonta.Clear();
                }
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
                
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillSifraKonta();
        }
    }
}
