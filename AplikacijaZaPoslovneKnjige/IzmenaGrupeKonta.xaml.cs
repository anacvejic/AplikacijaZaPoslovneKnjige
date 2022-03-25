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
    /// Interaction logic for IzmenaGrupeKonta.xaml
    /// </summary>
    public partial class IzmenaGrupeKonta : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        string grupa;
        string klasa;
        string grupaNaziv;
        public IzmenaGrupeKonta(string sifraGrupe, string nazivKlasa, string nazivGrupe)
        {
            InitializeComponent();
            grupa = sifraGrupe;
            klasa = nazivKlasa;
            grupaNaziv = nazivGrupe;
            textBoxNazivKlase.Text = klasa;
            textBoxNazivGrupeKonta.Text = grupaNaziv;
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxNazivKlase.Text) &&
                !string.IsNullOrEmpty(textBoxNazivGrupeKonta.Text))
            {
               
                if(!int.TryParse(textBoxNazivKlase.Text, out int _) && textBoxNazivKlase.Text.Length < 50)
                {
                    if(!int.TryParse(textBoxNazivGrupeKonta.Text, out int _) && textBoxNazivGrupeKonta.Text.Length < 100)
                    {
                        GrupaKonta izmena = (from f in gl.GrupaKontas
                                             where f.Grupa.Equals(grupa)
                                             select f).Single();
                        izmena.Klasa = textBoxNazivKlase.Text;
                        izmena.NazivGrupa = textBoxNazivGrupeKonta.Text;
                        try
                        {
                            gl.SubmitChanges();
                            MessageBox.Show("Uspešno ste uneli izmene u bazu!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                            KreiranjeKontnogOkvira.dataGrid.ItemsSource = gl.GrupaKontas.ToList();
                            textBoxNazivKlase.Clear();
                            textBoxNazivGrupeKonta.Clear();
                            this.Hide();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Podaci bazu ne mogu biti izmenjeni! Pokušajte ponovo!" + ex);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Naziv grupe konta ne sme biti samo broj!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Naziv klase ne sme biti samo broj i mora biti do 50 karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
         }
    }
}
