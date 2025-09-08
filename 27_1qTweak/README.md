27_1q tweak - WinForms Optimizer with Offline Licensing
=======================================================

Overview
--------
- GUI replicates your menu (Full Tweak, Boost Now, Game Mode, etc.).
- Runs commands directly via cmd (no temp .bat), logs streamed to UI.
- Admin manifest (UAC) enabled.
- Offline license: JWT RS256 + hardware fingerprint.

Build
-----
1) Install .NET 8 SDK.
2) Restore and build:
```
cd 27_1qTweak
 dotnet restore
 dotnet build -c Release
```
3) Publish single-file EXE (win-x64):
```
 dotnet publish -c Release -r win-x64 -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true --self-contained false
```
Output: `bin/Release/net8.0-windows/win-x64/publish/27_1qTweak.exe`

License (Offline) Workflow
--------------------------
- App shows the device fingerprint and requests a license on first run.
- You generate a JWT license offline (signed with your RSA private key) embedding that fingerprint.
- App validates: signature + expiration + matching fingerprint. If invalid, it exits.

Generate RSA keys (once)
------------------------
```
openssl genrsa -out private.pem 2048
openssl rsa -in private.pem -pubout -out public.pem
```
- Put `public.pem` content into `Program.cs` (PublicKeyPem).
- Keep `private.pem` secret on your machine.

Issue a license (Python)
------------------------
Use `make_license.py` to issue a license for a given fingerprint:
```
python make_license.py ABCDEF1234... > license.jwt
```
Give `license.jwt` to customer; they paste/save it via the app.

Where license is stored
-----------------------
- `C:\ProgramData\27_1qTweak\license.jwt`

Icon
----
- Replace `app.ico` with your icon file.

Notes
-----
- SmartScreen may show; user can click Run anyway.
- Some commands may require reboot or take time.
- Run as Administrator.