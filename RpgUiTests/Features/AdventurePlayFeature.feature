Feature: AdventurePlayFeature

	| Taak                | Succesactie                   | Bevestigingsbericht                 | Stats & level | Gerelateerde input field / knop | 
| - | ------------------- | ----------------------------- | ----------------------------------- | ------------- | ------------------------------- |
| 1 | Clicker             | 5 keer klikken                | *Great job! You levelled up*        | +1            | Disabled                        |
| 2 | Uploader            | 1 keer bestand uploaden       | *File selected, level up!*          | +1            | Disabled                        |
| 3 | Typer               | `"Lorem Ipsum"` typen         | *Dolar sit amet!*                   | +1            | Disabled                        |
| 4 | Slider              | Volledig naar rechts schuiven | *Slid to the next level!*           | +1            | Disabled                        |
| 5 | Alle taken voltooid | Bovenstaande vier acties      | *You've reached the highest level!* | n.v.t.        | n.v.t.                          |

- Nadat alle taken voltooit zijn, verschijnt de knop "PLay again". Klikken op deze knop brengt je terug naar de PreparePlayPage

**Aandachtspunten
- In feature file voor slider svp uitsluitend tientallen procenten opgeven als percentage, dus {0%, 10%, 20%, ..., 100%)
- Scenario U2 Uploader - Klikken op "bestand kiezen" en vervolgens klikken op "Annuleren" is niet te automatiseren. Daarom enkel checken of corfirmation message en level up bij aanvang niet aanwezig zijn.

*****BONUS Easter Egg*****
Via onderstaande tasks is het onbedoeld mogelijk misbruik te maken van een kwetsbaarheid in de code, waardoor je build maximale stats-waarden van 10 kunnen bereiken
1. Typer		-->	De uitschakeling van het element na het typen van "Lorem Ipsum" kan worden teruggedraaid. Door opnieuw "Lorem Ipsum" typen en dit proces te herhalen, kun je max stats bereiken.
2. Uploader		--> De uitschakeling van het element na het uploaden van de "cotton candy" file kan worden teruggedraaid. Door vervolgens de "rock in the ocean" te uploaden, 
					opnieuw de uitschakeling van het element na het uploaden terug te draaien en dit proces te herhalen, kun je max stats bereiken


				
	#Play again button

	#How to run plaatsen
	#Test zowel lokaal als tegen productie-URL kunnen runnen
	#level dynamisch

	#dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen voor specifieke build wellicht


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


Scenario Outline: H2 Een voltooiing van de task leidt tot bijbehorende confirmation message en verhoogt de waarde van stats en level met 1
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<Character>"
	When <Action>
	Then verschijnt voor de task "<Task>" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Examples:
	| Task     | Action                                                 | Character |
	| clicker  | de Click it! button 5 keer wordt ingedrukt             | Thief     |
	| uploader | het "cotton candy" bestand wordt geüpload              | Knight    |
	| typer    | het bericht "Lorem Ipsum" wordt getypt                 | Mage      |
	| slider   | de slider voor 100 procent naar rechts wordt geschoven | Brigadier |


Scenario Outline: H3 Een voltooiing van de task zorgt er voor dat het element van die task disabled raakt
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<Character>"
	Then is het element van de task "<Task>" enabled
	When <ActionToCompleteTask>
	Then is het element van de task "<Task>" disabled

Examples:
	| Task     | ActionToCompleteTask                                   | Character |
	| clicker  | de Click it! button 5 keer wordt ingedrukt             | Thief     |
	| uploader | het "cotton candy" bestand wordt geüpload              | Knight    |
	| typer    | het bericht "Lorem Ipsum" wordt getypt                 | Mage      |
	| slider   | de slider voor 100 procent naar rechts wordt geschoven | Brigadier |


Scenario: H4 De Click it! button start met de tekst 'Click me 5 times' en loopt per klik dynamisch terug van 5 naar 0
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Knight"
	Then loopt de dynamische teller in de click it button terug van 5 naar 0 bij elke klik


Scenario: H5 Het afronden van de vier tasks resulteert in een max level bevestigingsbericht, verhoogt de waarde van stats en level met 4 en een knop om opnieuw te spelen
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Brigadier"
	When de Click it! button 5 keer wordt ingedrukt
	And het "cotton candy" bestand wordt geüpload
	And het bericht "Lorem Ipsum" wordt getypt
	And de slider voor 100 procent naar rechts wordt geschoven
	Then verschijnt het max level bevestigingsbericht
	And zijn de waarden voor stats en level 4 hoger dan bij aanvang op de adventure play page pagina


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
	When het "cotton candy" bestand wordt geüpload
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

Scenario: U5 - Zo lang niet alle vier de taken zijn voltooid, is de Play again button niet zichtbaar, wel als het voltooid is
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Thief"
	Then is de "Play again" button niet zichtbaar
	When de slider voor 100 procent naar rechts wordt geschoven 
	Then is de "Play again" button niet zichtbaar
	When het "cotton candy" bestand wordt geüpload  
	Then is de "Play again" button niet zichtbaar
	When het bericht "Lorem Ipsum" wordt getypt
	Then is de "Play again" button niet zichtbaar
	When de Click it! button 5 keer wordt ingedrukt
	Then is de "Play again" button wel zichtbaar


Scenario: BONUS Easter Egg Typer: Misbruik kwetsbaarheid typer task resulteert in level 10 stats voor elke build
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<BuildType>"
	When het bericht "Lorem Ipsum" wordt getypt
	And in totaal <Aantal> keer de typer blokkade wordt uitgeschakeld en vervolgens het bericht "Lorem Ipsum" wordt getypt
	Then zijn de waarden voor alle stats gelijk aan level 10

Examples:
	| BuildType | Aantal |
	| Thief     |      8 |
	| Knight    |      8 |
	| Mage      |      9 |
	| Brigadier |      8 |


Scenario: BONUS Easter Egg Uploader: Misbruik kwetsbaarheid uploader task resulteert in level 10 stats voor elke build
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<BuildType>"
	When het "cotton candy" bestand wordt geüpload
	And gedurende <Aantal> iteraties wordt de uploaderblokkade uitgeschakeld met uploaden "rock in the ocean" gevolgd door "cotton candy" bestand
	Then zijn de waarden voor alle stats gelijk aan level 10

Examples:
	| BuildType | Aantal |
	| Thief     |      4 |
	| Knight    |      4 |
	| Mage      |      5 |
	| Brigadier |      4 |