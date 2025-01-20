#  TQ_TaskManager_Back

Instrucciones para ejecución del código:

1)Correr los scripts de la carpeta BdScripts en una Bd PostgreSql en el siguiente orden.

  1)  Tablas (desdocumentar la primera linea para crear la BD)
  2)  Inserts
      
2)Abrir el código y en el archivo appsettings.json

En la propiedad **tqBd** remplazar en la cadena de conexión los datos por los datos de el servidor donde esté corriendo el motor de Bd Postgresql: "Host=**host**;Port=**puerto**;Database=tq_taskmanagement;Username=**usuario**;Password=**contraseña**".

3)Correr el código.
