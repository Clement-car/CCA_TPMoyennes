using System;
using System.Collections.Generic;
using System.Linq;

namespace HNI_TPmoyennes
{
    //On utilise deux classes "Eleve" et "Classe" distincte pour correspondre aux instructions de la 
    //classe Programme
    class Eleve
    {
        //Chaque élève a un nom, un prénom,
        //des notes qui peuvent lui être ajoutées et une liste de matière suivie
        public string prenom { get; set; } = string.Empty;
        public string nom { get; set; } = string.Empty;
        public List<Note> notes = new List<Note>();
        //Les matières suivies par un élève sont directement liées à ces notes et sont donc
        //des entiers comme dans la classe Note
        public List<int> matières = new List<int>();

        //Un élève est initialisé avec un nom et un prénom
        public Eleve(string prenom_init, string nom_init)
        {
            prenom = prenom_init;
            nom = nom_init;
        }

        //On ajoute une note que si le nombre de note max (200) n'a pas été dépassé
        public void ajouterNote(Note note)
        {
            if (notes.Count > 200)
                Console.Write("Le nombre de notes maximum a été attribuée à l'élève");
            else
                notes.Add(note);
            // ajout de la matière de la note dans la liste des matières suivies par l'élève si
            // elle n'apparaît pas déjà dans la liste des matières suivies par l'élève
                if (!matières.Contains(note.matiere))
                    matières.Add(note.matiere);
        }

        //afin de tronquer les valeurs des moyennes au-delà du second chiffre après la virgule
        //on utilise la fonction Math.Round qui renvoie un double
        //ce faisant les moyennes qui seront utilisées seront toutes des doubles
        public double moyenneMatiere(int matiere)
        {
            float moyenne = 0;
            int nb_note = 0;
            foreach (Note note in notes)
            {
                if (note.matiere == matiere)
                {
                    moyenne += note.note;
                    nb_note++;
                }
            }
            return Math.Round(moyenne / nb_note, 2);
        }

        public double moyenneGeneral()
        {
            double moyenne_gen = 0;
            foreach (int matiere in matières)
            {
                moyenne_gen += moyenneMatiere(matiere) / matières.Count;
            }
            return Math.Round(moyenne_gen, 2);
        }
    }
    class Classe
    {
        //Une classe a un nom, une liste d'élèves et une liste de matières
        public string nomClasse { get; set; } = string.Empty;
        public List<Eleve> eleves = new List<Eleve>();
        public List<string> matieres = new List<string>();

        //Une classe est initialisée avec son nom
        public Classe(string name_init)
        {
            nomClasse = name_init;
        }

        //On ajoute un élève (ou une matière) à une classe que si
        //le nombre max d'éleves (ou de matières) a déjà été attribué (30 et 10)
        //On reprend la nomenclature de la classe Programme
        public void ajouterEleve(string prenom, string nom)
        {
            if (eleves.Count > 30)
                Console.Write("La classe est complète");
            else
                eleves.Add(new Eleve(prenom, nom));
        }
        public void ajouterMatiere(string nom)
        {
            if (matieres.Count > 10)
                Console.Write("Le nombre max de matières pour cette classe a déjà été atteint");
            else
                matieres.Add(nom);
        }
        public double moyenneMatiere(int matiere)
        {
            double moyenne = 0;
            foreach (Eleve eleve in eleves)
            {
                moyenne += eleve.moyenneMatiere(matiere);
            }
            return Math.Round(moyenne / eleves.Count, 2);
        }
        
        //Contrairement au calcul de la moyenne générale d'un élève
        //on ne peut pas passer par une boucle pour tout les éléments de la liste des matières
        //puisque celles-ci contient les noms des matières en accord avec la classe Programme
        public double moyenneGeneral()
        {
            double moyenne = 0;
            for (int i = 0; i < matieres.Count; i++)
            {
                moyenne += moyenneMatiere(i) / matieres.Count ;
            }
            return Math.Round(moyenne, 2);
        }
    }
    class Program
    {
        // Ne pas modifier
        static void Main(string[] args)
        {
            // Création d'une classe
            Classe sixiemeA = new Classe("6eme A");
            // Ajout des élèves à la classe
            sixiemeA.ajouterEleve("Jean", "RAGE");
            sixiemeA.ajouterEleve("Paul", "HAAR");
            sixiemeA.ajouterEleve("Sibylle", "BOQUET");
            sixiemeA.ajouterEleve("Annie", "CROCHE");
            sixiemeA.ajouterEleve("Alain", "PROVISTE");
            sixiemeA.ajouterEleve("Justin", "TYDERNIER");
            sixiemeA.ajouterEleve("Sacha", "TOUILLE");
            sixiemeA.ajouterEleve("Cesar", "TICHO");
            sixiemeA.ajouterEleve("Guy", "DON");
            // Ajout de matières étudiées par la classe
            sixiemeA.ajouterMatiere("Francais");
            sixiemeA.ajouterMatiere("Anglais");
            sixiemeA.ajouterMatiere("Physique/Chimie");
            sixiemeA.ajouterMatiere("Histoire");
            Random random = new Random();
            // Ajout de 5 notes à chaque élève et dans chaque matière
            for (int ieleve = 0; ieleve < sixiemeA.eleves.Count; ieleve++)
            {
                for (int matiere = 0; matiere < sixiemeA.matieres.Count; matiere++)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        sixiemeA.eleves[ieleve].ajouterNote(new Note(matiere, (float)((6.5 +
                       random.NextDouble() * 34)) / 2.0f));
                        // Note minimale = 3
                    }
                }
            }

            Eleve eleve = sixiemeA.eleves[6];
            // Afficher la moyenne d'un élève dans une matière
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            eleve.moyenneMatiere(1) + "\n");
            // Afficher la moyenne générale du même élève
            Console.Write(eleve.prenom + " " + eleve.nom + ", Moyenne Generale : " + eleve.moyenneGeneral() + "\n");
            // Afficher la moyenne de la classe dans une matière
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne en " + sixiemeA.matieres[1] + " : " +
            sixiemeA.moyenneMatiere(1) + "\n");
            // Afficher la moyenne générale de la classe
            Console.Write("Classe de " + sixiemeA.nomClasse + ", Moyenne Generale : " + sixiemeA.moyenneGeneral() + "\n");
            Console.Read();
        }
    }
}



