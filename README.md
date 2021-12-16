## Function app 6!

```powershell
$appName = "intitfunctions";
$rgName = "rg-$($appName)";
$location = "westeurope";
$storageAccountName = "st$($appName)";

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

$functionAppName = "func-$($appName)";
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
```
