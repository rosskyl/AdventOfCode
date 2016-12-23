def countViablePairs(lines):
    total = 0
    for p1 in lines:
        for p2 in lines:
            if p1 != p2 and p1[2] != "0T":
                used = int(p1[2][:-1])
                avail = int(p2[3][:-1])
                if used < avail:
                    total += 1
    return total

def parseLines(lines):
    nodes = {}
    for line in lines:
        line = line.split()
        file = line[0].split("/")[-1].split("-")
        x = int(file[1][1:])
        y = int(file[2][1:])
        size = int(line[1][:-1])
        used = int(line[2][:-1])
        nodes[(x,y)] = [size, used]
    return nodes

def printGrid(nodes, maxX, maxY, goal):
    for y in range(maxY+1):
        for x in range(maxX+1):
            if nodes[(x,y)][0] > nodes[(0,0)][0] * 2: # nodes size is greater than 2 times that of 0,0
                print("#", end=" ")
            elif (x, y) == goal:
                print("G", end=" ")
            elif nodes[(x,y)][1] == 0:
                print("_", end=" ")
            else:
                print(".", end=" ")
        print()

def game(nodes, maxX, maxY, goal):
    pass




#/dev/grid/node-x0-y12
#Filesystem              Size  Used  Avail  Use%



lines = """/dev/grid/node-x0-y0   10T    8T     2T   80%
/dev/grid/node-x0-y1   11T    6T     5T   54%
/dev/grid/node-x0-y2   32T   28T     4T   87%
/dev/grid/node-x1-y0    9T    7T     2T   77%
/dev/grid/node-x1-y1    8T    0T     8T    0%
/dev/grid/node-x1-y2   11T    7T     4T   63%
/dev/grid/node-x2-y0   10T    6T     4T   60%
/dev/grid/node-x2-y1    9T    8T     1T   88%
/dev/grid/node-x2-y2    9T    6T     3T   66%"""
lines = lines.splitlines()

#"""
inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()
lines.pop(0)
lines.pop(0)
#"""

"""
for i in range(len(lines)):
    lines[i] = lines[i].split()

total = countViablePairs(lines)
print(total)
#first answer is 910
"""

nodes = parseLines(lines)
#printGrid(nodes, 2, 2, (2, 0))

printGrid(nodes, 34, 26, (34, 0))
#final solution is 222