using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Oefening31
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void BtnControle_Click(object sender, RoutedEventArgs e)
        {
            //Voer controle uit en toon resultaat wanneer er op knop geklikt wordt
            ControleerEnToonResultaat();
        }
        private void TxtOndNr_KeyDown(object sender, KeyEventArgs e)
        {
            //Voer controle uit en toon resultaat wanneer er op enter gedrukt wordt in de textbox
            if (e.Key == Key.Enter)
            {
                ControleerEnToonResultaat();
            }
        }
        private void ControleerEnToonResultaat()
        {
            switch (ControleOndnr(TxtOndNr.Text))
            {
                case 1:
                    MessageBox.Show("Correct");
                    break;
                case 2:
                    MessageBox.Show("Format fout");
                    break;
                case 3:
                    MessageBox.Show("Controlenummer fout");
                    break;
                default:
                    break;
            }

            TxtOndNr.Focus(); //Plaats de cursor terug in de tekstbox
            TxtOndNr.SelectAll(); //Selecteer alle tekst van de tekstbox (zodat deze overschreven kan worden)
        }
        private int ControleOndnr(string inputOndNr)
        {
            if(ControleFormat(inputOndNr))
            {
                if(ControleModulus(inputOndNr))
                {
                    return 1; //zowel het formaat als het controlenummer zijn correct
                } else
                {
                    return 3; //het formaat is correct maar het controlenummer is fout
                }
            } else
            {
                return 2; //het formaat is niet correct
            }
        }
        private bool ControleFormat(string inputOndNr)
        {
            //BE 0000.000.000
            if (inputOndNr.Length == 15 && //Totale lengte moet 15 zijn
                inputOndNr.StartsWith("BE ") && //Moet beginnen met 'BE '
                inputOndNr.IndexOf(".") == 7 && //Het eerste punt moet op positie 7 staan
                inputOndNr.LastIndexOf(".") == 11) //Het tweede (laatste) punt moet op positie 11 staan
                return true;
            else
                return false;
        }
        private bool ControleModulus(string inputOndNr)
        {
            //BE 0000.000.000
            //Neem een deel van de tekst vanaf positie 3
            //en vervang 'punt' door een blanco tekst (= verwijder punten uit tekst)
            string inputBasis = inputOndNr.Substring(3).Replace(".","");

            int basis = int.Parse(inputBasis.Substring(0, 8)); //Eerste 8 cijfers converteren naar int
            int controle = int.Parse(inputBasis.Substring(8, 2)); //Laatste 2 cijfers converteren naar int

            int modulus = 97 - (basis % 97); //Algoritme voor controle btw-nummer

            if(controle == modulus)
                return true;
            else
                return false;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Bent u zeker dat u wil afsluiten?", "Afsluiten", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (answer == MessageBoxResult.No)
                e.Cancel = true;
            else
                e.Cancel = false;
        }
    }
}
