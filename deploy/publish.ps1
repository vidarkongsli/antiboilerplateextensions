param(
    [Parameter(Mandatory)]
    $apiKey,
    [Parameter(Mandatory)]
    $version
)
$ErrorActionPreference = 'stop'
dotnet nuget push "$PSScriptRoot\antiboilerplate.$($version).nupkg" -k $apiKey `
    -s https://api.nuget.org/v3/index.json
