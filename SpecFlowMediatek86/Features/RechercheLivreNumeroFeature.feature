Feature: RechercheLivreNumero
	Test sur la recherche d'un document par son numéro

@rechercheLivreNumero
Scenario: Chercher un livre par son numero
	Given Je saisis la valeur 00025
	When Je clic sur le bouton Rechercher
	Then Les informations détaillées doivent afficher le titre L'archipel du danger