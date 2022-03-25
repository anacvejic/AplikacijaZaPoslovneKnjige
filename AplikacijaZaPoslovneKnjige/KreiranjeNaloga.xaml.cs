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
    /// Interaction logic for KreiranjeNaloga.xaml
    /// </summary>
    public partial class KreiranjeNaloga : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        DateTime? datum;
        DateTime? datum1;
        public KreiranjeNaloga()
        {
            InitializeComponent();
            FillFirma();
            groupBoxStavkeNaloga.IsEnabled = false;
            btnPregledStavkaNaloga.IsEnabled = false;
            dataGridStavkeNaloga.IsEnabled = false;
            btnUkupno.IsEnabled = false;
            textBoxDugujeUkupno.IsEnabled = false;
            textBoxSaldo.IsEnabled = false;
            textBoxUkupnoPotrazuje.IsEnabled = false;
        }
        
        private void FillFirma()
        {
            var firma = from f in gl.Firmas
                        select new { f.IdFirma, f.Naziv };
            cmbFirma.ItemsSource = firma;
            cmbFirma.SelectedValuePath = "IdFirma";
            cmbFirma.DisplayMemberPath = "Naziv";
        }

        private void BtUnesiZaglavlje_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxBrojNaloga.Text) && !string.IsNullOrWhiteSpace(textboxVsrtaDokumnta.Text) &&
                !string.IsNullOrWhiteSpace(textBoxVrstaNaloga.Text) && datePickerDatum.SelectedDate != null && cmbFirma.SelectedIndex > -1)
            {
                try
                {
                    if (gl.Nalogs.Any(n => n.BrojNaloga == textBoxBrojNaloga.Text))
                    {
                        MessageBox.Show("Broj naloga postoji u bazi! Pogledajte poglavlje nalozi da biste videli evidenciju!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textBoxBrojNaloga.Clear();
                        textBoxVrstaNaloga.Clear();
                        textboxVsrtaDokumnta.Clear();
                    }
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
            if (!string.IsNullOrWhiteSpace(textBoxBrojNaloga.Text) && !string.IsNullOrWhiteSpace(textboxVsrtaDokumnta.Text) &&
                !string.IsNullOrWhiteSpace(textBoxVrstaNaloga.Text) && datePickerDatum.SelectedDate != null && cmbFirma.SelectedIndex > -1)
            {
                if(int.TryParse(textBoxBrojNaloga.Text, out int _)&& textBoxBrojNaloga.Text.Length <=5)
                {
                    if(int.TryParse(textboxVsrtaDokumnta.Text, out int _))
                    {
                        if(int.TryParse(textBoxVrstaNaloga.Text, out int _))
                        {
                            Nalog novi = new Nalog
                            {
                                BrojNaloga = textBoxBrojNaloga.Text,
                                VrstaDokumenta = Convert.ToInt32(textboxVsrtaDokumnta.Text),
                                VrstaNaloga = Convert.ToInt32(textBoxVrstaNaloga.Text),
                                DatumNaloga = datum.Value,
                                IdFirma = Convert.ToInt32(cmbFirma.SelectedValue)
                            };
                            gl.Nalogs.InsertOnSubmit(novi);
                            try
                            {
                                gl.SubmitChanges();
                                MessageBox.Show("Zaglavlje naloga je uspešno kreirano!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                groupBoxStavkeNaloga.IsEnabled = true;
                                //FillBrojNaloga();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Podaci nisu upisani u bazu, pokušajte ponovo!" + ex);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Vrsta naloga mora biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                            textBoxVrstaNaloga.Clear();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vrsta dokumenta mora biti broj!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                        textboxVsrtaDokumnta.Clear();
                    }
                }
                else
                {
                    MessageBox.Show("Broj naloga mora biti broj i ne sme biti duži od 5 cifara!", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    textBoxBrojNaloga.Clear();
                }
            }

            else { MessageBox.Show("Sva polja moraju biti popunjena!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information); }
        }

        private void BtnUnesiStavkeNaloga_Click(object sender, RoutedEventArgs e)
        {
            int idNalog = (gl.Nalogs.Where(n => n.BrojNaloga.Equals(textBoxBrojNalogaBajdovan.Text)).Select(r => r.IdNalog).Single());
            if (!string.IsNullOrEmpty(textBoxKonto.Text) && !string.IsNullOrEmpty(textBoxOpis.Text) &&
                !string.IsNullOrEmpty(textboxDuguje.Text) &&
                !string.IsNullOrEmpty(textBoxPotrazuje.Text) &&
                string.IsNullOrEmpty(textBoxPozivNaBroj.Text) && string.IsNullOrEmpty(textBoxKomada.Text) &&
                datePicerStavkaNaloga.SelectedDate == null)
            {
                if(int.TryParse(textBoxKonto.Text, out int _) && textBoxKonto.Text.Length <= 8)
                {
                    if (!int.TryParse(textBoxOpis.Text, out int _))
                    {
                        if (float.TryParse(textboxDuguje.Text, out float _))
                        {
                            if (float.TryParse(textBoxPotrazuje.Text, out float _))
                            {
                                StavkaNaloga nova = new StavkaNaloga
                                {
                                    IdNalog = idNalog,
                                    Konto = textBoxKonto.Text,
                                    Opis = textBoxOpis.Text,
                                    Duguje = Convert.ToSingle(textboxDuguje.Text),
                                    Potrazuje = Convert.ToSingle(textBoxPotrazuje.Text)
                                };
                                gl.StavkaNalogas.InsertOnSubmit(nova);
                                try
                                {
                                    gl.SubmitChanges();
                                    MessageBox.Show("Uspešno ste kreirali nalog u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                    textBoxKonto.Clear();
                                    textBoxOpis.Clear();
                                    textboxDuguje.Clear();
                                    textBoxPotrazuje.Clear();
                                    textBoxPozivNaBroj.Clear();
                                    textBoxKomada.Clear();
                                    btnPregledStavkaNaloga.IsEnabled = true;
                                    dataGridStavkeNaloga.IsEnabled = true;

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
                            textboxDuguje.Clear();
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
            else if(!string.IsNullOrEmpty(textBoxKonto.Text) && !string.IsNullOrEmpty(textBoxOpis.Text) &&
                !string.IsNullOrEmpty(textboxDuguje.Text) &&
                !string.IsNullOrEmpty(textBoxPotrazuje.Text) &&
                !string.IsNullOrEmpty(textBoxPozivNaBroj.Text) && !string.IsNullOrEmpty(textBoxKomada.Text) &&
                datePicerStavkaNaloga.SelectedDate != null)
            {
                if (int.TryParse(textBoxKonto.Text, out int _) && textBoxKonto.Text.Length <= 8)
                {
                    if (!int.TryParse(textBoxOpis.Text, out int _))
                    {
                        if (float.TryParse(textboxDuguje.Text, out float _))
                        {
                            if (float.TryParse(textBoxPotrazuje.Text, out float _))
                            {
                                if (int.TryParse(textBoxPozivNaBroj.Text, out int _) && textBoxPozivNaBroj.Text.Length <= 25)
                                {
                                    if (int.TryParse(textBoxKomada.Text, out int _))
                                    {
                                        StavkaNaloga nova = new StavkaNaloga
                                        {
                                            IdNalog = idNalog,
                                            Konto = textBoxKonto.Text,
                                            Opis = textBoxOpis.Text,
                                            Duguje = Convert.ToSingle(textboxDuguje.Text),
                                            Potrazuje = Convert.ToSingle(textBoxPotrazuje.Text),
                                            PozivNaBroj = textBoxPozivNaBroj.Text,
                                            DatumValute = datum1.Value,
                                            Komada = Convert.ToInt32(textBoxKomada.Text)
                                        };
                                        gl.StavkaNalogas.InsertOnSubmit(nova);
                                        try
                                        {
                                            gl.SubmitChanges();
                                            MessageBox.Show("Uspešno ste kreirali nalog u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                                            textBoxKonto.Clear();
                                            textBoxOpis.Clear();
                                            textboxDuguje.Clear();
                                            textBoxPotrazuje.Clear();
                                            textBoxPozivNaBroj.Clear();
                                            textBoxKomada.Clear();
                                            btnPregledStavkaNaloga.IsEnabled = true;
                                            dataGridStavkeNaloga.IsEnabled = true;

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
                            textboxDuguje.Clear();
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
                MessageBox.Show("Polja broj naloga, konto, opis, duguje, potražuje", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        

        private void BtnUkupno_Click(object sender, RoutedEventArgs e)
        {
            int idNalog = (gl.Nalogs.Where(n => n.BrojNaloga.Equals(textBoxBrojNalogaBajdovan.Text)).Select(r => r.IdNalog).Single());
            double potrazuje = (from n in gl.Nalogs
                                join st in gl.StavkaNalogas
                                on n.IdNalog equals st.IdNalog
                                where n.IdFirma.Equals(cmbFirma.SelectedValue) && st.IdNalog.Equals(idNalog)
                                select st.Potrazuje).Sum();

            textBoxUkupnoPotrazuje.Text = potrazuje.ToString();

            double duguje = (from n in gl.Nalogs
                             join st in gl.StavkaNalogas
                             on n.IdNalog equals st.IdNalog
                             where n.IdFirma.Equals(cmbFirma.SelectedValue) && st.IdNalog.Equals(idNalog)
                             select st.Duguje).Sum();
            textBoxDugujeUkupno.Text = duguje.ToString();

            textBoxSaldo.Text = (duguje - potrazuje).ToString();
        }

        private void BtnStampaj_Click(object sender, RoutedEventArgs e)
        {
            int idFirma = Convert.ToInt32(cmbFirma.SelectedValue);
            string brojNaloga = textBoxBrojNalogaBajdovan.Text;
            StampaNaloga stampa = new StampaNaloga(idFirma, brojNaloga);
            stampa.ShowDialog();
        }

        private void DatePickerDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

            var picker = sender as DatePicker;
            datum = picker.SelectedDate;
        }

        private void DatePicerStavkaNaloga_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            datum1 = picker.SelectedDate;
        }

        private void BtnPregledStavkaNaloga_Click(object sender, RoutedEventArgs e)
        {
            var data = from n in gl.Nalogs
                       join st in gl.StavkaNalogas
                       on n.IdNalog equals st.IdNalog
                       where n.IdFirma.Equals(cmbFirma.SelectedValue) && n.BrojNaloga.Equals(textBoxBrojNalogaBajdovan.Text)
                       select new { n.BrojNaloga, st.Konto, st.Opis, st.PozivNaBroj, st.DatumValute, st.Duguje, st.Potrazuje, st.Komada };

            dataGridStavkeNaloga.ItemsSource = data;

            btnUkupno.IsEnabled = true;
            textBoxDugujeUkupno.IsEnabled = true;
            textBoxSaldo.IsEnabled = true;
            textBoxUkupnoPotrazuje.IsEnabled = true;
        }
    }
}
