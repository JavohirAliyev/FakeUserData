using Bogus;


namespace Itransition5.Data
{
	public class DataGenerator
	{
		Faker<PersonModel> faker;

        public DataGenerator()
        {
            Randomizer.Seed = new Random(123);

            faker = new Faker<PersonModel>("it")
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber());
        }
        public PersonModel GeneratePerson()
        {
            
            return faker.Generate();
        }
    }
}
