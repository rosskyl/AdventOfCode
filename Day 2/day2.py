def calcWrappingPaper(dimens):
    dimens.sort()
    wrappingPaper = dimens[0] * dimens[1] * 2
    wrappingPaper += (dimens[0] * dimens[2] * 2)
    wrappingPaper += (dimens[1] * dimens[2] * 2)
    wrappingPaper += (dimens[0] * dimens[1])
    return wrappingPaper

def parseLine(line):
    line = line.rstrip()
    dimens = line.split("x")
    dimens[0] = int(dimens[0])
    dimens[1] = int(dimens[1])
    dimens[2] = int(dimens[2])
    return dimens

def calcRibbon(dimens):
    dimens.sort()
    ribbon = dimens[0] * 2 + dimens[1] * 2
    ribbon += (dimens[0] * dimens[1] * dimens[2])
    return ribbon



file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

totalWrappingPaper = 0
totalRibbon = 0
for line in lines:
    dimens = parseLine(line)
    totalWrappingPaper += calcWrappingPaper(dimens)
    totalRibbon += calcRibbon(dimens)

print(totalWrappingPaper)
#1588178

print(totalRibbon)
#3783758