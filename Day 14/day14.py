def parseLine(line, speedDict):
    line = line.rstrip()
    line = line.split()
    reindeer = line[0]
    speed = int(line[3])
    moveTime = int(line[6])
    restTime = int(line[-2])
    speedDict[reindeer] = [speed, moveTime, restTime]

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

def getDistance(endTime, reindeer, speedDict):
    speed = speedDict[reindeer][0]
    moveTime = speedDict[reindeer][1]
    restTime = speedDict[reindeer][2]
    time = 0
    distance = 0
    while time < endTime:
        if time + moveTime <= endTime:
            distance += (speed * moveTime)
            time += moveTime + restTime
        else:
            distance += speed
            time += 1
    return distance

def getFastestReindeer(speedDict, endTime):
    fastestReindeer = ""
    maxDistance = 0
    reindeer = set(flatten(list(speedDict.keys())))
    for each in reindeer:
        distance = getDistance(endTime, each, speedDict)
        if distance > maxDistance:
            maxDistance = distance
            fastestReindeer = each
    return fastestReindeer, maxDistance

def getMaxPointReindeer(speedDict, endTime):
    reindeer = set(flatten(list(speedDict.keys())))
    reindeerPoints = {}
    reindeerDistance = {}
    for each in reindeer:
        reindeerPoints[each] = 0
        reindeerDistance[each] = 0
    for i in range(1, endTime):
        for each in reindeer:
            reindeerDistance[each] = getDistance(i, each, speedDict)
        maxDistance = 0
        maxReindeer = ""
        for each in reindeerDistance.keys():
            if reindeerDistance[each] > maxDistance:
                maxDistance = reindeerDistance[each]
                maxReindeer = [each]
            elif reindeerDistance[each] == maxDistance:
                maxReindeer.append(each)
        for each in maxReindeer:
            reindeerPoints[each] += 1
    print(reindeerPoints)
    return max(reindeerPoints.values())


file = open("textFile.txt", "r")
lines = file.readlines()
file.close()




speedDict = {}
for line in lines:
    parseLine(line, speedDict)


print(getFastestReindeer(speedDict, 2503))
#2655


#speedDict = {"comet": [14, 10, 127], "dancer": [16, 11, 162]}
print(getMaxPointReindeer(speedDict, 2503))