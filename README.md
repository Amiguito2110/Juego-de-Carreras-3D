# 🚗 Videojuego de Carreras en Unity - Configuración y Clonación del Proyecto

## 📌 Requisitos Previos
Antes de clonar el proyecto, asegúrate de tener instalados:
- **Unity 2022.3 LTS** (Descargar desde [Unity Hub](https://unity.com/download))
- **Visual Studio 2022** con el módulo **Desarrollo con Unity**
- **Git** instalado ([Descargar Git](https://git-scm.com/downloads))

---

## 📥 Clonar el Proyecto desde GitHub
Para obtener la última versión del proyecto, sigue estos pasos:

1. **Abre una terminal o Git Bash en la carpeta donde quieras clonar el proyecto**
2. **Ejecuta el siguiente comando:**
   ```bash
   git clone https://github.com/TU_USUARIO/TU_REPOSITORIO.git
   ```
   *(Reemplaza `TU_USUARIO` y `TU_REPOSITORIO` con el usuario y nombre del repositorio real en GitHub.)*

3. **Accede a la carpeta del proyecto:**
   ```bash
   cd TU_REPOSITORIO
   ```
4. **Descarga la última versión del código:**
   ```bash
   git pull origin main
   ```

---

## ⚙️ Configurar Unity
1. **Abre Unity Hub y selecciona** `Open Project`
2. **Navega a la carpeta clonada y ábrela**
3. **Espera a que Unity cargue los archivos y regenere la configuración**

### **Configurar Visual Studio como Editor de Código**
Para asegurarte de que Unity usa **Visual Studio 2022**, sigue estos pasos:

1. En Unity, ve a **Edit → Preferences → External Tools**
2. En **External Script Editor**, selecciona **Visual Studio 2022**
3. Activa la opción **Regenerate project files**
4. Cierra Unity y vuelve a abrirlo

Esto asegurará que Visual Studio reconozca correctamente los scripts de Unity.

---

## 🚀 Buenas Prácticas al Usar Git
- **Antes de trabajar en el proyecto, siempre ejecuta:**
  ```bash
  git pull origin main
  ```
  Esto evitará conflictos con cambios de otros miembros del equipo.

- **Para agregar cambios y subirlos a GitHub:**
  ```bash
  git add .
  git commit -m "Descripción del cambio"
  git push origin main
  ```

- **Nunca subas las carpetas `Library/`, `Logs/`, `Temp/`, `UserSettings/`, ni archivos `.csproj` o `.sln`** (Unity los regenera automáticamente).

---

## ❓ Problemas Comunes y Soluciones
### 1️⃣ **No se reconocen los scripts en Visual Studio**
- Asegúrate de que **Visual Studio 2022** esté seleccionado en **External Tools** en Unity.
- Prueba hacer clic en **Regenerate project files** y reiniciar Unity.

### 2️⃣ **El proyecto no se abre correctamente en Unity**
- Borra las carpetas `Library/`, `Logs/`, `Temp/` y `Obj/`, luego reabre Unity.
- Si Unity no reconoce el proyecto, verifica que estés usando la versión correcta (2022.3 LTS).

### 3️⃣ **Error al hacer `git push`**
- Asegúrate de haber hecho `git pull origin main` antes de subir cambios.
- Verifica que tengas permisos para subir al repositorio.

---

Con estos pasos, todos los miembros del equipo podrán trabajar de manera organizada y sin problemas. 🚀🎮

**¡Buena suerte con el desarrollo del videojuego de carreras!** 🚗💨

