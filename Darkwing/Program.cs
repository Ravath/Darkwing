// See https://aka.ms/new-console-template for more information
using System.Xml.Serialization;

using DarkWing;

Position a, b, c;
a = new Position(1, 2);
b = new Position(2, 1);
c = new Position(1, 2);
Console.WriteLine(a == c);
Console.WriteLine(a.GetHashCode());
Console.WriteLine(b.GetHashCode());
Console.WriteLine(c.GetHashCode());
Game g = new();
g.Menu();
