using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Classes
{
    public  class Clients
    {
        public int identifiant;
        public string nom;
        public string adresse;
        public int numeroTelephone;
        public string email;


        public Clients(int identifiant, string nom, string adresse, int numeroTelephone, string email)
        {
            this.identifiant = identifiant;
            this.nom = nom;
            this.adresse = adresse;
            this.numeroTelephone = numeroTelephone;
            this.email = email;
        }

        public int Identifiant { get =>  identifiant; set => identifiant = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public int NumeroTelephone { get => numeroTelephone; set => numeroTelephone = value; }
        public string Email { get => email; set => email = value; }

        public override string? ToString()
        {
            return $"Identifiant: {identifiant} \nNom: {nom} \nAdresse: {adresse} \nNuméro de telephone: {numeroTelephone} \nEmail: {email}";
        }
    }
}
