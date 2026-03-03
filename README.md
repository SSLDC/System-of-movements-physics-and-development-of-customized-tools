# System of Movements, Physics & Customized Tools

Demo técnica centrada en **arquitectura modular**, control avanzado de estados y diseño de sistemas desacoplados.  

El proyecto demuestra organización escalable del código, separación clara de responsabilidades y capacidad para construir herramientas internas configurables.

---

## Objetivos del Proyecto

- Arquitectura limpia y modular  
- Control avanzado de físicas y estados  
- Gestión desacoplada de cámara  
- Resolución de edge cases complejos  
- Creación de herramientas personalizadas en el Editor  

---

# Sistema de Movimiento y Físicas

Implementación de un sistema robusto con múltiples estados y transiciones controladas:

- Dash
- Salto variable
- Caída controlada
- Colisiones laterales
- Corrección automática de esquinas
- Bloqueo contextual de acciones

El flujo se gestiona mediante:

- Control condicional avanzado
- Máquina de estados implícita
- Triggers basados en eventos
- Interpolaciones temporizadas con **coroutines**
- Ajustes dinámicos de parámetros (ej. damping de cámara según velocidad)

El sistema demuestra manejo de reglas complejas y validaciones constantes.

---

# Sistema de Cámara

Arquitectura desacoplada basada en patrón **Singleton** para la gestión centralizada.

Características:

- Seguimiento dinámico del jugador
- Ajustes de suavizado según velocidad
- Activación mediante triggers
- Configuración flexible desde el Inspector

---

# Herramientas Personalizadas

Incluye un **Editor extendido** que permite:

- Configurar cámaras dinámicamente
- Ajustar direcciones y comportamientos
- Modificar parámetros sin tocar código
- Crear sistemas reutilizables y configurables

Demuestra capacidad para desarrollar herramientas internas que optimizan el flujo de trabajo y mejoran la escalabilidad del proyecto.

---

# Estructura del Proyecto

Cada carpeta contiene su propio `README.md` con detalles específicos.

---

## CameraController

Gestión y control de la cámara.

- `CameraControlTrigger.cs`
- `CameraFollowObject.cs`
- `CameraManager.cs`

---

## Player_Movement&Physics

Sistema completo de movimiento y animación del jugador.

- `PlayerActions.cs`
- `PlayerAnimation.cs`
- `PlayerMovement.cs`

---

# Filosofía de Desarrollo

Este proyecto refleja:

- Diseño orientado a sistemas
- Código reutilizable y escalable
- Gestión avanzada de estados
- Resolución efectiva de casos límite
- Desarrollo de herramientas internas personalizadas
