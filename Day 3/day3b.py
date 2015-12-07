file = open("textFile.txt", "r")
lines = file.readlines()
file.close()


visitedCoords = []
santaCoords = [0,0]
robotCoords = [0,0]
visitedCoords.append(tuple(santaCoords))
for line in lines:
    line = line.rstrip()
    for i in range(0, len(line), 2):
        if line[i] == ">":
            santaCoords[0] += 1
        elif line[i] == "<":
            santaCoords[0] -= 1
        elif line[i] == "^":
            santaCoords[1] += 1
        elif line[i] == "v":
            santaCoords[1] -= 1
        else:
            print("This should not happen")
        visitedCoords.append(tuple(santaCoords))

        if line[i+1] == ">":
            robotCoords[0] += 1
        elif line[i+1] == "<":
            robotCoords[0] -= 1
        elif line[i+1] == "^":
            robotCoords[1] += 1
        elif line[i+1] == "v":
            robotCoords[1] -= 1
        else:
            print("This should not happen")
        visitedCoords.append(tuple(robotCoords))

visitedCoords = set(visitedCoords)
print(len(visitedCoords))
#2360