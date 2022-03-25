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
    /// Interaction logic for Pocetna.xaml
    /// </summary>
    public partial class Pocetna : Window
    {
        string noviUser;
        public Pocetna(string user)
        {
            InitializeComponent();
            noviUser = user;
        }

        private void MenuItemUnosFirme_Click(object sender, RoutedEventArgs e)
        {
            Unos_nove_firme nova = new Unos_nove_firme(noviUser);
            nova.Show();
        }

        private void MenuItemPregledFirmi_Click(object sender, RoutedEventArgs e)
        {
            PregledFirmi pregledFirmi = new PregledFirmi();
            pregledFirmi.ShowDialog();
        }

        private void MenuItemKreiranjeKOntnogOkvira_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeKontnogOkvira kont = new KreiranjeKontnogOkvira();
            kont.Show();
        }

        private void MenuItemKreiranjeKonta_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeKonta k = new KreiranjeKonta();
            k.Show();
        }

        private void MenuItemUnosUkontniPLan_Click(object sender, RoutedEventArgs e)
        {
            Kontni_plan unosKontniPLan = new Kontni_plan();
            unosKontniPLan.ShowDialog();
        }

        private void MenuItemKreirajNalog_Click(object sender, RoutedEventArgs e)
        {
            KreiranjeNaloga novNalog = new KreiranjeNaloga();
            novNalog.ShowDialog();
        }

        private void IzmeniNalog_Click(object sender, RoutedEventArgs e)
        {
            NoviNalog noviNalog = new NoviNalog();
            noviNalog.ShowDialog();
        }

        private void PretraziNalog_Click(object sender, RoutedEventArgs e)
        {
            PretragaNaloga pretragaNaloga = new PretragaNaloga();
            pretragaNaloga.ShowDialog();
        }

        private void MenuItemDinarskiPrometGlavneKnjige_Click(object sender, RoutedEventArgs e)
        {
            DinarskiPrometGlavneKnjige dinarskiPrometGlavneKnjige = new DinarskiPrometGlavneKnjige();
            dinarskiPrometGlavneKnjige.ShowDialog();
        }

        private void MenuItemAnaLitickiPrometGlavneKnjige_Click(object sender, RoutedEventArgs e)
        {
            AnalitickiPrometGlavneKnjige analitickiPrometGlavneKnjige = new AnalitickiPrometGlavneKnjige();
            analitickiPrometGlavneKnjige.ShowDialog();
        }
    }
}
