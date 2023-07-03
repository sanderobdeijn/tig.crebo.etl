using Tig.Crebo.Etl.Csv;

namespace Tig.Crebo.Etl;

public class OpleidingenService
{
    private List<Opleiding> _opleidingen;
    
    public List<Opleiding> GetOpleidingen()
    {
        if (_opleidingen == null)
        {
            List<Kwalificatie> kwalificatieDossiers = GetKwalificatieDossiers();
            List<VervallenKwalificatie> vervallenKwalificatieDossiers = GetVervallenKwalificatieDossiers();

            _opleidingen =
                MergeKwalificatieDossierAndVervallenKwalificatieDossier(kwalificatieDossiers,  vervallenKwalificatieDossiers);
        }

        return _opleidingen;
    }

    private List<Opleiding> MergeKwalificatieDossierAndVervallenKwalificatieDossier(List<Kwalificatie> kwalificatieDossiers, List<VervallenKwalificatie> vervallenKwalificatieDossiers)
    {
        List<Opleiding> opleidingen = kwalificatieDossiers.Select(d =>
            new Opleiding(d.CREBONummer, d.CREBONummer, d.Naam, d.Niveau)).ToList();

        foreach (var vervallenKwalificatieDossier in vervallenKwalificatieDossiers)
        {
            int vervangendeCREBONummer = GetVervallenCREBONummer(vervallenKwalificatieDossier);
            
            var vervangendeOpleiding = opleidingen.FirstOrDefault(x =>
                x.OrgineleCREBONummer == vervangendeCREBONummer);

            if (vervangendeOpleiding == null) continue;
            
            var nieuweOpleiding = vervangendeOpleiding with
            {
                OrgineleCREBONummer = vervallenKwalificatieDossier.VervallenCREBONummer
            };

            opleidingen.Add(nieuweOpleiding);
        }

        return opleidingen;
    }

    private int GetVervallenCREBONummer(VervallenKwalificatie vervallenKwalificatie)
    {
        return int.Parse(vervallenKwalificatie.VervangendeCREBONummer.Split("/").First().Trim());
    }

    private List<VervallenKwalificatie> GetVervallenKwalificatieDossiers()
    {
        const string filePath = @"Csv\VervallenKwalificatie.csv";

        using (FileStream fileStream = File.OpenRead(filePath))
        {
            return CsvParser.ParseCsvToListOfObjects<VervallenKwalificatie>(fileStream);
        }
    }

    private List<Kwalificatie> GetKwalificatieDossiers()
    {
        const string filePath = @"Csv\Kwalificatie.csv";

        using (FileStream fileStream = File.OpenRead(filePath))
        {
            return CsvParser.ParseCsvToListOfObjects<Kwalificatie>(fileStream);
        }
    }
}