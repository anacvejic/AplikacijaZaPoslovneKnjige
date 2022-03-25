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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace AplikacijaZaPoslovneKnjige
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public MainWindow()
        {
            InitializeComponent();
        }
        private string user { get; set; }
        

        private void BtUlogujSe_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxKorisnicko.Text) && !string.IsNullOrWhiteSpace(passSifra.Password))
            {

                try
                {
                    if (gl.Korisniks.Any(k => k.UserName == textBoxKorisnicko.Text && k.PassWord == passSifra.Password))
                    {
                        user = textBoxKorisnicko.Text;
                        Pocetna p = new Pocetna(user);
                        Unos_nove_firme novaFirma = new Unos_nove_firme(user);
                        p.ShowDialog();


                    }
                    else
                    {

                        MessageBox.Show("Korisničko ime ili šifra ne postoje u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        textBoxKorisnicko.Clear();
                        passSifra.Clear();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
            else
            {
                MessageBox.Show("Polja korisničko ime i šifra su obavezna!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
