using System.Collections.Generic;
using System.Linq;

namespace MyLists
{
    public static class ColourRepository
    {
        private static readonly List<Colour> _colours = new List<Colour>
        {
            new Colour("Blue",   "#0000FF"),
            new Colour("Red",    "#FF0000"),
            new Colour("Yellow", "#FFFF00"),
            new Colour("Green",  "#008000"),
            new Colour("Orange", "#FFA500"),
            new Colour("Purple", "#800080"),
            new Colour("Cyan",   "#00FFFF"),
            new Colour("Pink",   "#FFC0CB"),
            new Colour("White",  "#FFFFFF"),
            new Colour("Black",  "#000000"),
            new Colour("Peach",  "#FFE5B4"),
            new Colour("Gold",   "#FFD700"),
            new Colour("Maroon", "#800000"),
            new Colour("Violet", "#EE82EE")
        };

        public static List<Colour> GetAll()
        {
            return _colours.ToList();
        }

        public static bool Exists(string name)
        {
            var lower = name.ToLowerInvariant();
            return _colours.Any(c => c.Name.ToLowerInvariant() == lower);
        }

        public static void Add(string name, string hex)
        {
            if (!Exists(name))
                _colours.Add(new Colour(name, hex));
        }

        public static void Update(int index, string name, string hex)
        {
            if (index < 0 || index >= _colours.Count) return;
            _colours[index].Name = name;
            _colours[index].Hex = hex;
        }

        public static void RemoveAt(int index)
        {
            if (index < 0 || index >= _colours.Count) return;
            _colours.RemoveAt(index);
        }
    }
}
