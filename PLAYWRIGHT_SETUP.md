# Vikram Solar - Playwright Test Setup

## Prerequisites

Before running the Playwright tests, you must install the required browser binaries.

### Installation Steps

1. **Run the browser installation script:**

   ```powershell
   .\install-browsers.ps1
   ```

   Or manually run:

   ```powershell
   pwsh bin/Debug/net10.0/playwright.ps1 install
   ```

2. **Verify installation:**

   The browsers should be downloaded to `%USERPROFILE%\.ms-playwright\`

### Troubleshooting

If you encounter an error like:
```
Executable doesn't exist at C:\Users\...\ms-playwright\chromium_headless_shell-1169\chrome-win\headless_shell.exe
```

This means the Playwright browsers have not been installed. Please run the installation script above.

### Running Tests

After browsers are installed, you can run the tests normally:

```powershell
dotnet test
```

Or run specific tests using the NUnit Test Explorer in Visual Studio.

## Project Structure

- `UnitTest1.cs` - Contains Playwright NUnit tests
- `install-browsers.ps1` - Script to install Playwright browsers
- `Vikram_Solar.csproj` - Project file
