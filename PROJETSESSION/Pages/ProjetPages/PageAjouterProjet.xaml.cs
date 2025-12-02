using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI;
using PROJETSESSION.Classes;
using PROJETSESSION.Pages.EmployePages;
using PROJETSESSION.Singletons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace PROJETSESSION.Pages.ProjetPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAjouterProjet : Page
    {
        public PageAjouterProjet()
        {
            InitializeComponent();
            lvClient.ItemsSource = SingletonClient.getInstance().Liste;
            SingletonClient.getInstance().getAllClients();
        }

        private async void btnAjouter_Click(object sender, RoutedEventArgs e)
        {

            bool valide = true;
            errorTitre.Text = string.Empty;
            errorDebut.Text = string.Empty;
            errorDescription.Text = string.Empty;
            errorNbrEmploye.Text = string.Empty;
            errorSalaire.Text = string.Empty;
            errorClient.Text = string.Empty;
            errorStatut.Text = string.Empty;

            //VALIDATION 
            //TITRE
            if (string.IsNullOrWhiteSpace(tbxTitre.Text))
            {
                errorTitre.Text = "Le titre du projet est obligatoire svp.";
                valide = false;
            }

            //DATE DE DÉBUT
            DateTime dateDebut = dpkDateDebut.Date.DateTime;
            if (dateDebut == null)
            {
                errorDebut.Text = "Choisissez une date de debut du projet";
                valide = false;
            }
            if (dateDebut > DateTime.Now)
            {
                errorDebut.Text = "La date de début ne peut pas être dans le futur.";
                valide = false;
            }

            //DESCRIPTION
            if (string.IsNullOrWhiteSpace(tbxDescription.Text))
            {
                errorDescription.Text = "La description du projet est obligatoire svp.";
                valide = false;
            }

            //BUDJET
            if (string.IsNullOrWhiteSpace(nbrBudjet.Text))
            {
                errorBudjet.Text = "Entrer le budjet pour ce projet.";
                valide = false;
            }
            if (nbrBudjet.Value < 0)
            {
                errorBudjet.Text = "Entrer le budjet pour ce projet.";
                valide = false;
            }

            //TOTAL SALAIRES
            if (string.IsNullOrWhiteSpace(nbrSalaire.Text))
            {
                errorBudjet.Text = "Entrer le salaire total à payer pour ce projet.";
                valide = false;
            }
            if (nbrBudjet.Value < 0)
            {
                errorBudjet.Text = "Le salaire ne peut être négatif.";
                valide = false;
            }

            //NOMBRE D'EMPLOYÉS
            if (string.IsNullOrWhiteSpace(nbrEmployes.Text))
            {
                errorNbrEmploye.Text = "Un nombre d'employé est requis";
                valide = false;
            }
            if (nbrEmployes.Value > 5)
            {
                errorNbrEmploye.Text = "Vous ne pouvez pas avoir plus de 5 employés pour un projet.";
                valide = false;
            }

            //SELECTION DU CLIENT
            if (lvClient.SelectedItem == null)
            {
                errorClient.Text = "Ce projet doit être assigné à un projet";
                valide = false;
            }

            //STATUT
            if (cmbxStatut.SelectedItem == null)
            {
                errorStatut.Text = "Veuillez sélectionner un statut.";
            }

            if (valide)
            {
                Clients client = lvClient.SelectedItem as Clients;
                string titre = tbxTitre.Text;
                DateTime dateDeDebut = dpkDateDebut.Date.DateTime;
                string description = tbxDescription.Text;
                decimal budjet = (decimal)nbrBudjet.Value;
                int nbrEmploye = (int) nbrEmployes.Value;
                decimal totalSalaire = (int) nbrSalaire.Value;
                int noClient = client.Identifiant;
                string statut = cmbxStatut.SelectedItem?.ToString();


                if (SingletonProjet.getInstance().ajouter(titre,dateDebut, description, budjet, nbrEmploye, totalSalaire, noClient, statut))
                    Frame.Navigate(typeof(PageAfficherProjets));
                else
                {
                    ContentDialog dialog = new ContentDialog();
                    dialog.XamlRoot = this.XamlRoot;
                    dialog.Title = "Échec de l'ajout";
                    dialog.PrimaryButtonText = "Ok";
                    dialog.CloseButtonText = "Annuler";
                    dialog.DefaultButton = ContentDialogButton.Secondary;
                    dialog.Content = "Le projet n'a pas pu être ajouté. \nVeuillez réesseyer";

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
