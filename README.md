# Installation Guide and Information

## About this Framework

The scope of this framework is to create a working particle simulation in C#, to be implemented in Unity. This page specifies each 
classes and structs within the framework - the public and private attributes and methods.

The overall approach was to encapsulate iscolated instnaces of simulators within a [simulator manager](file:classSimulationManager.html). Each simulator contains its own
independent set of [Particles](file:classParticle.html), [Scales](file:classScales.html) and [Static](file:classStaticField.html) and [Dynamic](file:classDynamicField.html) fields that all work together to produce a simulation. To see the list of classes, click [here](file:annotated.html). To see the directory structure click [here](files.html). 

## Installing Unity

To install this framework, first you need to install unity on your system. To do that, navigate to Unity's [Downloads Page](https://unity.com/download) and install Unity Hub. After downloading and intsallting, create a Unity account or sign in with an existing one. From the unity hub you can download version 2020.3.16 or locate an existing install. It's from here that you can manage your installs of Unity and your projects.

## Installing the Framework

To install the framework, first navigate to our github page 

> [Small Molecules github page](https://github.com/SmallMolecules)

and run the following git command where you want the project folder to spawn. If you do not have git installed on your system, you can download it from [the git downloads page](https://git-scm.com/downloads). Next clone the repository onto your system by opening a terminal at that location and typing the following git command:

> git clone github.c[]()om/SmallMolecules

This will create a directory called *Small Molecules* which contains all the unity project files. You have now installed the framework.

## Running the Simulator

To run the simulator, simply add the folder *small-molecules/Particle Simulator* as a project, ensuring you have the correct version of Unity installed.

