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
    /// Interaction logic for InsertNovogKonta.xaml
    /// </summary>
    public partial class InsertNovogKonta : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public InsertNovogKonta()
        {
            InitializeComponent();
        }
        private void FillCombo() {
            var sifra = from s in gl.GrupaKontas
                        select new { s.Grupa, s.NazivGrupa };
            cmbSifraGrupeKonta.ItemsSource = sifra;
            cmbSifraGrupeKonta.SelectedValuePath = "Grupa";
            cmbSifraGrupeKonta.DisplayMemberPath = "NazivGrupa";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCombo();
        }

        private void BtnUnesiKonto_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxSifraKonta.Text) && !string.IsNullOrEmpty(textBoxOpisKonta.Text) && cmbSifraGrupeKonta.SelectedIndex > -1)
            {
                if(int.TryParse(textBoxSifraKonta.Text, out int _) && textBoxSifraKonta.Text.Length <=3)
                {
                    if(!gl.Kontas.Any(k=>k.SifraKonta == textBoxSifraKonta.Text))
                    {
                        if (!int.TryParse(textBoxOpisKonta.Text, out int _))
                        {
                            Konta nova = new Konta
                            {
                                SifraKonta = textBoxSifraKonta.Text,
                                OpisKonta = textBoxOpisKonta.Text,
                                Grupa = cmbSifraGrupeKonta.SelectedValue.ToString()
                            };
                            gl.Kontas.InsertOnSubmit(nova);
                            try
                            {
                                gl.SubmitChanges();
                                MessageBox.Show("Uspešno ste uneli konto u bazu!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                KreiranjeKonta.dataGrid.ItemsSource = gl.Kontas.ToList();
                                this.Hide();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Podaci ne mogu biti upisani u bazu! Pokušajte ponovo!" + ex);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Opis konta mora se satojati iz karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Šifra konta postoji u bazi! Morate uneti novu šifru!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Šifra konta mora biti broj i mora se sastojati od tri cifre!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

     
    }
}
