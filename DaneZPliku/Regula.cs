using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DaneZPliku
{
    public class Regula
    {
        public Dictionary<int, string> deskryptory = new Dictionary<int, string>();
        public string decyzja;
        public int support;

        public Regula(string[] obiekt, int[] kombinacje)
        {          
            this.decyzja = obiekt.Last();
            for (int i = 0; i < kombinacje.Length; i++)
            {
                int nrAtrybutu = kombinacje[i];
                string wartoscAtrybutu = obiekt[nrAtrybutu];
                this.deskryptory.Add(nrAtrybutu, wartoscAtrybutu);
            }           
        }
        public bool czyObiektSpelniaRegule(string[] obiekt)
        {
            foreach (var deskryptor in this.deskryptory)
            {
                if (obiekt[deskryptor.Key] != deskryptor.Value)
                    return false;
            }
            return true;
        }

        public bool czyRegulaSprzeczna(string[][] obiekty)
        {
            foreach (var obiekt in obiekty)
            {
                if (czyObiektSpelniaRegule(obiekt) && decyzja != obiekt.Last())
                    return false;
            }
            return true;
        }
        public int SupportReguly(string[][] obiekty)
        {
            support = 0;
            foreach (var ob in obiekty)
            {
                if (czyObiektSpelniaRegule(ob))
                    support++;
            }
            return support;
        }

        public List<int> generujPokjrycie(string[][] obiekty, List<int> maska)
        {
            int tmp = 0;
            foreach (var obiekt in obiekty)
            {
                if (czyObiektSpelniaRegule(obiekt))
                    maska.Remove(tmp);
                tmp++;
            }
            return maska;
        }


        public override string ToString()
        {
            string wynik = string.Empty;
            string r = "(a" + (deskryptory.First().Key + 1) + "=" + deskryptory.First().Value + ")";
            for (int i = 1; i < deskryptory.Count; i++)
            {
                int nrAtr = deskryptory.Keys.ElementAt(i) + 1;
                string wartAtr = deskryptory.Values.ElementAt(i);

                r += " ^ (a" + nrAtr + "=" + wartAtr + ")";
            }

            r += "=>(d=" + decyzja + ")";

            if (support > 1)
                r += "[" + support + "]";

            return r;
        }

    }
}
