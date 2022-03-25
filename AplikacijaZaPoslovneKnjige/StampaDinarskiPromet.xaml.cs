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
    /// Interaction logic for StampaDinarskiPromet.xaml
    /// </summary>
    public partial class StampaDinarskiPromet : Window
    {
        DateTime datOd;
        DateTime datDo;
        string kontoOd;
        string kontoDo;
        int idFirma;
        public StampaDinarskiPromet(string dat, string dat1, string konto1, string konto2, int idF)
        {
            InitializeComponent();
            datOd = Convert.ToDateTime(dat);
            datDo = Convert.ToDateTime(dat1);
            kontoOd = konto1;
            kontoDo = konto2;
            idFirma = idF;
            Baza z = new Baza();
            System.Data.DataTable dtView = new System.Data.DataTable();

            CrystalReport3 rptIzvestaj = new CrystalReport3();
            string cc = "";
            
            cc = "SELECT Konto,Opis,Duguje,Potrazuje,Duguje AS SumaDuguje,Potrazuje AS SumaPotrazuje,CONVERT(DATETIME,'" + datOd + "',104) AS DatumOd , CONVERT(DATETIME,'" + datDo + "',104) As DatumDo FROM StavkaNaloga ";
            cc = cc + "INNER JOIN Nalog ON StavkaNaloga.IdNalog = Nalog.IdNalog ";
            cc = cc + "WHERE SUBSTRING(Konto,1,3) BETWEEN '" + kontoOd + "' AND '" + kontoDo + "' ";
            cc = cc + "AND Nalog.DatumNaloga BETWEEN CONVERT(DATETIME,'" + datOd + "',104) AND CONVERT(DATETIME,'" + datDo + "',104) ";
            cc = cc + " AND Nalog.IdFirma = '" + idFirma + "'" ; 
            Baza.rsReport = z.DajPodatke(cc, "Nalog");
            dtView = Baza.rsReport.Tables[0];
            rptIzvestaj.SetDataSource(dtView);
            crvStampaDinarskiPromet.ViewerCore.ReportSource = rptIzvestaj;
        }
    }
}
