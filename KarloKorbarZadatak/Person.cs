using CsvHelper.Configuration.Attributes;

namespace KarloKorbarZadatak
{
    public class Person
    {
        [Index(0)]
        public string Ime { get; set; }
        [Index(1)]
        public string Prezime { get; set; }
        [Index(2)]
        public string PostanskiBr { get; set; }
        [Index(3)]
        public string Grad { get; set; }
        [Index(4)]
        public string Telefon { get; set; }
    }
}
