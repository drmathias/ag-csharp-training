namespace All.About.Objects
{
    public class Customer
    {
        public string _name;

        public int _age;

        public decimal totalSpend;

        public Customer(string name, int age)
        {
            _name = name;
            _age = age;
        }

        public decimal Spend(decimal amount)
        {
            totalSpend += amount;
            return totalSpend;
        }
    }
}