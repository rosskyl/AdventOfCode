def formatLine(line):
    split = line.rstrip().split("-")
    sector, checksum = split.pop().split("[")
    sector = int(sector)
    checksum = checksum[:-1]
    name = "".join(split)
    return [name, sector, checksum]

def count(name):
    counts = {}
    for letter in name:
        if letter in counts.keys():
            counts[letter] += 1
        else:
            counts[letter] = 1
    return counts

def orderDict(myDict):
    ordered = []
    keys = orderList(list(myDict.keys()))
    while len(keys) != 0:
        maxV = 0
        for i in range(len(keys)):
            if myDict[keys[i]] > maxV:
                maxV = myDict[keys[i]]
                maxK = keys[i]
                index = i
        ordered.append([maxK, maxV])
        keys.pop(index)
    return ordered

def orderList(myString):
    #myString is a list of letters
    ordered = []
    while len(myString) != 0:
        minL = myString[0]
        index = 0
        for i in range(1, len(myString)):
            if myString[i] < minL:
                minL = myString[i]
                index = i
        myString.pop(index)
        ordered.append(minL)
    return ordered

def isRoom(line):
    name = line[0]
    checksum = line[2]
    counts = count(name)
    ordered = orderDict(counts)
    for i in range(len(checksum)):
        if checksum[i] != ordered[i][0]:
            return False
    return True






#inputs = """not-a-real-room-404[oarel]
#a-b-c-d-e-f-g-h-987[abcde]
#aaaaa-bbb-z-y-x-123[abxyz]
#totally-real-room-200[decoy]"""
#lines = inputs.splitlines()

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()


total = 0
for i in range(len(lines)):
    lines[i] = formatLine(lines[i])
    if isRoom(lines[i]):
        total += lines[i][1]

print(total)
#answer is 173787