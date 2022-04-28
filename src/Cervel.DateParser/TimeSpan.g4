grammar TimeSpan;

timeSet
	: always
	| never
	;

always : ALWAYS ;
never : NEVER ;

ALWAYS : 'toujours' | 'tjrs' ;
NEVER : 'jamais' | 'jam' ;
