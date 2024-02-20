namespace U4_BW1_LL
{
    public class Product
    {
        private string nome;
        public string Nome { get => nome; }

        private string descrizione;
        public string Descrizione { get => descrizione; }

        private string imgUrl;
        public string ImgUrl { get => imgUrl; }

        private int id;
        public int Id { get => id; }

        private double prezzo;
        public double Prezzo { get => prezzo; }

        private int qta;
        public int Qta { get => qta; }

        public Product(int id, string nome, string descrizione, string imgUrl, double prezzo, int qta)
        {
            this.id = id;
            this.nome = nome;
            this.descrizione = descrizione;
            this.imgUrl = imgUrl;
            this.prezzo = prezzo;
            this.qta = qta;
        }

    }
}