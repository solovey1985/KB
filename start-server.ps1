

# Double-click to start server in new window and open Chrome
Start-Process powershell -ArgumentList '-NoExit', '-Command', 'Set-Location E:\Learn\KB; npm start'
Start-Sleep -Seconds 2
Start-Process "chrome.exe" "http://localhost:3001"
Write-Host "Documentation server started in a new window. Chrome opened at http://localhost:3000."
Pause
