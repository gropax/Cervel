grammar TimeExpression;

timeIntervals
	: always
	| never
	;

dateTimes
	: never
	;

always : ALWAYS ;
never : NEVER ;
test : 'test' ;

ALWAYS : 'toujours' | 'tjrs' ;
NEVER : 'jamais' | 'jam' ;
