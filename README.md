# Kembyela.tn – Gestion intelligente des lettres de change (Traites)

## 🧭 Navigation
- [Présentation](#-présentation-générale)
- [Objectifs](#-objectifs-du-projet)
- [Fonctionnalités](#-fonctionnalités-principales)
- [Captures d’écran](#-captures-décran)
- [Avantages](#-avantages-clés)
- [Compatibilité](#️-compatibilité-technique)
- [Licence](#-licence-et-modèle-commercial)
- [Succès commercial](#-adoption-et-succès-commercial)
- [Architecture](#️-architecture-du-projet)
- [Installation](#-installation-et-déploiement)
- [Support](#-support)

---

## 📌 Présentation générale

**Kembyela.tn** est une application professionnelle dédiée à la gestion des lettres de change (traites / الكمبيالات).  
Elle permet de créer, gérer, suivre et imprimer les traites de manière simple, rapide et sécurisée.

Développée avec **ASP.NET Core MVC** et **SQL Server**, la solution est destinée aux **entreprises**, **commerçants**, **cabinets comptables** ainsi qu’aux **personnes physiques**.

---

## 🎯 Objectifs du projet

- Digitaliser la gestion des lettres de change  
- Réduire les erreurs manuelles  
- Suivre efficacement les échéances et paiements  
- Simplifier l’impression des traites  
- Centraliser toutes les opérations dans une seule plateforme  

---

## 🧠 Fonctionnalités principales

### ✅ Gestion des traites
- Création et modification des lettres de change
- Gestion de plusieurs traites par client
- Suivi des échéances multiples
- Historique détaillé des opérations

### ✅ Suivi des paiements
- État des traites : payée / en attente / échue
- Visualisation claire des échéances
- Réduction des retards et oublis

### ✅ Impression professionnelle
- Impression directe des traites
- Compatible avec toutes les imprimantes standards
- Mise en page conforme aux usages commerciaux

### ✅ Interface intuitive
- Interface claire et ergonomique
- Prise en main rapide
- Navigation fluide et structurée

---

## 🖼️ Captures d’écran

> Les captures doivent être placées dans le dossier :  
> `/docs/screenshots/`

### Tableau de bord
![Dashboard](docs/screenshots/dashboard.png)

### Création d’une lettre de change
![Création Traite](docs/screenshots/traite-create.png)

### Liste et suivi des traites
![Liste Traites](docs/screenshots/traite-list.png)

### Impression professionnelle
![Impression](docs/screenshots/print.png)

---

## 💼 Avantages clés

- ✔️ Gain de temps considérable
- ✔️ Réduction des erreurs humaines
- ✔️ Centralisation et sécurité des données
- ✔️ Conforme aux pratiques commerciales
- ✔️ Solution stable et éprouvée

---

## 🖥️ Compatibilité technique

- **Systèmes d’exploitation** :
  - Windows 10
  - Windows 11

- **Technologies utilisées** :
  - ASP.NET Core MVC
  - Entity Framework Core
  - SQL Server
  - HTML5 / CSS3 / JavaScript
  - Bootstrap

---

## 🔐 Licence et modèle commercial

- ✅ Licence à vie
- ❌ Aucun abonnement
- ❌ Aucun frais mensuel
- 💳 Paiement unique

La licence inclut :
- Installation du logiciel
- Formation à l’utilisation
- Assistance technique

---

## 📊 Adoption et succès commercial

La solution **Kembyela.tn** a connu un réel succès commercial :

- ✔️ Plus de **100 licences vendues**
- ✔️ Utilisée par plus de **100 entreprises et clients**
- ✔️ Adoptée par des **personnes morales et physiques**

Ce taux d’adoption confirme la fiabilité et la valeur métier de la solution.

---

## 🏗️ Architecture du projet

- Application Web ASP.NET Core MVC
- Base de données SQL Server
- Architecture en couches :
  - Controllers
  - Models
  - Views
  - Services
  - Data Access Layer

---

## ⚙️ Installation et déploiement

### 🔹 Prérequis
- Windows 10 ou Windows 11
- .NET 6 ou .NET 7 Runtime
- SQL Server (LocalDB / Express / Standard)
- IIS (pour déploiement serveur)

---

### 🔹 Étape 1 : Cloner le projet
git clone https://github.com/Melek-Lahmar/kembyela-traites.git
### 🔹 Étape 2 : Configuration de la base de données

Modifier le fichier appsettings.json :

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=KembyelaDB;Trusted_Connection=True;"
}
Puis exécuter les migrations :

dotnet ef database update
### 🔹 Étape 3 : Lancer l’application en local
dotnet restore
dotnet run
