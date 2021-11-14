$dest = "8.8.8.7"
If(Test-Connection $dest -Quiet)
{
    Write-Output "Connecton test to $dest successful."
    exit 0;
}
else 
{
    throw "Connecton test to $dest failure."
    exit 1;   
}