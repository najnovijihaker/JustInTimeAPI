for ($i = 0; $i -lt 10; $i++) {
    $idx = $i;
    $params = @{
        "firstname"       = "Fake$idx";
        "lastName"        = "Acc$idx";
        "userName"        = "f$idx@mmserver.io";
        "email"           = "f$idx";
        "password"        = "fakeaccount$idx";
        "confirmPassword" = "fakeaccount$idx";
    }    
    Invoke-WebRequest -Uri https://localhost:7171/api/Account/register -Method POST -Body ($params | ConvertTo-Json) -ContentType "application/json" -UseBasicParsing
}