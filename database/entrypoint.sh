#!/bin/bash

# Iniciar SQL Server en segundo plano
/opt/mssql/bin/sqlservr &

# Esperar a que SQL Server esté listo
sleep 20

# Ejecutar el script SQL
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Prueba202501' -d master -i /database/DBSetup.sql

# Mantener el contenedor corriendo
tail -f /dev/null

chmod +x database/entrypoint.sh