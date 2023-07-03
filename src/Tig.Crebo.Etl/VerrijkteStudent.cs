namespace Tig.Crebo.Etl;

public class VerrijkteStudent
{
    public VerrijkteStudent(string Voornaam, string? Tussenvoegsel, string Achternaam, int? Crebonummer, int? VervallenCrebonummer, string? OpleidingNaam, int? Niveau)
    {
        this.Voornaam = Voornaam;
        this.Tussenvoegsel = Tussenvoegsel;
        this.Achternaam = Achternaam;
        this.Crebonummer = Crebonummer;
        this.VervallenCrebonummer = VervallenCrebonummer;
        this.OpleidingNaam = OpleidingNaam;
        this.Niveau = Niveau;
    }

    public string Voornaam { get; set; }
    public string? Tussenvoegsel { get; set; }
    public string Achternaam { get; set; }
    public int? Crebonummer { get; set; }
    public int? VervallenCrebonummer { get; set; }
    public string? OpleidingNaam { get; set; }
    public int? Niveau { get; set; }
}