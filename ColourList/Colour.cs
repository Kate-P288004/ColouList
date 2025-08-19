using System;

namespace MyLists
{
    [Serializable]
    public class Colour
    {
        public string Name { get; set; }
        public string Hex { get; set; }

        public Colour() { }

        public Colour(string name, string hex)
        {
            Name = name;
            Hex = hex;
        }

        public override string ToString()
        {
            return Name + " (" + Hex + ")";
        }
    }
}
