import sys, copy, random

def parseFile(filename):
    with open(filename, "r") as file:
        lines = file.readlines()
        boss = {}
        for line in lines:
            line = line.rstrip().split(": ")
            boss[line[0]] = int(line[1])
    return boss

def doTurn(player, boss, person, spells):
    for key, value in player["Effects"].items():
        if value > 0:
            if key == "Shield":
                player["Armor"] = 7
            elif key == "Poison":
                if value < 6:
                    boss["Hit Points"] -= 3
            elif key == "Recharge":
                player["Mana"] += 101
            player["Effects"][key] -= 1
    if person == "boss":
        bossDamage = boss["Damage"]
        bossDamage -= player["Armor"]
        if bossDamage <= 0:
            bossDamage = 1
        player["Hit Points"] -= bossDamage
    else:
        if player["Mana"] < spells[person][0]:
            return "not enough mana"
        elif player["Effects"][person] > 0:
            return "already used"
        else:
            if person=="Magic Missile":
                boss["Hit Points"] -= 4
                return "good"
            elif person == "Drain":
                boss["Hit Points"] -= 2
                player["Hit Points"] += 2
                return "good"
            else:
                return doSpell(player, person, spells)

def doSpell(player, spellName, spells):
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

def randomWin(player, boss, spells, minCost):
    cost = 0
    spellList = []
    while (not isDead(player)) and (not isDead(boss)):
        spell = random.choice(list(spells.keys()))

        result = doTurn(player, boss, spell, spells)
        if result == "good":
            doTurn(player, boss, "boss", spells)

            cost += spells[spell][1]
            spellList.append(spell)
            if cost > minCost:
                return cost, spellList
        elif result == "not enough mana":
            return sys.maxsize, spellList
    if isDead(boss):
        return cost, spellList
    elif isDead(player):
        return sys.maxsize, spellList
    print("hello")

def calcMinWin(player, boss, spells, iterations):
    minCost = sys.maxsize
    minSpellList = []
    for i in range(iterations):
        playerCopy = copy.deepcopy(player)
        bossCopy = copy.deepcopy(boss)
        cost, spellList = randomWin(playerCopy, bossCopy, spells, minCost)
        if cost < minCost:
            minCost = cost
            minSpellList = spellList
    return minCost, minSpellList

#boss = parseFile("textFile.txt")

spells = {"Magic Missile": [1, 53], "Drain": [1, 73], "Shield": [6, 113], "Poison": [6, 173], "Recharge": [5, 229]}

playerSpells = {"Magic Missile": 0, "Drain": 0, "Shield": 0, "Poison": 0, "Recharge": 0}

boss = {"Hit Points": 14, "Damage": 8}
player = {"Hit Points": 10, "Armor": 0, "Mana": 250, "Effects": playerSpells}

print(calcMinWin(player, boss, spells, 1000))