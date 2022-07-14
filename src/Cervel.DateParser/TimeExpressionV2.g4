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
	: daysScopedUnion except intervals
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
	: ordinalUnion daysExpr
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
	: dayOfMonthExpr (COMMA | ET) dayOfMonthIter
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


ordinalUnion
	: LE? ordinal (COMMA | ET) ordinalUnion
	| LE? ordinal
	;

ordinal
	: ordinalInDigits
	| ordinal1 | ordinal2 | ordinal3 | ordinal4 | ordinal5
	| ordinal6 | ordinal7 | ordinal8 | ordinal9 | ordinal10
	| ordinal11 | ordinal12 | ordinal13 | ordinal14 | ordinal15
	| ordinal16 | ordinal17 | ordinal18 | ordinal19 | ordinal20
	| ordinal20 | ordinal21 | ordinal22 | ordinal23 | ordinal24
	| ordinal25 | ordinal26 | ordinal27 | ordinal28 | ordinal29
	| ordinal30 | ordinal31
	;

ordinalInDigits :  ORDINAL ;

ordinal1 : ORDINAL1 | '1er' ;  // dirty fix for dayOfMonth
ordinal2 : ORDINAL2 ;
ordinal3 : ORDINAL3 ;
ordinal4 : ORDINAL4 ;
ordinal5 : ORDINAL5 ;
ordinal6 : ORDINAL6 ;
ordinal7 : ORDINAL7 ;
ordinal8 : ORDINAL8 ;
ordinal9 : ORDINAL9 ;
ordinal10 : ORDINAL10 ;
ordinal11 : ORDINAL11 ;
ordinal12 : ORDINAL12 ;
ordinal13 : ORDINAL13 ;
ordinal14 : ORDINAL14 ;
ordinal15 : ORDINAL15 ;
ordinal16 : ORDINAL16 ;
ordinal17 : NUMBER10 ORDINAL7 ;
ordinal18 : NUMBER10 ORDINAL8 ;
ordinal19 : NUMBER10 ORDINAL9 ;
ordinal20 : ORDINAL20 ;
ordinal21 : NUMBER20 ET? UNIEME ;
ordinal22 : NUMBER20 ORDINAL2 ;
ordinal23 : NUMBER20 ORDINAL3 ;
ordinal24 : NUMBER20 ORDINAL4 ;
ordinal25 : NUMBER20 ORDINAL5 ;
ordinal26 : NUMBER20 ORDINAL6 ;
ordinal27 : NUMBER20 ORDINAL7 ;
ordinal28 : NUMBER20 ORDINAL8 ;
ordinal29 : NUMBER20 ORDINAL9 ;
ordinal30 : ORDINAL30 ;
ordinal31 : NUMBER30 ET? UNIEME ;

number
	: numberInDigits
	| number1 | number2 | number3 | number4 | number5
	| number6 | number7 | number8 | number9 | number17
	| number18 | number19 | number10 | number11
	| number12 | number13 | number14 | number15 | number16 
	| number21 | number22 | number23 | number24 | number25
	| number26 | number27 | number28 | number29 | number20 
	| number31 | number30 ;

numberInDigits : {_input.Lt(1).Text.Length <= 2}? NUMBER ;

number1 : NUMBER1 ;
number2 : NUMBER2 ;
number3 : NUMBER3 ;
number4 : NUMBER4 ;
number5 : NUMBER5 ;
number6 : NUMBER6 ;
number7 : NUMBER7 ;
number8 : NUMBER8 ;
number9 : NUMBER9 ;
number10 : NUMBER10 ;
number11 : NUMBER11 ;
number12 : NUMBER12 ;
number13 : NUMBER13 ;
number14 : NUMBER14 ;
number15 : NUMBER15 ;
number16 : NUMBER16 ;
number17 : NUMBER10 NUMBER7 ;
number18 : NUMBER10 NUMBER8 ;
number19 : NUMBER10 NUMBER9 ;
number20 : NUMBER20 ;
number21 : NUMBER21 ;
number22 : NUMBER20 NUMBER2 ;
number23 : NUMBER20 NUMBER3 ;
number24 : NUMBER20 NUMBER4 ;
number25 : NUMBER20 NUMBER5 ;
number26 : NUMBER20 NUMBER6 ;
number27 : NUMBER20 NUMBER7 ;
number28 : NUMBER20 NUMBER8 ;
number29 : NUMBER20 NUMBER9 ;
number30 : NUMBER30 ;
number31 : NUMBER31 ;

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
	: yearsUntil
	;

