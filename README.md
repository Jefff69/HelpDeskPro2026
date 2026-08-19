HelpDesk Pro 2026 

Sistema web de gestión y seguimiento de incidencias desarrollado para el curso Programación V de la Universidad Castro Carazo. 

HelpDesk Pro permite administrar usuarios, roles, catálogos y tickets de soporte técnico, proporcionando una plataforma centralizada para registrar, consultar y dar seguimiento a las incidencias. 

 

Descripción del proyecto 

HelpDesk Pro es una aplicación web desarrollada bajo el patrón ASP.NET Core MVC, diseñada para facilitar la administración de solicitudes de soporte técnico. 

El sistema permite a los usuarios registrar tickets de soporte, consultar su información, agregar comentarios y adjuntar archivos. Además, los usuarios con permisos administrativos pueden administrar los diferentes catálogos y usuarios del sistema. 

El proyecto implementa autenticación y autorización basada en roles, permitiendo controlar el acceso a los diferentes módulos de acuerdo con el perfil del usuario. 

 

Objetivos 

Desarrollar una plataforma web para la gestión de incidencias. 

Implementar autenticación y autorización mediante roles. 

Administrar usuarios y perfiles. 

Administrar los catálogos utilizados por el sistema. 

Registrar y consultar tickets de soporte. 

Permitir el seguimiento de tickets mediante comentarios. 

Permitir la carga y consulta de archivos adjuntos. 

Implementar asignación automática de técnicos. 

Mantener la integridad de la información relacionada con los tickets. 

 

Funcionalidades principales 

Autenticación y autorización 

El sistema cuenta con inicio de sesión y control de acceso basado en roles. 

Los roles utilizados son: 

Super Usuario 

Administrador 

Técnico 

Usuario 

Cada rol posee diferentes permisos dentro de la aplicación. 

 

Gestión de usuarios 

Los usuarios con permisos administrativos pueden: 

Crear usuarios. 

Editar información de usuarios. 

Asignar roles. 

Activar o desactivar usuarios. 

Administrar fotografías de perfil. 

Consultar información de los usuarios. 

Además, cada usuario dispone de una sección de Mi Perfil, donde puede actualizar sus datos personales y fotografía. 

 

Gestión de catálogos 

El sistema permite administrar los catálogos utilizados por los tickets: 

Sistemas 

Categorías 

Riesgos 

Prioridades 

Estados 

Los catálogos cuentan con operaciones de creación, consulta, edición y eliminación, aplicando validaciones para mantener la integridad de la información. 

Los elementos relacionados con tickets no pueden eliminarse cuando su eliminación provocaría conflictos con la información existente. 

 

Gestión de tickets 

El módulo de tickets permite: 

Crear tickets. 

Consultar tickets. 

Listar tickets. 

Editar tickets según los permisos del usuario. 

Consultar el detalle de un ticket. 

Filtrar tickets. 

Agregar comentarios. 

Consultar archivos adjuntos. 

Asignar técnicos automáticamente. 

Administrar el estado de las incidencias. 

Cada ticket posee un código generado automáticamente de acuerdo con el sistema seleccionado. 

Ejemplos: 

