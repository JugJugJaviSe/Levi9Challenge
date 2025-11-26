# Levi9Challenge – README

## Korišćene tehnologije i verzije
- .NET SDK: 9.0.304
- C# verzija: 13
- xUnit: latest
- In-memory baza

## Podešavanje okruženja
1. Instalirajte .NET 9 SDK sa [dotnet.microsoft.com](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
2. Otvorite terminal u folderu projekta (ili klonirajte na drugi nacin)
3. Klonirajte repozitorijum:
```bash
git clone <repo-link>
```
4. Otvorite projekat u Visual Studio
5. Restore NuGet
```bash
dotnet restore
dotnet build
```
6. Aplikacija radi na portu 7093 za https i na portu 5020 za http
   
## Pokretanje aplikacije

```bash
dotnet run
```
(ili kliknite f5 ili zelenu strelicu)

## Pokretanje xUnit testova

```bash
cd Levi9Challenge.Tests
dotnet test
```
(ili kliknite Test iz taba na vrhu i onda Run All Tests)

Testovi koriste in-memory repozitorijume i ne mogu se pokretati dok aplikacija radi.
