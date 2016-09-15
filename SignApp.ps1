#keytool -genkey -v -keystore naastockscanner.keystore -alias naakey -keyalg RSA -keysize 2048 -validity 10000

$MsBuildPath = "C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe"
$ProjectPath = ".\NaaStockTrader.Droid\NaaStockScanner.Droid.csproj"
$Java6JarSigner = 'C:\Program Files\Java\jdk1.6.0_45\bin\jarsigner.exe'
$AndroidSdkZipAlign = 'C:\Users\stevenv\AppData\Local\Android\android-sdk\build-tools\23.0.0\zipalign.exe'
$KeyStore = '.\naastockscanner.keystore'
$SignedApkOutputPath = 'C:\Published\NaaStockScanner.apk'
$ReleaseBuildApkPath = '.\NaaStockTrader.Droid\bin\Release\NaaStockScanner.Droid.apk'
$AlignedApkOutputPath = 'C:\Published\NaaStockScanner-aligned.apk'

# First clean the Release target.
& $MsBuildPath $ProjectPath /p:Configuration=Release /t:Clean

# Now build the project, using the Release target.
& $MsBuildPath $ProjectPath /p:Configuration=Release /t:PackageForAndroid

# At this point there is only the unsigned APK - sign it.
# The script will pause here as jarsigner prompts for the password.
# It is possible to provide they keystore password for jarsigner.exe by adding an extra command line parameter -storepass, for example
& $Java6JarSigner -verbose -sigalg SHA1withRSA -digestalg SHA1 -keystore $KeyStore -storepass "123456" -signedjar $SignedApkOutputPath $ReleaseBuildApkPath naakey

# Now zipalign it.  The -v parameter tells zipalign to verify the APK afterwards.
& $AndroidSdkZipAlign -f -v 4 $SignedApkOutputPath $AlignedApkOutputPath