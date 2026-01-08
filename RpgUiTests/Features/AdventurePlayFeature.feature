Feature: AdventurePlayFeature

	- Clicking 5 times on the button should level up your character and show a confirmation message.
	- Selecting a file for upload should level up your character and show a confirmation message.
	- Typing 'Lorem Ipsum' into the input field should level up your character and show a confirmation message.
	- Moving the slider all the way to the right should level up your character and show a confirmation message.
	- After completing each task, the related input should be set to 'disabled'.

	Denk ook aan disabled / niet meer klikbaar van button

	Er zijn vier taken te vervullen welke bij succes resulteren in de volgende bevestigingsberichten:
	1. clicker		-->	5 keer klikken					-->	Great job! You levelled up
	2. uploader		-->	1 keer bestand oploaden			--> File selected, level up!
	3. typer		-->	"Lorem Ipsum" typen				-->	Dolar sit amet!
	4. slider		-->	Volledig naar rechts schuiven	-->	Slid to the next level!

	Slider percentage uitsluitend tientallen procenten opgeven, dus {0%, 10%, 20%, ..., 100%)

	Scenario U2 Uploader - Klikken op "bestand kiezen" en vervolgens klikken op "Annuleren" is niet te automatiseren. Daarom enkel checken of corfirmation message en level up bij aanvang niet aanwezig zijn.

	#level dynamisch

	#dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen voor specifieke build wellicht
	#Mixen van alle scenario's E2E leidt tot extra confirmation message


#Happy flows
Scenario Outline: H1 De getoonde stats op de prepare play page komen overeen met de getoonde stats op de adventure play page voor elke build
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<BuildType>"
	Then komen de getoonde stats op de adventure page overeen met de getoonde stats van de prepare page

Examples:
	| BuildType |
	| Thief     |
	| Knight    |
	| Mage      |
	| Brigadier |


Scenario Outline: H2 Een voltooiing van de task leidt tot bijbehorende confirmation message en level up
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<Character>"
	When <Action>
	Then verschijnt voor de task "<Task>" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Examples:
	| Task     | Action                                                 | Character |
	| clicker  | de Click it! button 5 keer wordt ingedrukt             | Thief     |
	| uploader | een bestand wordt geüpload                             | Knight    |
	| typer    | het bericht "Lorem Ipsum" wordt getypt                 | Mage      |
	| slider   | de slider voor 100 procent naar rechts wordt geschoven | Brigadier |

Scenario Outline: H3 Een voltooiing van de task zorgt er voor dat het element van die task disabled raakt
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<Character>"
	Then is het element van de task "<Task>" enabled
	When <Action>
	Then is het element van de task "<Task>" disabled

Examples:
	| Task     | Action                                                 | Character |
	| clicker  | de Click it! button 5 keer wordt ingedrukt             | Thief     |
	| uploader | een bestand wordt geüpload                             | Knight    |
	| typer    | het bericht "Lorem Ipsum" wordt getypt                 | Mage      |
	| slider   | de slider voor 100 procent naar rechts wordt geschoven | Brigadier |

Scenario: H4 De Click it! button start met de tekst 'Click me 5 times' en loopt per klik dynamisch terug van 5 naar 0
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Knight"
	Then loopt de dynamische teller in de click it button terug van 5 naar 0 bij elke klik

Scenario: H5 Het afronden van de vier tasks resulteert in een max level bevestigingsbericht en een knop om opnieuw te spelen
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Brigadier"
	When de Click it! button 5 keer wordt ingedrukt
	And het "cotton candy" bestand wordt geüpload
	And het bericht "Lorem Ipsum" wordt getypt
	And de slider voor 100 procent naar rechts wordt geschoven
	Then verschijnt het max level bevestigingsbericht

	#Play again mogelijk pas nadat alle vier de taken zijn afgerond. 
	#Element licht op en af

Scenario: BONUS Easter Egg: Misbruik kwetsbaarheid typer task resulteert in level 10 stats
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Mage"
	When het bericht "Lorem Ipsum" wordt getypt
	And in totaal 9 keer de typer blokkade wordt uitgeschakeld en vervolgens het bericht "Lorem Ipsum" wordt getypt
	Then zijn de waarden voor alle stats gelijk aan level 10

Scenario: BONUS Easter Egg: Misbruik kwetsbaarheid uploader task resulteert in level 10 stats
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Mage"

	


#Unhappy
Scenario: U1 Clicker - Zo lang het totale aantal clicks op de Click it! button kleiner is dan 5, resulteert deze actie niet in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Brigadier"
	When de Click it! button 1 keer wordt ingedrukt
	Then verschijnt voor de task "clicker" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When de Click it! button 3 keer wordt ingedrukt
	Then verschijnt voor de task "clicker" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When de Click it! button 1 keer wordt ingedrukt
	Then verschijnt voor de task "clicker" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Scenario: U2 Uploader - Zo lang er geen bestand is geüpload, is er geen level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Mage"
	Then verschijnt voor de task "uploader" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When een bestand wordt geüpload
	Then verschijnt voor de task "uploader" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Scenario: U3 Typer - Zo lang het woord "Lorem Ipsum" niet is getypt, resulteert deze actie niet in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Knight"
	When het bericht "lorem Ipsum" wordt getypt
	Then verschijnt voor de task "typer" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When het bericht "Lorem ipsum" wordt getypt
	Then verschijnt voor de task "typer" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When het bericht "lorem ipsum" wordt getypt
	Then verschijnt voor de task "typer" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When het bericht "Lorem Ipsum" wordt getypt
	Then verschijnt voor de task "typer" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Scenario: U4 Slider - Zo lang de slider niet volledig naar rechts geschoven is, resulteert deze actie niet in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Thief"
	When de slider voor 20 procent naar rechts wordt geschoven
	Then verschijnt voor de task "slider" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When de slider voor 40 procent naar rechts wordt geschoven
	Then verschijnt voor de task "slider" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When de slider voor 30 procent naar rechts wordt geschoven
	Then verschijnt voor de task "slider" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When de slider voor 10 procent naar rechts wordt geschoven
	Then verschijnt voor de task "slider" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina