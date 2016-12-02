import itertools

def parseLines(lines):
    containers = []
    for line in lines:
        line = line.rstrip()
        containers.append(int(line))
    return containers

def getStoreCombos(containerList, liters):
    combos = []
    for i in range(len(containerList)):
        for each in itertools.combinations(containerList, i):
            if sum(each) == liters:
                combos.append(each[:])
    return combos

def findMin(containerList):
    minList = []
    minNum = len(containerList[0])
    for each in containerList:
        if len(each) < minNum:
            minList = [each]
        elif len(each) == minNum:
            minList.append(each)
    return minList






file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

containers = parseLines(lines)



combos = getStoreCombos(containers, 150)
print(len(combos))
#654

minCombos = findMin(combos)
print(len(minCombos))
#57