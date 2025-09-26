using System;

namespace Itransition5.Data
{
    public record PersonModel
    {
        public Guid Id { get; init; }
        public string Name {  get; set; }
        public string Address {  get; set; }
        public string Phone {  get; set; }

        public PersonModel()
        {
            this.Id = Guid.NewGuid();
        }

    }


}