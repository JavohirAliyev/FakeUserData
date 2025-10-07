using Bogus;
using FakeUserData.Models;

namespace FakeUserData.Services
{
    public class DataGenerator
    {
        private readonly string locale;
        private int seed;
        private readonly Faker<PersonModel> faker;

        public DataGenerator(int _seed, string _locale)
        {
            seed = _seed;
            locale = _locale;
            Randomizer.Seed = new Random(seed);

            faker = new Faker<PersonModel>(locale)
                .UseSeed(seed)
                .RuleFor(u => u.Id, f => GenerateGuid(seed))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber());
        }

        public void UpdateSeed(int _seed)
        {
            seed = _seed;
        }

        public PersonModel GeneratePerson()
        {
            return faker.Generate();
        }

        public IEnumerable<PersonModel> GeneratePeople()
        {
            return faker.GenerateForever();
        }

        private static Guid GenerateGuid(int seed)
        {
            var random = new Random(seed);
            byte[] guidBytes = new byte[16];
            random.NextBytes(guidBytes);
            return new Guid(guidBytes);
        }
    }
}
