$ErrorActionPreference = 'Stop'
$required = @('## Purpose','## Inputs','## Preconditions','## Execution Steps','## Expected Outputs','## Validation Steps','## Approval Requirements','## Completion Evidence')
$skillFiles = Get-ChildItem "$PSScriptRoot/../../skills" -Recurse -Filter SKILL.md
if ($skillFiles.Count -eq 0) { throw 'No SKILL.md files found.' }
foreach ($file in $skillFiles) {
    $content = Get-Content $file.FullName -Raw
    foreach ($heading in $required) {
        if (-not $content.Contains($heading)) { throw "$($file.FullName) is missing $heading" }
    }
    Write-Host "[PASS] $($file.Directory.Name)"
}
Write-Host "Validated $($skillFiles.Count) Skills."
