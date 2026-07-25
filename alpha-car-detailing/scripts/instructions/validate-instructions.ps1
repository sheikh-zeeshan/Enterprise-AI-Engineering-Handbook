$ErrorActionPreference = 'Stop'
$requiredFiles = @('architecture.md','coding-standards.md','testing.md','skills.md')
foreach ($name in $requiredFiles) {
    $path = Join-Path "$PSScriptRoot/../../instructions" $name
    if (-not (Test-Path $path)) { throw "Missing instruction file: $name" }
    Write-Host "[PASS] $name"
}
