using System.Reflection;
using Bogus;
using Bogus.Platform;
using FakeUserData.Models;

namespace FakeUserData.Services
{
    public class ErrorGenerator
    {
        private int seed;
        private readonly Random random;
        private readonly Randomizer bogusRandomizer;

        public ErrorGenerator(int _seed)
        {
            seed = _seed;
            random = new Random(seed);
            bogusRandomizer = new Randomizer(seed);
        }

        public PersonModel ImplementError(PersonModel person, double error)
        {
            if (error > 0)
            {
                var fields = person.GetType().GetAllMembers(BindingFlags.Public);
                foreach (var field in fields)
                {
                    switch (field.Name)
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
            int j = random.Next(chars.Count + 1);
            chars.Insert(j, randomChar);

            return new string([.. chars]);
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
    }
}
