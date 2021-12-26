## Function app 6!

```powershell
$appName = "intitfunctions";
$rgName = "rg-$($appName)";
$location = "westeurope";
$storageAccountName = "st$($appName)";
$functionAppName = "func-$($appName)";

az group create -n $rgName -l $location --tags temp=false;


## Monitor

$monitorName = "la-$($appName)";
$monitorId = az monitor log-analytics workspace create `
	-g $rgName -n $monitorName `
	--query id -o tsv;


## Application Insight
$appInsightName = "appi-$($appName)";
az monitor app-insights component create -a $appInsightName -g $rgName `
	--workspace $monitorId -l $location;


## Create Storage account

$storageAccountName = "st$($appName)";
$storageId =  az storage account  create -g $rgName -l $location `
	-n $storageAccountName --query id -o tsv;
$storageId;


## Create Function App (6)!


$functionAppId = az functionapp create -g $rgName -n $functionAppName `
	-s $storageId --app-insights $appInsightName `
	--runtime dotnet-isolated --functions-version 4 `
	--consumption-plan-location $location `
	--query id -o tsv;
$functionAppId;

	
$functionAppName = "func-$($appName)";
func azure functionapp publish $functionAppName; 


### Create xslt container

az storage container create --name xslt --account-name $storageAccountName

az storage blob upload --account-name $storageAccountName `
    --container-name xslt --file ".\xslt\morten.xslt"

$url = "https://func-intitfunctions.azurewebsites.net/api/transform?code=ZosJOL1C/ezRUUWIpbbunywwq9rJzkOQ7LKG3IO2ejoe/u3RQ9zzsg==";

# $url = "http://localhost:7071/api/Transform" ;
invoke-webrequest -Uri $url  -method Post -Headers @{ "xslt" = "morten.xslt"; } -body "<Input />" | Select-Object -expandProperty Content;

```

