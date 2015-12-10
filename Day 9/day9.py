def parseLine(line, citiesDict):
    line = line.rstrip()
    line = line.split()
    loc1 = line[0]
    loc2 = line[2]
    distance = line[-1]
    citiesDict[(loc1, loc2)] = int(distance)

def getDistance(loc1, loc2, citiesDict):
    if (loc1, loc2) in citiesDict.keys():
        return citiesDict[(loc1, loc2)]
    elif (loc2, loc1) in citiesDict.keys():
        return citiesDict[(loc2, loc1)]
    else:
        return -1

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

def difference(list1, list2):
    newList = []
    for item in list1:
        if item not in list2:
            newList.append(item)
    return newList

def addCity(cities, city):
    newList = cities[:]
    newList.append(city)
    return newList

def getShortesDistance(citiesDict, visitedCities=[], numCities=-1, cities=None):
    if cities == None:
        cities = set(flatten(list(citiesDict.keys())))
    if numCities == -1:
        numCities = len(cities)
    unvisitedCities = difference(cities, visitedCities)

    if len(visitedCities) == len(cities) - 1:
        return getDistance(visitedCities[-1], difference(cities, visitedCities)[0], citiesDict)

    shortestDistance = None
    for city in unvisitedCities:
        if len(visitedCities) > 0:
            distance = getDistance(visitedCities[-1], city, citiesDict)
        else:
            distance = 0
        distance += getShortesDistance(citiesDict, addCity(visitedCities, city), numCities, cities)
        if shortestDistance == None:
            shortestDistance = distance
        elif distance < shortestDistance:
            shortestDistance = distance
    return shortestDistance

def getLongestDistance(citiesDict, visitedCities=[], numCities=-1, cities=None):
    if cities == None:
        cities = set(flatten(list(citiesDict.keys())))
    if numCities == -1:
        numCities = len(cities)
    unvisitedCities = difference(cities, visitedCities)

    if len(visitedCities) == len(cities) - 1:
        return getDistance(visitedCities[-1], difference(cities, visitedCities)[0], citiesDict)

    longestDistance = 0
    for city in unvisitedCities:
        if len(visitedCities) > 0:
            distance = getDistance(visitedCities[-1], city, citiesDict)
        else:
            distance = 0
        distance += getLongestDistance(citiesDict, addCity(visitedCities, city), numCities, cities)
        if distance > longestDistance:
            longestDistance = distance
    return longestDistance








file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

citiesDict = {}
for line in lines:
    parseLine(line, citiesDict)

print(getShortesDistance(citiesDict))
#141

print(getLongestDistance(citiesDict))
#736