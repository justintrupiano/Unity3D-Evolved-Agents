# Evolved Agents

**
This is a Unity 3D implementation of a generalizable artificially intelligent agent class.

***
The base Agent prefab is an empty game object with a connected Agent.cs script.
The Agent.cs script adds a randomly generated NeuralNetwork class object.

The NNCopy class extends the NeuralNetwork class by adding methods which can genetically evolve the values of those networks.

A base agent will do nothing except occupy RAM.

Agent extensions modify and utilize the base agent properties:

* XOR - This extension evolves the agents to act as XOR gates. Demonstrating the generalizable use-cases for this type of agent.