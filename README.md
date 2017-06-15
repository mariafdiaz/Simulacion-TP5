# Simulacion-TP5
## Supermercado
Un supermercado tiene una sección con góndolas de mercadería para el autoservicio de los clientes y secciones de verdulería, carnicería y panadería atendidas cada una por un único empleado.
Todas las compras efectuadas en cada una de estas tres últimas secciones son consideradas como un solo artículo por sección. En la sección góndola, el número de artículos por cliente está dado por la siguiente tabla:

|Cantidad de artículos|1|2|3|4|5|
| ------ | ------ | ------ | ------ |------ | ------ |
|Probabilidad por cliente|0,2|0,2|0,1|0,2|0,3|

A su vez las góndolas solo pueden admitir hasta un máximo de 50 clientes en forma simultánea. Los tiempos de atención de cada una de las secciones están dados en la siguiente tabla:

|SECCION|Verdulería|Carnicería|Panadería|Góndola|Caja|
| ------ | ------ | ------ | ------ |------ | ------ |
|Tiempo|2'|3' 1''|3'|1' por art.|1' 20'' por art.|
|Distribución|POISSON|UNIFORME|CONSTANTE|CONSTANTE|CONSTANTE|

Los clientes llegan al supermercado con un tiempo medio de llegadas de 30 segundos según una distribución Poissón y efectúan los recorridos descriptos en la siguiente tabla:

|Recorrido|V-P|V-C-G|P|C-P-G-V|G|
| ------ | ------ | ------ | ------ |------ | ------ |
|Probabilidad por cliente|0,2|0,3|0,1|0,2|0,2|

Luego todos los clientes deben pasar por alguna de las 3 cajas habilitadas. La caja uno llamada “caja rápida” puede ser utilizada por aquellos clientes que llevan hasta un máximo de tres artículos.
Determinar cuántos clientes en promedio, atiende el supermercado en 8 hs. (simular como mínimo 1000 hs).
