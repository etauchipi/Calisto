# Calisto - Sistema de Laboratorio (Clínica de la Mujer)

## 1. Descripción del Proyecto

**Calisto** es una aplicación de escritorio orientada a la consulta y visualización de datos de exámenes de laboratorio. Desarrollada para la "Clínica de la Mujer", la herramienta permite a los profesionales de la salud acceder a los registros médicos y laboratoriales de los pacientes. La aplicación se encarga de solicitar los datos a un servicio web, descomprimirlos y renderizarlos en una interfaz gráfica amigable utilizando tablas y vistas detalladas.

## 2. Pila Tecnológica (Tech Stack)

El proyecto está construido utilizando tecnologías del ecosistema de Microsoft:

*   **Lenguaje de Programación:** Visual Basic .NET (VB.NET)
*   **Framework:** .NET Framework 4.5.2
*   **Interfaz Gráfica (UI):** WPF (Windows Presentation Foundation) / XAML
*   **Gestión de Datos:** ADO.NET (Typed DataSets - `dsLaboratorio.xsd`)
*   **Comunicaciones y Servicios:** WCF (Windows Communication Foundation) mediante proxy
*   **Librerías Externas (DLLs incluidas):**
    *   `wsCalistoProxy.dll`: Cliente proxy para el consumo de los servicios web de laboratorio.
    *   `Compresion.dll`: Utilidad para descomprimir los `DataSet` recibidos por el servicio web.

## 3. Instalación y Configuración del Entorno Local

Siga estas instrucciones para compilar y ejecutar el proyecto desde cero en su entorno local de desarrollo.

### Requisitos Previos
*   Sistema Operativo Windows.
*   Visual Studio 2015 o superior (con la carga de trabajo de "Desarrollo de escritorio de .NET" instalada).
*   .NET Framework 4.5.2 instalado.

### Pasos de Instalación
1.  **Clonar el repositorio:**
    Clone este repositorio en su máquina local utilizando Git.
    ```bash
    git clone <url-del-repositorio>
    ```
2.  **Abrir la solución:**
    Abra el archivo `Calisto.sln` que se encuentra en el directorio raíz usando Visual Studio.
3.  **Verificar las referencias:**
    En el Explorador de Soluciones, expanda el proyecto `Calisto` y luego la sección "Referencias". Asegúrese de que no haya advertencias en:
    *   `Compresion`
    *   `wsCalistoProxy`
    *   *(En caso de tener alertas amarillas, elimine la referencia, haga clic derecho en "Agregar Referencia...", seleccione "Examinar" y busque las dlls dentro de la carpeta `Calisto/App_Code/`)*.
4.  **Restaurar paquetes:**
    Compile la solución (`Ctrl + Shift + B`) para asegurarse de que todos los componentes de WPF, código autogenerado de `dsLaboratorio.xsd` y recursos subyacentes se integren sin errores.
5.  **Ejecutar la aplicación:**
    Presione `F5` o el botón de "Iniciar" en Visual Studio para ejecutar la aplicación en modo Debug (configuración `Any CPU`).

*Nota sobre configuración de variables:* Las conexiones a los servicios web o rutas de endpoint no están expuestas como texto plano en el `App.config`, su lógica se encuentra manejada internamente por la librería de proxy `wsCalistoProxy.dll`.

## 4. Estructura Principal de Carpetas

La organización de los archivos en el repositorio es la siguiente:

```text
/
├── Calisto.sln               # Archivo de solución principal de Visual Studio.
├── Calisto/                  # Carpeta del proyecto principal.
│   ├── App_Code/             # Contiene las librerías dinámicas (DLLs) requeridas (Compresion, wsCalistoProxy).
│   ├── Data/                 # Define la estructura de datos locales, contiene el esquema dsLaboratorio.xsd.
│   ├── Images/               # Recursos de imagen de la interfaz de usuario (ej. Logo de la Clínica).
│   ├── Pages/                # Vistas de contenido de la aplicación. Incluye laboratoData.xaml.
│   ├── My Project/           # Archivos de configuración de VB.NET, Settings, Resources y AssemblyInfo.
│   ├── MainWindow.xaml       # Ventana principal de la aplicación WPF (NavigationWindow).
│   ├── App.config            # Archivo de configuración global de la aplicación.
│   └── Application.xaml      # Definición global de la aplicación WPF.
```

## 5. Guía Básica de Uso

Al ejecutarse, el proyecto inicializa la interfaz `MainWindow`, la cual carga como fuente primaria (`Source`) la página `Pages/laboratoData.xaml`.

El flujo de trabajo es manejado mayormente detrás de escena por el sistema:
1.  **Conexión y Consulta:** Internamente, la vista de laboratorio instancia el cliente WCF `wsCalistoProxy.IwsCalistoClient`.
2.  **Llamada al Servicio Web:** La aplicación invoca métodos como `get_data_Laboratorio(sDesde, sHasta)` para solicitar información según rangos de fecha.
3.  **Procesamiento:** Los datos transferidos suelen venir empaquetados. La librería `Compresion.dll` es llamada para transformar el flujo en un `DataSet` utilizable a través del método `DescomprimirDataset(sds)`.
4.  **Visualización:** El `DataSet` se asocia automáticamente a la propiedad `DataContext` de los controles `DataGrid` de WPF, mostrando en pantalla las filas de pacientes, exámenes y observaciones médicas en la tabla interactiva de la aplicación.
