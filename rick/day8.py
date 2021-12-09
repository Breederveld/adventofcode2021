input_data = open("8_1_input").read().splitlines()
input_data = [row.split(" | ") for row in input_data]
input_data = [[row[0].split(" "), row[1].split(" ")] for row in input_data]

def characters_not_in_string_count(chars, my_string) -> int:
    return abs(len([char for char in my_string if char in chars]) - len(chars))


def display_decoder(row) -> int:
    inputs_for_digit = [""] * 10

    for inputs in row[0]:
        inputs = ''.join(sorted(inputs))
        if len(inputs) == 2: # 1
            inputs_for_digit[1] = inputs
        elif len(inputs) == 4: # 4
            inputs_for_digit[4] = inputs
        elif len(inputs) == 3: # 7
            inputs_for_digit[7] = inputs
        elif len(inputs) == 7: # 8
            inputs_for_digit[8] = inputs

    for inputs in row[0]:
        inputs = ''.join(sorted(inputs))
        if len(inputs) == 6:  # 0, 6, 9
            if characters_not_in_string_count(inputs_for_digit[4], inputs) != 0 and \
                    characters_not_in_string_count(inputs_for_digit[1], inputs) == 0:
                inputs_for_digit[0] = inputs
            elif characters_not_in_string_count(inputs_for_digit[1], inputs) == 0:
                inputs_for_digit[9] = inputs
            else:
                inputs_for_digit[6] = inputs

    for inputs in row[0]:
        inputs = ''.join(sorted(inputs))
        if len(inputs) == 5: # 2, 3, 5
            if characters_not_in_string_count(inputs_for_digit[1], inputs) == 0:
                inputs_for_digit[3] = inputs
            elif characters_not_in_string_count(inputs_for_digit[9], inputs) == 1:
                inputs_for_digit[5] = inputs
            else:
                inputs_for_digit[2] = inputs

    digit_str = ""
    for output in row[1]:
        output = ''.join(sorted(output))
        for i, input in enumerate(inputs_for_digit):
            if input == output:
                digit_str += str(i)
    if len(digit_str) != 4:
        print('Not all decoded!')
    return int(digit_str)


def q8_1():
    count_1478 = 0
    for row in input_data:
        for ans in row[1]:
            if len(ans) == 2 or len(ans) == 4 or len(ans) == 3 or len(ans) == 7:
                count_1478 += 1
    print(count_1478)


def q8_2():
    tot = 0
    for row in input_data:
        tot += display_decoder(row)
    print(tot)

# q8_1()
q8_2()