Feature: PreparePlayFeature
	- Na het klikken op de "Click here to play" button, kom je terecht op de play page met de card "Choose a name and build"
	- Om het spel daadwerkelijk succesvol te kunnen starten (door op de 'Start!' button te klikken), moet een 'Character name' worden ingevuld. De build staat by default op "Thief", maar kan via de dropdown worden gewijzigd.
	- Na het succesvol klikken op de "Start!" button, kom je terecht op de play page met de card "Adventure time".
	- Indien succesvol klikken op de "Start!" button niet lukt, blijf je op de play page met de card "Choose a name and build" en krijg je de validatie-errors te zien.
	- De character name moet tussen de drie en twintig katakters zijn. De mogelijke errors zijn:
		* Te kort: Name must be at least 3 characters
		* Te lang: Name cannot be longer than 20 characters
	- Validatie-errors worden nóóit getoond zo lang de "Start!" button niet is ingedrukt. Ook niet als je een character name hebt ingevuld die niet aan de voorwaarden voldoet.
	- Indien je als gevolg van het klikken op de "Start!" button een validatie-error krijgt, deze waarde correct corrigeert en vervolgens voorziet van invalide waarde, krijg je ze validatie-error direct te zien zonder dat je hiervoor op de de "Start!" button moet klikken.
	- Via data binding is de ingevulde character name en geselecteerde build direct zichtbaar in het overzicht. 
	- Iedere build (Thief, Knight, Mage, Brigadier) heeft bij selectie vaste stats waarden (zie Domain folder), zichtbaar in FE wanneer build geselecteerd is. Thief is by default geselecteerd.
		    
#Happy flows
Scenario: H1. Kies een geldige character name, selecteer een build en start het spel
	Given ik op de button klik met de tekst "Click here to play"
	Then wordt de juiste pagina getoond met de card "Choose a name and build"
	When dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName | BuildType |
		| Spelersnaam   | Brigadier |
	And ik op de button klik met de tekst "Start!"
	Then wordt de juiste pagina getoond met de card "Adventure time"


Scenario: H2. De juiste error messages verschijnen en verdwijnen in de juiste situaties
	Given ik op de button klik met de tekst "Click here to play"
	And dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName | BuildType |
		| Hi            | Mage      |
	Then zie ik geen character name error message
	When ik op de button klik met de tekst "Start!"
	Then zie ik de character name error message: "Name must be at least 3 characters"
	When dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName | BuildType |
		| HiHiHi        | Mage      |
	Then zie ik geen character name error message
	When dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName         | BuildType |
		| HiHiHiHiHiHiHiHiHiHiH | Mage      |
	Then zie ik de character name error message: "Name cannot be longer than 20 characters"
	When dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName | BuildType |
		| HiHiHiHi        | Mage      |
	Then zie ik geen character name error message


Scenario: H3. Iedere geselecteerde build heeft eigen stats-waarden
	Given ik op de button klik met de tekst "Click here to play"
	When dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName   | BuildType   |
		| <CharacterName> | <BuildType> |
	Then zie ik de juiste character name, build type en stats waarden in het overzicht
		| CharacterName   | BuildType   | 
		| <CharacterName> | <BuildType> | 

Examples:
	| CharacterName  | BuildType |
	| Spelersnaam123 | Thief     |
	| Spelersnaam234 | Knight    |
	| Spelersnaam345 | Mage      |
	| Spelersnaam456 | Brigadier |


#Unhappy flows
Scenario: U1. Spel proberen te starten met character name die te kort is resulteert in error message
	Given ik op de button klik met de tekst "Click here to play"
	And dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName | BuildType |
		| Hi            | Mage      |
	When ik op de button klik met de tekst "Start!"
	Then zie ik de character name error message: "Name must be at least 3 characters"


Scenario: U2. Spel proberen te starten met character name die te lang is resulteert in error message
	Given ik op de button klik met de tekst "Click here to play"
	And dat ik de character name invul en een build selecteer met onderstaande details
		| CharacterName         | BuildType |
		| HiHiHiHiHiHiHiHiHiHiH | Mage      |
	When ik op de button klik met de tekst "Start!"
	Then zie ik de character name error message: "Name cannot be longer than 20 characters"
