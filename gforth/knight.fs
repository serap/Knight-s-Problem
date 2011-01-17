: n 8 ;
: m 8 ;
: s 24 ;

create board n m * cells allot

: init n m * 0 do -1 board I cells + ! loop ;
init

: pos_is_visited ( position -- true/false )
    board swap cells + @ -1 <> ;

: pos_is_oor ( position -- true/false )
    dup 0 <
    swap n m + >=
    or ;



: foo create board 8 0 do 0 44 POSTPONE emit loop ; immediate


    
see foo 

: display-row ( r -- )
    { r }
    48 r + emit
    n 0 do
	124 emit
	board i cells + @ r = if
	    42 emit
	else
	    32 emit
	endif
    loop
    124 emit
    cr ;

: display
    cr
    m 0 do
	i display-row
	loop ;

: mysquare ( n -- n^2 )
    dup * ;

: myinc ( n -- n )
    1 + ;

: mycalc ( -- n )
    6 7 8 * - 9 + ;

: newcalc  3 negate 4 * negate 5 - ;

: mycalc2 7 3 mod ;
: mycalc3 7 3 / ;
: mycalc4 7 3 /mod ;

: myfib ( n n -- n n n)
    over over + ;

: mynewcalc ( n n -- n )
    over swap - swap 1 + * ;

: mymult ( n n -- n)
    * ;

: mysquared ( n -- n^2 )
    dup * ;

: mycubed ( n -- n^3 )
    dup mysquared * ;

: myforth-power ( n -- n^4 )
    mysquared mysquared ;

: mynip ( x y z -- x z )
    swap drop ;

: mytuck ( x y z -- x z y z )
    swap over ;

: mynegate ( x -- -x )
    -1 * ;

: my/mod ( x y -- x y mod x y /)
    2dup mod rot rot / ;

