\ Knight's problem solver
\
\ The following parameters are adjustable
\ n ... length of board
\ m ... width of board
\ neighbour_precendence ... precedence which defines which neighbour
\   will be choosen if, there is more than one posibility

\ Here are the numbers of the possible neighbours:
\ +-+-+-+-+-+
\ | |1| |2| |
\ +-+-+-+-+-+
\ |8| | | |3|
\ +-+-+-+-+-+
\ | | |*| | |
\ +-+-+-+-+-+
\ |7| | | |4|
\ +-+-+-+-+-+
\ | |6| |5| |
\ +-+-+-+-+-+

8 constant n
8 constant m
create neighbour_precedence 5 , 4 , 2 , 6 , 8 , 3 , 1 , 7 ,

create board n m * cells allot
create neighbours 8 cells allot
create possible_neighbours_dx -1 , 1 , 2 , 2 , 1 , -1 , -2 , -2 ,
create possible_neighbours_dy -2 , -2 , -1 , 1 , 2 , 2 , 1 , -1 ,

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

: calc_neighbour_pos ( neighbour_id -- dx dy )
	dup
	possible_neighbours_dx swap cells + @
	swap
	possible_neighbours_dy swap cells + @ ;

: get_free_neighbour ( pos dx dy - neighbour t/f )
	rot ( dx dy pos )
	2dup swap >r >r ( dx dy pos )
	swap n ( dx pos dy n )
	* + + ( neighbour )
	dup board_is_valid_pos invert if r> r> 2drop false exit endif
	dup board_get_line r> board_get_line r> + = invert if false exit endif
	dup board_is_empty_pos invert if false exit endif
	true ;

: get_free_neighbours_raw ( pos -- no_of_neighbours )
	0 \ no_of_neighbours

	8 0 do
		over i calc_neighbour_pos get_free_neighbour nip
		if 1 + endif
	loop
	nip ;

: get_free_neighbours ( pos -- no_of_neighbours )
	0 \ no_of_neighbours
	8 0 do -1 neighbours i cells + ! loop

	8 0 do
		over i calc_neighbour_pos get_free_neighbour
		if
			neighbours i cells + !
			1 +
		else
			drop
		endif
	loop
	nip ;

: choose_best_neighbour ( -- best_neighbour )
	-1 ( best_neighbour = invalid position)
	n m * ( best_neighbour_neighbours = unreachable high value )

	8 0 do
		neighbour_precedence i cells + @ 1 - ( bn bnn n )
		neighbours swap cells + @ ( bn bnn neighbour )
		dup -1 <> if
			-rot 2>r ( neighbour | bn bnn )
			dup get_free_neighbours_raw ( neighbour #n | bn bnn )
			dup r@ ( neighbour #n bnn | bn bnn )
			< if ( neighbour #n | bn bnn)
				2rdrop ( neighbour #n)
			else
				2drop 2r> ( bn bnn )
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
