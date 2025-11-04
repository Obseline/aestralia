# Aestralia

## Ressources
- https://github.com/dotnet/runtime/blob/main/docs/coding-guidelines/coding-style.md

## Idées d'arborescence

```
Aestralia/
│
├── AestraliaBackend.sln               → Solution .NET
│
├── src/
│   ├── AestraliaBackend/              → Projet principal (backend / simulation)
│   │   ├── Program.cs
│   │   ├── appsettings.json           → Config (DB, logs, etc.)
│   │   │
│   │   ├── Data/                      → Base de données et contexte EF Core
│   │   │   ├── WorldContext.cs
│   │   │   └── Migrations/            → Fichiers auto-générés par EF
│   │   │
│   │   ├── Models/                    → Entités (tables du monde)
│   │   │   ├── NPC.cs
│   │   │   ├── Village.cs
│   │   │   ├── WorldEvent.cs
│   │   │   └── Resource.cs
│   │   │
│   │   ├── Simulation/                → Logique “vivante” du monde
│   │   │   ├── CycleManager.cs        → Gère les cycles / ticks
│   │   │   ├── ActionSystem.cs        → Exécute les actions PNJ
│   │   │   ├── NeedsSystem.cs         → Faim, énergie, bonheur, etc.
│   │   │   └── WorldInitializer.cs    → Création du monde initial
│   │   │
│   │   ├── Services/                  → Services généraux (accès DB, log, etc.)
│   │   │   ├── NPCService.cs
│   │   │   └── VillageService.cs
│   │   │
│   │   ├── Utils/                     → Fonctions utilitaires (random, logs…)
│   │   │   └── RandomHelper.cs
│   │   │
│   │   └── Logs/                      → Fichiers de logs journaliers (si tu veux)
│   │
│   └── AestraliaBackend.Tests/        → Tests unitaires
│       ├── SimulationTests.cs
│       └── DatabaseTests.cs
│
├── .gitignore
├── LICENSE
└── README.md
```
