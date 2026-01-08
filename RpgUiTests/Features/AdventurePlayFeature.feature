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
	#dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen voor specifieke build wellicht

#Happy flows
Scenario: H1 De getoonde stats op de prepare play page komen overeen met de getoonde stats op de adventure play page voor elke build
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "<BuildType>"
	Then komen de getoonde stats op de adventure page overeen met de getoonde stats van de prepare page

Examples:
	| BuildType |
	| Thief     |
	| Knight    |
	| Mage      |
	| Brigadier |

Scenario: H2 Vijf keer klikken op de Click it! button resulteert in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Thief"
	When de Click it! button 5 keer wordt ingedrukt
	Then verschijnt voor de task "clicker" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Scenario: H3 Het uploaden van een bestand resulteert in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Knight"
	When een bestand wordt geüpload
	Then verschijnt voor de task "typer" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Scenario: H4 Het typen van een juist bericht resulteert in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Mage"
	When het bericht "Lorem Ipsum" wordt getypt
	Then verschijnt voor de task "mage" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Scenario: H5 Het volledig naar rechts schuiven van de slider resulteert in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Brigadier"
	When de slider voor 100 procent naar rechts wordt geschoven
	Then verschijnt voor de task "slider" het bijbehorende bevestigingsbericht
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina

Scenario Outline: Voltooide acties per task leiden tot bijbehorende confirmation message en level up
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

#Unhappy
Scenario: U1 Zo lang het totale aantal clicks op de Click it! button kleiner is dan 5, resulteert deze actie niet in level up een bevestigingsbericht voor deze task
	Given dat ik de juiste handelingen heb verricht om op de play adventure pagina terecht te komen met character build type "Thief"
	When de Click it! button 1 keer wordt ingedrukt
	Then verschijnt voor de task "clicker" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When de Click it! button 3 keer wordt ingedrukt
	Then verschijnt voor de task "clicker" geen bevestigingsbericht
	And zijn de waarden voor stats en level 0 hoger dan bij aanvang op de adventure play page pagina
	When de Click it! button 1 keer wordt ingedrukt
	Then verschijnt voor de task "clicker" het bevestigingsbericht "Great job! You levelled up"
	And zijn de waarden voor stats en level 1 hoger dan bij aanvang op de adventure play page pagina



