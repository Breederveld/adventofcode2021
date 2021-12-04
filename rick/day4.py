import numpy as np

input_data = open("4_1_input").read().splitlines()
draws = input_data[0].split(',')
grids = []
grid_tmp = []

for input_line in input_data[1:]:
    if input_line == "":
        if len(grid_tmp) != 0:
            grids.append(grid_tmp)
        grid_tmp = []
    else:
        grid_line = []
        for index in range(0, len(input_line), 3):
            grid_line.append(int(''.join(input_line[index:index + 2])))
        grid_tmp.append(grid_line)


def run_number(x):
    global grids
    for grid in grids:
        for line in grid:
            for i, number in enumerate(line):
                if number == x:
                    line[i] = -1


def get_winners():
    winners = []
    for i, grid in enumerate(grids):
        cols = [[] for x in grid[0]]
        winner = False
        for line in grid:
            if line.count(-1) == len(line):
                winner = True
            for x in range (len(line)):
                cols[x].append(line[x])
        for col in cols:
            if col.count(-1) == len(col):
                winner = True
        if winner:
            winners.append(grid)
            grids.pop(i)
    return winners


def q4_1():
    for draw in draws:
        run_number(int(draw))
        winners = get_winners()
        if len(winners) > 0:
            print('The winner is:')
            print(np.matrix(winners[0]))
            print(f'Last draw: {draw}')
            answer = sum([sum([x for x in row if x != -1]) for row in winners[0]]) * int(draw)
            print(f'Answer: {answer}')
            return


def q4_2():
    last_winner = []
    last_winner_draw = None
    for draw in draws:
        if len(grids) == 0:
            break
        else:
            run_number(int(draw))
            winners = get_winners()
            if len(winners) > 0:
                last_winner = winners[-1]
                last_winner_draw = draw
    print('The last winner is:')
    print(np.matrix(last_winner))
    print(f'Last draw: {last_winner_draw}')
    answer = sum([sum([x for x in row if x != -1]) for row in last_winner]) * int(last_winner_draw)
    print(f'Answer: {answer}')


#q4_1()
q4_2()