using HalloBuilder;

Console.WriteLine("Hello, World!");


var s1 = new Schrank.Builder()
                    .SetAnzTüren(8)
                    .SetAnzBöden(4)
                    .SetOberfläche(Oberfläche.Lackiert)
                    .SetFarbe("gelb")
                    .Create();