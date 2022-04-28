grammar TimeExpression;

timeIntervals
	: always
	| never
	| today
	| nextDayOfWeek
	;

dateTimes
	: never
	| today
	| nextDayOfWeek
	;

today : 'aujourd\'hui' | 'aujourd hui' | 'ajd' ;
nextDayOfWeek : dayOfWeek NEXT?  ;

dayOfWeek : monday | tuesday | wednesday | thursday | friday | saturday | sunday ;

monday : MONDAY ;
tuesday : TUESDAY ;
wednesday : WEDNESDAY ;
thursday : THURSDAY ;
friday : FRIDAY ;
saturday : SATURDAY ;
sunday : SUNDAY ;

always : ALWAYS ;
never : NEVER ;

ALWAYS : 'toujours' | 'tjrs' | 'tj' ;
NEVER : 'jamais' | 'jam' | 'ja' ;
NEXT : 'prochain' | 'proc' | 'pro' ;

MONDAY : 'lundi' | 'lun' | 'lu' ;
TUESDAY : 'mardi' | 'mar' | 'ma' ;
WEDNESDAY : 'mercredi' | 'mer' | 'me' ;
THURSDAY : 'jeudi' | 'jeu' | 'je' ;
FRIDAY : 'vendredi' | 'ven' | 've' ;
SATURDAY : 'samedi' | 'sam' | 'sa' ;
SUNDAY : 'dimanche' | 'dim' | 'di' ;
