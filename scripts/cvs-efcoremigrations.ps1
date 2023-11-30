param (
    [string] $MigrationName,
    [switch] $UpdateDatabase
)

if ($UpdateDatabase) {
    $Action = "database update"
    $OperationLogMsg = ">>> Updating database to latest migration..."
    $FinalLogMsg = ">>> Database updated."
}
else {
    $Action = "migrations add $MigrationName --output-dir Persistence\Migrations"
    $OperationLogMsg = ">>> Creating EF migration '$MigrationName'..."
    $FinalLogMsg = ">>> Migration created."
}

$packageName = "Microsoft.EntityFrameworkCore.Design"
$packageVersion = "8.0.0"

$dotnetEfCommand = Get-Command dotnet-ef -ErrorAction SilentlyContinue
if ($dotnetEfCommand -eq $null) {
    Write-Output ">>> dotnet-ef tool is not installed. Installing..."
    dotnet tool install --global dotnet-ef
} else {
    Write-Output ">>> dotnet-ef tool is already installed. Updating..."
    dotnet tool update --global dotnet-ef
}

Write-Output ">>> Adding Microsoft.EntityFrameworkCore.Design package..."
dotnet add .\src\CabaVS.ExpenseTracker.API\CabaVS.ExpenseTracker.API.csproj package $packageName -v $packageVersion

Write-Output $OperationLogMsg
$Action = "dotnet ef " + $Action + " --project .\src\External\CabaVS.ExpenseTracker.Infrastructure\CabaVS.ExpenseTracker.Infrastructure.csproj --startup-project .\src\CabaVS.ExpenseTracker.API\CabaVS.ExpenseTracker.API.csproj";
Invoke-Expression -Command $Action

Write-Output ">>> Removing Microsoft.EntityFrameworkCore.Design package..."
dotnet remove .\src\CabaVS.ExpenseTracker.API\CabaVS.ExpenseTracker.API.csproj package Microsoft.EntityFrameworkCore.Design

Write-Output $FinalLogMsg