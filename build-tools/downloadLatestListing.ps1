curl -OutFile stock.json http://localhost:1337/downloadLatest
Write-Output "$(build.binariesdirectory)/$(BuildConfiguration)"