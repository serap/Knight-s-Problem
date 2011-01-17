#include <stdio.h>
#include <stdint.h>
#include <string.h>

#define BOARD_N 8 /* length of line */
#define BOARD_M 8 /* no of lines */
#define NO_OF_NEIGHBOURS 8

typedef int neighbours_t[NO_OF_NEIGHBOURS];

void init_board(int board[])
{
	int i;
	for (i=0; i<BOARD_N*BOARD_M; i++) {
		board[i] = -1;
	}
}

int board_completed(int board[])
{
	int i;
	for (i=0; i<BOARD_N*BOARD_M; i++) {
		if (board[i] == -1)
			return -1;
	}
	return 0;
}

void move_from_to(int board[], int from_pos, int to_pos)
{
	board[from_pos] = to_pos;
}

int board_is_empty_pos(int board[], int pos)
{
	if (board[pos] == -1)
		return 0;

	return -1;
}

int board_is_valid_pos(int board[], int pos)
{
	if (pos < 0 || pos >= BOARD_N*BOARD_M)
		return -1;

	return 0;
}

int board_get_line(int pos)
{
	return pos / BOARD_N;
}

int get_free_neighbour(int board[], int pos, neighbours_t neighbours, int i, int dx, int dy)
{
	int neighbour;

	if (neighbours != NULL) {
		neighbours[i] = -1;
	}
	neighbour = pos + (dx) + (dy) * 8; // <dx,dy>
	if (board_is_valid_pos(board, neighbour) == 0 && board_get_line(pos) + dy == board_get_line(neighbour) && board_is_empty_pos(board, neighbour) == 0) {
		if (neighbours != NULL) {
			neighbours[i] = neighbour;
		}
		return 0;
	}
	return -1;
}

int get_free_neighbours(int board[], int pos, neighbours_t neighbours)
{
	int no_of_neighbours = 0;

	if (get_free_neighbour(board, pos, neighbours, 0, -1, -2) == 0) //1 <-1, -2>
		no_of_neighbours++;

	if (get_free_neighbour(board, pos, neighbours, 1, +1, -2) == 0) //2 <+1, -2>
		no_of_neighbours++;

	if (get_free_neighbour(board, pos, neighbours, 2, +2, -1) == 0) //3 <+2, -1>
		no_of_neighbours++;

	if (get_free_neighbour(board, pos, neighbours, 3, +2, +1) == 0) //4 <+2, +1>
		no_of_neighbours++;

	if (get_free_neighbour(board, pos, neighbours, 4, +1, +2) == 0) //5 <+1, +2>
		no_of_neighbours++;

	if (get_free_neighbour(board, pos, neighbours, 5, -1, +2) == 0) //6 <-1, +2>
		no_of_neighbours++;

	if (get_free_neighbour(board, pos, neighbours, 6, -2, +1) == 0) //7 <-2, +1>
		no_of_neighbours++;

	if (get_free_neighbour(board, pos, neighbours, 7, -2, -1) == 0) //8 <-2, -1>
		no_of_neighbours++;

	return no_of_neighbours;
}

int choose_best_neighbour(int board[], neighbours_t neighbours)
{
	int i;
	int best_neighbour_neighbours = BOARD_N * BOARD_M; //Unreachable high value
	int best_neighbour = -1;

	for (i=0; i<NO_OF_NEIGHBOURS; i++) {
		if (neighbours[i] != -1 && get_free_neighbours(board, neighbours[i], NULL) < best_neighbour_neighbours) {
			best_neighbour = neighbours[i];
			best_neighbour_neighbours = get_free_neighbours(board, neighbours[i], NULL);
		}
	}
	return best_neighbour;
}

int solve_from_pos(int pos)
{
	int board[BOARD_M * BOARD_N];
	neighbours_t neighbours;

	init_board(board);

	while (get_free_neighbours(board, pos, neighbours) > 0) {
		int best_neighbour = choose_best_neighbour(board, neighbours);
		move_from_to(board, pos, best_neighbour);
		pos = best_neighbour;
	}

	move_from_to(board, pos, pos);

	if (board_completed(board) != 0) {
		return -1;
	}

	return 0;
}

int main()
{
	int pos;
	int ret;
	int good_start_pos;

	good_start_pos = 0;
	for (pos=0; pos < BOARD_M*BOARD_N; pos++) {
		ret = solve_from_pos(pos);
		if (ret == 0) {
			printf("A solution for start position %d found.\n", pos);
			good_start_pos++;
		} else {
			printf("No solution for start position %d found.\n", pos);
		}
	}

	printf("Found a solution in %d out of %d cases.\n", good_start_pos, BOARD_M*BOARD_N);

	return 0;
}
