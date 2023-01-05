for ($i = 0; $i -lt 10; $i++) {
    $idx = $i;
    $params = @{
        "projectId"   = "$idx";
        "accountId"   = "1";
        "hoursWorked" = "$idx";
    }    
    Invoke-WebRequest -Uri https://localhost:7171/api/Project/addhours -Method POST -Body ($params | ConvertTo-Json) -ContentType "application/json" -UseBasicParsing
}