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

def getState(floors, elevator):
    state = []
    for key in floors.keys():
        chips = []
        gens = []
        for v in floors[key]:
            if v[-1] == "m":
                chips.append(v)
            elif v[-1] == "g":
                gens.append(v)
        pairs = 0
        for chip in chips:
            if chip[:-1]+"g" in gens:
                pairs += 1
        state.append([len(chips), len(gens), pairs])
    state.append(elevator)
    return state

def test(floors, elevator, states):
#    removeComplete(floors, trials)
#    while len(floors[elevator]) == 0 and elevator < 4:
#        elevator += 1
    if not validFloors(floors):
        return -1
    elif floors[1] == [] and floors[2] == [] and floors[3] == []:
        return 0
    thisState = getState(floors, elevator)
    if thisState in states:
        return -1
    else:
        states.append(thisState)
    minV = 10**1000
    if len(floors[elevator]) >= 2:
        for i in range(1,3):
            for combo in combinations(floors[elevator], i):
                for dir in ["up", "down"]:
                    tmpFloors = deepCopy(floors)
                    if dir == "up" and elevator < 4:
                        tmpElevator = elevator + 1
                    elif dir == "down" and elevator == 2 and floors[1] == []:
                        continue
                    elif dir == "down" and elevator == 3 and floors[1] == [] and floors[2] == []:
                        continue
#                    elif dir == "down" and i == 2 and combo[0][:-1] == combo[1][:-1]:
#                        continue
                    elif dir == "down" and elevator > 1:
                        tmpElevator = elevator - 1
                    else:
                        continue
                    for v in combo:
                        tmpFloors[elevator].remove(v)
                        tmpFloors[tmpElevator].append(v)
                    v = test(tmpFloors, tmpElevator, states)
                    if v != -1 and v < minV:
                        minV = v
    else:
        for dir in ["up", "down"]:
            tmpFloors = deepCopy(floors)
            if dir == "up" and elevator < 4:
                tmpElevator = elevator + 1
            elif dir == "down" and elevator == 2 and floors[1] == []:
                continue
            elif dir == "down" and elevator == 2 and floors[1] == [] and floors[2] == []:
                continue
            elif dir == "down" and elevator > 1:
                tmpElevator = elevator - 1
            else:
                continue
            if len(tmpFloors[elevator]) == 0:
                return -1
            v = tmpFloors[elevator][0]
            tmpFloors[elevator].remove(v)
            tmpFloors[tmpElevator].append(v)
            v = test(tmpFloors, tmpElevator, states)
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





floors = {1:["lim", "him"], 2:["hig"], 3:["lig"], 4:[]}
#floors = {1:["prg", "prm"], 2:["cog","cug","rug","plg"], 3:["com","cum","rum","plm"], 4:[]}
elevator = 1
states = []
#m can't be on same floor as g of different type unless has g on same floor

print(test(floors, elevator, states))
#this does not work
#first solution should be 33
#final solution should be 57

