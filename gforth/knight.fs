: n 8 ;
: m 8 ;
: s 24 ;

create board n m * cells allot
create neighbours 8 cells allot

: init_board ( -- )
	n m * 0 do -1 board I cells + ! loop
;

: board_move_from_to ( from_pos to_pos -- )
	\ XXX
;

: board_is_empty_pos ( pos -- t/f )
	\ XXX
	board swap cells + @ -1 <>
;

: board_completed ( -- t/f )
	1
	n m * 0 do
		i board_is_empty_pos *
	loop
	dup	0 > if
		negate
	endif
;

: board_is_valid_pos ( pos -- t/f )
	\ XXX
	dup 0 <
	swap n m + >=
	or
;

: board_get_line ( pos -- t/f )
	\ XXX
;

: get_free_neighbour ( pos i dx dy -- valid)
	\ XXX
;

: get_free_neighbours ( pos -- no_of_neighbours )
	\ XXX
;

: choose_best_neighbour ( -- best_neighbour )
	\ XXX
;

: solve_from_pos ( pos -- pos success )
	\ XXX
	init_board
	false
;

: solve_all
	\ XXX
;

: solve_one ( pos -- )
	solve_from_pos
	true = if
		cr ." Found a solution for startpoint " . cr
	else
		cr ." Didn't find a solution for startpoint " . cr
	endif
;

: display-row ( r -- )
    { r }
    48 r + emit
    n 0 do
	124 emit
	board i r m * + cells + @ -1 = if
	    32 emit
	else
	    42 emit
	endif
    loop
    124 emit
    cr
;

: display
    cr
    m 0 do
	i display-row
	loop
;

: main
	\ solve_all
	2 solve_one
;

main
bye
