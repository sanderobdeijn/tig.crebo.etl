XLSXDOC=crebolijst2022.xlsx

ssconvert --import-type=Gnumeric_Excel:xlsx -O 'separator=; sheet=Vervallen' --export-type=Gnumeric_stf:stf_assistant $XLSXDOC "temp.txt"
sed '1d' temp.txt | sed '1d' | sed -n '/;;;;;/, /;;;;;/p' | sed '/;;;;;/d' | cut -d ';' -f 1,6 | sed '/\/$/d' > VervallenKwalificatie.csv
rm temp.txt

cp VervallenKwalificatie.csv "../src/Tig.Crebo.Etl/Csv"
cp VervallenKwalificatie.csv "../src/Tig.Crebo.Etl.Tests/Csv"

ssconvert --import-type=Gnumeric_Excel:xlsx -O "separator=; sheet='Complete lijst'" --export-type=Gnumeric_stf:stf_assistant $XLSXDOC "temp.txt"
cut -d ';' -f 4,5,6 temp.txt | sed '/^;/d' | sed '/^Crebo/d;/^;/d' > Kwalificatie.csv
rm temp.txt

cp Kwalificatie.csv "../src/Tig.Crebo.Etl/Csv"
cp Kwalificatie.csv "../src/Tig.Crebo.Etl.Tests/Csv"