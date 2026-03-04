# System of Movements, Physics & Customized Tools

## 🇬🇧 English

Technical demo focused on **modular architecture**, advanced state control, and decoupled system design.

This project demonstrates scalable code organization, clear separation of responsibilities, and the ability to build configurable internal tools.

---

## Project Goals

- Clean and modular architecture  
- Advanced physics and state control  
- Decoupled camera management  
- Complex edge case handling  
- Custom Editor tool development  

---

## Movement and Physics System

Implementation of a robust movement system with controlled state transitions:

- Dash  
- Variable jump  
- Controlled falling  
- Side collision handling  
- Automatic corner correction  
- Contextual action blocking  

The system flow is managed using:

- Advanced conditional logic  
- Implicit state machine behavior  
- Event-based triggers  
- Coroutine-based timed interpolation  
- Dynamic parameter tuning (e.g., camera damping based on velocity)

The system demonstrates complex rule management and constant state validation.

---

## Camera System

Decoupled architecture based on the **Singleton** pattern for centralized management.

Features:

- Dynamic player following  
- Velocity-based smoothing adjustments  
- Trigger-based activation  
- Flexible Inspector configuration  

---

## Custom Tools

Includes an **extended Editor tool** that allows:

- Dynamic camera configuration  
- Direction and behavior adjustment  
- Runtime parameter tuning without code modification  
- Reusable and configurable system setup  

This demonstrates the ability to build internal development tools that improve workflow efficiency and scalability.

---

## Project Structure

Each folder contains its own `README.md` with detailed internal documentation.

---

### CameraController

Camera control and tracking management.

- `CameraControlTrigger.cs`
- `CameraFollowObject.cs`
- `CameraManager.cs`

---

### Player_Movement&Physics

Complete player movement and animation system.

- `PlayerActions.cs`
- `PlayerAnimation.cs`
- `PlayerMovement.cs`

---

## Design Philosophy

This project is built following principles of:

- System-oriented design  
- Reusable and scalable code  
- Advanced state management  
- Effective edge case resolution  
- Custom internal tool development  

---

## 🇪🇸 Español

Demo técnica centrada en arquitectura modular, control avanzado de estados y diseño de sistemas desacoplados.

Este proyecto demuestra organización escalable del código, clara separación de responsabilidades y capacidad para desarrollar herramientas internas configurables.

---

### Objetivos del Proyecto

- Arquitectura limpia y modular  
- Control avanzado de físicas y estados  
- Gestión desacoplada de cámara  
- Manejo de casos límite complejos  
- Desarrollo de herramientas personalizadas en el Editor  

---

### Sistema de Movimiento y Físicas

Implementación de un sistema robusto con transiciones de estado controladas:

- Dash  
- Salto variable  
- Caída controlada  
- Colisiones laterales  
- Corrección automática de esquinas  
- Bloqueo contextual de acciones  

El flujo del sistema se gestiona mediante:

- Lógica condicional avanzada  
- Comportamiento de máquina de estados implícita  
- Triggers basados en eventos  
- Interpolación temporal con coroutines  
- Ajuste dinámico de parámetros (por ejemplo damping de cámara según velocidad)

---

### Sistema de Cámara

Arquitectura desacoplada basada en el patrón **Singleton** para gestión centralizada.

Características:

- Seguimiento dinámico del jugador  
- Ajustes de suavizado según velocidad  
- Activación mediante triggers  
- Configuración flexible desde el Inspector  

---

### Herramientas Personalizadas

Incluye un **Editor extendido** que permite:

- Configurar cámaras dinámicamente  
- Ajustar comportamientos y direcciones  
- Modificar parámetros sin alterar el código  
- Crear sistemas configurables y reutilizables  

Demuestra capacidad para desarrollar herramientas internas que optimizan el flujo de trabajo.

---

### Filosofía de Desarrollo

Este proyecto refleja:

- Diseño orientado a sistemas  
- Código reutilizable y escalable  
- Gestión avanzada de estados  
- Resolución de casos límite  
- Desarrollo de herramientas internas personalizadas
