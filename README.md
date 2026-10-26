# Carnival Target Shooting

A simple 2D Unity game prototype that demonstrates the **Object Pool**, **Builder**, and **Observer** design patterns. The player controls a shooter at the bottom of the screen and fires bullets at moving carnival targets. Each target is worth a different number of points.

## Group Members

Connor Hewitt

## How to Play

| Key | Action |
|---|---|
| **A / Left Arrow** | Move left |
| **D / Right Arrow** | Move right |
| **Space** | Shoot |

Hit the moving targets to score points. The score is shown in the top-left corner.

## Diagram

![Pattern diagram](Pattern_Diagram.png)

*(Export the slide from `Pattern_Diagram.pptx` as a PNG and place it in the repo root.)*

## Design Patterns

### 1. Object Pool Pattern (Bullets)

**Classes:** `BulletPool`, `Bullet`, `Shooter`

**What it does:** Instead of creating a new bullet with `Instantiate()` and removing it with `Destroy()` every time the player shoots, the game creates 20 bullets once when it starts and reuses them.

**How it works:**
- `BulletPool` creates the bullets in `Awake()`, turns them off, and stores them in a `Queue<Bullet>`.
- When the player presses Space, `Shooter` calls `BulletPool.Get()`. The pool takes an inactive bullet from the queue, moves it to the shooter's position, and turns it on.
- When a bullet hits a target or its lifetime runs out, it calls `BulletPool.Return()`. The pool turns the bullet off and puts it back in the queue.
- If the queue is ever empty, the pool creates one more bullet, so the player can never run out.

**Why it fits:** Bullets are created and removed constantly. Reusing them avoids repeated memory allocation and garbage collection, which keeps the game running smoothly. You can see it working by watching the children of the `BulletPool` object in the Hierarchy turn on and off while the game is running.

### 2. Builder Pattern (Targets)

**Classes:** `TargetBuilder`, `TargetDirector`, `TargetSpawner`, `Target`

**What it does:** Targets are built one step at a time using a builder, so each target type can have its own speed, size, color, and point value.

**How it works:**
- `TargetBuilder` has a method for each attribute: `SetSpeed()`, `SetSize()`, `SetPoints()`, and `SetColor()`. Each one returns the builder so the calls can be chained. `Build()` then creates the target from the prefab and applies all the values.
- `TargetDirector` holds the premade target recipes. Each recipe is a different chain of builder calls:

| Target | Speed | Size | Points | Color |
|---|---|---|---|---|
| Duck | 2 | 1.0 | 10 | Yellow |
| Rabbit | 5 | 0.6 | 30 | Cyan |
| Bear | 1 | 1.6 | 5 | Red |

- `TargetSpawner` randomly picks one of the director's recipes every 1.5 seconds and places the new target in the scene.

**Why it fits:** A target has several settings that change from type to type. The builder keeps that construction code in one place, so adding a new target type only means adding one new recipe in `TargetDirector`.

### 3. Observer Pattern (Score)

**Classes:** `IScoreObserver`, `TargetEvents`, `ScoreManager`, `Target`

**What it does:** When a target is hit, the score updates automatically. The target does not need to know anything about the score or the UI.

**How it works:**
- `IScoreObserver` is an interface with one method, `OnTargetHit(Target target)`.
- `TargetEvents` is the **subject**. It keeps a list of observers and has `Attach()`, `Detach()`, and `Notify()` methods.
- `ScoreManager` is the **observer**. It implements `IScoreObserver`, attaches itself in `OnEnable()`, and detaches in `OnDisable()`. When notified, it adds the target's points to the score and updates the on-screen text.
- When a bullet hits a target, `Target.Hit()` calls `TargetEvents.Notify(this)` and then destroys itself.

**Why it fits:** The target only announces that it was hit. Other systems, such as a score display, a sound effect, or a combo counter, can be added later by attaching more observers without changing the `Target` class.

## Project Structure

```
Assets/
  Scripts/
    Bullet.cs            (Object Pool)
    BulletPool.cs        (Object Pool)
    Shooter.cs           (player controls)
    Target.cs            (Builder product / Observer notifier)
    TargetBuilder.cs     (Builder)
    TargetDirector.cs    (Builder, premade types)
    TargetSpawner.cs     (spawns targets)
    IScoreObserver.cs    (Observer interface)
    TargetEvents.cs      (Observer subject)
    ScoreManager.cs      (Observer)
  Prefabs/
    Bullet.prefab
    Target.prefab
  Scenes/
    SampleScene.unity
```

## Requirements

- Unity 6 (6000.0.71f1)
- Input System package
- TextMeshPro

## Running the Project

1. Clone this repository.
2. Open the project folder in Unity Hub.
3. Open `Assets/Scenes/SampleScene`.
4. Press **Play**.
