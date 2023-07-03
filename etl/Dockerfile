FROM ubuntu:latest

RUN apt-get update && apt-get install -y \
    gnumeric \
    && rm -rf /var/lib/apt/lists/*
COPY crebolijst2022.xlsx .
CMD ["ssconvert", "--export-type=Gnumeric_stf:stf_csv crebolijst2022.xlsx convert.csv"]