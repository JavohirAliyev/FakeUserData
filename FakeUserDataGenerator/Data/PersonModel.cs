using System;

namespace FakeUserData.Data
{
    public record PersonModel
    {
        public Guid Id { get; init; }
        public string Name { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";

        public PersonModel()
        {
            Id = Guid.NewGuid();
        }

    }


}