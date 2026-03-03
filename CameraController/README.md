# Sistema Avanzado de Cámara

Este módulo implementa un sistema de control de cámara para el juego, diseñado para mejorar la experiencia de gameplay mediante movimientos suaves, cambios dinámicos de cámara y soporte para triggers del escenario.

---

## Características principales

### Seguimiento del jugador
La cámara sigue la posición del personaje principal en tiempo real, manteniendo la sensación de control y fluidez.

---

### Rotación suave según dirección del jugador

Se implementa interpolación lineal para realizar giros suaves de cámara cuando el personaje cambia de dirección.

Esto mejora la sensación visual y la calidad de la transición.

---

### Control dinámico de cámara con Cinemachine

Se utiliza un sistema de cámaras virtuales para gestionar:

- Cambio entre cámaras izquierda y derecha
- Movimiento panorámico de cámara
- Ajuste dinámico de damping vertical

El sistema permite adaptar la cámara a diferentes situaciones del gameplay.

---

## Efectos de suavizado

Se utilizan corrutinas para realizar interpolaciones suaves en:

- Rotación de cámara  
- Movimiento de pan  
- Ajustes de damping al caer o saltar  

Esto evita movimientos bruscos y mejora la sensación cinematográfica del juego.

---

## Sistema basado en Triggers

La cámara responde a colisiones del escenario:

- Al entrar en zonas específicas → se activa pan de cámara  
- Al salir de zonas → puede cambiar la cámara activa  

Este diseño permite construir niveles con control visual dinámico.

---

## Custom Inspector en Unity

Se implementó un editor personalizado para facilitar la configuración en el Inspector.

Permite:

- Activar cambio de cámara
- Configurar dirección de pan
- Ajustar distancia y tiempo de transición

Esto mejora la productividad del desarrollo.

---

## Qué demuestra este sistema

Este módulo evidencia habilidades en:

- Desarrollo de sistemas de cámara para videojuegos
- Uso de patrones Singleton
- Programación con eventos y corrutinas
- Optimización de experiencia visual
- Extensión del editor de Unity