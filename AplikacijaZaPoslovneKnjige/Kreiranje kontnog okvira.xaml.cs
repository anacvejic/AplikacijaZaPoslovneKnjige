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
    /// Interaction logic for Kreiranje_kontnog_okvira.xaml
    /// </summary>
    public partial class Kreiranje_kontnog_okvira : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        string grupa;
        string klasa;
        string grupaNaziv;
        public Kreiranje_kontnog_okvira(string sifraGrupe, string nazivKlasa, string nazivGrupe)
        {
            InitializeComponent();
            grupa = sifraGrupe;
            klasa = nazivKlasa;
            grupaNaziv = nazivGrupe;
        }
        private void cisti()
        {
            textBoxNazivGrupeKonta.Clear();
            textBoxNazivKlase.Clear();
            textBoxSifraGrupeKonta.Clear();
        }

        private void BtnGrupaKonta_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNazivKlase.Text) || string.IsNullOrEmpty(textBoxSifraGrupeKonta.Text) || string.IsNullOrEmpty(textBoxNazivGrupeKonta.Text))
            {
                MessageBox.Show("Sva polja moraju biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                cisti();
            }
            else
            {
                if(!int.TryParse(textBoxNazivKlase.Text, out int _) && textBoxNazivKlase.Text.Length < 50)
                {
                    if(!int.TryParse(textBoxNazivGrupeKonta.Text, out int _) && textBoxNazivGrupeKonta.Text.Length < 100)
                    {
                        if(int.TryParse(textBoxSifraGrupeKonta.Text, out int _) && textBoxSifraGrupeKonta.Text.Length == 2)
                        {
                            if (!gl.GrupaKontas.Any(n => n.Grupa == textBoxSifraGrupeKonta.Text))
                            {
                                GrupaKonta nova = new GrupaKonta()
                                {
                                    Grupa = textBoxSifraGrupeKonta.Text,
                                    NazivGrupa = textBoxNazivGrupeKonta.Text,
                                    Klasa = textBoxNazivKlase.Text
                                };
                                gl.GrupaKontas.InsertOnSubmit(nova);
                                try
                                {
                                    gl.SubmitChanges();
                                    MessageBox.Show("Uspešno ste uneli novu grupu konta u bazu!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                    KreiranjeKontnogOkvira.dataGrid.ItemsSource = gl.GrupaKontas.ToList();
                                    textBoxNazivKlase.Clear();
                                    textBoxSifraGrupeKonta.Clear();
                                    textBoxNazivGrupeKonta.Clear();
                                    this.Hide();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Podaci ne mogu biti upisani u bazu! Pokušajte ponovo!" + ex);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Šifra grupe konta postoji u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                cisti();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Šifra grupe konta mora biti broj i mora se sastojati od dve cifre!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
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
        }

       
            
        }
    }

