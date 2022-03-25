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
    /// Interaction logic for StampaNaloga.xaml
    /// </summary>
    public partial class StampaNaloga : Window
    {
        int idFirma;
        string brojNaloga;
        public StampaNaloga(int id, string brNalog)
        {
            InitializeComponent();
            Baza z = new Baza();
            System.Data.DataTable dtView = new System.Data.DataTable();


            idFirma = id;
            brojNaloga = brNalog;

            CrystalReport2 rptIzvestaj = new CrystalReport2();

            string cc = "";
            cc = "SELECT Nalog.VrstaNaloga, Firma.Naziv, Nalog.BrojNaloga,Nalog.VrstaDokumenta,Nalog.DatumNaloga,";
            cc = cc + "StavkaNaloga.Konto,StavkaNaloga.Opis,StavkaNaloga.PozivNaBroj,StavkaNaloga.DatumValute ,";
            cc = cc + "StavkaNaloga.Duguje,StavkaNaloga.Potrazuje,StavkaNaloga.Komada FROM Nalog ";
            cc = cc + "INNER JOIN StavkaNaloga ON Nalog.IdNalog = StavkaNaloga.IdNalog AND Nalog.IdFirma = '" + idFirma + "'";
            cc = cc + "INNER JOIN Firma ON Nalog.IdFirma = Firma.IdFirma ";
            cc = cc + "WHERE Nalog.IdFirma = '" + idFirma + "' AND Nalog.BrojNaloga = '" + brojNaloga + "'";

            Baza.rsReport = z.DajPodatke(cc, "Nalog");
            dtView = Baza.rsReport.Tables[0];
            rptIzvestaj.SetDataSource(dtView);
            crStampaNaloga.ViewerCore.ReportSource = rptIzvestaj;

        }
    }
}
