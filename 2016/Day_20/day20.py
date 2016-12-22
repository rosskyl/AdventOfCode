def createBlacklist(lines):
    blacklist = []
    for line in lines:
        line = line.split("-")
        start = int(line[0])
        end = int(line[-1])
        blacklist.append([start, end])
    blacklist.sort()
    return blacklist

def getLowestValid(blacklist):
    ip = 0
    index = 0
    lowest = None
    while lowest == None:
        start, end = blacklist[index]
        if ip >= start:
            if ip <= end:
                ip = end + 1
            else:
                index += 1
        else:
            lowest = ip
    return lowest



lines = """5-8
0-2
4-7"""
lines = lines.splitlines()
numIPs = 10


inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()
numIPs = 4294967295 + 1



blacklist = createBlacklist(lines)
lowest = getLowestValid(blacklist)
print(lowest)
#first answer is 31053880