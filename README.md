# Welcome to Dictator!

This project is a C# remake of the [ZX Spectrum](https://en.wikipedia.org/wiki/ZX_Spectrum "ZX Spectrum") game *Dictator* created
by _[Don Priestley](https://en.wikipedia.org/wiki/Don_Priestley)_ and published by [Dk'Tronics](https://en.wikipedia.org/wiki/DK%27Tronics "Dk'Tronics")
in 1983. The original game was developed using [Sinclair Basic](https://en.wikipedia.org/wiki/Sinclair_BASIC).

## General Information

_Dictator_ is a satirical strategy/management simulation game. The player takes on the role of the unscrupulous dictator of the
fictional banana republic Ritimba. The country is a notoriously unstable developing country, which is constantly on the verge
of civil war and whose main export are bananas.

The country is composed of several groups such as the peasants, the landowners, and the military. It also interacts with external
groups. Each of these groups tries to strengthen its own position at the expense of the others. The only neighbouring state of Ritimba
is the People's Republic of Leftoto, a left-wing agricultural country that is sponsored by Russia. In between the countries
there is a mountain range that serves as a refuge for the guerrillas, whose only purpose in life is to free Ritimba from the tyranny
of the player's dictatorship.

<p float="left">
<img src="https://github.com/sfvicente/Dictator/blob/main/Images/dictator1.png?raw=true" width="350" /> 
<img src="https://github.com/sfvicente/Dictator/blob/main/Images/dictator2.png?raw=true" width="350" /> 
<img src="https://github.com/sfvicente/Dictator/blob/main/Images/dictator3.png?raw=true" width="350" /> 
<img src="https://github.com/sfvicente/Dictator/blob/main/Images/dictator4.png?raw=true" width="350" /> 
<img src="https://github.com/sfvicente/Dictator/blob/main/Images/dictator5.png?raw=true" width="350" /> 
<img src="https://github.com/sfvicente/Dictator/blob/main/Images/dictator6.png?raw=true" width="350" /> 
</p>
<center><sup>Screenshots from the original game</sup></center>

## Objective

The objective of the game is to stay alive and in power as long as you can, while transferring as much state money and development
aid as possible to the player's private *Swiss* bank account.

## Game Mechanics

The game runs in rounds, with each round corresponding to one month. At the beginning of the month there is an audience in which one
of the groups puts forward a petition that the player can accept or reject. This either improves or worsens further relations with
groups. When a player's popularity with a group decreases too much, it can trigger revolutions or assassination attempts.

The game is deliberately designed in such a way that it is impossible to keep the country stable in the long term. Sooner or later
one or more groups will become unhappy and plot or rebel against you. It is therefore important to be resource efficient to
get away alive and as much money as possible when everything collapses in the end.  

As a game progresses, there is a constantly decreasing number of possible courses of action and an ongoing escalation of the political
situation. This is due to randomly interspersed events ensures that the game generates. A game frequently ends with a revolution or
assassination attempt by a dissatisfied group, which can result in the death of the dictator.

## Development Status

All the major functionality is implemented except for a small part in the revolution mechanic. In terms of the UI, it has
been built as a console application, so some graphics, design, and effects (such as flashing buttons and titles) are missing
or different from the original.

I am currently documenting the code, fixing small issues, adding tests, and finishing the missing revolution logic.

## Future Development

It should be easy to port it to an environment which can be used to simulate the _ZX Spectrum_ graphics (such as _JavaScript_ or _Blazor_
with the `Canvas` element). Most of the code would be used as-is and the UI part can be easily adapted.

This project can probably be converted into a modern game using _Unity_ or a similar framework with new original graphics with
support for mobile devices. Another nice feature would be to allow players to enjoy the game online and have an improved mechanism 
to store and display the multiple scores.