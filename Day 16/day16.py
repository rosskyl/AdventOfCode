def parseLine(line, auntDict):
    line = line.rstrip()
    line = line.split()
    number = line[1][:-1]
    item1 = line[2][:-1]
    item1Num = line[3][:-1]
    item2 = line[4][:-1]
    item2Num = line[5][:-1]
    item3 = line[6][:-1]
    item3Num = line[7]
    auntDict[number] = {item1: item1Num, item2: item2Num, item3: item3Num}

def parseSolution(lines):
    lines = lines.split("\n")
    solutionDict = {}
    for line in lines:
        line = line.split()
        item = line[0][:-1]
        itemNum = line[1]
        solutionDict[item] = itemNum
    return solutionDict

def findSolution(auntDict, solutionDict):
    solutions = []
    for auntNum in auntDict.keys():
        isMatch = True
        for item in auntDict[auntNum].keys():
            if auntDict[auntNum][item] != solutionDict[item]:
                isMatch = False
        if isMatch:
            solutions.append(auntNum)
    return solutions

def findSolution2(auntDict, solutionDict):
    solutions = []
    for auntNum in auntDict.keys():
        isMatch = True
        for item in auntDict[auntNum].keys():
            if item == "cats" or item == "trees":
                if not (auntDict[auntNum][item] > solutionDict[item]):
                    isMatch = False
            elif item == "pomeranians" or item == "goldfish":
                if not (auntDict[auntNum][item] < solutionDict[item]):
                    isMatch = False
            else:
                if auntDict[auntNum][item] != solutionDict[item]:
                    isMatch = False
        if isMatch:
            solutions.append(auntNum)
    return solutions




file = open("textFile.txt", "r")
lines = file.readlines()
file.close()


auntDict = {}
for line in lines:
    parseLine(line, auntDict)

solutionLines = """children: 3
cats: 7
samoyeds: 2
pomeranians: 3
akitas: 0
vizslas: 0
goldfish: 5
trees: 3
cars: 2
perfumes: 1"""

solutionDict = parseSolution(solutionLines)

print(findSolution(auntDict, solutionDict))
#40

solution = findSolution2(auntDict, solutionDict)
for each in solution:
    print(each, auntDict[each])
#241
#not sure why I get 357, should not because there are too many goldfish