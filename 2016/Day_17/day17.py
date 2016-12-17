import hashlib

def getHash(name):
    m = hashlib.md5()
    m.update(name.encode())
    return m.hexdigest()

def getOpenDoors(passcode, path):
    hash = getHash(passcode+path)
    openDoors = []
    if hash[0] in "bcdef":
        openDoors.append("U")
    if hash[1] in "bcdef":
        openDoors.append("D")
    if hash[2] in "bcdef":
        openDoors.append("L")
    if hash[3] in "bcdef":
        openDoors.append("R")
    return openDoors

def getLength(passcode, loc, path=""):
    global shortest
    if len(path) > 0 and path[-1] == "U":
        loc[1] -= 1
    elif len(path) > 0 and path[-1] == "D":
        loc[1] += 1
    elif len(path) > 0 and path[-1] == "L":
        loc[0] -= 1
    elif len(path) > 0 and path[-1] == "R":
        loc[0] += 1
    if loc[0] > 3 or loc[0] < 0:
        return None
    elif loc[1] > 3 or loc[1] < 0:
        return None
    elif loc == [3,3]:
        if len(path) < shortest:
            shortes = len(path)
        return path
    openDoors = getOpenDoors(passcode, path)
    finalPath = None
    for d in openDoors:
        tmpPath = path + d
        if len(tmpPath) > shortest:
            return None
        tmpLoc = loc[:]
        endPath = getLength(passcode, tmpLoc, tmpPath)
        if finalPath == None:
            finalPath = endPath
        elif endPath != None and len(endPath) < len(finalPath):
            finalPath = endPath
    return finalPath




loc = [0,0]
passcode = "hhhxzeay"
global shortest
shortest = 10*100
short = getLength(passcode, loc)
print(short, len(short))
#first answer is DDRUDLRRRD