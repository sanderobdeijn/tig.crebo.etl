using Tig.Crebo.Etl.Csv;

namespace Tig.Crebo.Etl;

public class StudentenService
{
    private readonly OpleidingenService _opleidingenService;

    public StudentenService(OpleidingenService opleidingenService)
    {
        this._opleidingenService = opleidingenService;
    }
    
    public List<VerrijkteStudent> GetVerrijkteStudenten(List<Student> studenten)
    {
        var opleidingen = _opleidingenService.GetOpleidingen();
        var verrijkteStudenten = new List<VerrijkteStudent>();
        
        foreach (var student in studenten)
        {
            var foundOpleiding = opleidingen.FirstOrDefault(o => o.OrgineleCREBONummer == student.CREBOnummer);
            verrijkteStudenten.Add(foundOpleiding is not null
                ? GetVerrijkteStudentMetGeldigeCREBO(student, foundOpleiding)
                : GetVerrijkteStudentMetOngeldigeCREBO(student));
        }

        return verrijkteStudenten;
    }

    private VerrijkteStudent GetVerrijkteStudentMetGeldigeCREBO(Student student, Opleiding foundOpleiding)
    {
        return new VerrijkteStudent(
            Voornaam: student.Voornaam,
            Tussenvoegsel: student.Tussenvoegsel,
            Achternaam: student.Achternaam,
            Crebonummer: foundOpleiding.CREBONummer,
            VervallenCrebonummer: foundOpleiding.OrgineleCREBONummer,
            OpleidingNaam: foundOpleiding.Naam,
            Niveau: foundOpleiding.Niveau
        );
    }
    
    private VerrijkteStudent GetVerrijkteStudentMetOngeldigeCREBO(Student student)
    {
        return new VerrijkteStudent(
            Voornaam: student.Voornaam,
            Tussenvoegsel: student.Tussenvoegsel,
            Achternaam: student.Achternaam,
            Crebonummer: student.CREBOnummer,
            VervallenCrebonummer: null,
            OpleidingNaam: null,
            Niveau: null
        );
    }
}