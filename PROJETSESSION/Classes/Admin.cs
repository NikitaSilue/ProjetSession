using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PROJETSESSION.Classes
{
    internal class Admin
    {
        int idAdmin;
        string nomUtilisateur;
        string motDePasse;


        public Admin(int idAdmin, string nomUtilisateur, string motDePasse)
        {
            this.idAdmin = idAdmin;
            this.nomUtilisateur = nomUtilisateur;
            this.motDePasse = motDePasse;
        }

        public int IdAdmin { get => idAdmin; set => idAdmin = value; }
        public string NomUtilisateur { get => nomUtilisateur; set => nomUtilisateur = value; }
        public string MotDePasse { get => motDePasse; set => motDePasse = value; }

        public override string? ToString()
        {
            return $"id: {idAdmin}, nom: {nomUtilisateur}, mot de passe {motDePasse}";
        }
    }
}
