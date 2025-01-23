# Usa la imagen oficial de SQL Server
FROM mcr.microsoft.com/mssql/server:2022-CU6-ubuntu-20.04


# Cambiar al usuario root para instalar herramientas
USER root

RUN apt-get clean && rm -rf /var/lib/apt/lists/* && mkdir -p /var/lib/apt/lists/partial


# Actualizar los repositorios y agregar herramientas necesarias
RUN apt-get update && apt-get install -y \
    curl \
    apt-transport-https \
    gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > /etc/apt/trusted.gpg.d/microsoft.asc.gpg && \
    curl https://packages.microsoft.com/config/debian/10/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> /etc/bash.bashrc && \
    apt-get clean && rm -rf /var/lib/apt/lists/*

# Cambiar al usuario predeterminado de SQL Server
USER mssql

# Establecer el directorio de trabajo
WORKDIR /database

# Copiar el script SQL
COPY DBSetup.sql /database/DBSetup.sql

# Exponer el puerto SQL Server
EXPOSE 1433

# Comando para iniciar SQL Server y ejecutar el script SQL
CMD /bin/bash -c "/opt/mssql/bin/sqlservr & sleep 20 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Prueba202501' -d master -i /database/DBSetup.sql && tail -f /dev/null"
