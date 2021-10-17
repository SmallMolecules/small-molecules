# Guide for Developers

This page documents each component of our particle simulator framework. See below:

- [Class List](file:annotated.html)
- [Files](file:files.html)
- [Github and Installation Guide](https://github.com/SmallMolecules/small-molecules)

The overall structure of this framework is to manage multiple simulators, which each hold their own set of particles, fields and scales. All simulators are managed by a Simulation Manager which controlls global properties such as the pause state, the max number of particles and the creation and deletion of the simulators. The fields classes (StaticField and DynamicField) are superclasses that can be extended to produce custom fields. 

## Simulator    

The Simulator class is a MonoBehaviour class that is attached to an empty GameObject. It contains an two array of fields, one for the static fields and one for the dynamic fields. If you wish to add your own custom field, navigate to ``start()`` function of the Simulator class and add

```cs
DynmaicFields.add(new YourDynamicField(scales)); //For Dynamic Fields
// or
StaticFields.add(new YourStaticField(scales)); //For Static Fields
```

Each frame the Simulator will iterate through each field and add the force contribution of that field to all the particles. Each time a force is added to a particle, it changes its velocity based on its previous velocity and mass. Once all particles' velocities have been completed updated, the particles' positions are stepped forward by their velocity multiplied by some timestep (see Scales and Scale).

The scales object of the Simulator is referenced by the particles and the fields in their calculations.

## Simulator Manager

The Simulator Manager class is a MonoBehaviour class that is attached to an empty GameObject. it is responsible for managing global properties outside of each simulator.

## Particle

The Particle class manages the properties of a particle. Each particle class contains a particle GameObject, but is not itself attached to it (see ParticleBehaviour). It contains staitc fields for the mass, charge and radius and well as fields for the position and velocity. 

The static fields are expressed in scaled units that we refer to as "Unity Units" as the calculations in the fields are done on these units. See Scales for the conversions used.

The position and velocity fields are similarly expressed in "Unity Units". A user that wishes to measure the "real-world" representation of the position or velocity will recieve a conversion from the Unity Units to the real-world unit based on the Scales object of the partent simulator.

The particle's velocity is initialised to some value and updated by a fields's ``AddForce()`` method. The position of the particle is updated by its parent simulator's ``UpdatePositions()`` method. 

## Scales and the Scale Struct

Each simulator contains an Scales object - an object that contains multiple scale structs each representing the conversion between some Unity unit and the corresponding real-world representation. The Scales can be updated through the callback methods used in by UI elements or by calling the inbuilt functions. 

There is a scale field for the length and time scale (which can change), and static values for the mass and charge values.

The scale struct is necessary over a floating point representation of the value as the presicion required in calcualtions of unit conversions exceed the range of allowable float values. It contains the coefficient, exponent and float value of some conversion factor. The Scales class can multiply the coefficient and exponent of each scale seperately in order to maintain the precision.

## Dynamic and Static Field Classes

The dynamic and static fields classes are super classes that should be extended to provide functionality for the ``AddForce()`` method (see Coloumb and Wind for two examples of an implementation). 

Each field class contains two dictionaries, shown below:

```cs
protected Dictionary<string, float> constants;
private Dictionary<string, int[]> units;
```

If you wish to implement your own field, you must use the ``RegisterConstant(string name, float val, int[] unit)`` function. This function converts the SI value of the constant to unity units for use in interal calculation by the framework. Apon any time or length scaling of the simulator, this value, stored in ``constants``, will be updated according to the values stored in ``units``. 

The value of the units dictionary is an int array of size 4 that specify the index of the mass (kg), length (m), time (s) and charge (Q) units. For example, if your units are of the form kg m s<sup>-2</sup>, the unit value would be {1, 1, -2, 0}.

See StaticField and DynamicField for more details. 

Using an example from the Coloumb class, we can see how the constructor should be implemented.

```cs
public Coloumb(Simulator sim) : base(sim)
{
    int[] units = { 1, 3, -2, -2 };
    RegisterConstant("coloumb", 8.988E+9f, units);
}
```
To access your constants from your ``FieldDynamics`` override function simply reference it as follows:

```cs
float coloumb = constants["coloumb"];
```


Note: avoid using functions that have a (1/r) dependence as these lead to singularities which break the realism of the simulation. Instead split the dynamics into two regimes - one where outside the collision zone and one inside the collision zone.
