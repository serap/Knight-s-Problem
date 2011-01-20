: n 8 ;
: m 8 ;
: s 24 ;

create board n m * cells allot
create neighbours 8 cells allot
create neighbour_precedence 5 , 4 , 2 , 6 , 8 , 3 , 1 , 7 ,

: init_board ( -- )
	n m * 0 do -1 board I cells + ! loop
;

: board_move_from_to ( from_pos to_pos -- )
	\ XXX board[from_pos] = to_pos;
	swap board swap cells + !
;

: board_is_empty_pos ( pos -- t/f )
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
	dup 0 >=
	swap n m * <
	and
;

: board_get_line ( pos -- t/f )
	n /
;

: get_free_neighbour_raw ( pos dx dy - neighbour t/f )
	-rot ( dy pos dx ) 0
	2 over ( dy pos dx 0 dy pos )
	swap ( dy pos dy 0 pos dy ) 8
	* + + + ( dy pos neighbour )
	dup board_is_valid_pos invert if false exit endif
	dup board_get_line 3 pick 3 pick board_get_line + = invert if false exit endif
	dup board_is_empty_pos invert if false exit endif
	true
;

: get_free_neighbour ( i pos dx dy -- t/f)
	8 0 do -1 board I cells + ! loop
	get_free_neighbour_raw ( neighbour t/f )
	if
		swap ( neighbour i )
		neighbours swap ( neighbour neighbours i )
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
	8 0 do
	\ XXX
	loop
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

: display-row ( r -- )
    { r }
    48 r + emit
    n 0 do
	124 emit
	board i r m * + cells + @ 48 + emit
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
