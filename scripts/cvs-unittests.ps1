$tempFolderPath = [System.IO.Path]::Combine([System.IO.Path]::GetTempPath(), "CVS-ExpenseTracker-CoverageReport")
$toolName = "dotnet-reportgenerator-globaltool"
$toolVersion = "5.2.0"

$installedTool = dotnet tool list --global | Where-Object { $_ -match $toolName -and $_ -match $toolVersion }

if ($installedTool.Count -eq 0) {
    Write-Host ">>> Installing $toolName version $toolVersion..."
    dotnet tool install --global $toolName --version $toolVersion | Out-Null
    Write-Host ">>> $toolName version $toolVersion has been installed."
} else {
    Write-Host ">>> Already installed $toolName version $toolVersion."
}

Write-Host ">>> Running tests..."
dotnet test .\CabaVS.ExpenseTracker.sln --collect:"XPlat Code Coverage" | Out-Null

Write-Host ">>> Generating HTML report..."
reportgenerator -reports:./**/coverage.cobertura.xml -targetdir:$tempFolderPath -reporttypes:HtmlInline_AzurePipelines_Light | Out-Null

Write-Host ">>> Scanning for Test Results folders to remove..."
$foldersToRemove = Get-ChildItem -Directory -Recurse -Filter "TestResults"

foreach ($folder in $foldersToRemove) {
    Write-Host ">>> Removing folder: $($folder.FullName)"
    Remove-Item -Path $folder.FullName -Recurse -Force
}

Write-Host ">>> Opening coverage report..."
Start-Process "$tempFolderPath\index.html" -Verb "open"