using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for NoviNalog.xaml
    /// </summary>
    public partial class NoviNalog : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        DateTime? datum;
        DateTime? datum1;
        public NoviNalog()
        {
            InitializeComponent();
            FillFirma();
            NalogNevidljiv();
            StavkeNalogaNevidljive();
            
        }
        private void NalogVidljiv()
        {
            dgvZaglavljeNaloga.IsEnabled = true;
            textBoxVrstaNaloga.IsEnabled = true;
            cmbFirma.IsEnabled = true;
            textBoxVrstaDokumenta.IsEnabled = true;
            datePickerDatumNaloga.IsEnabled = true;
            btnIzmeni.IsEnabled = true;
        }
        private void StavkeVidljive()
        {
            dgvStavkeNaloga.IsEnabled = true;
            gbStavkeNaloga.IsEnabled = true;
            textBoxKonto.IsEnabled = true;
            textBoxKomada.IsEnabled = true;
            textBoxOpis.IsEnabled = true;
            textBoxPozivNaBroj.IsEnabled = true;
            textBoxDuguje.IsEnabled = true;
            textBoxPozivNaBroj.IsEnabled = true;
            datePicerStavkaNaloga.IsEnabled = true;
        }
        private void NalogNevidljiv()
        {
            dgvZaglavljeNaloga.IsEnabled = false;
            textBoxVrstaNaloga.IsEnabled = false;
            cmbFirma.IsEnabled = false;
            textBoxVrstaDokumenta.IsEnabled = false;
            datePickerDatumNaloga.IsEnabled = false;
            btnIzmeni.IsEnabled = false;
        }
        private void StavkeNalogaNevidljive()
        {
            dgvStavkeNaloga.IsEnabled = false;
            gbStavkeNaloga.IsEnabled = false;
            textBoxKonto.IsEnabled = false;
            textBoxKomada.IsEnabled = false;
            textBoxOpis.IsEnabled = false;
            textBoxPozivNaBroj.IsEnabled = false;
            textBoxDuguje.IsEnabled = false;
            textBoxPozivNaBroj.IsEnabled = false;
            datePicerStavkaNaloga.IsEnabled = false;
        }
        private void FillFirma()
        {
            var firma = from f in gl.Firmas
                        select new { f.IdFirma, f.Naziv };
            cmbNazivFirme.ItemsSource = firma;
            cmbNazivFirme.SelectedValuePath = "IdFirma";
            cmbNazivFirme.DisplayMemberPath = "Naziv";
            cmbFirma.ItemsSource = firma;
            cmbFirma.SelectedValuePath = "IdFirma";
            cmbFirma.DisplayMemberPath = "Naziv";
        }

        private void DatePickerDatumNaloga_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            datum = picker.SelectedDate;
        }

        private void BtnPretrazi_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxSifra.Text) && cmbNazivFirme.SelectedIndex > -1)
            {
                if (int.TryParse(textBoxSifra.Text, out int _))
                {
                    if (gl.Nalogs.Any(br => br.BrojNaloga == textBoxSifra.Text && br.IdFirma == Convert.ToInt32(cmbNazivFirme.SelectedValue)))
                    {
                        dgvZaglavljeNaloga.IsEnabled = true;
                        var nalogZaglavlje = from n in gl.Nalogs
                                             where n.BrojNaloga.Equals(textBoxSifra.Text) && n.IdFirma.Equals(cmbNazivFirme.SelectedValue)
                                             select new { n.IdNalog, n.BrojNaloga, n.VrstaDokumenta, n.VrstaNaloga, n.DatumNaloga };
                        dgvZaglavljeNaloga.ItemsSource = nalogZaglavlje;
                        NalogVidljiv();
                        StavkeVidljive();
                        FillStavkeNaloga();
                    }
                    else
                    {
                        MessageBox.Show("Uneti broj naloga ne postoji u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                        textBoxSifra.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Broj naloga mora biti broj!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                    textBoxSifra.Clear();
                }
            }
            else
            {
                MessageBox.Show("Polje šifra firme mora biti popunjeno i firma mora biti odabrana!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
       
        private void BtnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(textBoxBrojNaloga.Text) 
                && !string.IsNullOrWhiteSpace(textBoxVrstaNaloga.Text) &&
                !string.IsNullOrWhiteSpace(textBoxVrstaDokumenta.Text) && 
                datePickerDatumNaloga.SelectedDate != null && cmbFirma.SelectedIndex > -1)
            {
                
                    if (int.TryParse(textBoxBrojNaloga.Text, out int _) &&
                        int.TryParse(textBoxVrstaDokumenta.Text, out int _) &&
                        int.TryParse(textBoxVrstaNaloga.Text, out int _))
                    {
                    int id = Convert.ToInt32(textBoxIdNalog.Text);

                    Nalog izmena = (from n in gl.Nalogs
                                        where n.IdNalog.Equals(id)
                                        select n).Single();
                        izmena.BrojNaloga = textBoxBrojNaloga.Text;
                        izmena.DatumNaloga = Convert.ToDateTime(datePickerDatumNaloga.SelectedDate.Value.ToShortDateString());
                        izmena.IdFirma = Convert.ToInt32(cmbFirma.SelectedValue);
                        izmena.VrstaDokumenta = Convert.ToInt32(textBoxVrstaDokumenta.Text);
                        izmena.VrstaNaloga = Convert.ToInt32(textBoxVrstaNaloga.Text);
                        try
                        {
                            gl.SubmitChanges();
                            MessageBox.Show("Uspešno ste izmenili podatke u bazi!", "Obaveštenje", MessageBoxButton.OK);
                        var nalogZaglavlje = from n in gl.Nalogs
                                             where n.BrojNaloga.Equals(textBoxSifra.Text) && n.IdFirma.Equals(cmbNazivFirme.SelectedValue)
                                             select new { n.IdNalog, n.BrojNaloga, n.VrstaDokumenta, n.VrstaNaloga, n.DatumNaloga };
                        dgvZaglavljeNaloga.ItemsSource = nalogZaglavlje;
                    }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ne možete izmeniti podatke u bazi!" + ex.Message);
                        }
                    
                    }
                    else
                    {
                        MessageBox.Show("Polja broj naloga, vrsta dokumenta i vrsta naloga moraju biti broj!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                
            }
            else
            {
                MessageBox.Show("Sva polja su obavezna!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnIzmeniStavku_Click(object sender, RoutedEventArgs e)
        {
            
            int idNalog = (gl.Nalogs.Where(n => n.BrojNaloga.Equals(textBoxSifra.Text) && n.IdFirma.Equals(Convert.ToInt32(cmbNazivFirme.SelectedValue))).Select(r => r.IdNalog).Single());
            if (!string.IsNullOrEmpty(textBoxKonto.Text) && !string.IsNullOrEmpty(textBoxOpis.Text) &&
                 !string.IsNullOrEmpty(textBoxDuguje.Text) &&
                 !string.IsNullOrEmpty(textBoxPotrazuje.Text) &&
                 string.IsNullOrEmpty(textBoxPozivNaBroj.Text) && string.IsNullOrEmpty(textBoxKomada.Text) &&
                 datePicerStavkaNaloga.SelectedDate == null)
            {
                if (int.TryParse(textBoxKonto.Text, out int _) && textBoxKonto.Text.Length <= 8)
                {
                    if (!int.TryParse(textBoxOpis.Text, out int _))
                    {
                        if (float.TryParse(textBoxDuguje.Text, out float _))
                        {
                            if (float.TryParse(textBoxPotrazuje.Text, out float _))
                            {
                                int idStavka = Convert.ToInt32(textBoxIdStavka.Text);
                                StavkaNaloga izmena = (from s in gl.StavkaNalogas
                                                       where s.IdStavka.Equals(idStavka)
                                                       select s).Single();
                                izmena.Konto = textBoxKonto.Text;
                                izmena.Opis = textBoxOpis.Text;
                                izmena.Duguje = Convert.ToSingle(textBoxDuguje.Text);
                                izmena.Potrazuje = Convert.ToSingle(textBoxPotrazuje.Text);

                                try
                                {
                                    gl.SubmitChanges();
                                    MessageBox.Show("Uspešno ste izmenili nalog u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                    FillStavkeNaloga();
                                    textBoxKonto.Clear();
                                    textBoxOpis.Clear();
                                    textBoxDuguje.Clear();
                                    textBoxPotrazuje.Clear();
                                    textBoxPozivNaBroj.Clear();
                                    textBoxKomada.Clear();
                                }
                                catch (Exception ex) { MessageBox.Show("Greška pri upisu u bazu!" + ex); }
                            }
                            else
                            {
                                MessageBox.Show("Potražuje mora biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                textBoxPotrazuje.Clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Duguje mora biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxDuguje.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opis ne sme biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxOpis.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Broj konta mora biti broj i ne sme biti duži od 8 cifara", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    textBoxKonto.Clear();
                }
            }
            else if (!string.IsNullOrEmpty(textBoxKonto.Text) && !string.IsNullOrEmpty(textBoxOpis.Text) &&
                !string.IsNullOrEmpty(textBoxDuguje.Text) &&
                !string.IsNullOrEmpty(textBoxPotrazuje.Text) &&
                !string.IsNullOrEmpty(textBoxPozivNaBroj.Text) && !string.IsNullOrEmpty(textBoxKomada.Text) &&
                datePicerStavkaNaloga.SelectedDate != null)
            {
                if (int.TryParse(textBoxKonto.Text, out int _) && textBoxKonto.Text.Length <= 8)
                {
                    if (!int.TryParse(textBoxOpis.Text, out int _))
                    {
                        if (float.TryParse(textBoxDuguje.Text, out float _))
                        {
                            if (float.TryParse(textBoxPotrazuje.Text, out float _))
                            {
                                if (int.TryParse(textBoxPozivNaBroj.Text, out int _) && textBoxPozivNaBroj.Text.Length <= 25)
                                {
                                    if (int.TryParse(textBoxKomada.Text, out int _))
                                    {
                                        int idStavka = Convert.ToInt32(textBoxIdStavka.Text);
                                        StavkaNaloga izmena = (from s in gl.StavkaNalogas
                                                               where s.IdStavka.Equals(idStavka)
                                                               select s).Single();
                                        izmena.Konto = textBoxKonto.Text;
                                        izmena.Opis = textBoxOpis.Text;
                                        izmena.PozivNaBroj = textBoxPozivNaBroj.Text;
                                        izmena.DatumValute = datum1.Value;
                                        izmena.Duguje = Convert.ToSingle(textBoxDuguje.Text);
                                        izmena.Potrazuje = Convert.ToSingle(textBoxPotrazuje.Text);
                                        izmena.Komada = Convert.ToInt32(textBoxKomada.Text);
                                        try
                                        {
                                            gl.SubmitChanges();
                                            MessageBox.Show("Uspešno ste izmenili nalog u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                            FillStavkeNaloga();
                                            textBoxKonto.Clear();
                                            textBoxOpis.Clear();
                                            textBoxDuguje.Clear();
                                            textBoxPotrazuje.Clear();
                                            textBoxPozivNaBroj.Clear();
                                            textBoxKomada.Clear();
                                        }
                                        catch (Exception ex) { MessageBox.Show("Greška pri upisu u bazu!" + ex); }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Polje komada mora biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        textBoxKomada.Clear();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Poziv na broj mora biti broj i ne sme biti duži od 5 cifara", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    textBoxPozivNaBroj.Clear();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Potražuje mora biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                textBoxPotrazuje.Clear();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Duguje mora biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxDuguje.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Opis ne sme biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxOpis.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Broj konta mora biti broj i ne sme biti duži od 8 cifara", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    textBoxKonto.Clear();
                }
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena da bi se podaci izmenili!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        private void DataPickerStavkeNaloga_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            datum1 = picker.SelectedDate;
        }
        private void FillStavkeNaloga()
        {
            var nalogStavke = from s in gl.StavkaNalogas
                              join n in gl.Nalogs
                              on s.IdNalog equals n.IdNalog
                              where n.BrojNaloga.Equals(textBoxSifra.Text) && n.IdFirma.Equals(cmbNazivFirme.SelectedValue)
                              select new { s.IdStavka, s.Konto, s.Opis, s.PozivNaBroj, s.DatumValute, s.Duguje, s.Potrazuje, s.Komada };
            dgvStavkeNaloga.ItemsSource = nalogStavke;
        }
    }
}
