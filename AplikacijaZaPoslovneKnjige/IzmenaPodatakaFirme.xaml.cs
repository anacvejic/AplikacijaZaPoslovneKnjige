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
    /// Interaction logic for IzmenaPodatakaFirme.xaml
    /// </summary>
    public partial class IzmenaPodatakaFirme : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        int sifraFirme;
        string imeFirme;
        string adresaFirme;
        string tel;
        public IzmenaPodatakaFirme(int firmaId, string naziv, string adresa, string telefon)
        {
            InitializeComponent();
            sifraFirme = firmaId;
            imeFirme = naziv;
            adresaFirme = adresa;
            tel = telefon;
            textBoxNaziv.Text = imeFirme;
            textBoxAdresa.Text = adresaFirme;
            textBoxTelefon.Text = tel;
        }

        private void BtnIzmeniFirmu_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxNaziv.Text) &&
                !string.IsNullOrWhiteSpace(textBoxAdresa.Text) && 
                !string.IsNullOrWhiteSpace(textBoxTelefon.Text))
            {
                if (!int.TryParse(textBoxNaziv.Text, out int _) && textBoxNaziv.Text.Length < 30)
                {
                    if (!int.TryParse(textBoxAdresa.Text, out int _) && textBoxAdresa.Text.Length < 50)
                    {
                        if (!int.TryParse(textBoxTelefon.Text, out int _) && textBoxTelefon.Text.Length <= 12)
                        {
                            Firma izmena = (from f in gl.Firmas
                                            where f.IdFirma.Equals(sifraFirme)
                                            select f).Single();
                            izmena.Naziv = textBoxNaziv.Text;
                            izmena.Adresa = textBoxAdresa.Text;
                            izmena.Telefon = textBoxTelefon.Text;

                            try
                            {
                                gl.SubmitChanges();
                                MessageBox.Show("Podaci su uspešno izmenjeni u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                PregledFirmi.dataGrid.ItemsSource = gl.Firmas.ToList();
                                this.Hide();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Podaci ne mogu biti izmenjeni!!" + ex.Message);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Telefon mora biti u formatu 000/000-0000 i ne sme biti duži od 12 karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Adresa se mora sastojati od karaktera i brojeva i ne sme biti duža od 50 karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Naziv se mora sastojati od karaktera i brojeva, i ne sme biti duži od 30 karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
