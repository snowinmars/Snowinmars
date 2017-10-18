Write-Host "Constructing..."
Get-Content (dir | % { $_.Name }) | clip
Write-Host "Done by coping to the clipboard";