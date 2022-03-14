# Game Of Life

This is a simplified variant of Conway's Game of Life. It is a console application, all output is to the console, it does not cater for all edge cases. The rules are modified from the usual as follows

+ If a cell is live and it has less than 2 live neighbours, it dies
+ If a cell is live and has more than 3  live neighbours, it dies
+ If a cell is live and it has 2 or 3 live neighbours, it lives
+ If a cell is dead and it has 3 live neighbours, it lives
+ a dead cell becomes a live cell 10% of the time

## Overview

On running the program the user is asked to enter the number of iterations they wish to add, this is in addition to the initial input which is consdered to be generation 0, if a non numeric value is entered the application will exit.

The user is then asked to input generation 0 as a series of strings. Each string should be the same length, if that's not the case the application will exit.

The strings can contain any characters but the following interpretation will be applied

+ if a character is 0 the cell will be considered dead
+ any other character, including space and other punctuation, will be interpreted as a live cell

When the user has completed their data entry they should press Return on an empty line. The application will now display the generations starting with generation 0 - the original input 

## Pre-requisites

This application runs on .NET 6.0 (LTS)
It has been tested on Windows 10 and Mac

SE March 2022
