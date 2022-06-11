<h1>Routing in the data network</h1>

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Setup](#setup)
* [Lexer](#lexer)
* [Parser](#parser)
* [Code generator](#code_generator)

## General info

This is a C# Desktop Application for modeling the functioning of a computer network and studying the characteristics of the transmission of messages of different sizes with the determination of the size of information and service traffic, the number of transmitted packets, and the optimal size of information packets for specified transmission conditions.
	
## Tools and Technologies Used

* C# Programming language: ISO/IEC 9899:2011
* Microsoft Visual Studio (2019)
* .NET Framework 4.7.2
* Windows 11 or Windows 10 v1903 (18362) or newer.

 ## Use
 
 - development of a virtual data network for testing and research of real computer networks
 - educational product for meeting with the concepts of network relations

## Features and Requirments

- quick manual input of components of the network topology with the mouse - nodes and channels (full duplex and half duplex);
- random generation of network structure with specified creation policies;
- policies for channels: random weight selection within specified limits and constant value from a given set of values;
- policies for channel buffers: random weight selection within specified limits and constant value from a given set of values;
- implementation of basic user controls: adding, deleting, selecting, dragging nodes and channels;
- when capturing an object with the mouse, basic information about the object is displayed (for example, routing table, channel weight, loading buffers, etc.);
- the ability to disable, enable selected nodes and channels;
- review of step-by-step execution of algorithms;
- generating random message traffic;
- menu for sending specific messages from one network workstation to another, indicating their size.

## Setup

1. Download and Clone this project
2. Open the solution file in visual studio
2. You are Ready to Go

## General overview

Combining networks consisting of different subnets is achieved through routers, which in turn perform the following functions:

- providing communication between networks;
- providing routing and data delivery, which exchange processes on end systems connected to different types of networks;
- presentation of these functions in such a way that it is not necessary to change the architecture of any of the connected subnets.

The main components of the network are routers connected by communication lines, as well as devices belonging to the client.

There are three ways to transmit data packets, namely: simplex, half-duplex and full-duplex. The transmission mode determines the direction of signal flow between the two connected devices.
The main difference between the three modes of transmission is that in the simplex mode of transmission, the connection is unidirectional or unilateral; whereas in half-duplex transmission mode, the connection is bidirectional, but the channel is used interchangeably by both connected devices. On the other hand, in full-duplex transmission, the connection is two-way or two-way, and the channel is used by both connected devices at the same time.

An important function of the network layer is to select the route for transmitting packets from the start to the end node. In most networks, packets need to go through multiple routers. Route selection algorithms and the data structure they use (algorithms) are the main goal when designing a network layer.
The routing algorithm is implemented by the part of the network layer software that is responsible for selecting the source line to send the incoming packet. You also need to understand that routing and forwarding are different processes. Forwarding is the processing of incoming packets and choosing for them, according to the routing table, the source line. Routing is responsible for filling in and updating routing tables. This uses routing algorithms.

<h3>Distributed adaptive routing algorithm</h3>

In this algorithm, the connected nodes exchange information for further distribution. As a result, a recalculation of the routing table each time the node receives information.
Additionally, one network can have several optimum routes. The set of optimal paths from all senders to recipients is called the input tree. The task of all route selection algorithms is to calculate and use the input tree for all routers. The input tree does not contain loops, so each packet is delivered to the recipient for a limited number of forwardings. However, in real life, communication lines and routers may fail during certain operations. Therefore, different routers may have various ideas about the current network topology.

There are several algorithms for finding the shortest path between two nodes. One of them was proposed by Edsger Wybe Dijkstra in 1959. Each node "notices" the distance to it from the sender's node in the least known way. Initially, the paths are unknown, so all nodes are considered inaccessible. After finding distances, the node marks change and indicates the optimal path. Once the way is clear, the mark corresponds to the shortest path, transforming into a permanent relation.

<h3>Expected output</h3>

- transmission time from the initial node to the final;
- the number of packets into which the message was broken;
- the amount of official information;
- transmission route.


## Lexer



## Parser




## Code generator


