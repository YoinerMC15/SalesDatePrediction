Instrucciones para Configurar la Base de Datos SQL Server en Docker
Crear un Contenedor SQL Server Ejecuta el siguiente comando para crear y ejecutar el contenedor de SQL Server:

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Prueba202501" -p 1433:1433 --name sqlserver-container -d mcr.microsoft.com/mssql/server:2022-latest

Este comando crea el contenedor con el nombre sqlserver-container, configura la contraseña del usuario sa como Prueba202501, y expone el puerto 1433.
Verificar el Contenedor Comprueba que el contenedor esté corriendo:

docker ps
Copiar el Script SQL al Contenedor Para copiar el archivo DBSetup.sql al contenedor, ejecuta:

docker cp ./database/DBSetup.sql sqlserver-container:/DBSetup.sql
Acceder al Contenedor como Root Usa el siguiente comando para acceder al contenedor con permisos de administrador:

docker exec -it --user root sqlserver-container bash
Instalar Herramientas SQL (sqlcmd) en el Contenedor Ejecuta los siguientes comandos dentro del contenedor:

Actualizar repositorios e instalar dependencias:

apt-get update && apt-get install -y curl apt-transport-https gnupg
Agregar claves y repositorio de Microsoft:

curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add -
curl https://packages.microsoft.com/config/debian/11/prod.list > /etc/apt/sources.list.d/mssql-release.list
Actualizar nuevamente los repositorios:

apt-get update
Instalar las herramientas SQL Server (sqlcmd):

ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev
Agregar las herramientas al PATH del sistema:

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc
Verificar la Instalación de sqlcmd Confirma que la herramienta fue instalada correctamente ejecutando:

/opt/mssql-tools/bin/sqlcmd -?
Ejecutar el Script SQL Usa sqlcmd para ejecutar el script SQL que copiaste al contenedor:


/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Prueba202501 -i /DBSetup.sql
Conectarte a la Base de Datos en el Contenedor Para conectarte a la base de datos desde el contenedor, ejecuta:

docker exec -it sqlserver-container bash
/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Prueba202501
Generar Entidades Desde la Base de Datos Si necesitas generar las entidades en tu proyecto desde la base de datos, usa este comando:

dotnet ef dbcontext scaffold "Server=localhost,1433;Database=StoreSample;User Id=sa;Password=Prueba202501;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o "RutaDeSalida" --context-dir ../Infrastructure/Persistence --context ApplicationDbContext
Reemplaza RutaDeSalida con la ruta donde quieres generar las entidades.
Reconstruir Contenedores Faltantes o Actualizar Si necesitas reconstruir contenedores que falten o actualizar las imágenes de Docker, ejecuta:

docker-compose up --build