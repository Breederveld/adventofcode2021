input_data = open("2_1_input").read().splitlines()
#input_list = [int(x) for x in input_data]

def q2_1():
    x = 0
    depth = 0

    for move in input_data:
        move = move.split(" ")
        if move[0] == 'forward':
            x += int(move[1])
        elif move[0] == 'up':
            depth -= int(move[1])
        elif move[0] == 'down':
            depth += int(move[1])
    print(x * depth)


def q2_2():
    x = 0
    depth = 0
    aim = 0

    for move in input_data:
        move = move.split(" ")
        if move[0] == 'forward':
            x += int(move[1])
            depth += aim * int(move[1])
        elif move[0] == 'up':
            aim -= int(move[1])
        elif move[0] == 'down':
            aim += int(move[1])
    print(x * depth)


#q2_1()
q2_2()