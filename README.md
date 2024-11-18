## Project Overview
This project is a dynamic 2D game built with Unity that implements an object pooling and optimization system. It features efficient memory management and smooth gameplay by reusing objects instead of constantly creating and destroying them. The project focuses on rapid spawning of bullets, enemies, and particle effects, ensuring high performance and scalability.

## Core Classes

### 1. **ObjectPool**
- **Purpose**: Manages the pooling of game objects to optimize performance.
- **Key Features**:
  - Initializes a pool of inactive game objects.
  - Handles the retrieval (`GetObject()`) and return (`ReturnObject()`) of objects.
  - Manages active objects with collision adjustments and health resets.

### 2. **PlayerMovement**
- **Purpose**: Controls the player's movement along a vertical axis.
- **Key Features**:
  - Translates mouse movement into player position updates.
  - Ensures the player stays within set boundaries.

### 3. **PlayerShooting**
- **Purpose**: Manages the player's shooting mechanics.
- **Key Features**:
  - Uses the object pool to spawn bullets.
  - Integrates with animations and responds to player input for shooting.

### 4. **Bullet**
- **Purpose**: Represents bullets shot by the player.
- **Key Features**:
  - Moves toward defined path points.
  - Interacts with various objects (e.g., enemies, obstacles) and triggers specific responses like damage and effects.
  - Can multiply itself under specific conditions, increasing its impact.

### 5. **BulletExplosion**
- **Purpose**: Handles the explosion effects when a bullet impacts an obstacle or enemy.
- **Key Features**:
  - Uses an object pool to create and recycle explosion particles.

### 6. **Enemy**
- **Purpose**: Controls enemy movement and interaction with the player.
- **Key Features**:
  - Moves toward a target and returns to the pool when defeated or out of range.
  - Integrates with the `HealthController` for health management.

### 7. **EnemySpawner**
- **Purpose**: Spawns enemies in waves.
- **Key Features**:
  - Uses an object pool for efficient enemy spawning.
  - Adjusts difficulty by increasing the number of enemies and reducing the interval between spawns over time.

### 8. **HealthController**
- **Purpose**: Manages health for objects, including enemies and obstacles.
- **Key Features**:
  - Controls damage, death animations, and object recycling.
  - Resets health when an object is reused from the pool.

### 9. **Multiplier**
- **Purpose**: Represents a gameplay mechanic that multiplies the effects of bullets.
- **Key Features**:
  - Displays a multiplier value and interacts with bullets to enhance their behavior.

### 10. **Obstacle**
- **Purpose**: Represents obstacles that the player must avoid or destroy.
- **Key Features**:
  - Displays health and takes damage from collisions with bullets.

## Gameplay Benefits of Pooling
- **Performance**: Reduces CPU load by minimizing `Instantiate()` and `Destroy()` calls.
- **Smooth Gameplay**: Maintains stable frame rates, even with many active objects.
- **Memory Efficiency**: Lowers garbage collection impact, enhancing real-time performance.

## Future Enhancements
- **Gameplay Expansion**: Introduce new enemy types and power-ups.
- **Improved Visuals**: Add more detailed effects and animations.
- **Extended Pooling Logic**: Optimize pooling strategies for more complex game mechanics.

## License
This project is licensed under the MIT License. See the `LICENSE` file for details.

## Contact
For questions or contributions, feel free to open an issue or reach out via the [GitHub repository](https://github.com/EvilSasu/UnityPoolingAndOptimization).
