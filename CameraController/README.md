# Advanced Camera System

## English

This module implements a camera control system designed to enhance gameplay experience through smooth movement, dynamic camera switching, and support for scene triggers.

---

## Player Following System

The camera follows the main character position in real time, maintaining a sense of control and fluidity.

---

## Smooth Rotation Based on Player Direction

Linear interpolation is used to perform smooth camera rotation when the character changes direction.

This improves visual quality and transition smoothness.

---

## Dynamic Camera Control with Cinemachine

A virtual camera system is used to manage:

- Left and right camera switching  
- Panoramic camera movement  
- Dynamic vertical damping adjustment  

The system allows camera behavior adaptation to different gameplay situations.

---

## Smoothing Effects

Coroutines are used to perform smooth interpolation for:

- Camera rotation  
- Pan movement  
- Damping adjustment during jumping or falling  

This avoids abrupt movements and improves the cinematic feel of the game.

---

## Trigger-Based Camera System

The camera responds to scene collision zones:

- Entering specific zones → camera pan activation  
- Leaving zones → active camera may change  

This design allows building levels with dynamic visual control.

---

## Custom Unity Inspector Tooling

A custom editor was implemented to simplify configuration inside the Inspector.

Allows:

- Enabling camera switching  
- Configuring pan direction  
- Adjusting transition distance and duration  

This improves development workflow efficiency.

---

## Contribution of This System

This module demonstrates skills in:

- Game camera system development  
- Singleton pattern usage  
- Event-driven programming and coroutines  
- Visual experience optimization  
- Unity Editor extension development  

---

## Español

# Sistema Avanzado de Cámara

Este módulo implementa un sistema de control de cámara diseñado para mejorar la experiencia de juego mediante movimiento suave, cambios dinámicos de cámara y soporte para triggers del escenario.

---

## Sistema de Seguimiento del Jugador

La cámara sigue la posición del personaje principal en tiempo real, manteniendo sensación de fluidez y control.

---

## Rotación Suave según Dirección del Jugador

Se utiliza interpolación lineal para realizar rotaciones suaves de cámara cuando el personaje cambia de dirección.

Esto mejora la calidad visual de las transiciones.

---

## Control Dinámico de Cámara con Cinemachine

Se utiliza un sistema de cámaras virtuales para gestionar:

- Cambio entre cámaras izquierda y derecha  
- Movimiento panorámico  
- Ajuste dinámico de damping vertical  

El sistema permite adaptar el comportamiento de la cámara según las situaciones del gameplay.

---

## Efectos de Suavizado

Se utilizan coroutines para interpolaciones suaves en:

- Rotación de cámara  
- Movimiento panorámico  
- Ajuste de damping durante salto o caída  

Esto evita movimientos bruscos y mejora la sensación cinematográfica.

---

## Sistema Basado en Triggers

La cámara responde a zonas de colisión del escenario:

- Al entrar en zonas específicas → activación de pan de cámara  
- Al salir de zonas → posible cambio de cámara activa  

Este diseño permite construir niveles con control visual dinámico.

---

## Inspector Personalizado en Unity

Se implementó un editor personalizado para facilitar la configuración.

Permite:

- Activar cambio de cámara  
- Configurar dirección del pan  
- Ajustar distancia y tiempo de transición  

Mejora la productividad del desarrollo.

---

## Qué demuestra este sistema

Este módulo evidencia habilidades en:

- Desarrollo de sistemas de cámara para videojuegos  
- Uso del patrón Singleton  
- Programación orientada a eventos y coroutines  
- Optimización de la experiencia visual  
- Desarrollo de extensiones del editor en Unity  
