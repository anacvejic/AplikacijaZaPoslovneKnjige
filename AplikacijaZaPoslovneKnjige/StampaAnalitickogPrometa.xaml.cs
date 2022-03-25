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
    /// Interaction logic for StampaAnalitickogPrometa.xaml
    /// </summary>
    public partial class StampaAnalitickogPrometa : Window
    {
        DateTime datOd;
        DateTime datDo;
        string kontoOd;
        string kontoDo;
        int idFirma;
        public StampaAnalitickogPrometa(string dat, string dat1, string konto1, string konto2, int id)
        {
            InitializeComponent();
            datOd = Convert.ToDateTime(dat);
            datDo = Convert.ToDateTime(dat1);
            kontoOd = konto1;
            kontoDo = konto2;
            idFirma = id;
            Baza z = new Baza();
            System.Data.DataTable dtView = new System.Data.DataTable();

            Analiticki rptIzvestaj = new Analiticki();
            string cc = "";
            cc = "SELECT Firma.Naziv AS Firma, Konto,StavkaNaloga.Opis AS Naziv,";
            cc = cc + "(CASE WHEN Nalog.DatumNaloga BETWEEN CONVERT(DATETIME, '" + datOd + "', 104) AND CONVERT(DATETIME,'" + datDo + "',104)";
            cc = cc + "THEN StavkaNaloga.Duguje ELSE 0 END) AS TekuceDuguje,";
            cc = cc + "(CASE WHEN Nalog.DatumNaloga BETWEEN CONVERT(DATETIME, '" + datOd + "', 104) AND CONVERT(DATETIME, '" + datDo + "', 104)";
            cc = cc + "THEN StavkaNaloga.Potrazuje ELSE 0 END) AS TekucePotrazuje,";
            cc = cc + "(CASE WHEN Nalog.DatumNaloga < CONVERT(DATETIME, '" + datOd + "', 104)";
            cc = cc + "THEN StavkaNaloga.Duguje ELSE 0 END) AS PocetnoDuguje,";
            cc = cc + "(CASE WHEN Nalog.DatumNaloga < CONVERT(DATETIME, '" + datOd + "', 104)";
            cc = cc + "THEN StavkaNaloga.Potrazuje ELSE 0 END) AS PocetnoPotrazuje,";
            cc = cc + "CONVERT(DATETIME, '01.02.2020', 104) AS DatumOd, CONVERT(DATETIME, '" + datDo + "', 104) As DatumDo FROM StavkaNaloga ";
            cc = cc + "INNER JOIN Nalog ON StavkaNaloga.IdNalog = Nalog.IdNalog ";
            cc = cc + "INNER JOIN Firma ON Nalog.IdFirma = Firma.IdFirma ";
            cc = cc + "WHERE SUBSTRING(Konto,1,3) BETWEEN '" + kontoOd + "' AND '" + kontoDo + "' ";
            cc = cc + "AND Nalog.DatumNaloga <= CONVERT(DATETIME, '" + datDo + "', 104) AND Nalog.IdFirma = '" + idFirma + "' ";
            cc = cc + "ORDER BY Konto ASC";

            Baza.rsReport = z.DajPodatke(cc, "Nalog");
            dtView = Baza.rsReport.Tables[0];
            rptIzvestaj.SetDataSource(dtView);
            rptStampaAnalitickogPrometa.ViewerCore.ReportSource = rptIzvestaj;
        }
    }
}
