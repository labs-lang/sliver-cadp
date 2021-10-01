
# The LAbS language: example specifications 

This repository contains some examples of multi-agent systems specified in the
LAbS language [[1]](#references). They may be analyzed with the
[SLiVER](https://github.com/labs-lang/sliver) tool.

## Description of the examples

### `alpha.labs`

A (tentative) implementation of the alpha-algorithm for swarm aggregation
[[5]](#references).

### `approx-majority.labs`

Implements the *approximate majority* population protocol [[3]](#references).

### `boids-[c?a?s?w?].labs`

These are several variants of the *boids* model of flocking behavior
[[2]](#references).
Agents move in a 2D arena and try to agree on the direction of movement or
on the *leader* of the flock (depending on the variant).

Agents have up to three behavioral rules, called *cohesion*, *alignment*, and
*separation*.
The letters `c`, `a`, `s` in the file name indicate which rules are enabled
for that file.
The letter `w` denotes systems where the arena wraps around (i.e. an agent can
cross the edge of the arena and get to the opposite side).

### `formation.labs`

Agents move on a line of size `_size` and attempt to reach a state where their distance from
each other is at least `_delta`.

### `leader.labs`

A simple leader election over a stigmergy. Nodes repeatedly publish their own
id on a stigmergic variable: the node with the lowest id (namely, 0) eventually
is chosen as leader by all nodes.

### `majority.labs`

Implements a (correct) *majority* population protocol [[4]](#references).

### `philosophers.labs`

A LAbS implementation of the well-known dining philosophers problem.
This example showcases some LAbS features such as arrays and environment
variables (which act as a shared memory) 


## Support

If you encounter any issues, please submit
an [issue](https://github.com/labs-lang/labs-examples/issues).

## References

[1] R. De Nicola, L. Di Stefano, and O. Inverso, “Multi-Agent Systems with Virtual Stigmergy,” in: Mazzara M., Ober I., Salaün G. (eds) Software Technologies: Applications and Foundations. STAF 2018. Lecture Notes in Computer Science, vol 11176. Springer, 2018. [Link](https://link.springer.com/chapter/10.1007%2F978-3-030-04771-9_26)

[2] C. W. Reynolds, “Flocks, herds and schools: A distributed behavioral model,” in 14th Annual Conference on Computer Graphics and                Interactive Techniques (SIGGRAPH), Anaheim, CA, USA, 1987, vol. 21, pp. 25–34.

[3] D. Angluin, J. Aspnes, and D. Eisenstat, “A simple population protocol for fast robust approximate majority,” Distributed Computing, vol. 21, no. 2, pp. 87–102, 2008.

[4] J. Aspnes and E. Ruppert, “An Introduction to Population Protocols,” in Middleware for Network Eccentric and Mobile Applications, B. Garbinato, H. Miranda, and L. E. T. Rodrigues, Eds. Springer, 2009, pp. 97–120.

[5] A. F. T. Winfield, W. Liu, J. Nembrini, and A. Martinoli, “Modelling a wireless connected swarm of mobile robots,” Swarm Intelligence, vol. 2, no. 2–4, pp. 241–266, 2008, doi: 10.1007/s11721-008-0018-0.

