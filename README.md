# Flowfields
Hi,
In this project, I will talk about Flowfields and show a basic implementation.

What are Flowfields?

Flowfields, which you can also call vector fields is a technique to calculate the best path to a destination from somewhere in the world. The world is divided into a grid, every cell has its own vector. The vectors point towards a neighboring node, which gives the fastest path towards the goal. When an agent passes over a cell, it uses the vector inside that cell to influence its velocity. The agent traverses over every cell like this until it reaches the goal. This pathfinding system is often used in RTS games, some of the first flowfields were used in Supreme Command 2 and in Planetary Annihilation.

How to create a flowfield?

I decided to create my implementation in Unity. I made my setup by creating a simple ground(grass) plane, creating a 30x30 grid. I also implemented some visual debuginformation whilst working on the project. 


Creating a flowfield takes a couple of steps.
First of all, you need to create a cost field. A cost field gives every cell a value between 1 and a maximum amount. My maximum amount was 300, but this can be any other large number. Every cell is set to a value of 1 by default. I created 2 terrain types, mud and walls and added some of these objects to my scene. I then went over each cell, upon colliding with one of these terrain types i increased their respective cell. I increased the cost of mud cells by 4, so they have a value of 5 and wall cells got a value of 300.

![](FlowfieldSCAndVid/CostField.png)

After this, it is time to create the integration field. This is most of the calculations for the flowfield. It calculates the cost to travel from a cell in the world to the goal cell.
I used this algorithm and code:

1. The besCost of a cell is the lowest cost that it takes to get from that cell to the goal cell. By default every bestCost is set to a high value, I used 100000.
2. Set the destinationCel(the goal cell) cost and bestCost to 0 and add it to a queue.
3. While we still have cells to check, take the current cell, dequeue it and get its neighbors. 
4. For each (horizontal and vertical) neighbor, if their cost plus the currentcel its bestCost is lower than the neighbor its bestCost. Set the bestCost from the neighbor to this sum and add the neighbor back to the queue. If the neighborcost is equal to a wall(maxCostIncrease), it gets ignored. This causes the walls to stay at a bestCost of 100000, making them untraversable.
5. This repeats until the queue is empty.

![](FlowfieldSCAndVid/IntegrationFieldCode.PNG)

I made my destination cell the bottom left corner. The integration field then looks like this.

![](FlowfieldSCAndVid/IntegrationField.png)

With the integration field setup, we can create our actual flowfield. I go through every cell in the grid and compare the bestCost from the cell to its neighbors, this time I do not only compare it with the vertical and horizontal neighbors, but also with the diagonal neighbors. When I find the neighbor with the lowest bestCost, i store a vector in the cell that points to that neighbor. If the neighboring cells are untraversable(because they are a wall), the cell gets a zero vector. 
I visualized this in my project.

![](FlowfieldSCAndVid/FlowField.png)

After that, I implemented some agents. While they traverse a cell their velocity gets adjusted by the vector inside that cell until they reach the goal node. Every time I change the goal cell, the path gets recalculated. Resulting in this. 


https://user-images.githubusercontent.com/97398099/151199493-d12831b1-699c-4d5b-a47d-cecff0f78ada.mp4

In my project, you can spawn agents with s, delete them with d. Show the different kinds of fields by changing the debug object and change the destination field with the left mouse button.

Without the debug visualisation, this is the final result.


https://user-images.githubusercontent.com/97398099/151199881-e8858861-b219-46a7-8172-a6fa4489e708.mp4



End note:
Flowfield pathfinding is a great algorithm to lead large groups of agents to a destination. Combining this with other pathfinding methods can lead to some very optimized systems. I am certain it will keep on being used for a lot of RTS games in the future.


Sources:
https://www.jdxdev.com/blog/2020/05/03/flowfields/
https://howtorts.github.io/2014/01/04/basic-flow-fields.html#:~:text=Flow%20Fields%20are%20a%20technique,very%20common%20in%20RTS%20games.
https://www.youtube.com/watch?v=zr6ObNVgytk
https://www.youtube.com/watch?v=tSe6ZqDKB0Y
https://leifnode.com/2013/12/flow-field-pathfinding/



