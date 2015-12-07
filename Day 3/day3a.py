file = open("textFile.txt", "r")
lines = file.readlines()
file.close()


visitedCoords = []
santaCoords = [0,0]
visitedCoords.append(tuple(santaCoords))
for line in lines:
    line = line.rstrip()
    for char in line:
        if char == ">":
            santaCoords[0] += 1
        elif char == "<":
            santaCoords[0] -= 1
        elif char == "^":
            santaCoords[1] += 1
        elif char == "v":
            santaCoords[1] -= 1
        else:
            print("This should not happen")
        visitedCoords.append(tuple(santaCoords))

visitedCoords = set(visitedCoords)
print(len(visitedCoords))
#2592