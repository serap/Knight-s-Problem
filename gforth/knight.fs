8 constant n
8 constant m

create board n m * cells allot
create neighbours 8 cells allot
create neighbour_precedence 5 , 4 , 2 , 6 , 8 , 3 , 1 , 7 ,
create possible_neighbours -17 , -15 , -10 , -6 , 6 , 10 , 15 , 17

: init_board ( -- )
	n m * 0 do -1 board I cells + ! loop ;

: board_move_from_to ( from_pos to_pos -- )
 	swap board swap cells + ! ;

: board_is_empty_pos ( pos -- t/f )
	board swap cells + @ -1 = ;

: board_completed ( -- t/f )
	n m * 0 do
		i board_is_empty_pos
			if
				false unloop exit
			endif
	loop
	true ;

: board_is_valid_pos ( pos -- t/f )
	dup 0 >=
	swap n m * <
	and ;

: board_get_line ( pos -- t/f )
	n / ;

: 3pick ( n0 n1 n2 n3 -- n0 n1 n2 n3 n0 )
	>r >r >r dup r> swap r> swap r> swap ;

: get_free_neighbour ( pos dx dy - neighbour t/f )
	-rot ( dy pos dx ) 0
	2over ( dy pos dx 0 dy pos )
	swap ( dy pos dx 0 pos dy ) 8
	* + + + ( dy pos neighbour )
	dup board_is_valid_pos invert if nip nip false exit endif
	dup board_get_line 3pick 3pick board_get_line + = invert if nip nip false exit endif
	dup board_is_empty_pos invert if nip nip false exit endif
	nip nip true ;

: get_free_neighbours_raw ( pos -- no_of_neighbours )
	0 \ no_of_neighbours

	over -1 -2 get_free_neighbour nip
	if 1 + endif

	over 1 -2 get_free_neighbour nip
	if 1 + endif

	over 2 -1 get_free_neighbour nip
	if 1 + endif

	over 2 1 get_free_neighbour nip
	if 1 + endif

	over 1 2 get_free_neighbour nip
	if 1 + endif

	over -1 2 get_free_neighbour nip
	if 1 + endif

	over -2 1 get_free_neighbour nip
	if 1 + endif

	over -2 -1 get_free_neighbour nip
	if 1 + endif

	nip ;

: get_free_neighbours ( pos -- no_of_neighbours )
	0 \ no_of_neighbours
	8 0 do -1 neighbours i cells + ! loop

	over -1 -2 get_free_neighbour
	if
		neighbours 0 cells + !
		1 +
	else
		drop
	endif

	over 1 -2 get_free_neighbour
	if
		neighbours 1 cells + !
		1 +
	else
		drop
	endif

	over 2 -1 get_free_neighbour
	if
		neighbours 2 cells + !
		1 +
	else
		drop
	endif

	over 2 1 get_free_neighbour
	if
		neighbours 3 cells + !
		1 +
	else
		drop
	endif

	over 1 2 get_free_neighbour
	if
		neighbours 4 cells + !
		1 +
	else
		drop
	endif

	over -1 2 get_free_neighbour
	if
		neighbours 5 cells + !
		1 +
	else
		drop
	endif

	over -2 1 get_free_neighbour
	if
		neighbours 6 cells + !
		1 +
	else
		drop
	endif

	over -2 -1 get_free_neighbour
	if
		neighbours 7 cells + !
		1 +
	else
		drop
	endif

	nip ;

: choose_best_neighbour ( -- best_neighbour )
	-1 ( best_neighbour = invalid position)
	n m * ( best_neighbour_neighbours = unreachable high value )

	8 0 do
		neighbour_precedence i cells + @ 1 - ( bn bnn n )
		neighbours swap cells + @ ( bn bnn neighbour )
		dup -1 <> if
			dup get_free_neighbours_raw ( bn bnn neighbour #n )
			dup 3 pick ( bn bnn neighbour #n #n bnn )
			< if ( bn bnn neighbour #n )
				2nip ( neighbour #n)
			else
				2drop ( bn bnn )
			endif
		else
			drop
		endif
	loop
	drop ;

: solve_from_pos ( pos -- success )
	init_board

	n m * 0 do
		dup						( pos pos )
		get_free_neighbours 0 = ( pos t/f )
		if leave endif			( pos )
		choose_best_neighbour	( pos best_neighbour )
		2dup					( pos best_neighbour pos best_neighbour)
		board_move_from_to		( pos best_neighbour )
		nip 					( best_neighbour )
	loop

	dup
	board_move_from_to
	
	board_completed ;

: display_row ( row -- )
    dup
    48 + emit ( print line no )
    n 0 do
	124 emit ( | )
	dup board swap m * i + cells + @ .
    loop
    124 emit
    cr
	drop ;

: display
    cr
    m 0 do
	i display_row
	loop
	cr cr ;

: solve_all
	0 ( number of positions where we found a solution )
	n m * 0 do
		dup i solve_from_pos
		if
			." Found a solution for startpoint " . cr
			1 +
		else
			." Didn't find a solution for startpoint " . cr
		endif
		display
	loop

	." Found a solution in " . ." out of " n m * . ." cases." cr ;

: solve_one ( pos -- )
	dup solve_from_pos
	if
		cr ." Found a solution for startpoint " . cr
	else
		cr ." Didn't find a solution for startpoint " . cr
	endif ;

: main
	solve_all
	\ 2 solve_one
	;

main
bye
