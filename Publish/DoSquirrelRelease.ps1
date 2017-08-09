param(
# Full path to NuGet.exe
$aNuGetExe,
# Full path to Squirrel.exe
$aSquirrelExe,
# Output directory including trailing backslash
$aOutDir,
# Version string typically in the following format: "1.2.3"
$aVersion,
# Full path to nuspec
$aNuSpecPath,
# Application download URL, including trailing forwardslash
$aUrl,
# Application ID, should match the name of the NuSpec file
$aAppId
)

# Print input parameters
Write-Output("Squirrel Release")
Write-Output("NuGet path: $aNuGetExe")
Write-Output("Squirrel path: $aSquirrelExe")
Write-Output("Output directory: $aOutDir")
Write-Output("Version: $aVersion")
Write-Output("NuSpec path: $aNuSpecPath")
Write-Output("Download URL: $aUrl")
Write-Output("App ID: $aAppId")

# Check if the nuspec exists
if (-not(Test-Path $aNuSpecPath))
{
    Write-Warning("NuSpec not found")
    exit 0;
}

# Generate nupkg from nuspec, version and output directory
Write-Output("Generating nupkg...")
[System.Diagnostics.Process]::Start($aNuGetExe,
"pack $aNuSpecPath -Properties Configuration=Release;Version=$aVersion -OutputDirectory $aOutDir -BasePath $aOutDir"
).WaitForExit()

# Create download folder below our output directory, if needed
$squirrelReleaseDir = $aOutDir + "Squirrel\";
if (-not(Test-Path $squirrelReleaseDir))
{
    New-Item $squirrelReleaseDir -ItemType Directory | Out-Null;
}

# Download RELEASES file
Write-Output("Downloading Squirrel RELEASES...")
$localReleasesFileName = $squirrelReleaseDir + "RELEASES"
$remoteFileName = $aUrl + "RELEASES"

$firstRelease = $false;

# Try downloading RELEASES file from remote location
Try
{
   Invoke-WebRequest -OutFile $localReleasesFileName $remoteFileName;
}
Catch [System.Net.WebException] 
{
    # Something went wrong
    # Check if we failed to locate our 
    if ( $_.Exception.Response.StatusCode.value__ -eq 404 )
    {
        Write-Warning ("Remote RELEASES file not found, assuming first release...");
        # Just keep going
        $firstRelease = $true;
    }
    else
    {
        Write-Warning ("Something went wrong with RELEASES download, giving up!");
        Write-Warning ($_.ToString());
        exit 1; # Abort and fail the build
    }
}

if (-not $firstRelease)
{
    # Parse RELEASES file to obtain the name of our last package
    $reader = [System.IO.File]::OpenText( $localReleasesFileName )
    while($null -ne ($line = $reader.ReadLine()))
    {
        $lastFileName = $line.Split(" ")[1]    
    }
    $reader.Close();

    # Work out version number
    $version = $lastFileName.Split("-")[1]
    $major = $version.Split(".")[0]
    $minor = $version.Split(".")[1]
    $build = $version.Split(".")[2]

    if ($aVersion -eq $version)
    {
        # Warn
        Write-Warning ("Version $version already published!")
        # Delete downloaded RELEASES to avoid uploading them back pointlessly 
        Remove-Item $localReleasesFileName
        # Still a successful exit as this should not fail the build
        exit 0
    }

    # Download last package
    Write-Output("Downloading last Squirrel package...")
    $localFileName = $squirrelReleaseDir + $lastFileName
    $remoteFileName = $aUrl + $lastFileName
    Invoke-WebRequest -OutFile $localFileName $remoteFileName;
}

# Do our Squirrel release
Write-Output("Generate Squirrel release...")
Write-Output("Output directory: $squirrelReleaseDir")
[System.Diagnostics.Process]::Start($aSquirrelExe," --r $squirrelReleaseDir --releasify $aOutDir$aAppId.$aVersion.nupkg").WaitForExit()

# Clean-up by removing the downloaded Squirrel package
if (-not $firstRelease)
{
    Remove-Item $localFileName
}


# From here on the Squirrel folder contains only stuff we need to upload

# Success
exit 0