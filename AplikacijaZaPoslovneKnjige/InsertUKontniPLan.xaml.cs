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
    /// Interaction logic for InsertUKontniPLan.xaml
    /// </summary>
    public partial class InsertUKontniPLan : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        public InsertUKontniPLan()
        {
            InitializeComponent();
        }
        private void FillSifraKonta() {
            var sifraKonta = from k in gl.Kontas
                             select new { k.SifraKonta };
            cmbSifraKonta.ItemsSource = sifraKonta;
            cmbSifraKonta.SelectedValuePath = "SifraKonta";
            cmbSifraKonta.DisplayMemberPath = "SifraKonta";
        }
        private void FillFirma() {
            var firma = from f in gl.Firmas
                        select new { f.IdFirma, f.Naziv };
            cmbFirma.ItemsSource = firma;
            cmbFirma.SelectedValuePath = "IdFirma";
            cmbFirma.DisplayMemberPath = "Naziv";
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillSifraKonta();
            FillFirma();
        }
        private void BtnUnesiKonto_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSifraKonta.SelectedIndex > -1 && !string.IsNullOrEmpty(textboxIndetifikacioniBroj.Text) &&
                cmbOpis.SelectedIndex > -1 && cmbFirma.SelectedIndex > -1)
            {
                if (int.TryParse(textboxIndetifikacioniBroj.Text, out int _) && textboxIndetifikacioniBroj.Text.Length <=3)
                {
                    if (!gl.KontniPlans.Any(n => n.SifraKonta == cmbSifraKonta.Text &&
                     n.IdentifikacioniBroj == textboxIndetifikacioniBroj.Text))
                    {
                        KontniPlan novi = new KontniPlan
                        {
                            SifraKonta = cmbSifraKonta.SelectedValue.ToString(),
                            IdentifikacioniBroj = textboxIndetifikacioniBroj.Text,
                            Opis = cmbOpis.Text,
                            IdFirma = Convert.ToInt32(cmbFirma.SelectedValue)
                        };
                        gl.KontniPlans.InsertOnSubmit(novi);
                        try
                        {
                            gl.SubmitChanges();
                            MessageBox.Show("Uspešno ste uneli konto u kontni plan!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                            Kontni_plan.dataGrid.ItemsSource = gl.KontniPlans.ToList();
                            this.Hide();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Podaci ne mogu biti upisani u bazu! Pokušajte ponovo!" + ex.Message);
                        }
                    }
                    else
                    {
                      MessageBox.Show("Šifra konta sa unetim indetifikacionim brojem već postoje u bazi! Molimo Vas pregledajte evidenciju", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        textboxIndetifikacioniBroj.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Indetifikacioni broj mora biti broj i mora imati tri cifre!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    textboxIndetifikacioniBroj.Clear();
                }

            }
            else { MessageBox.Show("Sva polja moraju  biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning); }
        }
        private void CmbSifraKonta_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (cmbSifraKonta.SelectedIndex != -1)
            {
                var opis = from k in gl.Kontas
                           where k.SifraKonta.Equals(cmbSifraKonta.SelectedValue)
                           select new { k.OpisKonta };
                cmbOpis.ItemsSource = opis;
                cmbOpis.SelectedValuePath = "OpisKonta";
                cmbOpis.DisplayMemberPath = "OpisKonta";
            }
        }
    }
}
