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
    /// Interaction logic for Unos_nove_firme.xaml
    /// </summary>
    public partial class Unos_nove_firme : Window
    {
        private static GlavnaKnjigaDataContext gl1 = new GlavnaKnjigaDataContext();
        string unosUser;
        public Unos_nove_firme(string user)
        {
            InitializeComponent();
            unosUser = user;
        }
        private void OcistiPolja()
        {
            tbNaziv.Clear();
            tbAdresa.Clear();
            tbTelefon.Clear();
        }
        
        private void BtUnesi_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbNaziv.Text) && !string.IsNullOrEmpty(tbAdresa.Text) && !string.IsNullOrEmpty(tbTelefon.Text))
            {
                if (!int.TryParse(tbNaziv.Text, out int _) && tbNaziv.Text.Length < 30)
                {
                    if(!int.TryParse(tbAdresa.Text, out int _) && tbAdresa.Text.Length < 50)
                    {
                       if(!int.TryParse(tbTelefon.Text, out int _) && tbTelefon.Text.Length <= 12)
                        {

                            Firma nova = new Firma
                            {
                                Naziv = tbNaziv.Text,
                                Adresa = tbAdresa.Text,
                                Telefon = tbTelefon.Text,
                                UserNameUneo = unosUser

                            };
                            gl1.Firmas.InsertOnSubmit(nova);
                            try
                            {
                                gl1.SubmitChanges();
                                MessageBox.Show("Uspešno ste uneli novu firmu u bazu!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                tbNaziv.Clear();
                                tbAdresa.Clear();
                                tbTelefon.Clear();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Podaci ne mogu biti upisani u bazu! Pokušajte ponovo!" + ex);
                                OcistiPolja();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Telefon mora biti u formatu 000/000-0000 i ne sme biti duži od 12 karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                            OcistiPolja();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Adresa se mora sastojati od karaktera i brojeva, i ne sme biti duža od 50 karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        OcistiPolja();
                    }
                }
                else
                {
                    MessageBox.Show("Naziv se mora sastojati od karaktera i brojeva, i ne sme biti duži od 30 karaktera!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    OcistiPolja();
                }
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
