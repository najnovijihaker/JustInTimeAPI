for ($i = 0; $i -lt 10; $i++) {
    $idx = $i;
    $params = @{
        "teamId"      = "$idx";
        "name"        = "Proj$idx";
        "description" = "f$idx proj";
        "clientName"  = "tr$idx";
    }    
    Invoke-WebRequest -Uri https://localhost:7171/api/Project/create -Method POST -Body ($params | ConvertTo-Json) -ContentType "application/json" -UseBasicParsing
}