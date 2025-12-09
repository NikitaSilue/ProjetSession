using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using PROJETSESSION.Classes;
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

namespace PROJETSESSION.Boites_de_dialogue
{
    public sealed partial class DialogueModifierProjet : ContentDialog
    {
        Projets projets;
        public DialogueModifierProjet()
        {
            InitializeComponent();
        }

        public void setProjets(Projets projets)
        {
            this.projets = projets;
            tbxDescription.Text = projets.description;
            tbxTitre.Text = projets.titre;
            nbrBudjet.Value = Convert.ToDouble(projets.budjet);
            nbrEmploye.Value = Convert.ToInt32(projets.nbEmploye);
            nbrSalaire.Value = Convert.ToDouble(projets.totalSalaire);
            cmbStatut.SelectedItem = projets.statut;

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            bool valide = true;
            errorTitre.Text = string.Empty;
            errorDescription.Text = string.Empty;
            errorEmploye.Text = string.Empty;
            errorSalaire.Text = string.Empty;
            errorStatut.Text = string.Empty;

            //VALIDATION 
            //TITRE
            if (string.IsNullOrWhiteSpace(tbxTitre.Text))
            {
                errorTitre.Text = "Le titre du projet est obligatoire svp.";
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
            if (string.IsNullOrWhiteSpace(nbrEmploye.Text))
            {
                errorEmploye.Text = "Un nombre d'employé est requis";
                valide = false;
            }
            if (nbrEmploye.Value > 5)
            {
                errorEmploye.Text = "Vous ne pouvez pas avoir plus de 5 employés pour un projet.";
                valide = false;
            }

            //STATUT
            if (cmbStatut.SelectedItem == null)
            {
                errorStatut.Text = "Veuillez sélectionner un statut.";
            }



            if (valide)
            {
                string titre = tbxTitre.Text;
                string description = tbxDescription.Text;
                decimal budjet = (decimal)nbrBudjet.Value;
                int nbEmploye = (int)nbrEmploye.Value;
                decimal totalSalaire = (int)nbrSalaire.Value;
                string statut = cmbStatut.SelectedItem?.ToString();

                if (SingletonProjet.getInstance().modifier(projets.noProjet, titre, description, budjet, nbEmploye, totalSalaire, statut) == false)
                {
                    args.Cancel = true;
                }
                else
                    args.Cancel = false;

            }
            else
            {
                args.Cancel = true;
            }
        }
    }
}
