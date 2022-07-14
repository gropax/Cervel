grammar TimeExpressionV2;


time : intervals ;  // Parsing entry point

intervals
	: days
	| months
	| years
	;

// ------------------------------------------------------------
//                Dates express in terms of days
// ------------------------------------------------------------

days
	: daysUntil
	;

daysUntil
	: daysSince until intervals
	| daysSince
	;

daysSince
	: daysExcept since intervals
	| daysExcept
	;

daysExcept
	: daysScopedUnion SAUF intervals
	| daysScopedUnion
	;

daysScopedUnion : daysScopedIter ;
daysScopedIter   // l'ordre des règles est inversé pour privilégier une interprétation locale de ET
	: daysScoped
	| daysScoped (COMMA | ET)? daysScopedIter
	;

daysScoped
    : daysNEveryM DE? months
    | daysNEveryM
    ;

daysNEveryM
    : number daysSeq SUR number
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
	: ordinal1
	| number
	;

ordinal1 : '1er' | '1e' | 'premier' ;
ordinal2 : '2eme' | '2e' | 'deuxième' ;
ordinal3 : '3eme' | '3e' | 'troisième' ;
ordinal4 : '4eme' | '4e' | 'quatrième' ;
ordinal5 : '5eme' | '5e' | 'cinquième' ;

number
	: numberInDigits
	| number1 | number2 | number3 | number4 | number5
	| number6 | number7 | number8 | number9 | number10
	| number11 | number12 | number13 | number14 | number15
	| number16 | number17 | number18 | number19 | number20
	| number20 | number21 | number22 | number23 | number24
	| number25 | number26 | number27 | number28 | number29
	| number30 | number31 ;

numberInDigits : NUMBER ; 

number1 : 'un' ;
number2 : 'deux' ;
number3 : 'trois' ;
number4 : 'quatre' ;
number5 : 'cinq' ;
number6 : 'six' ;
number7 : 'sept' ;
number8 : 'huit' ;
number9 : 'neuf' ;
number10 : 'dix' ;
number11 : 'onze' ;
number12 : 'douze' ;
number13 : 'treize' ;
number14 : 'quatorze' ;
number15 : 'quinze' ;
number16 : 'seize' ;
number17 : 'dix sept' ;
number18 : 'dix huit' ;
number19 : 'dix neuf' ;
number20 : 'vingt' ;
number21 : 'vingt et un' ;
number22 : 'vingt deux' ;
number23 : 'vingt trois' ;
number24 : 'vingt quatre' ;
number25 : 'vingt cinq' ;
number26 : 'vingt six' ;
number27 : 'vingt sept' ;
number28 : 'vingt huit' ;
number29 : 'vingt neuf' ;
number30 : 'trente' ;
number31 : 'trente et un' ;

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
//             Dates express in terms of months
// ------------------------------------------------------------

months
	: monthsSince
	;

monthsSince
	: monthsExpr since monthsUntil
	| monthsUntil
	;

monthsUntil
	: monthsExpr until monthsExpr
	| monthsExpr
	;

monthsExpr
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



// ------------------------------------------------------------
//             Dates express in terms of years
// ------------------------------------------------------------

years
	: yearsSince
	;

yearsSince
	: yearsExpr since yearsUntil
	| yearsUntil
	;

yearsUntil
	: yearsExpr until yearsExpr
	| yearsExpr
	;

yearsExpr
	: everyYear
	//| yearNameUnion
	;

everyYear
	: CHAQUE? ANNEE
	| TOUT? LE ANNEE
	;

//yearNameUnion : yearNameIter ;
//yearNameIter
//	: yearName (COMMA | ET)? yearNameIter
//	| yearName
//	;



until : JUSQUE A ;
since : A PARTIR DE ;


LPAR : '(' ;
RPAR : ')' ;

//ORDINAL : ('0' .. '9')+ ('er' | 'eme' | 'e') ;
NUMBER : ('0' .. '9')+ ;

SAUF : 'sauf' | 'excepte' ;
CHAQUE : 'chaque' ;
JOUR : 'jour' | 'jours' | 'j' ;
MOIS : 'mois' ;
ANNEE : 'an' | 'ans' | 'annee' | 'annees' ;
TOUT : 'tout' | 'toute' | 'tous' | 'toutes' | 'tt' ;
LE : 'le' | 'la' | 'les' | 'l' | 'l\'' ;
A : 'a' | 'à' ;
DE : 'de' | 'd\'' | 'd' ;
PARTIR : 'partir' ;
COMPTER : 'compter' ;
DEPUIS : 'depuis' ;
JUSQUE : 'jusque' | 'jusqu' | 'jusqu\'' ;
SUR : 'sur' ;
