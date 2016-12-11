from itertools import combinations
import sys

def validFloors(floors):
    for values in floors.values():
        chips = []
        gens = []
        for v in values:
            if v[-1] == "g":
                gens.append(v)
            else:
                chips.append(v)
        if len(gens) > 0:
            for chip in chips:
                if chip[:-1]+"g" not in gens:
                    return False
    return True

def removeComplete(floors, trials):
    for floor in floors.keys():
        for v in floors[floor]:
            if v[-1] == "m" and v[:-1]+"g" in floors[floor]:
                floors[floor].remove(v)
                floors[floor].remove(v[:-1]+"g")
                print(v, "found on floor",floor,"with",trials,"trials")

def deepCopy(myDict):
    newDict = {}
    for key in myDict.keys():
        newDict[key] = myDict[key].copy()
    return newDict

def test(floors, elevator, trials=0):
    removeComplete(floors, trials)
    while len(floors[elevator]) == 0 and elevator < 4:
        elevator += 1
    if not validFloors(floors):
        return -1
    elif floors[1] == [] and floors[2] == [] and floors[3] == []:
        return 0
    minV = 10**1000
    if len(floors[elevator]) >= 2:
        for i in range(1,3):
            for combo in combinations(floors[elevator], i):
                for dir in ["up", "down"]:
                    tmpFloors = deepCopy(floors)
                    if dir == "up" and elevator < 4:
                        tmpElevator = elevator + 1
                    elif dir == "down" and elevator == 2 and floors[1] == []:
                        break
                    elif dir == "down" and elevator == 2 and floors[1] == [] and floors[2] == []:
                        break
                    elif dir == "down" and i == 2 and combo[0][:-1] == combo[1][:-1]:
                        break
                    elif dir == "down" and elevator > 1:
                        tmpElevator = elevator - 1
                    else:
                        break
                    for v in combo:
                        tmpFloors[elevator].remove(v)
                        tmpFloors[tmpElevator].append(v)
                    v = test(tmpFloors, tmpElevator, trials+1)
                    if v != -1 and v < minV:
                        minV = v
    else:
        for dir in ["up", "down"]:
            tmpFloors = deepCopy(floors)
            if dir == "up" and elevator < 4:
                tmpElevator = elevator + 1
            elif dir == "down" and elevator == 2 and floors[1] == []:
                break
            elif dir == "down" and elevator == 2 and floors[1] == [] and floors[2] == []:
                break
            elif dir == "down" and elevator > 1:
                tmpElevator = elevator - 1
            else:
                break
            v = tmpFloors[elevator][0]
            tmpFloors[elevator].remove(v)
            tmpFloors[tmpElevator].append(v)
            v = test(tmpFloors, tmpElevator, trials+1)
            if v != -1 and v < minV:
                minV = v
    return minV + 1

def printFloors(floors, elevator):
    floorNums = list(floors.keys())
    floorNums.sort(reverse=True)
    values = []
    for v in floors.values():
        values = values + v
    values.sort()
    for floor in floorNums:
        printed = "F" + str(floor)
        if floor == elevator:
            printed += " E"
        else:
            printed += " ."
        for v in values:
            if v in floors[floor]:
                printed += " " + v
            else:
                printed += " .  "
        print(printed)





floors = {1:[], 2:["lim"], 3:["lig"], 4:[]}
floors = {1:["prg", "prm"], 2:["cog","cug","rug","plg"], 3:["com","cum","rum","plm"], 4:[]}
elevator = 1
cap = 2
"""
floors = {1:[], 2:["cog","cug","rug","plg","prg", "prm"], 3:["com","cum","rum","plm"], 4:[]}
elevator = 2
floors = {1:[], 2:["cog","cug","rug","plg","prg"], 3:["com","cum","rum","plm", "prm"], 4:[]}
elevator = 3
floors = {1:[], 2:["cog","cug","rug","plg","prg","com","cum"], 3:["rum","plm", "prm"], 4:[]}
elevator = 2
floors = {1:[], 2:["cog","cug","rug","plg","prg","com"], 3:["rum","plm", "prm","cum"], 4:[]}
elevator = 3
floors = {1:[], 2:["cog","cug","rug","plg","prg","com","rum","plm"], 3:["prm","cum"], 4:[]}
elevator = 2
floors = {1:[], 2:["cog","cug","rug","plg","prg","com","rum"], 3:["prm","cum","plm"], 4:[]}
elevator = 3
floors = {1:[], 2:["cog","cug","rug","plg","prg","com","rum","cum","plm"], 3:["prm"], 4:[]}
elevator = 2
floors = {1:[], 2:["cog","cug","rug","plg","prg","com","rum","cum"], 3:["prm","plm"], 4:[]}
elevator = 3
floors = {1:[], 2:["cog","cug","rug","plg","prg","com","rum","cum","prm","plm"], 3:[], 4:[]}
elevator = 2
"""
#m can't be on same floor as g of different type unless has g on same floor

print(test(floors, elevator))
#this does not work
#first solution should be 33
#final solution should be 57