# Installation Guide and Information

## About this Framework

The scope of this framework is to create a working particle simulation in C#, to be implemented in Unity. This page specifies each 
classes and structs within the framework - the public and private attributes and methods.

The overall approach was to encapsulate iscolated instnaces of simulators within a [simulator manager](file:classSimulationManager.html). Each simulator contains its own
independent set of [Particles](file:classParticle.html), [Scales](file:classScales.html) and [Static](file:classStaticField.html) and [Dynamic](file:classDynamicField.html) fields that all work together to produce a simulation. To see the list of classes, click [here](file:annotated.html). To see the directory structure click [here](files.html). 

## Installing Unity

To install this framework, first you need to install unity on your system. To do that, download version 2020.3.16 from Unity's [Downloads Page](https://unity3d.com/get-unity/download/archive). Alternatively if you already have a working install of Unity, you can download and install version 2020.3.16 from Unity Hub.

Downloaded should be the *Unity Hub* desktop app. It is from here where we can manage our Unity installs and projects.

## Installing the Framework

To install the framework, first navigate to our github page 

> [Small Molecules github page](https://github.com/SmallMolecules)

and run the following git command where you want the project folder to spawn. If you do not have git installed on your system, you can download it from [the git downloads page](https://git-scm.com/downloads). Next clone the repository onto your system by opening a terminal at that location and typing the following git command:

> git clone github.c[]()om/SmallMolecules

This will create a directory called *Small Molecules* which contains all the unity project files. You have now installed the framework.

