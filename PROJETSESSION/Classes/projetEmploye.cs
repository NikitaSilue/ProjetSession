using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Classes
{
    internal class projetEmploye
    {
        public string noEmploye;
        public string noProjet;
        public decimal heuresTravaille;
        public decimal tauxHoraires;
        public decimal salaire => tauxHoraires * heuresTravaille;

        public projetEmploye(string noEmploye, string noProjet, decimal heuresTravaille, decimal tauxHoraires) 
        { 
            this.noProjet = noProjet;
            this.noEmploye = noEmploye;
            this.heuresTravaille = heuresTravaille;
            this.tauxHoraires = tauxHoraires;
        }

        public string NoProjet { get => noProjet; set => noProjet = value; }
        public string NoEmploye {  get => noEmploye; set => noEmploye = value; }
        public decimal HeuresTravaille { get => heuresTravaille; set => heuresTravaille = value; }
        public decimal TauxHoraires { get => tauxHoraires; set => tauxHoraires = value;}

        public override string? ToString()
        {
            return $"noProjet: {noProjet}, noEmploye: {noEmploye}, heure de travail: {heuresTravaille}, tauxHoraires: {tauxHoraires}, Salaire: {salaire}";
        }
    }
}
