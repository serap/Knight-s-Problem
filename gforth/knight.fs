: n 8 ;
: m 8 ;
: s 24 ;

create board n m * cells allot
create neighbours 8 cells allot

: init_board ( -- )
	n m * 0 do -1 board I cells + ! loop
;

: board_completed ( -- t/f )
	\ XXX
;

: board_move_from_to ( from_pos to_pos -- )
	\ XXX
;

: board_is_empty_pos ( pos -- t/f )
	\ XXX
	board swap cells + @ -1 <>
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

: get_free_neighbour_raw ( pos dx dy - neighbour t/f )
	-rot ( dy pos dx ) 0
	2 over ( dy pos dx 0 dy pos )
	swap ( dy pos dy 0 pos dy ) 8
	* + + + ( dy pos neighbour )
	dup board_is_valid_pos invert if false exit
	dup board_get_line 3 pick 3 pick board_get_line + = invert if false exit
	dup board_is_empty_pos invert if false exit
	true
	exit
;

: get_free_neighbour ( i pos dx dy -- t/f)
	8 0 do -1 board I cells + ! loop
	get_free_neighbour_raw ( neighbour t/f )
	if
		swap ( neighbour i )
		neighbours swap ( neighbour neighbours i)
		cells + !
		true
	else
		drop false
	endif
;

: get_free_neighbours ( pos -- no_of_neighbours )
	0 \ no_of_neighbours

	over 0 swap -1 -2 get_free_neighbour
	if 1 + endif

	over 1 swap 1 -2 get_free_neighbour
	if 1 + endif

	over 2 swap 2 -1 get_free_neighbour
	if 1 + endif

	over 3 swap 2 1 get_free_neighbour
	if 1 + endif

	over 4 swap 1 2 get_free_neighbour
	if 1 + endif

	over 5 swap -1 2 get_free_neighbour
	if 1 + endif

	over 6 swap -2 1 get_free_neighbour
	if 1 + endif

	over 7 swap -2 -1 get_free_neighbour
	if 1 + endif
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
	if
		cr ." Found a solution for startpoint " . cr
	else
		cr ." Didn't find a solution for startpoint " . cr
	endif
;

: main
	\ solve_all
	2 solve_one
;

main
bye
