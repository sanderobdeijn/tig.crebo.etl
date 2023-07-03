# tig.crebo.etl

## How to run
Run ```./etl/crebo.etl.sh``` to extract Kwalificatie.csv & VervallenKwalificatie.csv from the CREBOLijst excel file.

Run unittest tests in src/Tig.Crebo.Etl.Tests/Tig.Crebo.Etl.Tests.csproj

Run src/Tig.Crebo.Etl/Tig.Crebo.Etl.csproj to run the project

## Future improvements
- Create github actions to download and extract excel files regularly
- Move merging of kwalificatie & VervallenKwalificatie from OpleidingenService to data extract in github actions
- Find a of the shelf Extraction, Transformation and Load product to replace excel extract and data transform (preferable in docker)
- Add github actions to build, test and deploy to azure
- Dockerize app
- Find out if UI is necessary of if we directly connect to the school system to input the data by api and not copy and paste the data.
