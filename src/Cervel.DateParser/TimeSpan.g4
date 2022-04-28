grammar TimeSpan;

timeSpans
	: always
	| never
	;

dateTimes
	: never
	;

always : ALWAYS ;
never : NEVER ;

ALWAYS : 'toujours' | 'tjrs' ;
NEVER : 'jamais' | 'jam' ;
