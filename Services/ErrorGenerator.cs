using Bogus;
using FakeUserData.Models;
using FakeUserData.Utils;

namespace FakeUserData.Services
{
    public class ErrorGenerator
    {
        private int seed;
        private readonly Randomizer bogusRandomizer;

        public ErrorGenerator(int _seed)
        {
            seed = _seed;
            Randomizer.Seed = new Random(seed);
            bogusRandomizer = new Randomizer(seed);
        }

        public PersonModel ImplementError(PersonModel person, double error)
        {
            if (error > 0)
            {
                var type = person.GetType();
                var properties = type.GetProperties().ToList();
                foreach (var prop in properties)
                {
                    switch (prop.Name)
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
            var actions = new List<Func<string, string>>
            {
                SwapCharacters,
                RemoveACharacter,
                AddDigit
            };

            var actionIndex = bogusRandomizer.Number(0, actions.Count - 1);
            var action = actions[actionIndex];
            return action.Invoke(input);
        }

        public void UpdateSeed(int _seed)
        {
            seed = _seed;
        }

        private string ModifyString(string input)
        {
            var actions = new List<Func<string, string>>
            {
                SwapCharacters,
                RemoveACharacter,
                AddACharacter
            };

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
            int i = bogusRandomizer.Int(0, chars.Length - 1);
            int j = bogusRandomizer.Int(0, chars.Length - 1);
            (chars[i], chars[j]) = (chars[j], chars[i]);

            return new string(chars);
        }

        private string RemoveACharacter(string s)
        {
            if (s.Length <= 8)
            {
                return s;
            }

            char[] chars = s.ToCharArray();
            int index = bogusRandomizer.Int(0, chars.Length - 1);
            chars.RemoveFromArrayAtIndex(index);

            return new string([.. chars]);
        }

        private string AddACharacter(string s)
        {
            if (s.Length > 50)
            {
                return s;
            }
            List<char> chars = [.. s];
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
            int j = bogusRandomizer.Int(0, chars.Count - 1);
            chars.Insert(j, randomChar);

            return new string([.. chars]);
        }

        private string AddDigit(string s)
        {
            if (s.Length > 12)
                return s;
            List<char> chars = s.ToList();
            char randomChar = bogusRandomizer.Char('0', '9');
            int j = bogusRandomizer.Int(0, chars.Count - 1);
            chars.Insert(j, randomChar);

            return new string([.. chars]);
        }
    }
}
