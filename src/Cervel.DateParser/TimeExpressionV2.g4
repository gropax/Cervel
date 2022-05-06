grammar TimeExpressionV2;

timeIntervals
	: always
	| never
	| dateIntervals
	;

dateIntervals
	: shiftedDate
	;

dateTimes
	: never
	| now
	| shiftedDate
	;

shiftedDate : shiftedDateIter ;
shiftedDateIter
	: shiftOperator shiftedDateIter
	| simpleDate
	;

simpleDate
	: relativeToNow
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

nDaysAgo : IL_Y_A number JOUR ;
dayBeforeYesterday : AVANT '-'? HIER ;
yesterday : HIER ;
today : 'aujourd\'hui' | 'aujourd hui' | 'ajd' ;
tomorrow : DEMAIN ;
dayAfterTomorrow : APRES '-'? DEMAIN ;
nDaysFromNow : DANS number JOUR ;

shiftOperator
	: nDaysBefore
	| twoDaysBefore
	| theDayBefore
	| theDayAfter
	| twoDaysAfter
	| nDaysAfter
	;

nDaysBefore : number JOUR AVANT ;
twoDaysBefore : LE AVANT VEILLE DE ;
theDayBefore : LE VEILLE DE ;
theDayAfter : LE LENDEMAIN DE ;
twoDaysAfter : LE SURLENDEMAIN DE ;
nDaysAfter : number JOUR APRES ;

now : 'maintenant' | 'mnt' ;

number : NUMBER ;

IL_Y_A : 'il y a' | 'ya' ;
JOUR : 'jours' | 'jour' | 'j' ;
AVANT : 'avant' | 'av' ;
APRES : 'après' | 'aprés' | 'apres' | 'ap' ;
HIER : 'hier' ;
DEMAIN : 'demain' | 'dem' ;
DANS : 'dans' | 'ds' ;
VEILLE : 'veille' | 'veil' ;
LENDEMAIN : 'lendemain' | 'lendem' | 'lend' ;
SURLENDEMAIN : 'surlendemain' | 'surlendem' | 'surlend' ;


nextDayOfWeek : dayOfWeek NEXT? ;

everyDayOfWeek
    : dayOfWeek DE CHAQUE SEMAINE
    | LE dayOfWeek
	| CHAQUE dayOfWeek
	| LE dayOfWeek DE? CHAQUE SEMAINE
    | TOUT LE dayOfWeek
	;


LE : 'le' | 'la' | 'l' | 'l\'' | 'les' ;

CHAQUE : 'chaque' | 'ch' ;
SEMAINE : 'semaine' | 'sem' | 'se' ;
TOUT : 'tout' | 'tous' | 'ts' | 'tt' ;
DE : 'de' | 'du' | 'd' | 'd\'' ;

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
