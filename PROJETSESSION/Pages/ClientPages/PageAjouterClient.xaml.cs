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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            bool valide = true;
            errorNom.Text = string.Empty;
            errorAdresse.Text = string.Empty;
            errorTelephone.Text = string.Empty;
            errorEmail.Text = string.Empty;


            //VALIDATION 
            //NOM
            if (string.IsNullOrWhiteSpace(tbxNom.Text))
            {
                errorNom.Text = "Le nom est obligatoire svp.";
                valide = false;
            }
            //EMAIL
            if (string.IsNullOrWhiteSpace(tbxEmail.Text))
            {
                errorEmail.Text = "L'email est obligatoire svp.";
                valide = false;
            }
            if (!Regex.IsMatch(tbxEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorEmail.Text = "Format email invalide (ex: exemple@gmail.com).";
                valide = false;
            }

            //ADRESSE
            if (string.IsNullOrWhiteSpace(tbxAdresse.Text))
            {
                errorAdresse.Text = "L'adresse est obligatoire svp.";
                valide = false;
            }
            //NUMERO DE TÉLÉPHONE
            string tel = tbxTelephone.Text.Trim();
            if (string.IsNullOrWhiteSpace(tbxTelephone.Text))
            {
                errorTelephone.Text = "Veuillez entrer un numéro de téléphone.";
                valide = false;
            }
            if (!Regex.IsMatch(tel, @"^\d+$"))
            {
                errorTelephone.Text = "Le numéro de téléphone doit contenir uniquement des chiffres.";
                valide = false;
            }
            else if (tel.Length > 10)
            {
                errorTelephone.Text = "Le numéro de téléphone ne doit pas dépasser 10 chiffres.";
                valide = false;
            }

            if (valide)
            {
                string nom = tbxNom.Text;
                string email = tbxEmail.Text;
                string adresse = tbxAdresse.Text;
                string numeroTelephone =  tbxTelephone.Text;

                if (SingletonClient.getInstance().ajouter(nom, adresse, numeroTelephone, email))
                    Frame.Navigate(typeof(PageAfficherClient));
                else
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = this.XamlRoot;
                    dialog.Title = "Échec de l'ajout";
                    dialog.PrimaryButtonText = "Ok";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Secondary;
                    dialog.Content = "Le client n'a pas pu être ajouté. \nVeuillez réesseyer";

                    ContentDialogResult resultat = await dialog.ShowAsync();
                    if (resultat == ContentDialogResult.Primary)
                    {
                        Debug.WriteLine("Nouvelle tentative");
                    }
                    else
                    {
                        Debug.WriteLine("Annulé");
                    }

                }
            }


        }
    }
}
