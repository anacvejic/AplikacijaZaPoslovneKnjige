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
    /// Interaction logic for DinarskiPrometGlavneKnjige.xaml
    /// </summary>
    public partial class DinarskiPrometGlavneKnjige : Window
    {
        GlavnaKnjigaDataContext gl = new GlavnaKnjigaDataContext();
        DateTime? datum;
        DateTime? datum1;
        string datOd;
        string datDo;
        string kontoOd;
        string kontoDo;
        int idFirma;
        public DinarskiPrometGlavneKnjige()
        {
            InitializeComponent();
            FillFirma();
            FillKontoOd();
            FillKontoDo();
        }
        private void FillFirma()
        {
            var firma = from f in gl.Firmas
                        select new { f.IdFirma, f.Naziv };
            cmbFirma.ItemsSource = firma;
            cmbFirma.SelectedValuePath = "IdFirma";
            cmbFirma.DisplayMemberPath = "Naziv";
        }
        private void FillKontoOd()
        {
            var kontoOD = from k in gl.Kontas
                          select new { k.SifraKonta };
            cmbKontoOd.ItemsSource = kontoOD;
            cmbKontoOd.SelectedValuePath = "SifraKonta";
            cmbKontoOd.DisplayMemberPath = "SifraKonta";
        }
        private void FillKontoDo()
        {
            var konto = from k in gl.Kontas
                        select new { k.SifraKonta };
            cmbKontoDo.ItemsSource = konto;
            cmbKontoDo.SelectedValue = "SifraKonta";
            cmbKontoDo.DisplayMemberPath = "SifraKonta";
        }

        private void BtnPretrazi_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerDatOd.SelectedDate != null && datePicekrDatDo.SelectedDate != null &&
                cmbKontoOd.SelectedIndex > -1 && cmbKontoDo.SelectedIndex > -1 && cmbFirma.SelectedIndex > -1)
            {
                if (gl.Nalogs.Any(n => n.DatumNaloga == datum.Value))
                {

                    datOd = datum.Value.ToShortDateString();
                    datDo = datum1.Value.ToShortDateString();
                    kontoOd = cmbKontoOd.Text;
                    kontoDo = cmbKontoDo.Text;
                    idFirma = Convert.ToInt32(cmbFirma.SelectedValue);
                    StampaDinarskiPromet stampaDinarskiPromet = new StampaDinarskiPromet(datOd, datDo, kontoOd, kontoDo, idFirma);
                    stampaDinarskiPromet.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Datum od ne postoji u bazi!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti selektovana!", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DatePickerDatOd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            datum = picker.SelectedDate;
        }

        private void DatePicekrDatDo_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var picker = sender as DatePicker;
            datum1 = picker.SelectedDate;
        }
    }
}
