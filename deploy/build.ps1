
dotnet pack $psscriptroot\..\src\antiboilerplate\antiboilerplate.csproj `
    --include-symbols -p:SymbolPackageFormat=snupkg --configuration release `
    --output $psscriptroot
