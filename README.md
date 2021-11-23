# learn-Net6

Implementazione di Web API con .NET Core 6

In questo repo è presente una semplice implementazione di una Web API con .NET Core 6:
è presente sia una versione con Minimal API che una versione con MVC, configurabile tramite settings su appsettings.json ( "MinimalOrMVC").

Vengono implementate delle API esposte con Swagger che gestiscono il CRUD di una tabella "Employee".
In un caso vengono direttamente risposte nel Program.cs di avvio, mentre nel secondo caso passano per Controller e poi Service; 
in entrambi i casi viene usato Entity Framework (in questo caso con MSSQLLocalDB), è presente un piccolo script sql per generare la tabella.   


23/11/21 
Michael Bazzanti
