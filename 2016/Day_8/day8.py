def parseLine(line, screen):
    line = line.split()
    if line[0] == "rect":
        y,x = line[1].split("x")
        rect(screen, int(x), int(y))
    elif line[0] == "rotate" and line[1] == "column":
        tmp,x = line[2].split("=")
        amt = line[-1]
        rotateCol(screen, int(x), int(amt))
    elif line[0] == "rotate" and line[1] == "row":
        tmp,y = line[2].split("=")
        amt = line[-1]
        rotateRow(screen, int(y), int(amt))

def rect(screen, x, y):
    for i in range(x):
        for j in range(y):
            screen[i][j] = "#"

def rotateRow(screen, y, amount):
    for i in range(amount):
        previous = screen[y][0]
        for j in range(1, len(screen[y])):
            tmp = screen[y][j]
            screen[y][j] = previous
            previous = tmp
        screen[y][0] = previous

def rotateCol(screen, x, amt):
    for i in range(amt):
        previous = screen[0][x]
        for j in range(len(screen)):
            tmp = screen[j][x]
            screen[j][x] = previous
            previous = tmp
        screen[0][x] = previous

def printScreen(screen):
    for i in range(len(screen)):
        for j in range(len(screen[i])):
            print(screen[i][j], end="")
        print()

def countLit(screen):
    total = 0
    for i in range(len(screen)):
        total += screen[i].count("#")
    return total


lines = """rect 3x2
rotate column x=1 by 1
rotate row y=0 by 4
rotate column x=1 by 1"""
lines = lines.splitlines()

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()

rows = 6
cols = 50

screen = [ [ "." for i in range(cols)] for j in range(rows)]

for line in lines:
    parseLine(line, screen)

print(countLit(screen))
#first solution is 119

printScreen(screen)
#final solution is ZFHFSFOGPO