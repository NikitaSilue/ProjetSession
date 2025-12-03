using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Classes
{
    public class Projets
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

        public string NoProjet {  get => noProjet; set => noProjet = value; }
        public string Titre { get => titre; set => titre = value; }
        public DateTime DateDebut { get => dateDebut; set => dateDebut = value; }
        public String DateDebutString { get => dateDebut.ToString("d"); }
        public string Description { get => description; set => description = value; }
        public decimal Budjet { get => budjet; set => budjet = value; }
        public int NbrEmploye { get => nbEmploye; set => NbrEmploye = value; }
        public decimal TotalSalaire { get => totalSalaire; set => totalSalaire = value; }
        public int NoClient { get => noClient; set => noClient = value; }
        public string Statut { get => statut; set => statut = value; }


        public override string? ToString()
        {
            return $"Projet no: {noProjet} \nTitre: {titre} \nDate de début: {dateDebut} \nDescription: {description}" +
                   $"\nBudjet: {budjet} \nNombre d'employe sur le projet: {nbEmploye} \nTotal des salaires: {totalSalaire}" +
                   $"Client no: {noClient} \nStatut du projet: {statut}";
        }
    }
}
