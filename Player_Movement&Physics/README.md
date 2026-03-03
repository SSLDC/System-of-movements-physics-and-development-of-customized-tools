# Advanced Player Movement System

## English

This module implements a complete 2D character control system designed to provide a smooth and responsive gameplay experience.

It includes custom physics handling, animation control, dash mechanics, variable jump height, and collision detection using raycasting.

---

## Main Features

### Input System Integration

The **New Unity Input System** is used to manage player input.

Supports:

- Horizontal movement  
- Jumping  
- Attacking  
- Mouse aiming  

---

## Player State Management

The character can only perform actions when valid conditions are met.

Controlled states include:

- Movement enabled state  
- Dash state  
- Attack state  
- Aim state  

This prevents conflicts between physics and animation systems.

---

## Custom Physics and Collision Detection

A raycast-based detection system is implemented to manage:

- Ground detection  
- Wall detection  
- Platform interaction  
- Edge correction and corner handling  

This improves movement precision and prevents clipping issues.

---

## Dash System with Cooldown Control

The dash mechanic includes:

- Limited duration  
- Directional movement based on input or character orientation  
- Cooldown timer to prevent ability spamming  

---

## Variable Height Jump System

Jump height can be controlled depending on how long the jump key is held.

This provides more natural and professional movement control.

---

## Camera Integration

The movement system is connected to the camera system to:

- Adjust damping when the player falls  
- Perform smooth rotation based on character direction  

---

## Animation State System

Responsibilities are separated into three components:

- Player → Physics and movement logic  
- PlayerAnimation → Visual animation control  
- PlayerActions → Combat interaction logic  

This follows the **separation of concerns** principle.

---

## Contribution of This System

This module demonstrates skills in:

- Advanced gameplay programming in Unity  
- Raycasting-based 2D physics handling  
- Modular software architecture  
- Animation state management  
- Modern input system integration  
- Interactive gameplay system development  

---

## Español

# Sistema Avanzado de Movimiento del Jugador

Este módulo implementa un sistema completo de control del personaje en 2D, diseñado para ofrecer una experiencia de juego fluida y responsiva.

Incluye física personalizada, control de animaciones, mecánicas de dash, salto variable y detección de colisiones mediante raycasting.

---

## Características Principales

### Integración del Nuevo Input System

Se utiliza el **Nuevo Input System de Unity** para gestionar las entradas del jugador.

Permite:

- Movimiento horizontal  
- Salto  
- Ataque  
- Apuntado con ratón  

---

## Gestión del Estado del Jugador

El personaje solo puede ejecutar acciones cuando se cumplen las condiciones necesarias.

Se controlan estados como:

- Movimiento permitido  
- Estado de dash  
- Estado de ataque  
- Estado de apuntado  

Esto evita conflictos entre sistemas de animación y física.

---

## Física Personalizada y Detección de Colisiones

Se implementa un sistema basado en raycasting para controlar:

- Detección de suelo  
- Detección de paredes  
- Interacción con plataformas  
- Corrección de bordes y esquinas  

Esto mejora la precisión del movimiento y evita problemas de clipping.

---

## Sistema de Dash con Cooldown

El dash incluye:

- Duración limitada  
- Dirección automática según entrada o orientación del personaje  
- Temporizador de espera para evitar el uso continuo de la habilidad  

---

## Salto de Altura Variable

El salto permite controlar la altura según el tiempo que se mantenga presionada la tecla.

Esto genera un movimiento más natural y profesional.

---

## Integración con la Cámara

El sistema de movimiento se conecta con la cámara para:

- Ajustar el damping durante caídas  
- Realizar rotaciones suaves según la dirección del personaje  

---

## Sistema de Animación Basado en Estados

Las responsabilidades se dividen en tres componentes:

- Player → Física y movimiento  
- PlayerAnimation → Control visual  
- PlayerActions → Lógica de combate  

Este diseño sigue el principio de **separación de responsabilidades**.

---

## Qué demuestra este sistema

Este módulo evidencia habilidades en:

- Programación avanzada de gameplay en Unity  
- Manejo de física 2D mediante raycasting  
- Arquitectura modular de software  
- Gestión de estados de animación  
- Integración del sistema moderno de input  
- Desarrollo de sistemas interactivos  
