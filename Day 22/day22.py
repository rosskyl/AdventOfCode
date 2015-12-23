import sys


def parseFile(filename):
    with open(filename, "r") as file:
        lines = file.readlines()
        boss = {}
        for line in lines:
            line = line.rstrip().split(": ")
            boss[line[0]] = int(line[1])
    return boss

def doTurn(player, boss):
    for key, value in player["Effects"].items():
        if value > 0:
            if key == "Magic Missile":
                boss["Hit Points"] -= 4
            elif key == "Drain":
                boss["Hit Points"] -= 2
                player["Hit Points"] += 2
            elif key == "Shield":
                player["Armor"] = 7
            elif key == "Poison":
                boss["Hit Points"] -= 3
            elif key == "Recharge":
                player["Mana"] += 101
            player["Effects"][key] -= 1

    bossDamage = boss["Damage"]
    bossDamage -= player["Armor"]
    if bossDamage < 0:
        bossDamage = 1
    player["Hit Points"] -= bossDamage

def doAction(player, spellName, spells):
    if player["Mana"] < spells[spellName][0]:
        return "not enough mana"
    elif player["Effects"][spellName] > 0:
        return "already used"
    else:
        player["Effects"][spellName] = spells[spellName][0]
        return "good"

def isDead(person):
    if person["Hit Points"] <= 0:
        return True
    else:
        return False

def calcLowestManaWin(player, boss, spells, action="", counter=0):
    if action == "":
        minCost = sys.maxsize
        for key in player["Effects"].keys():
            cost = calcLowestManaWin(player, boss, spells, key, counter+1)
            if cost < minCost:
                minCost = cost
        return minCost
    else:
        actionResult = doAction(player, action, spells)
        if actionResult == "not enough mana":
            return sys.maxsize
        elif actionResult == "already used":
            return sys.maxsize
        else:
            doTurn(player, boss)
            if isDead(boss):
                return spells[action][1]
            elif isDead(player):
                return sys.maxsize
            else:
                minCost = sys.maxsize
                for key, value in player["Effects"].items():
                    if value == 0:
                        print("key", key)
                        print("counter", counter)
                        cost = calcLowestManaWin(player, boss, spells, key, counter+1)
                        if cost < minCost:
                            minCost = cost
                return minCost + spells[action][1]


boss = parseFile("textFile.txt")

spells = {"Magic Missile": [1, 53], "Drain": [1, 73], "Shield": [6, 113], "Poison": [6, 173], "Recharge": [5, 229]}

playerSpells = {"Magic Missile": 0, "Drain": 0, "Shield": 0, "Poison": 0, "Recharge": 0}
player = {"Hit Points": 50, "Damage": 0, "Armor": 0, "Mana": 500, "Effects": playerSpells}

print(calcLowestManaWin(player, boss, spells))