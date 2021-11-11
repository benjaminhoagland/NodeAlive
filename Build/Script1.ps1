If(Test-Connection 8.8.8.8 -Quiet)
{
    Write-Output "Connecton test to 8.8.8.8 successful."
    exit 0;
}
else 
{
    Write-Output "Connecton test to 8.8.8.8 failure."
    exit 1;   
}