yearsUntil
	: yearsSince until years
	| yearsSince
	;

yearsSince
	: yearsExcept since years
	| yearsExcept
	;

yearsExcept
	: yearsSeq except years
	| yearsSeq
	;

yearsSeq
	: yearsNEveryM
	| nthYearUnion
	| yearsExpr
	;

yearsNEveryM
    : number yearsExpr SUR number
    ;

nthYearUnion : nthYearIter ;
nthYearIter
	: nthYearExpr (COMMA | ET)? nthYearIter
	| nthYearExpr
	;

nthYearExpr
	: ordinalUnion yearsExpr
	;

yearsExpr
	: everyYear
	| yearNameUnion
	;

everyYear
	: CHAQUE? ANNEE
	| TOUT? LE ANNEE
	;

yearNameUnion : yearNameIter ;
yearNameIter
	: yearByName (COMMA | ET)? yearNameIter
	| yearByName
	;

yearByName
	: EN? yearName
	| LE? ANNEE yearName
	;

yearName : {_input.Lt(1).Text.Length == 4}? NUMBER ;


until : 'jusqu a' | 'jusqu en' ;
since : 'depuis' | 'a partir de' | 'a compter de' | 'des' ;
except : 'sauf' | 'excepte' | 'hors' | 'hormis' | 'a part' | 'a l exception de' | 'a l exclusion de' | 'en dehors de' ;


LPAR : '(' ;
RPAR : ')' ;

ORDINAL : ('0' .. '9')+ ('er' | 'ere' | 're' | 'eme' | 'me' | 'e') ;
NUMBER : ('0' .. '9')+ ;

UNIEME : 'unieme' 's'? ;
ORDINAL1 : 'premier' 'e'? 's'? ;
ORDINAL2 : 'deuxieme' 's'? ;
ORDINAL3 : 'troisieme' 's'? ;
ORDINAL4 : 'quatrieme' 's'? ;
ORDINAL5 : 'cinquieme' 's'? ;
ORDINAL6 : 'sixieme' 's'? ;
ORDINAL7 : 'septieme' 's'? ;
ORDINAL8 : 'huitieme' 's'? ;
ORDINAL9 : 'neuvieme' 's'? ;
ORDINAL10 : 'dixieme' 's'? ;
ORDINAL11 : 'onzieme' 's'? ;
ORDINAL12 : 'douzieme' 's'? ;
ORDINAL13 : 'treizieme' 's'? ;
ORDINAL14 : 'quatorzieme' 's'? ;
ORDINAL15 : 'quinzieme' 's'? ;
ORDINAL16 : 'seizieme' 's'? ;
ORDINAL20 : 'vingtieme' 's'? ;
ORDINAL30 : 'trentieme' 's'? ;

NUMBER1 : 'un' ;
NUMBER2 : 'deux' ;
NUMBER3 : 'trois' ;
NUMBER4 : 'quatre' ;
NUMBER5 : 'cinq' ;
NUMBER6 : 'six' ;
NUMBER7 : 'sept' ;
NUMBER8 : 'huit' ;
NUMBER9 : 'neuf' ;
NUMBER10 : 'dix' ;
NUMBER11 : 'onze' ;
NUMBER12 : 'douze' ;
NUMBER13 : 'treize' ;
NUMBER14 : 'quatorze' ;
NUMBER15 : 'quinze' ;
NUMBER16 : 'seize' ;
NUMBER20 : 'vingt' ;
NUMBER21 : 'vingt et un' ;
NUMBER30 : 'trente' ;
NUMBER31 : 'trente et un' ;

CHAQUE : 'chaque' ;
TOUT : 'tout' | 'toute' | 'tous' | 'toutes' | 'tt' ;

JOUR : 'jour' | 'jours' | 'j' ;
MOIS : 'mois' ;
ANNEE : 'an' | 'ans' | 'annee' | 'annees' ;

LE : 'le' | 'la' | 'les' | 'l' | 'l\'' ;
A : 'a' | 'à' ;
DE : 'de' | 'd\'' | 'd' ;
SUR : 'sur' ;
EN : 'en' ;
ET : 'et' ;
COMMA : ',' ;
