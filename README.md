# Flowfields
Hi,
In this project i will research Flowfields.

What are Flowfields?

Flowfields, which you can also call vector fields is a technique to calculate the best path to a destination from somewhere in the world. The world is divided into a grid, every cell has their own vector. The vectors point towards their neighboring node, which gives the fastest path towards the goal. When an agent passes over a cell, it uses the vector inside that cell to influence its velocity. The agent traverses over every cell like this until it reaches the goal. 

How to create a flowfield?

I decided to create my implementation in unity. I made my setup by creating a simple ground(grass) plane and creating a 30x30 grid. Now we can start of creating our flowfield.

Creating a flowfield takes a couple of steps.
First of all you need to create a cost field. A cost field gives every cell a value between 1 and a maximum amount. My maximum amount was 300, but this can be any other large number. Every cell is set to a value of 1 by default. I created 2 terrain types, mud and walls and added some of these objects to my scene. I then went over each cell, upon colliding with one of these terrain types i increased their respective cell. I increased the cost of mud cells by 4, so they have a value of 5 and wall cells got a value of 300.

![](FlowfieldsSCAndVid/Costfield.png)
