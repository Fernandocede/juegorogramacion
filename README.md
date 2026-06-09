# Proyecto Final - Realidad Virtual y Videojuegos

## Prototipo técnico completo en Unity

Este repositorio contiene el primer prototipo técnico de un nivel para la asignatura de programacion de videojuegos.

## Integrantes

- Integrante 1: Fernando Rogerio Cedeño Loor
- Integrante 2: Darwin velez

## Descripción del proyecto

El prototipo consiste en un nivel en primera persona desarrollado en Unity. El jugador debe avanzar por un escenario en estado de Blocking, recoger una llave, abrir una puerta bloqueada y llegar a una zona final.

## Criterios de entrega cubiertos

### 1. Repositorio utilizando GIT

El proyecto está preparado para subirse a GitHub mediante GIT. Incluye archivo `.gitignore` para evitar subir carpetas innecesarias de Unity.

### 2. Nivel en estado de Blocking

El nivel utiliza formas básicas para representar el diseño inicial:

- Suelo principal.
- Camino del jugador.
- Paredes de límite.
- Bloqueos internos.
- Obstáculos.
- Zona de llave.
- Puerta bloqueada.
- Zona final.

### 3. Mecánica principal programada

La mecánica principal consiste en:

1. El jugador se mueve por el escenario.
2. Busca y recoge una llave.
3. Usa la llave para abrir una puerta.
4. Avanza hasta la zona final.
5. El sistema muestra el estado del prototipo en pantalla.

## Controles

- W, A, S, D: mover al jugador.
- Mouse: mover la cámara.
- Esc: liberar el cursor.

## Escena principal

`Assets/Scenes/Nivel_Prototipo_Completo.unity`

Si la escena no aparece al abrir Unity, usar:

`Proyecto Final > Crear o regenerar proyecto completo`

## Scripts principales

- `PlayerMovement.cs`: movimiento en primera persona.
- `PlayerInteraction.cs`: interacción con llave, puerta y meta.
- `KeyItem.cs`: comportamiento de la llave.
- `DoorController.cs`: apertura de puerta.
- `GoalZone.cs`: zona final.
- `GameManager.cs`: mensajes y estado en pantalla.

## Cómo abrir el proyecto

1. Descargar el ZIP.
2. Descomprimirlo.
3. Abrir Unity Hub.
4. Seleccionar `Add project from disk`.
5. Elegir la carpeta `Proyecto_Unity_Completo_RV_Videojuegos`.
6. Esperar a que Unity compile.
7. Abrir la escena principal.
8. Presionar Play.

## Cómo subir a GitHub

En la carpeta principal del proyecto, ejecutar:

```bash
git init
git add .
git commit -m "Creación inicial del proyecto Unity"
git branch -M main
git remote add origin URL_DEL_REPOSITORIO
git push -u origin main
```

Luego de hacer cambios:

```bash
git add .
git commit -m "Actualización del prototipo técnico"
git push
```

## Texto sugerido para entregar al docente

Estimado docente, envío el enlace del repositorio correspondiente al primer prototipo técnico del nivel final de asignatura. El proyecto fue desarrollado en Unity e incluye el nivel en estado de Blocking, además de la mecánica principal programada, que consiste en recoger una llave, abrir una puerta y llegar a la zona final.
