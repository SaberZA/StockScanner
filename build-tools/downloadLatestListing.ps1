curl -OutFile stock.json https://serene-spire-69580.herokuapp.com/downloadLatest
Write-Output "$(build.binariesdirectory)/$(BuildConfiguration)"