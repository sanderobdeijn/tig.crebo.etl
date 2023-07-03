using FluentAssertions;
using Tig.Crebo.Etl.Csv;

namespace Tig.Crebo.Etl.Tests;

public class StudentenServiceTests
{
    private readonly OpleidingenService _opleidingenService;

    public StudentenServiceTests()
    {
        this._opleidingenService = new OpleidingenService();
    }
    
    // [Theory]
    // public void GetVerrijkteStudenten_Should_Return_FullVerijkteStudent(string Voornaam, string Tussenvoegsel, string Achternaam, int CreboNummer,  string OpleidingsNaam, int Niveau)
    // {
    //     var studenten = new List<Student>
    //     {
    //         new (Voornaam, Tussenvoegsel, Achternaam, "test@test.com",CreboNummer)
    //     };
    //
    //     var studentenService = new StudentenService(_opleidingenService);
    //
    //     var verijkteStudenten = studentenService.GetVerrijkteStudenten(studenten);
    //
    //     verijkteStudenten.Should().ContainSingle();
    //     verijkteStudenten.First().Voornaam.Should().Be(Voornaam);
    //     verijkteStudenten.First().Tussenvoegsel.Should().Be(Tussenvoegsel);
    //     verijkteStudenten.First().Achternaam.Should().Be(Achternaam);
    //     verijkteStudenten.First().OpleidingNaam.Should().Be(OpleidingsNaam);
    //     verijkteStudenten.First().Niveau.Should().Be(Niveau);
    // }
    
    [Theory]
    [InlineData("Emre", null, "Doğul",23281)]
    [InlineData("Erwin", null, "Jansen",23215)]
    [InlineData("Ruben", "van der", "Bos",23289)]
    [InlineData("Wiegert", null, "Vermeulen",23291)]
    [InlineData("Willem", "van", "Doorn",11111)]
    
    public void GetVerrijkteStudenten_With_NotExistingKwalificatie_Should_Return_StudentNameOnly(string Voornaam, string Tussenvoegsel, string Achternaam, int CreboNummer)
    {
        var studenten = new List<Student>
        {
            new (Voornaam, Tussenvoegsel, Achternaam, "test@test.com",CreboNummer)
        };
        var studentenService = new StudentenService(_opleidingenService);

        var verijkteStudenten = studentenService.GetVerrijkteStudenten(studenten);

        verijkteStudenten.Should().ContainSingle();
        verijkteStudenten.First().Voornaam.Should().Be(Voornaam);
        verijkteStudenten.First().Tussenvoegsel.Should().Be(Tussenvoegsel);
        verijkteStudenten.First().Achternaam.Should().Be(Achternaam);
        verijkteStudenten.First().OpleidingNaam.Should().BeNull();
        verijkteStudenten.First().Niveau.Should().BeNull();
    }


    
    [Theory]
    [InlineData("Sarah", null, "Smeets",25173, "Uitvoerend bakker", 2)]
    [InlineData("Thirza", null, "Rossi",25578, "Proefdierverzorger", 3)]
    public void GetVerrijkteStudenten_With_VervallenOpleiding_Should_Return_FullVerijkteStudent(string Voornaam, string Tussenvoegsel, string Achternaam, int CreboNummer,  string OpleidingsNaam, int Niveau)
    {
        var studenten = new List<Student>
        {
            new Student(Voornaam, Tussenvoegsel, Achternaam, "test@test.com",CreboNummer)
        };

        var studentenService = new StudentenService(_opleidingenService);

        var verijkteStudenten = studentenService.GetVerrijkteStudenten(studenten);

        verijkteStudenten.Should().ContainSingle();
        verijkteStudenten.First().Voornaam.Should().Be(Voornaam);
        verijkteStudenten.First().Tussenvoegsel.Should().Be(Tussenvoegsel);
        verijkteStudenten.First().Achternaam.Should().Be(Achternaam);
        verijkteStudenten.First().OpleidingNaam.Should().Be(OpleidingsNaam);
        verijkteStudenten.First().Niveau.Should().Be(Niveau);
    }
}