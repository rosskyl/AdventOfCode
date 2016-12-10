def uncompress(line):
    newLine = ""
    index = 0
    while index < len(line):
        if line[index] == "(":
            marker = ""
            while line[index] != ")":
                marker = marker + line[index]
                index += 1
            numChar, repeat = marker[1:].split("x") # ) is not included because while loop stops when it encounters )
            index += 1
            for i in range(int(repeat)):
                newLine = newLine + line[index:index+int(numChar)]
            index += int(numChar)
        else:
            newLine = newLine + line[index]
            index += 1
    return newLine

def getMarker(line):
    marker = ""
    index = 0
    while line[index] != ")":
        marker += line[index]
        index += 1
    numChar, repeat = marker[1:].split("x") # ) is not included because while loop stops when it encounters )
    index += 1
    return int(numChar), int(repeat), index

def expandMaker(line):
    newLine = ""
    numChar, repeat, mIndex = getMarker(line)
    index = mIndex
    for i in range(repeat-1):
        newLine += line[index:index+numChar]
    return newLine, index

"""
def uncompressV2(line):
    total = 0
    index = 0
    while index < len(line):
        if line[index] == "(":
            line = line[index:]
            index = 0
            rLine, rIndex = expandMaker(line[index:])
            index += rIndex
            line = line[:index] + rLine + line[index:]
        else:
            total += 1
            index += 1
    return total
"""
def uncompressV2(line):
    #algorithm from https://www.reddit.com/r/adventofcode/comments/5hbygy/2016_day_9_solutions/dazentu/
    values = [1 for i in range(len(line))]
    total = 0
    i = 0
    while i < len(line):
        if line[i] == "(":
            numChar, repeat, index = getMarker(line[i:])
            for j in range(numChar):
                values[i+j+index] *= repeat
            i += index
        else:
            total += values[i]
            i += 1
    return total





inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()

line = lines[0]

newLine = uncompress(line)
print(len(newLine))
#first solution is 123908

print(uncompressV2("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN"))
print(uncompressV2(line))
#final solution is 10755693147