using Microsoft.VisualBasic.FileIO;

namespace Tig.Crebo.Etl.Csv;

public static class CsvParser
{
    public static List<T> ParseCsvToListOfObjects<T>(Stream stream) where T : class
    {
        var objects = new List<T>();

        using (var parser = new TextFieldParser(stream))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(";"); // Set the delimiter used in your CSV file

            // Skip header if needed
            if (!parser.EndOfData)
            {
                parser.ReadLine();
            }

            while (!parser.EndOfData)
            {
                var parsedObject = (object)(typeof(T) switch
                {
                    var type when type == typeof(Student) => ParseFieldsToStudent(parser),
                    var type when type == typeof(Kwalificatie) => ParseFieldsToKwalificatie(parser),
                    var type when type == typeof(VervallenKwalificatie) => ParseFieldsToVervallenKwalificatie(parser),
                    _ => throw new InvalidOperationException("Unexpected type used as input")
                });

                if (parsedObject is T castObject)
                {
                    objects.Add(castObject);
                }
            }
        }

        return objects;
    }

    private static Student ParseFieldsToStudent(TextFieldParser parser)
    {
        var fields = parser.ReadFields();

        if (fields == null)
        {
            throw new NullReferenceException($"Csv fields are null on line number {parser.LineNumber}");
        }

        return new Student(
            Voornaam: fields[0],
            Tussenvoegsel: fields[1],
            Achternaam: fields[2],
            Email: fields[3],
            CREBOnummer: int.Parse(fields[4])
        );
    }

    private static Kwalificatie ParseFieldsToKwalificatie(TextFieldParser parser)
    {
        var fields = parser.ReadFields();

        if (fields == null)
        {
            throw new NullReferenceException($"Csv fields are null on line number {parser.LineNumber}");
        }

        return new Kwalificatie(
            CREBONummer: int.Parse(fields[0]),
            Naam: fields[1],
            Niveau: int.Parse(fields[2])
        );
    }

    private static VervallenKwalificatie ParseFieldsToVervallenKwalificatie(TextFieldParser parser)
    {
        var fields = parser.ReadFields();

        if (fields == null)
        {
            throw new NullReferenceException($"Csv fields are null on line number {parser.LineNumber}");
        }

        return new VervallenKwalificatie(
            VervallenCREBONummer: int.Parse(fields[0]),
            VervangendeCREBONummer: fields[1]
        );
    }
}