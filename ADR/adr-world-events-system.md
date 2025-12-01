# ADR: World Event System - 2025-12-01

Version: 0.1

Status: Proposed

## Context

To make the simulation "progress" we need a notion of time.
Multiple models exists, the simplest one AFAIK is a tick system.

## Possible Solutions

### Solution A - Tick system

#### Description

A tick system is a discrete representation of time.

Each tick, all "actions" are processed, then the time is incremented.

#### Pros

- Simpler to implement
- Easy to think about and track what happened
- Easy to sync. actions
- Resilient to "lag" (as long as there is no client in real time)

#### Cons

- Actions will happen at the same time, need to define a mechanism/solution
  when 2 entities will try to access the same ressource

## Decision

## References
