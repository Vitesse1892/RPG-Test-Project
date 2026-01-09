Feature: PlayAgainFeature

-----------------------------------------------------------------------------*****BUG*****-------------------------------------------------------------------------------------------------
Gegeven dat je een spel succesvol voitooid hebt met build "Thief"
En je op de "Play again" button klikt

Werkelijk resultaat: 
- Alle waarden voor stats in de verschenen PreparePlayPage staan op 1.
- Indien je nu doorgaat door valide character name op te geven zonder dat je expliciet een build selecteert, staan alle waarden voor stats in de verschenen AdventurePlayPage ook op 1.

Gewenst resultaat:
- Altijd startwaarden zoals in weergegeven in file "Domain --> BuildStatsRepository"
-----------------------------------------------------------------------------*****BUG*****-------------------------------------------------------------------------------------------------

#Expected actualValue to be "6" because Expected Agility value to be "6", but found "1", but "1" differs near "1" (index 0). --> | Thief | Thief  |
Scenario: H1 Klikken op de play again button redirect je naar de PreparePlayPage en toont de juiste build stats voor de default geselecteerde build
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<BuildType>"
	And dat ik alle taken succesvol heb afgerond
	When ik op de button klik met de tekst "Play again"
	Then zie ik character "<DefaultBuildType>" geselecteerd met de juiste startwaarden voor stats

Examples:
	| BuildType | DefaultBuildType |
	| Thief     | Thief            |
	| Knight    | Thief            |
	| Mage      | Thief            |
	| Brigadier | Thief            |

#En hier zou je dus verder kunnen automatiseren om vervolgens tegen de tweede bug aan te lopen zoals beschreven in de bug.
