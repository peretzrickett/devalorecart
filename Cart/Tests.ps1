$item1 = @{
    "ExternalSource" = "Test"
    "ExternalId" = "WGT1"
    "Contents" = "Widget"
    "UnitPrice" = 21.00
    "Quantity" = 4
}

$item2 = @{
    "ExternalSource" = "Test"
    "ExternalId" = "FGT2"
    "Contents" = "Fidget"
    "UnitPrice" = 17.00
    "Quantity" = 3
}

$headers = New-Object "System.Collections.Generic.Dictionary[[String],[String]]"
$headers.Add("Content-Type", "application/json")

$cart = Invoke-RestMethod 'http://localhost:50154/api/shoppingcart' -Method 'GET' -Headers $headers -Body $body
$cartId = $cart.id
$apiUrl = "http://localhost:50154/api/shoppingcart/${cartId}"
Write-Host $apiUrl
write-Host "#### Start with empty cart ####"
$cart | ConvertTo-Json

Write-Host "#### Add 4 Widgets and 3 Fidgets ####"

Invoke-RestMethod $apiUrl -Method 'PUT' -Headers $headers -Body ($item1 | ConvertTo-Json)
Invoke-RestMethod $apiUrl -Method 'PUT' -Headers $headers -Body ($item2 | ConvertTo-Json)

Write-Host "#### Cart Contents ####"
$cart = Invoke-RestMethod $apiUrl -Method 'GET' -Headers $headers -Body $body
$cart | ConvertTo-Json

Write-Host "#### Assert itemCount 7 and sumTotal 135 #####"
$success = $cart.itemCount -eq 7 -and $cart.sumTotal -eq 135
if ($success) {
    Write-Host "#### SUCCESS ####"
} else {
    Write-Host "#### FAIL ####"
}

Write-Host "#### Remove 1 Widget and 2 Figets ####"
$item1.Quantity = 1
$item2.Quantity = 2
Invoke-RestMethod $apiUrl -Method 'DELETE' -Headers $headers -Body ($item1 | ConvertTo-Json)
Invoke-RestMethod $apiUrl -Method 'DELETE' -Headers $headers -Body ($item2 | ConvertTo-Json)

Write-Host "#### Cart Contents ####"
$cart = Invoke-RestMethod $apiUrl -Method 'GET' -Headers $headers -Body $body
$cart | ConvertTo-Json

Write-Host "#### Assert itemCount 4 and sumTotal 80 #####"
$success = $cart.itemCount -eq 4 -and $cart.sumTotal -eq 80
if ($success) {
    Write-Host "#### SUCCESS ####"
} else {
    Write-Host "#### FAIL ####"
}

Write-Host "#### Remove last Figet ####"
Invoke-RestMethod $apiUrl -Method 'DELETE' -Headers $headers -Body ($item2 | ConvertTo-Json)

Write-Host "#### Cart Contents ####"
$cart = Invoke-RestMethod $apiUrl -Method 'GET' -Headers $headers -Body $body
$cart | ConvertTo-Json

Write-Host "#### Assert itemCount 3 and sumTotal 63 #####"
$success = $cart.itemCount -eq 3 -and $cart.sumTotal -eq 63
if ($success) {
    Write-Host "#### SUCCESS ####"
} else {
    Write-Host "#### FAIL ####"
}

