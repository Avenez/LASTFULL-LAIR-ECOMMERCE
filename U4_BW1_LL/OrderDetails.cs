namespace U4_BW1_LL
{
    public class OrderDetails
    {
        private int id;
        public int Id { get => id; }

        private double prezzo;
        public double Prezzo { get => prezzo; set => prezzo = value; }

        private int qta;
        public int Qta { get => qta; set => qta = value; }
        private string imgUrl;
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        private string name;
        public string Name { get => name; set => name = value; }

        public OrderDetails(int id, int qta, double prezzo)
        {
            this.id = id;
            this.qta = qta;
            this.prezzo = prezzo;
        }
    }
}