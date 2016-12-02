def parseLine(line, replaceDict):
    line = line.rstrip().split()
    initial = line[0]
    final = line[-1]
    if initial in replaceDict.keys():
        replaceDict[initial].append(final)
    else:
        replaceDict[initial] = [final]

def findDistinctMolecules(initialSequence, replaceDict):
    distinceMolecules = []
    for key in replaceDict.keys():
        loc = -1
        indexes = []
        while True:
            loc = initialSequence.find(key, loc+1)
            if loc != -1:
                indexes.append(loc)
            else:
                break
        distinceMolecules.append(replace(initialSequence, replaceDict, indexes, key))
    return list(set(flatten(distinceMolecules)))



def replace(initialSequence, replaceDict, indexes, atom):
    molecules = []
    for final in replaceDict[atom]:
        for index in indexes:
            molecules.append(initialSequence[:index] + final + initialSequence[index+len(atom):])
    return molecules

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









file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

##lines = """H => HO
##H => OH
##O => HH
##
##HOH"""
##lines = lines.split("\n")


initialSequence = lines.pop(-1)
lines.pop(-1)

replaceDict = {}
for line in lines:
    parseLine(line, replaceDict)

numMolecules = len(findDistinctMolecules(initialSequence, replaceDict))
print(numMolecules)
#509