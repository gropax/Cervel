grammar TimeExpression;

timeIntervals
	: always
	| never
	| today
	| nextDayOfWeek
	| everyDayOfWeek
	;

dateTimes
	: never
	| now
	| today
	| yesterday
	| tomorrow
	| nextDayOfWeek
	| everyDayOfWeek
	;

now : 'maintenant' | 'mnt' ;
today : 'aujourd\'hui' | 'aujourd hui' | 'ajd' ;
yesterday : 'hier' ;
tomorrow : 'demain' | 'dem' ;

nextDayOfWeek : dayOfWeek NEXT? ;

everyDayOfWeek
    : dayOfWeek DE CHAQUE SEMAINE
    | LES dayOfWeek
	| CHAQUE dayOfWeek
	| LES dayOfWeek DE? CHAQUE SEMAINE
    | TOUT LES dayOfWeek
	;

LES : 'les' | 'le' | 'ls' ;
CHAQUE : 'chaque' | 'ch' ;
SEMAINE : 'semaine' | 'sem' | 'se' ;
TOUT : 'tout' | 'tous' | 'ts' | 'tt' ;
DE : 'de' | 'du' ;

dayOfWeek : monday | tuesday | wednesday | thursday | friday | saturday | sunday ;

monday : 'lundi' | 'lundis' | 'lun' | 'lu' ;
tuesday : 'mardi' | 'mardis' | 'mar' | 'ma' ;
wednesday : 'mercredi' | 'mercredis' | 'mer' | 'me' ;
thursday : 'jeudi' | 'jeudis' | 'jeu' | 'je' ;
friday : 'vendredi' | 'vendredis' | 'ven' | 've' ;
saturday : 'samedi' | 'samedis' | 'sam' | 'sa' ;
sunday : 'dimanche' | 'dimanches' | 'dim' | 'di' ;

always : 'toujours' | 'tjrs' | 'tj' ;
never : 'jamais' | 'jam' | 'ja' ;

NEXT : 'prochain' | 'proc' | 'pro' ;
