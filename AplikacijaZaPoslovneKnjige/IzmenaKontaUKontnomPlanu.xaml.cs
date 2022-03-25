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
    /// Interaction logic for IzmenaKontaUKontnomPlanu.xaml
    /// </summary>
    public partial class IzmenaKontaUKontnomPlanu : Window
    {
        private static GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        int idKP;
        string idBroj;
        public IzmenaKontaUKontnomPlanu(int id, string identifikacioni)
        {
            InitializeComponent();
            idKP = id;
            idBroj = identifikacioni;
            textBoxIndetifikacioniBroj.Text = idBroj;
            FillFirma();
            FillSifraKonta();
         }
        private void FillSifraKonta()
        {
            var sifraKonta = from k in gl.Kontas
                             select new { k.SifraKonta };
            cmbSifraKonta.ItemsSource = sifraKonta;
            cmbSifraKonta.SelectedValuePath = "SifraKonta";
            cmbSifraKonta.DisplayMemberPath = "SifraKonta";
        }
        private void FillFirma()
        {
            var firma = from f in gl.Firmas
                        select new { f.IdFirma, f.Naziv };
            cmbNazivFirme.ItemsSource = firma;
            cmbNazivFirme.SelectedValuePath = "IdFirma";
            cmbNazivFirme.DisplayMemberPath = "Naziv";
        }

        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSifraKonta.SelectedIndex > -1
                 && !string.IsNullOrWhiteSpace(textBoxIndetifikacioniBroj.Text)
                 && cmbOpis.SelectedIndex == -1 && cmbNazivFirme.SelectedIndex == -1)
            {
                if (int.TryParse(textBoxIndetifikacioniBroj.Text, out int _) && textBoxIndetifikacioniBroj.Text.Length <= 3)
                {
                    KontniPlan izmena = (from kp in gl.KontniPlans
                                         where kp.Id.Equals(idKP)
                                         select kp).Single();
                    izmena.SifraKonta = Convert.ToString(cmbSifraKonta.SelectedValue);
                    izmena.IdentifikacioniBroj = textBoxIndetifikacioniBroj.Text;
                    try
                    {
                        gl.SubmitChanges();
                        MessageBox.Show("Šifra konta je uspešno izemnjena u bazi", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        Kontni_plan.dataGrid.ItemsSource = gl.KontniPlans.ToList();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Šifra konta ne može biti izmenjena!" + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Indetifikacioni broj mora biti broj i sastojati se od tri cifre!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxIndetifikacioniBroj.Clear();
                }
            }
            else if (cmbSifraKonta.SelectedIndex > -1
                && !string.IsNullOrWhiteSpace(textBoxIndetifikacioniBroj.Text)
                && cmbOpis.SelectedIndex == -1 && cmbNazivFirme.SelectedIndex == -1)
            {
                if (int.TryParse(textBoxIndetifikacioniBroj.Text, out int _) && textBoxIndetifikacioniBroj.Text.Length <= 3)
                {
                    KontniPlan izmena = (from kp in gl.KontniPlans
                                         where kp.Id.Equals(idKP)
                                         select kp).Single();
                    izmena.SifraKonta = Convert.ToString(cmbSifraKonta.SelectedValue);
                    izmena.IdentifikacioniBroj = textBoxIndetifikacioniBroj.Text;
                    try
                    {
                        gl.SubmitChanges();
                        MessageBox.Show("Podaci su uspešno izmenjeni u bazi", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        Kontni_plan.dataGrid.ItemsSource = gl.KontniPlans.ToList();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Indetifikacioni broj ne može biti izmenjen!" + ex.Message);
                        textBoxIndetifikacioniBroj.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Indetifikacioni broj mora biti broj i mora se sastojati od tri cifre!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if (cmbSifraKonta.SelectedIndex > -1
                 && !string.IsNullOrWhiteSpace(textBoxIndetifikacioniBroj.Text)
                 && cmbOpis.SelectedIndex > -1 && cmbNazivFirme.SelectedIndex == -1)
            {
                if (int.TryParse(textBoxIndetifikacioniBroj.Text, out int _) && textBoxIndetifikacioniBroj.Text.Length <= 3)
                {
                    KontniPlan izmena = (from kp in gl.KontniPlans
                                         where kp.Id.Equals(idKP)
                                         select kp).Single();
                    izmena.SifraKonta = Convert.ToString(cmbSifraKonta.SelectedValue);
                    izmena.IdentifikacioniBroj = textBoxIndetifikacioniBroj.Text;
                    izmena.Opis = cmbOpis.Text;
                    try
                    {
                        gl.SubmitChanges();
                        MessageBox.Show("Podaci su uspešno izmenjeni u bazi", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        Kontni_plan.dataGrid.ItemsSource = gl.KontniPlans.ToList();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Indetifikacioni broj ne može biti izmenjen!" + ex.Message);
                        textBoxIndetifikacioniBroj.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Indetifikacioni broj mora biti broj i mora se sastojati od tri cifre!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else if(cmbSifraKonta.SelectedIndex > -1
                 && !string.IsNullOrWhiteSpace(textBoxIndetifikacioniBroj.Text)
                 && cmbOpis.SelectedIndex > -1 && cmbNazivFirme.SelectedIndex > -1)
            {
                if (int.TryParse(textBoxIndetifikacioniBroj.Text, out int _) && textBoxIndetifikacioniBroj.Text.Length <= 3)
                {
                    KontniPlan izmena = (from kp in gl.KontniPlans
                                         where kp.Id.Equals(idKP)
                                         select kp).Single();
                    izmena.SifraKonta = Convert.ToString(cmbSifraKonta.SelectedValue);
                    izmena.IdentifikacioniBroj = textBoxIndetifikacioniBroj.Text;
                    izmena.Opis = cmbOpis.Text;
                    izmena.IdFirma = Convert.ToInt32(cmbNazivFirme.SelectedValue);
                    try
                    {
                        gl.SubmitChanges();
                        MessageBox.Show("Podaci su uspešno izmenjeni u bazi", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        Kontni_plan.dataGrid.ItemsSource = gl.KontniPlans.ToList();
                        this.Hide();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Podaci ne mogu biti izmenjeni!" + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Indetifikacioni broj mora biti broj i mora se sastojati od tri cifre!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxIndetifikacioniBroj.Clear();
                }
            }
            else
            {
                MessageBox.Show("Morate uneti neki izmenu da bi bila zapamćena u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CmbSifraKonta_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
