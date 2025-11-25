using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PROJETSESSION.Pages.EmployePages;
using PROJETSESSION.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PROJETSESSION.Pages.ClientPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouterClient : Page
    {
        public PageAjouterClient()
        {
            InitializeComponent();
        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;
            errorNom.Text = string.Empty;
            errorAdresse.Text = string.Empty;
            errorTelephone.Text = string.Empty;
            errorEmail.Text = string.Empty;


            if (string.IsNullOrWhiteSpace(tbxNom.Text))
            {
                errorNom.Text = "Le nom est obligatoire svp.";
                valide = false;
            }
            if (string.IsNullOrWhiteSpace(tbxAdresse.Text))
            {
                errorAdresse.Text = "L'adresse est obligatoire svp.";
                valide = false;
            }
            if (nbrTelephone.Value == 0)
            {
                errorTelephone.Text = "Entrer un taux horaire validesvp.";
                valide = false;
            }
            if (string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                errorEmail.Text = "L'email est obligatoire svp.";
                valide = false;
            }
            //NUMERO DE TÉLÉPHONE
            if (double.IsNaN(nbrTelephone.Value))
            {
                errorTelephone.Text = "Veuillez entrer un numéro de téléphone.";
                valide = false;
            }
            if (nbrTelephone.Value <= 0)
            {
                errorTelephone.Text = "Le numéro de téléphone ne peut pas être vide.";
                valide = false;
            }
          
            if (nbrTelephone.Value < 1000000000 || nbrTelephone.Value > 9999999999)
            {
                errorTelephone.Text = "Le numéro de téléphone doit contenir 10 chiffres.";
                valide = false;
            }

            if (valide)
            {
                string nom = tbxNom.Text;
                string adresse = tbxAdresse.Text;
                int numeroTelephone = (int) nbrTelephone.Value;
                string email = tbxEmail.Text;
                SingletonClient.getInstance().ajouter(nom, adresse, numeroTelephone, email);

                Frame.Navigate(typeof(PageAfficherClient));
            }


        }
    }
}
