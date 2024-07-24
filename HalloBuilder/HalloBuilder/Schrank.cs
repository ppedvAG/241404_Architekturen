using System.Runtime.InteropServices;

namespace HalloBuilder
{
    internal class Schrank
    {
        public int AnzahlTüren { get; private set; } = 4;
        public int AnzahlBöden { get; private set; } = 4;
        public string Farbe { get; private set; } = string.Empty;
        public Oberfläche Oberfläche { get; private set; } = Oberfläche.Natur;

        internal class Builder
        {
            private Schrank _schrank = new Schrank();

            public Schrank Create()
            {
                return _schrank;
            }

            public Builder SetAnzTüren(int anzTüren)
            {
                if (anzTüren < 0 || anzTüren > 10)
                {
                    throw new ArgumentException("Es nur max 10 Türen erlaubt und nicht weniger als 0");
                }

                _schrank.AnzahlTüren = anzTüren;

                return this;
            }

            public Builder SetAnzBöden(int anzBöden)
            {
                if (anzBöden < 0 || anzBöden > 10)
                {
                    throw new ArgumentException("Es nur max 10 Türen erlaubt und nicht weniger als 0");
                }

                _schrank.AnzahlBöden = anzBöden;

                return this;
            }

            public Builder SetFarbe(string farbe)
            {
                if (_schrank.Oberfläche != Oberfläche.Lackiert)
                    throw new ArgumentException("Nur lackierte Schränke können eine Farbe haben");

                _schrank.Farbe = farbe;
                return this;
            }

            public Builder SetOberfläche(Oberfläche oberfläche)
            {
                if (oberfläche != Oberfläche.Lackiert)
                    _schrank.Farbe = string.Empty;

                _schrank.Oberfläche = oberfläche;
                return this;
            }
        }
    }

    internal enum Oberfläche
    {
        Natur,
        Gewachst,
        Lackiert
    }
}
