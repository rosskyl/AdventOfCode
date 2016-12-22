def swapPosition(line, pwd):
    x = int(line[2])
    y = int(line[-1])
    tmp = pwd[x]
    pwd[x] = pwd[y]
    pwd[y] = tmp
    return pwd

def swapLetter(line, pwd):
    x = line[2]
    y = line[-1]
    posX = pwd.index(x)
    posY = pwd.index(y)
    pwd[posX] = y
    pwd[posY] = x
    return pwd

def rotateSteps(line, pwd):
    dir = line[1]
    x = int(line[2])
    for i in range(x):
        if dir == "right":
            tmp = pwd[-1]
            pwd = pwd[:-1]
            pwd.insert(0, tmp)
        elif dir == "left":
            tmp = pwd[0]
            pwd = pwd[1:]
            pwd.append(tmp)
    return pwd

def rotateBased(line, pwd):
    x = line[-1]
    posX = pwd.index(x)
    numRot = posX + 1
    if posX >= 4:
        numRot += 1
    for i in range(numRot):
        tmp = pwd[-1]
        pwd = pwd[:-1]
        pwd.insert(0, tmp)
    return pwd

def reverse(line, pwd):
    x = int(line[2])
    y = int(line[-1])
    tmpPwd = "".join(pwd)
    tmpPwd = tmpPwd[:x] + tmpPwd[y:x:-1] + tmpPwd[x] + tmpPwd[y+1:]
    pwd = list(tmpPwd)
    return pwd

def move(line, pwd):
    x = int(line[2])
    y = int(line[-1])
    letX = pwd.pop(x)
    pwd.insert(y, letX)
    return pwd

def parseLine(line, pwd):
    line = line.split()
    if line[0] == "swap":
        if line[1] == "position":
            pwd = swapPosition(line, pwd)
        elif line[1] == "letter":
            pwd = swapLetter(line, pwd)
    elif line[0] == "rotate":
        if line[-1] == "steps" or line[-1] == "step":
            pwd = rotateSteps(line, pwd)
        elif line[1] == "based":
            pwd = rotateBased(line, pwd)
    elif line[0] == "reverse":
        pwd = reverse(line, pwd)
    elif line[0] == "move":
        pwd = move(line, pwd)
    return pwd

def part1(lines, pwd):
    for line in lines:
        pwd = parseLine(line, pwd)
    return "".join(pwd)



lines = """swap position 4 with position 0
swap letter d with letter b
reverse positions 0 through 4
rotate left 1 step
move position 1 to position 4
move position 3 to position 0
rotate based on position of letter b
rotate based on position of letter d"""
lines = lines.splitlines()
pwd = list("abcde")

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()
pwd = list("abcdefgh")

print(part1(lines, pwd))
#first solution is gcedfahb