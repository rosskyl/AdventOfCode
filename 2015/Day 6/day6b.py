def parseLine(line):
    line = line.rstrip()
    line = line.split()
    if line[0] == "toggle":
        action = "toggle"
        startPair = line[1].split(",")
    else:
        action = line[1]
        startPair = line[2].split(",")
    endPair = line[-1].split(",")
    return action, startPair, endPair

def doAction(action, startPair, endPair, lightsDict):
    startX = int(startPair[0])
    #print(startPair)
    startY = int(startPair[1])
    endX = int(endPair[0])
    endY = int(endPair[1])

    for i in range(startX, endX+1):
        for j in range(startY, endY+1):
            if action == "toggle":
                lightsDict[(i, j)] += 2
            elif action == "on":
                lightsDict[(i, j)] += 1
            elif action == "off":
                lightsDict[(i, j)] -= 1
                if lightsDict[(i, j)] < 0:
                    lightsDict[(i, j)] = 0
            else:
                print("This should not happen")

def calcNumOn(lightsDict):
    values = list(lightsDict.values())
    return sum(values)






lightsDict = {}
for i in range(1000):
    for j in range(1000):
        lightsDict[(i,j)] = 0

file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

for line in lines:
    action, startPair, endPair = parseLine(line)
    doAction(action, startPair, endPair, lightsDict)

print(calcNumOn(lightsDict))
#17836115