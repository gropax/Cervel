grammar TimeExpression;

timeIntervals
	: always
	| never
	| relativeToNow
	| nextDayOfWeek
	| everyDayOfWeek
	;

dateTimes
	: never
	| now
	| relativeToNow
	| nextDayOfWeek
	| everyDayOfWeek
	;

relativeToNow
	: nDaysAgo
	| dayBeforeYesterday
	| yesterday
	| today
	| tomorrow
	| dayAfterTomorrow
	| nDaysFromNow
	;

now : 'maintenant' | 'mnt' ;

nDaysAgo : IL_Y_A number JOUR ;
dayBeforeYesterday : AVANT '-'? HIER ;
yesterday : HIER ;
today : 'aujourd\'hui' | 'aujourd hui' | 'ajd' ;
tomorrow : DEMAIN ;
dayAfterTomorrow : APRES '-'? DEMAIN ;
nDaysFromNow : DANS number JOUR ;

number : NUMBER ;

IL_Y_A : 'il y a' | 'ya' ;
JOUR : 'jours' | 'jour' | 'j' ;
AVANT : 'avant' | 'av' ;
APRES : 'après' | 'aprés' | 'apres' | 'ap' ;
HIER : 'hier' ;
DEMAIN : 'demain' | 'dem' ;
DANS : 'dans' | 'ds' ;


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

NUMBER : [0-9]+ ;
