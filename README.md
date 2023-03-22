# **Deliverance CORP.**

Deliverance CORP. is a game in which you play as a warehouse worker sifting through piles of boxes, delivering what you should and incinarating what you should not.

The goal of this project is to reproduce optimization implementations to allow the game to support a higher number of objects in a scene, lightweight runtime game object instantiation, and optimized rendering techniques. The project is developed in Unity Engine using C#. It incorporates several design patterns and principles to achieve efficient and flexible game development. It follow Microsoft's C# best practices and [naming conventions](https://www.c-sharpcorner.com/UploadFile/8a67c0/C-Sharp-coding-standards-and-naming-conventions/).

WATCH DEMO: [Deliverance Corp. - Prototype Demo](https://youtu.be/OhzSyJxB9YE)
 
*Contents*
- [**Deliverance CORP.**](#deliverance-corp)
  * [**Architecture**](#architecture)
    + [*Event-Driven architecture*](#event-driven-architecture)
  * [**Design Patterns**](#design-patterns)
    + [*Flyweight structural pattern*](#flyweight-structural-pattern)
    + [*Factory creational pattern*](#factory-creational-pattern)
    + [*The State behavioural pattern*](#the-state-behavioural-pattern)
    + [*The Mediator behavioural pattern*](#the-mediator-behavioural-pattern)
  * [**Rendering**](#rendering)
    + [*Frustum Culling*](#frustum-culling)
    + [*Independent Background Rendering*](#independent-background-rendering)
  * [**Challenges**](#challenges)
    + [*Rigidbody Manipulation*](#rigidbody-manipulation)
  * [**Principles**](#principles)
  * [**Results**](#results)

 
## **Architecture**
### *Event-Driven architecture*

The project follows an Event-Driven architecture where software components communicate with each other by producing and consuming events rather than through direct method calls. This approach helps to decouple components and make the system more scalable and flexible. It makes it easy to create new complex objects and interactions.
 
## **Design Patterns**

### *Flyweight structural pattern*

Is used to reduce memory usage when dealing with many similar objects that share common properties. One implementation is the Pooling class which allows other classes to pool its objects instead of instantiating them on the fly. The [Pooling Class](https://github.com/HealthReminder/Optimization-Implementations/blob/featuresRendering/Assets/Scripts/Pooling/Pooling.cs) is a generic class, meaning it can create pools of any type of components.

```
public class Pooling<PoolObject> where PoolObject : Component
{
    Queue<PoolObject> _poolQueue;
    PoolObject _prefab;
    Transform _parent;
    LayerMask _layer;

    public Pooling(PoolObject prefab, int initialPoolSize, Transform parent, int layerIndex)
    {
       ...
    }
    public PoolObject GetFromPool(Vector3 position, Quaternion rotation)
    {
       ... 
    public void ReturnToPool(PoolObject obj)
    {
       ...
    }
}
```

### *Factory creational pattern*
Provides an interface for creating objects but allows subclasses to decide which class to instantiate. Any object that is responsible for spawning multiple game objects uses the Pooling class for performance reasons. The spawner classes are capable of spawning objects in different ways. The [CircleSpawner Class](https://github.com/HealthReminder/Optimization-Implementations/blob/featuresRendering/Assets/Scripts/Pooling/CircleSpawner.cs) instantiate a set amount of objects on awake so they can be pooled later. It has functions to spawn these pulled objects randomly in positions of a circumference. In other words, it spawns objects in a circle.

```
public class CircleSpawner : MonoBehaviour
{
    ...
    private void Awake()
    {
        _pool = new Pooling<Transform>(Prefab, PoolSize, transform, PoolLayer);
    }
   
    public void InstantiateTimed()
    {
        ...
    }
   
    IEnumerator SpawnRoutine()
    {
        ...
        yield break;
    }

    public void InstantiateRandom()
    {
       ....
    }
     public void ReturnToPool (Transform t)
    {
        ...
    }
}
```


### *The State behavioural pattern*
Allows an object to alter its behavior when its internal state changes. It is used for objects or agents like lamps, or the [Conveyor Belt Class](https://github.com/HealthReminder/Optimization-Implementations/blob/develop/Assets/Scripts/Objects/ConveyorBelt.cs).

```

public class ConveyorBelt : MonoBehaviour
{
    bool IsOn;
    ...
    private void OnCollisionStay(Collision collision)
    {
        if (!IsOn)
            return;
        }
        ...
    }
}
```




### *The Mediator behavioural pattern*
Defines an object that encapsulates how a set of objects interact and promotes loose coupling between them. It is used for managing the game, or the level. Level Managers will be loosely coupled to different objects in the scene and mediate the interactions between the Player and the Level.


## **Rendering**
### *Frustum Culling*
Frustum culling is a technique used in computer graphics to optimize the rendering of 3D scenes by reducing the number of objects that need to be processed and rendered. Frustum culling works by determining which objects in the scene are inside or outside of the frustum and only rendering the objects that are inside. This is achieved by testing each object in the scene against the frustum, using a simple geometric test. If an object is entirely outside the frustum, it is discarded and not rendered. This can significantly reduce the number of objects that need to be processed and can greatly improve the performance of a 3D application. 

In Deliverance CORP. frustum culling is excercised by the [Frustum Culling Class](https://github.com/HealthReminder/Optimization-Implementations/blob/develop/Assets/Scripts/Rendering/FrustumCulling.cs). The frustum culling class do simple geometry calculations to determine if a given renderer is inside or outside the camera's frustum. The frustum culling class occludes renderers from a list of renderers. This list of renderer is dynamically filled by the complementary [Occludee Class](https://github.com/HealthReminder/Optimization-Implementations/blob/develop/Assets/Scripts/Rendering/Occludee.cs). The Occludee class is attached to every object that has a renderer that will be occluded. On start, the occludee will add itself to the list of occludees in the frustum culling manager.

```
public class FrustumCulling : MonoBehaviour
{
    Camera Camera; /// Frustum planes will be calculated from this camera
    List<Renderer> _renderers; /// Occludees 
    
    void Update()
    {
        for (int i = 0; i < _renderers.Count; i++)
        {
            ...
                if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(Camera), rend.bounds))
                   // Object is visible, so enable rendering
                else
                   // Object is not visible, so disable rendering
        }

    }
    public void AddOccludee(Renderer renderer)
    {
    }
}
```

```
[RequireComponent(typeof(Renderer))]
public class Occludee : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<FrustumCulling>().AddOccludee(GetComponent<Renderer>());
    }
}
```

### *Independent Background Rendering*
Independent background rendering is a technique that uses two cameras to render the game world. The player camera is used to render the foreground, objects in the world that the player can interact with. The background camera, is used to render the background. The background camera is isolated from the foreground camera, and its objects are not interactive. 

There are several reasons why you would choose to do this:
- Improved performance: By using smaller objects and a separate camera for the background, you can reduce the number of polygons and objects that need to be rendered at any given time. This can improve performance and allow for more complex scenes to be rendered without sacrificing performance.

- Increased flexibility: Independent background rendering allows for greater flexibility in designing and constructing game environments. With this technique, you can create more detailed and immersive backgrounds, without worrying about the impact on performance or the need to create massive objects that may not fit well in the game world.

- Better player experience: By creating more immersive and detailed game environments, players are more likely to become engaged in the game and enjoy the experience. The use of two separate cameras can help to create a sense of depth and scale, making the game world feel more realistic and immersive.

This project implements this technique using the [Background Renderer Class](https://github.com/HealthReminder/Optimization-Implementations/blob/develop/Assets/Scripts/Rendering/BackgroundRenderer.cs) that renders the camera to a texture every frame. In turn, there is a plane with a material, that constantly follows the foreground camera at all times. This plane has a material that uses the rendered texture. Essentially, it means that the background will be rendered to a plane that will be in the background of the game world, creating the illusion of an extensive world.

```
public class BackgroundRenderer : MonoBehaviour
{
    Camera BackgroundCamera;     //Camera from which the render texture will be generated
    Material BackgroundMaterial; //Material that will be rendering the camera

    void Update()
    {
        ...
        RenderTexture renderTexture = BackgroundCamera.targetTexture;
        BackgroundMaterial.mainTexture = renderTexture;
        ...
    }
}
```



## **Challenges**
### *Rigidbody Manipulation*
The idea of the game was to have the player moving boxes, or any rigidbody, around. The manipulation of these rigidbodies represented a challenge to achieve a smooth movement that felt natural. There were a number of solutions implemented before the final solution. 

The first idea was using a transform translation to move the object to a point in front of the player when it is being held. Transform movements, though, are not smooth and therefore feel artificial. Another solution tried was to implement a system to add forces to the rigidbodies towards the target position. This, in turn, did not work because it was too unpredictable and interacted too much with external factors. So for another test, a system of spring joints were used to hold the object in place. Because it uses forces instead of translations, it felt natural. Additionaly, using springs enables the rigidbodies to interact with the world, but withing the joint's constraints. 

The final solution was a mixture of force management and one single spring joint condensed in an object called [PlayerHands](https://github.com/HealthReminder/Optimization-Implementations/blob/develop/Assets/Scripts/Player/PlayerHands.cs). The script enables input from the player to hold, grab and throw a rigidbody. The object utilizes a spring joint to attract the rigidbody being held, and a mixture of counterbalancing forces to damper the movement oscillations caused by the spring joint.

```
public class PlayerHands : MonoBehaviour
{
    float _throwMultiplier; ///Multiplies the force applied to throw the object being held
    float _forceMultiplier; ///Multiplies the force applied to move the object being held
    Transform _anchorPoint; /// The position the held object will move to
    Camera _playerCamera;  /// The player camera from where rays will be shot
    Rigidbody _currentlyHolding;    /// The rigidbody being held by the player
    float _springStrength; /// How hard the rigidbody will be pulled towards the anchor
    float _springDamping; /// How much damping applied to the rigidbody
    SpringJoint _springJoint; /// The spring used to hold objects

    private void Update()
    {
        // Gets input for throwing, dropping, grabbing, and holding rigidbodies
        ...
    }
    public void ThrowRigidbody()
    {
       ...
    }
    public void DropRigidbody()
    {
       ...
    }
    public void GrabRigidbody()
    {
        ...
    }
  
    public void HoldRigidbody()
    {
        ...
    }
}
```

## **Principles**

- **SOLID** principles
- **Composition over inheritance**: Instead of inheriting complex behaviour, the program uses a composition of different simpler scripts to achieve the same.
- **DRY (Don't Repeat Yourself)**: Avoid duplicating code, and keep code that performs a particular task in a single place.
- **KISS (Keep It Simple, Silly)**: Keep designs and implementations as simple as possible, and avoid unnecessary complexity.
- **YAGNI (You Ain't Gonna Need It)**: Avoid adding functionality that is not currently needed, and only implement features that are required.

## **Results**

The final prototype product for Deliverance CORP. was a satisfactory game project with powerful rendering and optimization techniques. The physics-based object manipulation system looks natural and fun to use. The Rendering aspect of the game allows for the presence of multiple game objects at once in a scene. The implemented Pooling techniques enables the runtime spawning of multiple objects without burdening the processor. The chosen architectures and patterns are perfect for the concept of the game. The Event-Driven Architecture is useful for quick prototyping and fast development of many different but complex objects.

