def parseLine(line, replaceDict):
    line = line.rstrip().split()
    initial = line[0]
    final = line[-1]
    if initial in replaceDict.keys():
        replaceDict[initial].append(final)
    else:
        replaceDict[initial] = [final]

def




file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

lines = """H => HO
H => OH
O => HH

HOH"""
lines = lines.split("\n")


target = lines.pop(-1)
lines.pop(-1)

replaceDict = {}
for line in lines:
    parseLine(line, replaceDict)