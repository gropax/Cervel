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
	: dayDateParen
	;

dayDateParen
	: LPAR dayDateParen RPAR
	| dayDateExpr
	;

dayDateExpr
	: dayOfWeekUnion
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
