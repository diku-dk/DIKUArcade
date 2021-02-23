
<p align="center">
  <img width="540" src="https://github.com/diku-dk/DIKUArcade/blob/master/Logo/DIKU-Arcade.png?raw=true" alt="Material Bread logo">
</p>
<br>

**DIKUArcade** is a simple 2D Game Engine, created specifically for the SoftwareDevelopment (SU)
course at DIKU (Department of Computer Science, University of Copenhagen) 2018, and maintained to this date.

### Core Features ###

* A comprehensible API, designed for students with minimal programming experience
* An event-bus system, designed for issuing commands in parallel
* 2D-rendering using OpenGL 2.0's fixed-function pipeline
* A simple animation system
* Timer for direct control over game loop FPS and UPS.

### Requested Features ###

* An audio engine for effects and music
* Support for OpenGL 3.3 for a more optimized rendering pipeline
* Newtonian force functions for artificial gravity
* More throrough unit-testing
* GUI Framework

### Development ###

DIKUArcade has been running stable since 2018, providing students in SU with a framework
on which to base their coding assignments.

Latest features includes an update to dependencies (OpenTK 4.5.0, latest at day of writing),
and .NET Core 5.0. DIKUArcade has been tested and trialed on all major desktop platforms,
but if you do run into a problem, find a bug, or wish for a feature to be added: Please feel
free to post an [issue](https://github.com/diku-dk/DIKUArcade/issues).
Pull-requests are also welcome.

### Gettings Started ###

If you wish to contribute to the engine's source code, the best way to get started is to
download and build the engine, and run one of the test programs in `TestDIKUArcade/Programs.cs`:

```
$ git clone git@github.com:diku-dk/DIKUArcade.git
$ cd DIKUArcade/
$ dotnet build DIKUArcade/
$ dotnet build TestDIKUArcade/
$ dotnet run -p TestDIKUArcade/
```

### List of contributors

Boris Düdder (SU course responsible, EventBus and rendering)<br>
Oleksandr Shturmov<br>
Alexander Christensen (main architect, alch@di.ku.dk)<br>
Christian Olsen<br>
Simon Surland Andersen<br>
Mads Obitsoe<br>
Benjamin Kanding<br>
_(your name here... ?)_
