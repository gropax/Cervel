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
	: dayDateScoped SAUF dayIntvDist
	| dayDateScoped
	;

dayDateScoped
	: dayDateExpr DE monthIntvDist
	| dayDateExpr
	;

dayDateExpr
	: everyDay
	| dayOfWeekUnion
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
