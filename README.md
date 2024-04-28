Desarrollé este videojuego para una materia de la facultad llamada "Trabajo de Campo". La materia consistía en llevar a cabo un proyecto a nuestro gusto de manera autodidacta.

Objetivo:
El objetivo de este proyecto es la realización de una aplicación móvil que consiste en un videojuego el cual se encuentre dentro de las categorías de Supervivencia, Estrategia y Disparos. 
Justificación:
Mediante una combinación de estas categorías se aspira a crear una experiencia cautivadora para los usuarios de dispositivos móviles. Además, les proporcionará a los jugadores una mejor capacidad de pensamiento estratégico a la hora de elegir una acción.
Límites:
Desde: El comienzo del juego en la Ronda 1.
Hasta: Terminar la Ronda 3.
Estado inicial del juego: Primer Ronda, el jugador aparece en el Hangar y los zombies empiezan a generarse en distintos punto del Hangar.
Estado final del juego: Ronda 3 completada, el jugador gana o, antes de esto el jugador pierde al ser eliminado por un zombie.
Alcance:
Lo que hará:
-	Permitir desplazarse libremente por el Hangar.
-	Apuntar, disparar y recargar el arma.
-	Generar oleadas de zombies que aumenten en cantidad y dificultad a medida que avanza el juego.
-	Mostrar en pantalla un contador de la vida del jugador, de las balas restantes del arma y del dinero.
-	Crear un sistema de dinero donde el jugador ganará dinero por eliminar zombies.
-	Adquirir armas a través de una “Caja Misteriosa” a cambio de dinero.
-	Permitir el uso de bebidas especiales que le otorguen ventajas al jugador a cambio de dinero.
-	Regenerar la vida del jugador si no es atacado durante 10 segundos.
-	Permitir la mejora de armas a través de una maquina a cambio de dinero.
Lo que no hará:
-	Permitir salir del Hangar.
-	Modo multijugador.

Estrategia del juego:

-	Estrategia Central:

El videojuego se centra en la supervivencia del jugador ante oleadas de zombies que aparecerán en distintos puntos de un Hangar que representará el mapa del juego. El objetivo es eliminar a los zombies para mantenerse a salvo.

-	Jugador: El jugador iniciará el juego con un valor de vida de 100 que podrá disminuir a través de los ataques de los zombies.

-	Regeneración de Vida del Jugador:

La vida del jugador se regenerará automáticamente si no es atacado durante 10 segundos. Esto permite una breve recuperación estratégica de la vida.

-	Interfaz de Usuario del Jugador: En la Interfaz de Usuario del Jugador se tiene:

	Contador de dinero acumulado.
	Número de Ronda.
	Botones para disparar, recargar, apuntar, saltar y moverse.
	Un icono del arma que esté utilizando el Jugador junto con sus balas en el cargador y balas restantes.
	Un punto de mira en el centro de la pantalla.
	Un contador de FPS (Frames por Segundo).


-	Armas y equipamiento:

Al inicio del juego el jugador tendrá un revolver. La única forma para obtener nuevas armas es comprando la “caja misteriosa” que otorgará un arma aleatoria a cambio de dinero. En cuanto a la munición, las formas de recargar la munición completa son comprando la “caja misteriosa” e intercambiar de esa manera el arma sin munición. Existen 4 tipos de armas:

	Pistola:

	Revolver: Disparo semi-automático. Daño por disparo de 50 de vida. Velocidad de disparo de 0.6 segundos. 6 balas por cargador y 60 balas en total.
	Pistola 9mm: Disparo automático. Daño por disparo de 20 de vida. Velocidad de disparo de 0.3 segundos. 12 balas por cargador y 120 balas en total.

	Subfusil:

	UMP45: Disparo automático. Daño por disparo de 20 de vida. Velocidad de disparo de 0.15 segundos. 20 balas en el cargador y 140 balas en el total. A mayor distancia de disparo se reduce su daño.
	Mini-subfusil: Disparo automático. Daño por disparo de 10 de vida. Velocidad de disparo de 0.1 segundos. 15 balas por cargador y 90 balas en total. A mayor distancia de disparo se reduce su daño.

	Fusil:

	AK-47: Disparo automático. Daño por disparo de 40 de vida. Velocidad de disparo de 0.3 segundos. 30 balas por cargador y 180 balas en total.

	Escopeta:

	Remington: Disparo semi-automático. Daño por disparo de 75 de vida. Velocidad de disparo de 1.2 segundos. 8 balas por cargador y 32 balas en total. A mayor distancia de disparo se reduce su daño.

	Caja Misteriosa: Te permite comprar un arma al azar de las mencionadas con anterioridad a cambio de $950.

-	Duplicador de daño a la cabeza: El jugador al dispararle al zombie en la cabeza duplicará por dos el daño que tenga el arma por disparo.

-	Mejoras de armas:

Podrás acceder a una máquina la cual te permitirá mejorar tu arma actual a cambio de $3500. Esta mejora incrementará el daño y velocidad de disparo de tu arma un 50%, lo cual será importante en rondas altas.

-	Zombies:

Los zombies se generarán automáticamente en distintos puntos del Hangar. En base a la ronda en el que se encuentre el juego tendrán distintos atributos:

	Ronda 1: 100 de vida y le quitaran 15 de vida al jugador por ataque.
	Ronda 2: 150 de vida y le quitaran 20 de vida al jugador por ataque.
	Ronda 3: 200 de vida y le quitaran 30 de vida al jugador por ataque.

-	Máquinas expendedoras:

Podrás hacer uso de máquinas expendedoras que se encuentren en el Hangar, las cuales ofrecen bebidas especiales que te otorgarán ventajas en el juego como mayor vida y velocidad de disparo. Para obtener estas bebidas deberás canjear una cierta cantidad de dinero dependiendo de la máquina.

	Máquina de vida: Te permite comprar una bebida que te duplica la vida a un valor de 200 a cambio de $3000. Se podrá canjear una única vez.

	Máquina para disparar más rápido: Te permite comprar una bebida que te incrementa en un 50% la velocidad de disparo que tenga el arma que estés utilizando a cambio de $3500. Se podrá canjear una única vez por arma que se esté utilizando.

-	Acumulación de dinero:

Cada eliminación de un zombie te dará $150. Este dinero puede ser acumulado y usado para comprar armas a través de la caja misteriosa, para acceder a las bebidas especiales o para mejorar un arma.

-	Dificultad progresiva:

El juego consiste en rondas. A medida que el Jugador avanza en los rondas la cantidad y resistencia de los zombies aumentará, provocando así una mayor dificultad para sobrevivir. Cantidad de zombies por ronda:
	Ronda 1: 25 zombies.
	Ronda 2: 50 zombies.
	Ronda 3: 100 zombies.