```text CONT-00001 RRHH-00001 SOP-00001 

Filtros de tickets 

El listado de tickets permite filtrar información mediante diferentes criterios: 

Sistema.  

Estado.  

Solicitante.  

Fecha desde.  

Fecha hasta.  

Esto facilita la consulta y seguimiento de las incidencias registradas. 

 

Comentarios 

Los usuarios pueden agregar comentarios a los tickets para mantener un historial de seguimiento. 

Cada comentario muestra: 

Nombre del usuario.  

Fotografía del usuario.  

Comentario realizado.  

Fecha y hora.  

 

Archivos adjuntos 

Los tickets permiten almacenar archivos adjuntos asociados a cada incidencia. 

El sistema permite trabajar con archivos como: 

Imágenes.  

PDF.  

Documentos de Word.  

Los archivos pueden consultarse desde el detalle del ticket. 

 

Asignación automática de técnicos 

Al crear un ticket, el sistema puede asignarlo automáticamente al técnico con menor cantidad de casos activos. 

Esto permite distribuir las incidencias de manera más equilibrada entre los técnicos disponibles. 

 

Control de acceso 

El sistema utiliza autorización basada en roles para restringir el acceso a los diferentes módulos. 

Usuario 

Puede: 

Consultar tickets.  

Crear tickets.  

Consultar detalles.  

Agregar comentarios.  

Técnico 

Además de las funciones de usuario, puede: 

Administrar catálogos.  

Editar tickets.  

Gestionar estados de los tickets.  

Dar seguimiento a incidencias asignadas.  

Administrador 

Puede: 

Consultar y administrar tickets.  

Administrar catálogos.  

Administrar usuarios.  

Gestionar información del sistema.  

Super Usuario 

Cuenta con acceso completo a los módulos administrativos y de gestión del sistema. 

 

Arquitectura 

El proyecto utiliza el patrón: 

ASP.NET Core MVC 

La aplicación se encuentra organizada principalmente en: 

HelpDeskPro2026 

│ 

├── Controllers 

├── Models 

├── ViewModels 

├── Views 

├── Services 

├── Interfaces 

├── Data 

├── Configuration 

├── Migrations 

├── wwwroot 

│   ├── css 

│   ├── js 

│   ├── img 

│   └── archivos 

│ 

└── Program.cs 

Controllers 

Gestionan las solicitudes HTTP y coordinan la comunicación entre las vistas, servicios y datos. 

Models 

Representan las entidades principales del sistema. 

ViewModels 

Contienen los modelos específicos utilizados para las diferentes vistas y operaciones. 

Services 

Contienen la lógica de negocio de la aplicación. 

Interfaces 

Definen los contratos utilizados por los servicios. 

Data 

Contiene la configuración del contexto de Entity Framework Core y el acceso a la base de datos. 

Views 

Contienen la interfaz gráfica desarrollada con Razor y Bootstrap. 

 

Tecnologías utilizadas 

Tecnología 

Uso 

ASP.NET Core MVC 

Desarrollo de la aplicación web 

C# 

Lenguaje de programación 

Entity Framework Core 

Acceso y gestión de datos 

SQL Server 

Base de datos principal 

Supabase Authentication 

Autenticación 

Supabase Storage 

Almacenamiento de fotografías 

Razor Views 

Interfaz de usuario 

Bootstrap 5 

Diseño e interfaz 

Bootstrap Icons 

Iconografía 

Git 

Control de versiones 

GitHub 

Repositorio del proyecto 

 

Base de datos 

La aplicación utiliza SQL Server como sistema gestor de base de datos. 

Entre las principales entidades se encuentran: 

Usuarios  

Roles  

Sistemas  

Categorías  

Riesgos  

Prioridades  

Estados  

Tickets  

Comentarios  

Adjuntos  

Las relaciones entre estas entidades permiten mantener la información organizada y garantizar la integridad referencial. 

 

Configuración 

Para ejecutar el proyecto localmente es necesario configurar los datos de conexión a la base de datos y los servicios de Supabase. 

Las configuraciones sensibles no deben almacenarse directamente en el repositorio público. 

Se recomienda utilizar: 

appsettings.json  

appsettings.Development.json  

User Secrets  

Variables de entorno  

según corresponda al entorno de ejecución. 

 

Ejecución del proyecto 

Requisitos 

Antes de ejecutar el proyecto se requiere: 

.NET SDK compatible con el proyecto.  

SQL Server.  

Visual Studio o un IDE compatible con ASP.NET Core.  

Una instancia configurada de Supabase para Authentication y Storage.  

Pasos 

Clonar el repositorio.  

git clone URL_DEL_REPOSITORIO 

Abrir el proyecto en Visual Studio.  

Configurar la conexión a SQL Server.  

Configurar las credenciales necesarias de Supabase.  

Aplicar las migraciones de Entity Framework Core si es necesario.  

dotnet ef database update 

Ejecutar el proyecto.  

dotnet run 

 

Pruebas realizadas 

Durante el desarrollo se realizaron pruebas sobre: 

Inicio de sesión.  

Autorización por roles.  

Creación de usuarios.  

Administración de perfiles.  

Carga de fotografías.  

CRUD de catálogos.  

Creación de tickets.  

Edición de tickets.  

Consulta de detalles.  

Filtros de tickets.  

Comentarios.  

Fotografías de usuarios en comentarios.  

Archivos adjuntos.  

Asignación automática de técnicos.  

Cambio de estados.  

Restricciones de eliminación de catálogos relacionados con tickets.  

 

Desarrollo 

Proyecto desarrollado para el curso: 

Programación V 

Universidad Castro Carazo 

2026 

Integrantes 

Jeffrey  

Natalia  

 

Estado del proyecto 

Estado actual: Funcional 

El sistema cuenta con los módulos principales implementados y funcionando. 

Actualmente se encuentra en una etapa de mejoras, pruebas e integración, por lo que pueden incorporarse nuevas funcionalidades y mejoras de interfaz posteriormente. 

 

Posibles mejoras futuras 

Entre las mejoras que pueden incorporarse posteriormente se encuentran: 

Notificaciones de nuevos tickets.  

Dashboard con estadísticas.  

Historial detallado de cambios.  

Mejoras en el sistema de prioridades.  

Indicadores visuales adicionales.  

Paginación del listado de tickets.  

Mejoras en búsquedas.  

Sistema de notificaciones para técnicos.  

Reportes de incidencias.  

Mejoras de experiencia de usuario.  

 

Licencia 

Proyecto desarrollado con fines académicos para el curso Programación V de la Universidad Castro Carazo. 

 
