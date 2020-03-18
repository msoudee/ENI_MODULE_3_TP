using ProjetLinq.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3TPAuteurs
{
    class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

        static void Main(string[] args)
        {
            InitialiserDatas();


            // Afficher la liste des prénoms des auteurs dont le nom commence par "G"
            var listeAuteursPrenomG = ListeAuteurs.Where(a => a.Nom.Substring(0, 1).Equals("G"));
            Console.WriteLine("Liste des prénoms des auteurs dont le nom commence par G :");
            foreach (var auteur in listeAuteursPrenomG)
            {
                Console.WriteLine(auteur.Prenom);
            }


            // Afficher l'auteur ayant écrit le plus de livres
            var auteurPlusLivres = ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(l => l.Count()).FirstOrDefault().Key;
            Console.WriteLine("\nAuteur ayant écrit le plus de livres");
            Console.WriteLine(auteurPlusLivres.Prenom + " " + auteurPlusLivres.Nom);


            // Afficher le nombre moyen de pages par livre par auteur
            var livresParAuteurs = ListeLivres.GroupBy(l => l.Auteur);
            Console.WriteLine("\nNombre moyen de pages par livres par auteur : ");
            foreach(var livres in livresParAuteurs)
            {
                Console.WriteLine($"{livres.Key.Prenom} {livres.Key.Nom} : {livres.Average(l => l.NbPages)}");
            }


            // Afficher le titre du livre avec le plus de pages
            var livrePlusPage = ListeLivres.Where(lpa => lpa.NbPages == ListeLivres.Max(l => l.NbPages));
            Console.WriteLine("\nTitre du livre avec le plus de pages : ");
            Console.WriteLine(livrePlusPage.FirstOrDefault().Titre);


            // Afficher combien ont gagné les auteurs en moyenne
            var gains = ListeAuteurs.Average(a => a.Factures.Sum(f => f.Montant));
            Console.WriteLine("\nGains moyens de tous les auteurs : ");
            Console.WriteLine(gains);


            // Afficher les auteurs et la liste de leurs livres
            livresParAuteurs = ListeLivres.GroupBy(l => l.Auteur);
            Console.WriteLine("\nLes auteurs avec la liste de leurs livres : ");
            foreach (var livres in livresParAuteurs)
            {
                Console.WriteLine($"{livres.Key.Prenom} {livres.Key.Nom}");
                foreach(var livre in livres)
                {
                    Console.WriteLine($"    {livre.Titre}");
                }
            }


            // Afficher les titres de tous les livres triés par ordre alphabétique
            var titresOrdreAlphabétique = ListeLivres.OrderBy(l => l.Titre);
            Console.WriteLine("\nTitres de tous les livres par ordre alaphabétique : ");
            foreach (var livre in titresOrdreAlphabétique)
            {
                Console.WriteLine(livre.Titre);
            }


            // Afficher la liste des livres dont le nombre de page s est supérieur à la moyenne
            var livresPlusDePageQueMoyenne = ListeLivres.Where(l => l.NbPages > ListeLivres.Average(l2 => l2.NbPages));
            Console.WriteLine("\nTitres de tous les livres par ordre alaphabétique : ");
            foreach (var livre in livresPlusDePageQueMoyenne)
            {
                Console.WriteLine(livre.Titre);
            }


            // Afficher l'auteur ayant écrit le moins de livres
            var auteurMoinsLivres = ListeLivres.GroupBy(l => l.Auteur).OrderBy(l => l.Count()).FirstOrDefault().Key;
            Console.WriteLine("\nAuteur ayant écrit le moins de livres");
            Console.WriteLine(auteurMoinsLivres.Prenom + " " + auteurMoinsLivres.Nom);


            Console.ReadKey();
        }
    }
}
