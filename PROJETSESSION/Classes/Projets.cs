using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Classes
{
    internal class Projets
    {

        public string noProjet;
        public string titre;
        public DateTime dateDebut;
        public string description;
        public decimal budjet;
        public int nbEmploye;
        public decimal totalSalaire;
        public int noClient;
        public string statut;

        public Projets(string noProjet, string titre, DateTime dateDebut, string description, decimal budjet, int nbEmploye, decimal totalSalaire,int noClient, string statut)
        {
            this.noProjet = noProjet;
            this.titre = titre;
            this.dateDebut = dateDebut;
            this.description = description;
            this.budjet = budjet;
            this.nbEmploye = nbEmploye;
            this.totalSalaire = totalSalaire;
            this.noClient = noClient;
            this.statut = statut;
        }

        public override string? ToString()
        {
            return $"Projet no: {noProjet} \nTitre: {titre} \nDate de début: {dateDebut} \nDescription: {description}" +
                   $"\nBudjet: {budjet} \nNombre d'employe sur le projet: {nbEmploye} \nTotal des salaires: {totalSalaire}" +
                   $"Client no: {noClient} \nStatut du projet: {statut}";
        }
    }
}
