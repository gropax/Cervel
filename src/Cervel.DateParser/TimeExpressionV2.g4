grammar TimeExpressionV2;


intvDist  // Point d'entrée du parsing des intervalles
	: dayIntvDist
	| monthIntvDist
	;

dayIntvDist
	: dayDateDist
	;

monthIntvDist
	: monthDateDist
	;


dateDist  // Point d'entrée du parsing des dates
	: dayDateDist
	| monthDateDist
	;


// ------------------------------------------------------------
//                Dates express in terms of days
// ------------------------------------------------------------

dayDateDist  // Appelée par `dayIntvDist`
	: dayDateUntil
	;

dayDateUntil
	: dayDateSince until dayDateExpr
	| dayDateSince
	;

dayDateSince
	: dayDateExcept since dayDateExpr
	| dayDateExcept since monthDateExpr
	| dayDateExcept
	;

dayDateExcept
	: dayDateScopedUnion SAUF dayIntvDist
	| dayDateScopedUnion
	;

dayDateScopedUnion : dayDateScopedIter ;
dayDateScopedIter   // l'ordre des règles est inverser pour privilégier une interprétation locale de ET
	: dayDateScoped
	| dayDateScoped (COMMA | ET)? dayDateScopedIter
	;

dayDateScoped
	: dayDateExpr DE? monthIntvDist
	| dayDateExpr
	;

dayDateExpr
	: everyDay
	| dayOfWeekUnion
	| dayOfMonthInMonth
	| dayOfWeekOfMonthInMonth
	;

everyDay
	: CHAQUE? JOUR
	| TOUT LE JOUR
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

dayOfMonthInMonth
	: dayOfMonthUnion DE? monthDateExpr
	| dayOfMonthUnion
	;

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

dayOfWeekOfMonthInMonth
	: dayOfWeekOfMonthUnion DE? monthDateExpr
	| dayOfWeekOfMonthUnion
	;

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

monthDateDist  // Appelée par `monthIntvDist`
	: monthDateSince
	;

monthDateSince
	: monthDateExpr since monthDateUntil
	| monthDateUntil
	;

monthDateUntil
	: monthDateExpr until monthDateExpr
	| monthDateExpr
	;

monthDateExpr
	: everyMonth
	| monthNameUnion
	;

everyMonth
	: CHAQUE? MOIS
	| TOUT LE MOIS
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
february : 'février' ;
march : 'mars' ;
april : 'avril' ;
may : 'mai' ;
june : 'juin' ;
july : 'juillet' ;
august : 'août' ;
september : 'septembre' ;
october : 'octobre' ;
november : 'novembre' ;
december : 'décembre' ;



until : JUSQUE A ;
since : A PARTIR DE ;


LPAR : '(' ;
RPAR : ')' ;

SAUF : 'sauf' | 'excepté' ;
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
