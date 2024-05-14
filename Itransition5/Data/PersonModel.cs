namespace Itransition5.Data
{
    public record PersonModel()
    {
        public Guid Id { get; set; }
        public string Name {  get; set; }
        public string Address {  get; set; }
        public string Phone {  get; set; }
    }


}