namespace WpfApp1.Model
{
    public class Manager : IOperator

    {
        public override string ToString() => Name;


        public string Name => "Manager";
    }
}