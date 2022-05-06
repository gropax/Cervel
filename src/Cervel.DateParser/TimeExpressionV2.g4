grammar TimeExpressionV2;

intvDist
	: dayIntvDist
	;

dayIntvDist
	: dayDateDist
	;

dateDist
	: dayDateDist
	;

dayDateDist
	: dayDateSince
	;

dayDateSince
	: dayDateExpr since dayDateExpr
	| dayDateExpr
	;

since : A PARTIR DE ;

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
	: dayOfWeek (COMMA | ET)? dayOfWeekIter
	| dayOfWeek
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


LPAR : '(' ;
RPAR : ')' ;

CHAQUE : 'chaque' ;
JOUR : 'jour' | 'jours' | 'j' ;
TOUT : 'tout' | 'toute' | 'tous' | 'toutes' | 'tt' ;
LE : 'le' | 'la' | 'les' | 'l' | 'l\'' ;
A : 'a' | 'à' ;
DE : 'de' ;
PARTIR : 'partir' ;
COMPTER : 'compter' ;
DEPUIS : 'depuis' ;
