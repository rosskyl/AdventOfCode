def createBlacklist(lines):
    blacklist = []
    for line in lines:
        line = line.split("-")
        start = int(line[0])
        end = int(line[-1])
        blacklist.append([start, end])
    blacklist.sort()
    return blacklist

def getValid(blacklist, max):
    ip = 0
    index = 0
    lowest = None
    total = 0
    while ip <= max:
        if index < len(blacklist):
            start, end = blacklist[index]
            if ip >= start and ip <= end:
                ip = end + 1
                index += 1
            elif ip >= start and ip > end:
                index += 1
            else:
                total += 1
                if lowest == None:
                    lowest = ip
                ip += 1
        else:
            total += 1
            ip += 1

    return lowest, total



lines = """5-8
0-2
4-7"""
lines = lines.splitlines()
numIPs = 9


inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()
numIPs = 4294967295


blacklist = createBlacklist(lines)
lowest = getValid(blacklist, numIPs)
print(lowest)
#first answer is 31053880
#final answer is 117