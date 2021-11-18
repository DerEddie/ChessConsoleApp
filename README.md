# ChessConsoleApp

OOP-Project: Implementing Chess as a Concole App.

Currently working on the logic of the pieces.
Especially King interaction like pins ect. aren't implemented yet.

In the follwing chapter I go through the concepts of OOP and give examples related to my chess project.

## Inheritance:
A parent class has some functionality which the child class can inherit from.
All types in .Net inherit from the Object class meaning functions like Equals, ToString, GetType already exist.

## Polymorphism
Implementing methods that must be implemented by any derived classes
For example:
The Class Piece (parent class) has a Method called GetPossibleMoves(). Classes like pawn, knight etc. inherit from the piece class.
Since every kind of piece moves different the implementation of GetPossibleMoves() is unique inside the child classes.
The Input for the GetPossibleMoves is the currentChessboardObject (to detect blocking and other illegal moves). The returned list is really different
depending on which child class called the GetPossibleMoves()-Method.
