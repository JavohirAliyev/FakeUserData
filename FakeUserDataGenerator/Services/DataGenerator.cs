using Bogus;
using FakeUserData.Models;

namespace FakeUserData.Services
{
    public class DataGenerator
    {
        private string locale;
        private int seed;
        private Faker<PersonModel> faker = null!;

        public DataGenerator(int _seed, string _locale)
        {
            seed = _seed;
            locale = _locale;
            ConfigureFaker();
        }

        public void UpdateSeedAndLocale(int _seed, string _locale)
        {
            seed = _seed;
            locale = _locale;
            ConfigureFaker();
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

        private void ConfigureFaker()
        {
            Randomizer.Seed = new Random(seed);
            faker = new Faker<PersonModel>(locale)
                .RuleFor(u => u.Id, (f, u) => GenerateGuid(seed + f.IndexFaker))
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber());
        }
    }
}
