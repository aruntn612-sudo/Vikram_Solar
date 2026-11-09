# Playwright Browser Installation Script
# This script installs the required Playwright browsers for running tests

Write-Host "Installing Playwright browsers..." -ForegroundColor Green

# Try using dotnet playwright CLI first (if installed as a tool)
try {
	dotnet playwright install
	Write-Host "Successfully installed browsers using dotnet playwright CLI" -ForegroundColor Green
	exit 0
}
catch {
	Write-Host "dotnet playwright CLI not found, trying alternate method..." -ForegroundColor Yellow
}

# Try using the NuGet tool manifest approach
$toolManifestPath = "./.config/dotnet-tools.json"
if (Test-Path $toolManifestPath) {
	dotnet tool restore
	dotnet playwright install
	Write-Host "Successfully installed browsers" -ForegroundColor Green
	exit 0
}

# Fallback: Use Microsoft.Playwright assembly
Write-Host "Using Playwright NuGet package to install browsers..." -ForegroundColor Yellow
$playwrightBinary = Get-ChildItem -Path "$env:USERPROFILE\.nuget\packages\microsoft.playwright\*" -Filter "playwright.ps1" -Recurse | Select-Object -First 1

if ($playwrightBinary) {
	& $playwrightBinary.FullName install
	Write-Host "Successfully installed browsers" -ForegroundColor Green
	exit 0
}
else {
	Write-Host "ERROR: Could not find Playwright installation script!" -ForegroundColor Red
	Write-Host "Please ensure Microsoft.Playwright NuGet package is installed." -ForegroundColor Red
	exit 1
}
