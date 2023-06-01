# Unity-ML-Agents-Training-a-Robot
Train a virtual robot to walk towards a target using Unity, ML Agents, and Reinforcement Learning. This project combines RL and Imitation Learning techniques to optimize the robot's movements. Explore the intersection of Unity, ML Agents, and RL, and learn to train intelligent agents for robotics and autonomous systems.

![image](https://github.com/sushantmenon1/Unity-ML-Agents-Training-a-Robot/assets/74258021/cc133f1c-2e37-44ce-8ceb-f5f91a22974c)

## About
**Unity-ML-Agents-Training-a-Robot** is an implementation of reinforcement learning in an old basic Unity tutorial project.

## Contents
1. [Environment](#environment)
2. [Getting started](#getting-started)
3. [Reward System](#reward-system)
4. [Observation Space](#observation-space)
5. [Action Space](#action-space)
6. [How it was trained](#how-it-was-trained)

## Environment
* Python 3.7.9
* PyTorch 1.9.1 `pip install torch~=1.9.1 -f https://download.pytorch.org/whl/torch_stable.html`
* ML Agents 0.27.0 python package `pip install mlagents`
* ML Agents unity package (I installed pre-released version 2.0.0 from the package manager)

## Getting Started

### Play the game

To play the original game, open the `playground` in Asstes/Scenes. Set the `BehaviorType` to heuristic under the component `BehaviorParameters` and just hit play. 

### Watch the agent in action

To see the trained agent in action, open the `playground` scene. Make sure that the component `BehaviorParameters` on the gameObject `Player_Agent` has its `BehaviorType` set to `Inference Only` and has its `Model` set to a brain (in my case, the trained brain I use is `My behaviour.onnx` in `results/ppo/`).

Hit play and watch the results.

### Train the agent

To train the agent, open the `playground` scene. 

1. Set the `BehaviorType` of the agent to `Default`
2. Open a Command Prompt at the project root
3. Type `mlagents-learn config/configuration.yaml --run-id=test1 --time-scale=7`
4. Hit play in the editor

**To speed up the training** Feel free to add or remove training environments depending on your system performance. 

**Enjoy the training !**

## Reward System
The agent learns by given it rewards and punishments. This is how it is rewarded:

1. The agent gets `0.3f` if it hits the target
2. The agent gets `-0.3f` if the distance between the player and target is greater than 5f
3. The agent gets `-0.5f` if the distance between the player and target is greater than 10f

## Observation Space

As observations, the agent has its own location and the target location.

## Action Space

### Continuous Actions

1. Horizontal Movement
2. Vertical Movement

### Discrete Action

1. Move X (0,1,-1)
2. Move Y (0,1,-1)

## How it was trained

I started the training with **imitation learning**. I set the `BehaviorType` to `Heuristic` and added a `DemonstrationRecorder` to record a demonstration.




## Disclaimers

* I do not own the original project, the project is from [Unity Learn](https://learn.unity.com/project/survival-shooter-tutorial).
* Since the original project was not intended to have an implementation of ML Agents, I had to make workarounds and I did not used the best programming practices to plug in a ML agent.
