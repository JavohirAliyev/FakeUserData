using Bogus;
using System;
using System.Collections.Generic;
using System.Globalization;


namespace Itransition5.Data
{
	public class DataGenerator
	{
        public string locale;
        public static int seed;
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
        private Random random = new Random(seed);
        private Bogus.Randomizer bogusRandomizer = new Bogus.Randomizer(seed);

        public PersonModel ImplementError(PersonModel person, double error)
        {
            if (error > 0)
            {
                var bogusRandomizer = new Bogus.Randomizer();

                var fields = bogusRandomizer.ListItems(new[] { "Name", "Address", "Phone" }, 3);

                foreach (var field in fields)
                {
                    switch (field)
                    {
                        case "Name":
                            person.Name = ModifyString(person.Name);
                            break;
                        case "Address":
                            person.Address = ModifyString(person.Address);
                            break;
                        case "Phone":
                            person.Phone = ModifyPhone(person.Phone);
                            break;
                    }
                }

                return ImplementError(person, error - 1);
            }
            return person;
        }

        private string ModifyPhone(string input)
        {
            var bogusRandomizer = new Bogus.Randomizer();
            var actions = new List<Func<string, string>>();

            actions.Add(SwapCharacters);
            actions.Add(RemoveACharacter);
            actions.Add(AddDigit);

            var actionIndex = bogusRandomizer.Number(0, actions.Count - 1);
            var action = actions[actionIndex];
            return action.Invoke(input);
        }

        private string ModifyString(string input)
        {
            var bogusRandomizer = new Bogus.Randomizer();
            var actions = new List<Func<string, string>>();

            actions.Add(SwapCharacters);
            actions.Add(RemoveACharacter);
            actions.Add(AddACharacter);

            var actionIndex = bogusRandomizer.Number(0, actions.Count - 1);
            var action = actions[actionIndex];
            return action.Invoke(input);

        }

        private string SwapCharacters(string s)
        {
            if (s.Length <= 1)
            {
                return s;
            }

            char[] chars = s.ToCharArray();
            int i = random.Next(chars.Length);
            int j = random.Next(chars.Length);
            (chars[i], chars[j]) = (chars[j], chars[i]);

            return new string(chars);
        }

        private string RemoveACharacter(string s)
        {
            if (s.Length <= 8)
            {
                return s;
            }

            List<char> chars = s.ToList();
            int j = random.Next(chars.Count);
            chars.RemoveAt(j);

            return new string(chars.ToArray());
        }
        private string AddACharacter(string s)
        {
            if (s.Length > 50)
            {
                return s;
            }
            List<char> chars = s.ToList();
            bool isUpperCase = bogusRandomizer.Bool();
            char randomChar;

            if (isUpperCase)
            {
                randomChar = bogusRandomizer.Char('A', 'Z');
            }
            else
            {
                randomChar = bogusRandomizer.Char('a', 'z');
            }
            int j = random.Next(chars.Count + 1);
            chars.Insert(j, randomChar);

            return new string(chars.ToArray());
        }
        private string AddDigit(string s)
        {
            if (s.Length > 12)
                return s;
            List<char> chars = s.ToList();
            char randomChar = bogusRandomizer.Char('0', '9');
            int j = random.Next(chars.Count + 1);
            chars.Insert(j, randomChar);

            return new string(chars.ToArray());
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

