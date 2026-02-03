# Kembyela.tn – Gestion intelligente des lettres de change (Traites)

## 🧭 Navigation
- [Présentation](#-présentation-générale)
- [Objectifs](#-objectifs-du-projet)
- [Fonctionnalités](#-fonctionnalités-principales)
- [Captures d’écran](#-captures-d%E2%80%99%C3%A9cran)
- [Avantages](#-avantages-clés)
- [Compatibilité](#-compatibilité-technique)
- [Licence](#-licence-et-modèle-commercial)
- [Succès commercial](#-adoption-et-succès-commercial)
- [Architecture](#-architecture-du-projet)
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

> Les captures doivent être placées dans le dossier : `/docs/screenshots/`

### Tableau de bord
<img width="1297" height="825" alt="Dashboard" src="https://github.com/user-attachments/assets/9b764903-1a95-4909-b3a3-3e29fb3f9d11" />

### Création d’une lettre de change
<img width="862" height="873" alt="Création Traite" src="https://github.com/user-attachments/assets/8c5b239c-98c7-4297-861a-85bdf7a649de" />

### Liste et suivi des traites
<img width="430" height="577" alt="Liste Traites" src="https://github.com/user-attachments/assets/d536e127-022a-4093-a0ce-14850bd70b4d" />

### Impression professionnelle
<img width="706" height="387" alt="Impression" src="https://github.com/user-attachments/assets/ebc41eaf-0286-4c8d-949d-1971a3cf52e2" />
![Uploading image.png…]()


---

## 💼 Avantages clés

- ✔️ Gain de temps considérable
- ✔️ Réduction des erreurs humaines
- ✔️ Centralisation et sécurité des données
- ✔️ Conforme aux pratiques commerciales
- ✔️ Solution stable et éprouvée

---

## 🖥️ Compatibilité technique

- **Systèmes d’exploitation :**
  - Windows 10
  - Windows 11

- **Technologies utilisées :**
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

- ✔️ Plus de **100 licences vendues**  
- ✔️ Utilisée par plus de **100 entreprises et clients**  
- ✔️ Clients **personnes morales et physiques**  

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
- Windows 10 / Windows 11
- .NET 6 ou .NET 7 Runtime
- SQL Server (LocalDB / Express / Standard)
- IIS (pour déploiement serveur)

---

### 🔹 Étape 1 : Cloner le projet
``bash
git clone https://github.com/Melek-Lahmar/kembyela-traites.git
🔹 Étape 2 : Configurer la base de données
Modifier le fichier appsettings.json :

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=KembyelaDB;Trusted_Connection=True;"
}
Puis appliquer les migrations :

dotnet ef database update
🔹 Étape 3 : Lancer l’application en local
dotnet restore
dotnet run
Accéder à l’application :

https://localhost:5001
🔹 Étape 4 : Déploiement sur IIS
Publier le projet depuis Visual Studio

Copier les fichiers générés sur le serveur

Créer un site IIS

Configurer la connexion SQL

Démarrer le site

🔹 Étape 5 : Installation pour le client
Installation sur poste Windows

Création d’un raccourci bureau

Formation utilisateur incluse

Assistance technique assurée

📞 Support
LinkedIn: Melek Lahmar

Email: lahmarmelek67@gmail.com

GitHub: github.com/Melek-Lahmar

👨‍💻 Auteur
Développé par Melek Lahmar
© Kembyela.tn – Tous droits réservés


---

Si tu veux, je peux maintenant te faire **une version encore plus “pro commerciale”**, avec des **badges GitHub**, **tableau résumé des fonctionnalités**, et **README prêt pour client ou PFE**.  

Veux‑tu que je fasse ça ?
