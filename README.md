# SpendWisely
<div align="left">
  <img src="https://github.com/PatricioPoncini/SpendWisely/assets/76538747/2b942442-a3a2-413e-a0c7-6929ec6ef24f" alt="Logo Spend Wisely" width="600" />
</div>

Proyecto para Laboratorio IV. Se trata de una app web para registrar gastos e ingresos pero mejorado, con un sistema de analisis de gastos y tendencia de estos, poder ver en un dashboard los gastos por tiempo y además una sección de noticias que consume datos de una API externa para traer novedades del mundo financiero

### Bloc de notas de Notion:
https://tricolor-hubcap-fab.notion.site/Proyecto-Lab-IV-31a6ebcacbd0413487fa7b0072088430?pvs=4

## Migración de entidades
Para poder realizar la migración a la base de datos, una vez que tengas todo configurado ejecuta el comando 
```
Add-Migration SpendWiseyMigration -Context ApplicationDbContext
```
Esto te realizara la migración completa. Una vez terminado, ejecuta 
```
Update-Database -Context ApplicationDbContext
```
Esto te permitirá actualizar la base de datos completa con la migración que realizaste anteriormente.
