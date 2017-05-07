var gulp = require("gulp");
var msbuild = require("gulp-msbuild");
 
gulp.task("default", function() {
    return gulp.src("./NaaStockTrader.Droid/NaaStockScanner.Droid.csproj")
        .pipe(msbuild());
});