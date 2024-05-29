# Proyecto Magnetrón

## *Descripción*
El Proyecto Magnetrón es una solución de software para la facturación de productos, desarrollada en .NET Core 8

## *Pre-requisitos*
Antes de clonar y ejecutar esta aplicación, asegúrate de tener instalados los siguientes componentes en tu computadora:
- Git
- Un IDE de desarrollo compatible con .NET Core
- SQL Server
- Docker

# Clonar repositorio
Para clonar el repositorio, ejecuta el siguiente comando desde tu línea de comandos:
```
$ git clone https://github.com/JoseTavera89/ProyectoMagnetron.git
```
# Ejecutar Script de Base de Datos
Dentro del repositorio descargado, encontrarás el archivo scriptDb.sql. Abre este archivo y ejecútalo en tu instancia de SQL Server para crear la base de datos y las estructuras de tablas necesarias.

# Ajustes en el Archivo Dockerfile

Antes de ejecutar el proyecto, necesitas ajustar la configuración de conexión a la base de datos en el archivo Dockerfile del proyecto FacturacionMagnetron.Api.

Sigue estos pasos:

- Ubica el archivo Dockerfile dentro del proyecto FacturacionMagnetron.Api. Este se encuentra en la raíz del proyecto.

- Abre el archivo Dockerfile en un editor de texto.

- Dentro del archivo Dockerfile, busca la definición de la variable de entorno DB_CONNECTION_STRING.

- La cadena de conexión se verá algo como esto:
``` 
ENV DB_CONNECTION_STRING="Server=127.0.0.1,1433;Database=Magnetron;User Id=usuario;Password=contraseña;MultipleActiveResultSets=true;TrustServerCertificate=True" 
```
- Reemplaza los valores de la cadena de conexión con los nuevos datos de conexión a tu servidor de base de datos. Esto incluye la dirección IP del servidor de base de datos, el nombre de usuario y la contraseña.

- Guarda los cambios en el archivo Dockerfile.

- Una vez que hayas realizado estos cambios, la aplicación utilizará la nueva configuración de conexión a la base de datos cuando se despliegue en un entorno Dockerizado.

# Despliegue de la Aplicación en Docker
Para desplegar la aplicación en Docker, sigue estos pasos:
- Ejecuta Docker Desktop.

- Desde tu línea de comandos, ejecuta los siguientes comandos:
```
docker build -t facturacionmagnetron -f FacturacionMagnetron.Api/Dockerfile .
docker run -d -p 8080:8080 --name netcontainer facturacionmagnetron
```
- Una vez desplegada la aplicación en Docker, puedes acceder a ella desde tu navegador utilizando la siguiente URL: [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html), donde podrás validar que se visualice correctamente el Swagger del proyecto.


## *Módulos del proyecto*

El Proyecto Magnetrón está estructurado siguiendo una arquitectura Onion, que se caracteriza por el uso de la inversión de control e inyección de dependencias.

- FacturacionMagnetron.Api:
  Contiene los controladores y métodos expuestos, así como un Middleware para interceptar las peticiones y guardar los logs de errores. Este proyecto depende de los proyectos Application, Domain e Infrastructure.

- FacturacionMagnetron.Application:
  Este proyecto se contiene la lógica del negocio. Se implementa el paquete de Mapster para hacer un mapeo entre las entidades y los DTO, ya que en ocasiones solo se va a requerir mostrar algunas propiedades y no toda la entidad. Este proyecto depende del proyecto Domain.

- FacturacionMagnetron.Domain:
  Proyecto donde se crea la estructura de las entidades y los DTO a trabajar, también lleva a su vez las interfaces que se utilizaran. Se creo una interfaz genérica para consumir servicios, maximizando la reutilización.
  
- FacturacionMagnetron.Infrastructure:
  Módulo que controla la conexión a base de datos y servicios con los cuales se tiene comunicación. Se utilizaron los patrones Repository y el  Unit Of Work para centralizar las peticiones. 

- FacturacionMagnetron.Test
  Proyecto con prueba unitarias.

Esta estructura modular facilita el mantenimiento y la escalabilidad del proyecto, manteniendo una clara separación de responsabilidades
