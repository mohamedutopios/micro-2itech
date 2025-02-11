$services = @("ProductService","OrderService", "CustomerService")

foreach ($service in $services) {
    if( $service ){
        New-Item -Path $service -ItemType Directory
    }
    New-Item -Path "$service\Controllers" -ItemType Directory
    New-Item -Path "$service\Models" -ItemType Directory
    New-Item -Path "$service\Services" -ItemType Directory
    New-Item -Path "$service\Repositories" -ItemType Directory
    New-Item -Path "$service\Data" -ItemType Directory
    Write-Host "Dossiers créés pour $service"


}