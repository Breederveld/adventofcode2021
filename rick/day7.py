input_data = open("7_1_input").read().splitlines()
input_data = input_data[0].split(',')
input_data = [int(x) for x in input_data]


def q7_1():
    crabby_min  = min(input_data)
    crabby_max = max(input_data)
    fuels = []

    for pos in range(crabby_min, crabby_max + 1):
        fuel = 0
        for crabby in input_data:
            fuel += abs(pos - crabby)
        fuels.append(fuel)
    print(min(fuels))


def q7_2():
    crabby_min  = min(input_data)
    crabby_max = max(input_data)
    fuels = []

    for pos in range(crabby_min, crabby_max + 1):
        fuel = 0
        for crabby in input_data:
            fuel += sum(range(abs(pos - crabby)+1))

        fuels.append(fuel)
    print(min(fuels))


# q7_1()
q7_2()