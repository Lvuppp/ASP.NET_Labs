namespace Web_153504_Bagrovets_Lab1.Entities
{
    public class ListData
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Cart { 
        public Cart(int id, int money) 
        {
            Id = id;
            Money = money;
        }
        public int Id { get; set; }
        public double Money { get; set; }
    }

}
