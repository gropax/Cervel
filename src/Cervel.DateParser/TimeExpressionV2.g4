grammar TimeExpressionV2;


dates  // Point d'entrée du parsing des dates
	: days
	| monthes
	//| years
	;

intervals  // Point d'entrée du parsing des intervalles
	: dayIntervals
	| monthIntervals
	//| yearIntervals
	;

dayIntervals : days ;
monthIntervals : monthes ;
//yearIntervals : years ;


// ------------------------------------------------------------
//                Dates express in terms of days
// ------------------------------------------------------------

days  // Appelée par `dayIntervals`
	: daysUntil
	;

daysUntil
	: daysSince until daysExpr
	| daysSince
	;

daysSince
	: daysExcept since daysExpr
	| daysExcept since monthesExpr
	| daysExcept
	;

daysExcept
	: daysScopedUnion SAUF dayIntervals
	| daysScopedUnion
	;

daysScopedUnion : daysScopedIter ;
daysScopedIter   // l'ordre des règles est inversé pour privilégier une interprétation locale de ET
	: daysScoped
	| daysScoped (COMMA | ET)? daysScopedIter
	;

daysScoped
	: daysSeq DE? monthIntervals
	| daysSeq
	;

daysSeq
	: nthDayUnion
	| daysExpr
	;

nthDayUnion : nthDayIter ;
nthDayIter
	: nthDayExpr (COMMA | ET)? nthDayIter
	| nthDayExpr
	;

nthDayExpr
	: LE? ordinal daysExpr
	;

ordinal
	: ordinal1
	| ordinal2
	| ordinal3
	| ordinal4
	| ordinal5
	;

daysExpr
	: everyDay
	| dayOfWeekUnion
	| dayOfMonthUnion
	| dayOfWeekOfMonthUnion
	;

everyDay
	: CHAQUE? JOUR
	| TOUT? LE JOUR
	;

dayOfWeekUnion : dayOfWeekIter ;
dayOfWeekIter
	: dayOfWeekExpr (COMMA | ET)? dayOfWeekIter
	| dayOfWeekExpr
	;

dayOfWeekExpr
	: CHAQUE? dayOfWeek
	| TOUT? LE dayOfWeek
	;

dayOfWeek
	: monday
	| tuesday
	| wednesday
	| thursday
	| friday
	| saturday
	| sunday
	;

monday : 'lundi' | 'lundis' | 'lun' | 'lu' ;
tuesday : 'mardi' | 'mardis' | 'mar' | 'ma' ;
wednesday : 'mercredi' | 'mercredis' | 'mer' | 'me' ;
thursday : 'jeudi' | 'jeudis' | 'jeu' | 'je' ;
friday : 'vendredi' | 'vendredis' | 'ven' | 've' ;
saturday : 'samedi' | 'samedis' | 'sam' | 'sa' ;
sunday : 'dimanche' | 'dimanches' | 'dim' | 'di' ;

dayOfMonthUnion : dayOfMonthIter ;
dayOfMonthIter
	: dayOfMonthExpr (COMMA | ET)? dayOfMonthIter
	| dayOfMonthExpr
	;

dayOfMonthExpr
	: CHAQUE? dayOfMonth
	| TOUT? LE dayOfMonth
	;

dayOfMonth
	: number1 | number2 | number3 | number4 | number5
	| number6 | number7 | number8 | number9 | number10
	| number11 | number12 | number13 | number14 | number15
	| number16 | number17 | number18 | number19 | number20
	| number20 | number21 | number22 | number23 | number24
	| number25 | number26 | number27 | number28 | number29
	| number30 | number31 ;

ordinal1 : '1er' | '1e' | 'premier' ;
ordinal2 : '2eme' | '2e' | 'deuxième' ;
ordinal3 : '3eme' | '3e' | 'troisième' ;
ordinal4 : '4eme' | '4e' | 'quatrième' ;
ordinal5 : '5eme' | '5e' | 'cinquième' ;

number1 : '1' | '1er' | '1e' | 'premier' ;
number2 : '2' | 'deux' ;
number3 : '3' | 'trois' ;
number4 : '4' | 'quatre' ;
number5 : '5' | 'cinq' ;
number6 : '6' | 'six' ;
number7 : '7' | 'sept' ;
number8 : '8' | 'huit' ;
number9 : '9' | 'neuf' ;
number10 : '10' | 'dix' ;
number11 : '11' | 'onze' ;
number12 : '12' | 'douze' ;
number13 : '13' | 'treize' ;
number14 : '14' | 'quatorze' ;
number15 : '15' | 'quinze' ;
number16 : '16' | 'seize' ;
number17 : '17' | 'dix sept' ;
number18 : '18' | 'dix huit' ;
number19 : '19' | 'dix neuf' ;
number20 : '20' | 'vingt' ;
number21 : '21' | 'vingt et un' ;
number22 : '22' | 'vingt deux' ;
number23 : '23' | 'vingt trois' ;
number24 : '24' | 'vingt quatre' ;
number25 : '25' | 'vingt cinq' ;
number26 : '26' | 'vingt six' ;
number27 : '27' | 'vingt sept' ;
number28 : '28' | 'vingt huit' ;
number29 : '29' | 'vingt neuf' ;
number30 : '30' | 'trente' ;
number31 : '31' | 'trente et un' ;

dayOfWeekOfMonthUnion : dayOfWeekOfMonthIter ;
dayOfWeekOfMonthIter
	: dayOfWeekOfMonthExpr (COMMA | ET)? dayOfWeekOfMonthIter
	| dayOfWeekOfMonthExpr
	;

dayOfWeekOfMonthExpr
	: CHAQUE? dayOfWeek dayOfMonth
	| TOUT? LE dayOfWeek dayOfMonth
	;


// ------------------------------------------------------------
//             Dates express in terms of monthes
// ------------------------------------------------------------

monthes  // Appelée par `monthIntervals`
	: monthesSince
	;

monthesSince
	: monthesExpr since monthesUntil
	| monthesUntil
	;

monthesUntil
	: monthesExpr until monthesExpr
	| monthesExpr
	;

monthesExpr
	: everyMonth
	| monthNameUnion
	;

everyMonth
	: CHAQUE? MOIS
	| TOUT? LE MOIS
	;

monthNameUnion : monthNameIter ;
monthNameIter
	: monthName (COMMA | ET)? monthNameIter
	| monthName
	;

monthName
	: january
	| february
	| march
	| april
	| may
	| june
	| july
	| august
	| september
	| october
	| november
	| december
	;

january : 'janvier' ;
february : 'fevrier' ;
march : 'mars' ;
april : 'avril' ;
may : 'mai' ;
june : 'juin' ;
july : 'juillet' ;
august : 'août' ;
september : 'septembre' ;
october : 'octobre' ;
november : 'novembre' ;
december : 'decembre' ;



until : JUSQUE A ;
since : A PARTIR DE ;


LPAR : '(' ;
RPAR : ')' ;

SAUF : 'sauf' | 'excepte' ;
CHAQUE : 'chaque' ;
JOUR : 'jour' | 'jours' | 'j' ;
MOIS : 'mois' ;
TOUT : 'tout' | 'toute' | 'tous' | 'toutes' | 'tt' ;
LE : 'le' | 'la' | 'les' | 'l' | 'l\'' ;
A : 'a' | 'à' ;
DE : 'de' | 'd\'' | 'd' ;
PARTIR : 'partir' ;
COMPTER : 'compter' ;
DEPUIS : 'depuis' ;
JUSQUE : 'jusque' | 'jusqu' | 'jusqu\'' ;
