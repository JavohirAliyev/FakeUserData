using Bogus;
using System;


namespace Itransition5.Data
{
	public class DataGenerator
	{
        private string locale = "ru";
        private int seed = 123;

        Faker<PersonModel> faker;
        public void SetSeedAndLocale(int _seed, string _locale)
        {
            seed = _seed;
            locale = _locale;

            Randomizer.Seed = new Random(seed);

            Guid GenerateGuid()
            {
                byte[] guidBytes = new byte[16];
                Randomizer.Seed.NextBytes(guidBytes);
                return new Guid(guidBytes);
            }

            faker = new Faker<PersonModel>(locale)
                .RuleFor(u => u.Id, f => GenerateGuid())
                .RuleFor(u => u.Name, f => f.Name.FullName())
                .RuleFor(u => u.Address, f => f.Address.FullAddress())
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber());
        }


        public DataGenerator()
        {
            
        }
        public PersonModel GeneratePerson()
        {
            return faker.Generate();
        }

        public IEnumerable<PersonModel> GeneratePeople()
        {
            return faker.GenerateForever();
        }



    }

}

