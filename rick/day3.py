input_data = open("3_1_input").read().splitlines()


def q3_1():
    cols = []
    gamma = ""
    epsilon = ""

    for x in range(len(input_data[0])):
        cols.append(list())
        for row in input_data:
            cols[x].append(row[x])
        ones = cols[x].count('1')
        zeros = len(input_data) - ones
        if ones > zeros:
            gamma += '1'
            epsilon += '0'
        else:
            gamma += '0'
            epsilon += '1'
    print(int(gamma, 2) * int(epsilon, 2))


def get_rating(strings_list, index, type):
    # type: OX or CO2
    if len(strings_list) > 1:
        col = [x[index] for x in strings_list]
        ones = col.count('1')
        zeros = len(strings_list) - ones
        if type == 'OX':
            if ones >= zeros:
                crit = '1'
            else:
                crit = '0'
        elif type == 'CO2':
            if zeros > ones:
                crit = '1'
            else:
                crit = '0'
        new_list = [x for x in strings_list if x[index] == crit]
        return get_rating(new_list, index + 1, type)
    elif len(strings_list) == 1:
        return strings_list[0]
    else:
        return None


def q3_2():
    # Oxygen Generator Rating = Most prevalent
    # CO2 Scrubber Rating = Least prevalent
    ox = int(get_rating(input_data, 0, 'OX'), 2)
    co2 = int(get_rating(input_data, 0, 'CO2'), 2)
    answer = ox * co2
    print(f'{ox} | {co2} | answer: {answer}')


# q3_1()
q3_2()