
<p align="center">
  <img width="540" src="https://github.com/diku-dk/DIKUArcade/blob/master/Logo/DIKU-Arcade.png?raw=true" alt="Material Bread logo">
</p>
<br>

**DIKUArcade** is a simple 2D Game Engine, created specifically for the SoftwareDevelopment (SU)
course at DIKU (Department of Computer Science, University of Copenhagen) 2018, and maintained to this date.

### Core Features ###

* A comprehensible API, designed for students with minimal programming experience.
* An event-bus system.
* 2D-rendering using DIKUCanvas which is based on Six Labors and SDL 2.0.
* A simple animation system.
* Timer for direct control over game loop FPS and UPS.

### Requested Features ###

* An audio engine for effects and music.
* Newtonian force functions for artificial gravity.
* More throrough unit-testing.
* GUI Framework.

### Development ###

DIKUArcade has been running stable since 2018, providing students in SU with a framework
on which to base their coding assignments.

DIKUArcade has been tested and trialed on all major desktop platforms, but if you do run
into a problem, find a bug, or wish for a feature to be added: Please feel free to post
an [issue](https://github.com/diku-dk/DIKUArcade/issues). Pull-requests are also welcome.

### Gettings Started ###
If you wish to contribute to the engine's source code, the best way to get started is to
download and build the engine, and run one of the test programs in `TestDIKUArcade/Programs.cs`:

```
$ git clone git@github.com:diku-dk/DIKUArcade.git
$ cd DIKUArcade/
$ dotnet restore
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
