Get-ChildItem "$PSScriptRoot/../.." -Recurse -Filter *.csproj |
    ForEach-Object { $_.FullName.Replace((Resolve-Path "$PSScriptRoot/../..").Path + [IO.Path]::DirectorySeparatorChar, '') } |
    Sort-Object
