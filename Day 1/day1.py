file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

floor = 0
position = 0
basementPositions = []
for line in lines:
    line = line.rstrip()
    for char in line:
        position += 1
        if char == "(":
            floor += 1
        elif char == ")":
            floor -= 1
        else:
            print("This should not happen")
        if floor == -1:
            basementPositions.append(position)

print(floor)
#232

print(basementPositions[0])
#1783