input_data = open("6_1_input").read().splitlines()
input_data = input_data[0].split(',')
fishy_count = 0

class Fishy():
    def __init__(self, days_to_go):
        global fishy_count
        fishy_count += 1
        self.children = []
        self.days_to_go = days_to_go

    def new_day(self):
        for child in self.children:
            child.new_day()
        if self.days_to_go > 0:
            self.days_to_go -= 1
        else:
            self.days_to_go = 6
            self.children.append(Fishy(8))

    def count_children(self):
        count = 0
        for child in self.children:
            count += child.count_children()
        return count


def q6(days):
    fishies = [Fishy(int(t)) for t in input_data]

    for times in range(days):
        for fish in fishies:
            fish.new_day()
    print(fishy_count)


def q6_2_try2(count):
    fishies = [int(x) for x in input_data]
    for time in range(count):
        new_fishies = []
        for i, fishy in enumerate(fishies):
            if fishy == 0:
                fishies[i] = 6
                new_fishies.append(8)
            else:
                fishies[i] -= 1
        fishies.extend(new_fishies)
        print(f'Days: {time} | Count: {len(fishies)}')


def q6_2_try3(count):
    input = [int(x) for x in input_data]
    fishies = [0] * 9
    for fish in input:
        fishies[fish] += 1

    for time in range(count):
        zero_count = fishies[0]
        fishies.pop(0)
        fishies.append(zero_count)
        fishies[6] += zero_count

    print(sum(fishies))


# q6(80)
# q6(256)
# q6_2_try2(256)
q6_2_try3(256)