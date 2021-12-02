input_data = open("1_1_input").read().splitlines()
input_list = [int(x) for x in input_data]

def q1_1():
    counter = 0
    for i in range(1, len(input_list)):
        if input_list[i] > input_list[i-1]:
            counter += 1
    print(counter)


def q1_2():
    counter = 0
    for i in range(1, len(input_list)-2):
        a = sum(input_list[i-1:i+2])
        b = sum(input_list[i:i+3])

        if b > a:
            counter += 1
    print(counter)


q1_2()

