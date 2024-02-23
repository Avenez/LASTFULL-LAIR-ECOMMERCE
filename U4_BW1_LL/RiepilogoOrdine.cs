using System;

namespace U4_BW1_LL
{
    public class RiepilogoOrdine
    {
        public int IdUtente { get; set; }
        public string Username { get; set; }
        public int IdOrdine { get; set; }
        public decimal PrezzoTotale { get; set; }
        public DateTime DataOrdine { get; set; }


        public RiepilogoOrdine(int idUtente, string username, int idOrdine, decimal prezzoTotale, DateTime dataOrdine)
        {
            IdUtente = idUtente;
            Username = username;
            IdOrdine = idOrdine;
            PrezzoTotale = prezzoTotale;
            DataOrdine = dataOrdine;
        }
    }
}