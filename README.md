# ProyectoMagnetron

## *Descripción*
Solución para software de facturación de productos , creado en .net core  8 
## *Pre-requisitos*
Para clonar y ejecutar esta aplicación, necesitará [Git](https://git-scm.com), Un Id de desarrollo (visual estudio Code o visual estudio 2022), Sql Server y Docker instalados en su computadora. 


# Clonar repositorio
Desde su línea de comando:
$ git clone https://github.com/JoseTavera89/ProyectoMagnetron.git

# Ejecutar Script de base de datos
en el repositorio descargado se encuentre el archivo scriptDb.sql , el cual se debe abrir y ejecutar en base de datos sql server , para asi crear la base de datos y estructuras de tablas.

#Ajustes antes de ejecutar
abrir la solucion FacturacionMagnetron.sln
Luego buscar dentro del proyecto FacturacionMagnetron.Api el archivo Dockerfile y modificar la variable de entorno DB_CONNECTION_STRING, para la cual se debe modificar los datos de conexion a base de datos es decir remplazar la ip del servidor de base de datos , el usuario y la contraseña 

# Correr aplicación en docker
ejecutar Docker desktop

Desde su línea de comando:

docker build -t facturacionmagnetron/tag -f FacturacionMagnetron.Api/Dockerfile .

docker run -d -p 8080:80 facturacionmagnetron/tag

Luego podrá acceder desde el [navegador](http://localhost:8080/swagger/index.html) para validar que se visualice correctamente el Swagger del proyecto.

```

## *Módulos del proyecto*

El proyecto Esta conformado por una arquitectura Onio , donde se destaca el uso de la inversion de dependencias y la inyeccion de dependencias 

- FacturacionMagnetron.Api:
  Proyecto donde están los controladores y los métodos que se exponen , se agregó un Middware para interceptar las peticiones y a su vez poder guardar los log de errores este proyecto depende (Application,Domain e Infrastructure)

- FacturacionMagnetron.Application:
  Este proyecto se encarga de guardar la lógica del negocio, se implementa el paquete de Mapster para hacer un mapeo entre las entidades y los Dto , ya que en ocasiones solo se va a requerir mostrar algunas propiedades no toda la entidad. Este proyecto depende del proyecto Domain.

- FacturacionMagnetron.Domain:
  Proyecto donde se crea la estructura de las entidades y los Dto a trabajar, también lleva a su vez las interfaces que se utilizaran, Se creo una interfaz genérica para consumir servicios ya que esto maximiza la reutilización.
  
- FacturacionMagnetron.Infrastructure:
  Módulo que controla la conexión base de datos y servicios con los cuales se tiene comunicación. Se utilizo el patrón repositorio u el Unit of work para centralizar las peticiones. 

- FacturacionMagnetron.Test
  Proyecto con prueba unitarias.
