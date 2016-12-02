import itertools


def parseLine(line, seatingDict):
    line = line.rstrip()
    line = line.split()
    person1 = line[0]
    person2 = line[-1][:-1]
    action = line[2]
    amount = int(line[3])
    if action == "lose":
        amount = -amount
    seatingDict[(person1, person2)] = amount

def flatten(myList):
    newList = []
    for item in myList:
        if type(item) == list or type(item) == tuple:
            subList = flatten(item)
            for each in subList:
                newList.append(each)
        else:
            newList.append(item)
    return newList

def getHappiness(seatingDict, person1, person2):
    if person1 == "self" or person2 == "self":
        return 0
    else:
        return seatingDict[(person1, person2)]

def findBestHappiness(seatingDict):
    people = set(flatten(list(seatingDict.keys())))
    maxHappiness = 0
    for chart in itertools.permutations(people):
        totalHappiness = 0
        for i in range(len(chart)):
            if i == 0:
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i+1])
                totalHappiness += getHappiness(seatingDict, chart[i], chart[-1])
            elif i == len(chart) - 1:
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i-1])
                totalHappiness += getHappiness(seatingDict, chart[i], chart[0])
            else:
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i-1])
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i+1])
        if totalHappiness > maxHappiness:
            maxHappiness = totalHappiness
    return maxHappiness

def findBestHappinessWithSelf(seatingDict):
    people = set(flatten(list(seatingDict.keys())))
    people.update(["self"])
    maxHappiness = 0
    for chart in itertools.permutations(people):
        totalHappiness = 0
        for i in range(len(chart)):
            if i == 0:
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i+1])
                totalHappiness += getHappiness(seatingDict, chart[i], chart[-1])
            elif i == len(chart) - 1:
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i-1])
                totalHappiness += getHappiness(seatingDict, chart[i], chart[0])
            else:
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i-1])
                totalHappiness += getHappiness(seatingDict, chart[i], chart[i+1])
        if totalHappiness > maxHappiness:
            maxHappiness = totalHappiness
    return maxHappiness




file = open("textFile.txt", "r")
lines = file.readlines()
file.close()


seatingDict = {}
for line in lines:
    parseLine(line, seatingDict)

print(findBestHappiness(seatingDict))
#664

print(findBestHappinessWithSelf(seatingDict))
#640