#include <stdio.h>
#include <stdint.h>
#include <string.h>

#define BOARD_N 8
#define BOARD_M 8

typedef int neighbours_t[8];

void init_board(int8_t board[])
{
	memset(board, -1, sizeof(board));
}

int board_completed(int8_t board[])
{
	int i;

	for (i=0; i<sizeof(board); i++) {
		if (board[i] == -1)
			return -1;
	}
	return 0;
}

int board_is_empty_pos(int8_t board[], int pos)
{
	if (board[pos] != -1)
		return 0;

	return -1;
}

int board_is_valid_pos(int8_t board[], int pos)
{
	if (pos > 0 && pos < sizeof(board))
		return 0;

	return -1;
}

int get_free_neighbours(int8_t board[], int pos, neighbours_t neighbours)
{
	int no_of_neighbours = 0;
	int neighbour;

	if (neighbours != NULL)
		memset(neighbours, -1, sizeof(neighbours));

	//1
	neighbour = pos + (-1) + (-2) * 8; // <-1,-2>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[0] = neighbour;
		}
	}

	//2
	neighbour = pos + (+1) + (-2) * 8; // <+1,-2>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[1] = neighbour;
		}
	}

	//3
	neighbour = pos + (+2) + (-1) * 8; // <+2,-1>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[2] = neighbour;
		}
	}

	//4
	neighbour = pos + (+1) + (+2) * 8; // <+1,+2>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[3] = neighbour;
		}
	}

	//5
	neighbour = pos + (+2) + (+1) * 8; // <+2,+1>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[4] = neighbour;
		}
	}

	//6
	neighbour = pos + (-1) + (+2) * 8; // <-1,+2>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[5] = neighbour;
		}
	}

	//7
	neighbour = pos + (-2) + (+1) * 8; // <-2,+1>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[6] = neighbour;
		}
	}

	//8
	neighbour = pos + (-2) + (-1) * 8; // <-2,-1>
	if (board_is_valid_pos(board, neighbour) && board_is_empty_pos(board, neighbour)) {
		no_of_neighbours++;
		if (neighbours != NULL) {
			neighbours[7] = neighbour;
		}
	}

	return no_of_neighbours;
}

int choose_best_neighbour(int8_t board[], neighbours_t neighbours)
{
	int i;
	int best_neighbour_neighbours = BOARD_N * BOARD_M; //Unreachable high value
	int best_neighbour = -1;

	for (i=0; i<8; i++) {
		if (neighbours[i] != -1 && get_free_neighbours(board, neighbours[i], NULL) < best_neighbour_neighbours) {
			best_neighbour = neighbours[i];
			best_neighbour_neighbours = get_free_neighbours(board, neighbours[i], NULL);
		}
	}
	return best_neighbour;
}

void move_from_to(int8_t board[], int from_pos, int to_pos)
{
	board[from_pos] = to_pos;
}

int solve_from_pos(int pos)
{
	int8_t board[BOARD_M * BOARD_N];
	neighbours_t neighbours;

	init_board(board);

	while (get_free_neighbours(board, pos, neighbours) > 0) {
		int best_neighbour = choose_best_neighbour(board, neighbours);
		move_from_to(board, pos, best_neighbour);
		pos = best_neighbour;
	}

	if (!board_completed(board) == 0) {
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
