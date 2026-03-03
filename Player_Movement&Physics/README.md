# Sistema Avanzado de Movimiento del Jugador

Este módulo implementa un sistema completo de control del personaje en 2D, diseñado para ofrecer una experiencia de gameplay fluida y responsiva.

Incluye física personalizada, control de animaciones, dash, salto variable y detección de colisiones mediante raycasting.

---

## Características principales

### Control de movimiento con Input System

Se utiliza el **Nuevo Input System de Unity** para gestionar las entradas del jugador.

Permite:

- Movimiento horizontal
- Salto
- Ataque
- Apuntado con mouse

---

## Sistema de estado del jugador

El personaje solo puede realizar acciones si cumple las condiciones necesarias.

Se controla mediante propiedades como:

- Movimiento permitido
- Estado de dash
- Estado de ataque
- Estado de apuntado

Esto evita conflictos entre animaciones y física.

---

## Física personalizada y detección de colisiones

Se implementa un sistema de detección basado en raycasts para controlar:

- Suelo
- Paredes laterales
- Plataformas
- Bordes y corrección de esquinas

Esto mejora la precisión del movimiento y evita problemas de clipping.

---

## Sistema de Dash con control de cooldown

El dash incluye:

- Duración limitada
- Dirección automática según input o orientación del personaje
- Temporizador de espera para evitar spam de habilidad

---

## Salto con altura variable

El sistema de salto permite controlar la altura dependiendo del tiempo que el jugador mantenga presionada la tecla.

Esto genera un control más natural y profesional del movimiento.

---

## Integración con cámara dinámica

Se conecta con el sistema de cámara para:

- Ajustar damping cuando el jugador cae
- Realizar rotaciones suaves según dirección del personaje

---

## Sistema de animaciones basado en estado

Se separaron las responsabilidades en tres componentes:

- Player → Física y movimiento
- PlayerAnimation → Control visual
- PlayerActions → Lógica de combate

Este diseño sigue el principio de **separation of concerns**.

---

## Qué demuestra este sistema

Este módulo evidencia habilidades en:

- Programación gameplay avanzada en Unity
- Uso de Raycasting para física 2D
- Arquitectura modular de código
- Manejo de estados de animación
- Implementación de input moderno
- Desarrollo de sistemas interactivos
