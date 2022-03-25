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
    /// Interaction logic for PretragaNaloga.xaml
    /// </summary>
    public partial class PretragaNaloga : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public PretragaNaloga()
        {
            InitializeComponent();
            dataGridZaglavljeNaloga.IsEnabled = false;
            dataGridStavkeNaloga.IsEnabled = false;
            textBoxDuguje.IsEnabled = false;
            textBoxPotrazuje.IsEnabled = false;
            textBoxSaldo.IsEnabled = false;
            tblUkupno.IsEnabled = false;
            tblSaldo.IsEnabled = false;
            btnOdstampaj.IsEnabled = false;
        }
        private void FillFirma()
        {
            var firma = from f in gl.Firmas
                        select new { f.IdFirma, f.Naziv };
            cmbNazivFirme.ItemsSource = firma;
            cmbNazivFirme.SelectedValuePath = "IdFirma";
            cmbNazivFirme.DisplayMemberPath = "Naziv";
        }
        private void BtnPretraži_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxBrojNaloga.Text) && cmbNazivFirme.SelectedIndex > -1)
            {
                if(int.TryParse(textBoxBrojNaloga.Text, out int _))
                {
                    if(gl.Nalogs.Any(br=>br.BrojNaloga == textBoxBrojNaloga.Text && br.IdFirma==Convert.ToInt32(cmbNazivFirme.SelectedValue)))
                    {
                        dataGridZaglavljeNaloga.IsEnabled = true;
                        dataGridStavkeNaloga.IsEnabled = true;
                        textBoxDuguje.IsEnabled = true;
                        textBoxPotrazuje.IsEnabled = true;
                        textBoxSaldo.IsEnabled = true;
                        tblUkupno.IsEnabled = true;
                        tblSaldo.IsEnabled = true;
                        btnOdstampaj.IsEnabled = true;

                        var zaglavleNaloga = from n in gl.Nalogs
                                             join f in gl.Firmas
                                             on n.IdFirma equals f.IdFirma
                                             where n.BrojNaloga.Equals(textBoxBrojNaloga.Text) && n.IdFirma.Equals(cmbNazivFirme.SelectedValue)
                                             select new { n.BrojNaloga, n.VrstaDokumenta, n.VrstaNaloga, n.DatumNaloga, f.Naziv };
                        dataGridZaglavljeNaloga.ItemsSource = zaglavleNaloga;

                        var stavkeNaloga = from n in gl.Nalogs
                                           join s in gl.StavkaNalogas
                                           on n.IdNalog equals s.IdNalog
                                           where n.BrojNaloga.Equals(textBoxBrojNaloga.Text) && n.IdFirma.Equals(cmbNazivFirme.SelectedValue)
                                           select new {s.Konto, s.Opis, s.PozivNaBroj, s.DatumValute, s.Duguje, s.Potrazuje, s.Komada };
                        dataGridStavkeNaloga.ItemsSource = stavkeNaloga;

                        double potrazuje = (from n in gl.Nalogs
                                            join st in gl.StavkaNalogas
                                            on n.IdNalog equals st.IdNalog
                                            where n.IdFirma.Equals(cmbNazivFirme.SelectedValue) && n.BrojNaloga.Equals(textBoxBrojNaloga.Text)
                                            select st.Potrazuje).Sum();
                        textBoxPotrazuje.Text = potrazuje.ToString();

                        double duguje = (from n in gl.Nalogs
                                            join st in gl.StavkaNalogas
                                            on n.IdNalog equals st.IdNalog
                                            where n.IdFirma.Equals(cmbNazivFirme.SelectedValue) && n.BrojNaloga.Equals(textBoxBrojNaloga.Text)
                                            select st.Duguje).Sum();
                        textBoxDuguje.Text = duguje.ToString();

                        textBoxSaldo.Text = (duguje - potrazuje).ToString();
                    }
                    else
                    {
                        MessageBox.Show("Uneti broj naloga ne postoji u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        textBoxBrojNaloga.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Broj naloga mora biti broj!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxBrojNaloga.Clear();
                }
            }
            else
            {
                MessageBox.Show("Polje šifra firme mora biti popunjeno i firma mora biti odabrana!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillFirma();
        }

        private void BtnOdstampaj_Click(object sender, RoutedEventArgs e)
        {
            int idFirma = Convert.ToInt32(cmbNazivFirme.SelectedValue);
            string brojNaloga = textBoxBrojNaloga.Text;
            StampaNaloga stampa = new StampaNaloga(idFirma, brojNaloga);
            stampa.ShowDialog();
        }
    }
}
