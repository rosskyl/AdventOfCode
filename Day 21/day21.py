import itertools, sys


def parseWeapons(string):
    weapons = {}
    string = string.split("\n")
    string.pop(0)
    for line in string:
        line = line.split()
        weapons[line[0]] = {"Cost": int(line[1]), "Damage": int(line[2]), "Armor": int(line[3])}
    return weapons

def parseArmor(string):
    armor = {}
    string = string.split("\n")
    string.pop(0)
    for line in string:
        line = line.split()
        armor[line[0]] = {"Cost": int(line[1]), "Damage": int(line[2]), "Armor": int(line[3])}
    return armor

def parseRings(string):
    rings = {}
    string = string.split("\n")
    string.pop(0)
    for line in string:
        line = line.split()
        rings[line[0]+line[1]] = {"Cost": int(line[2]), "Damage": int(line[3]), "Armor": int(line[4])}
    return rings

def parseFile(filename):
    with open(filename, "r") as file:
        lines = file.readlines()
        boss = {}
        for line in lines:
            line = line.rstrip().split(": ")
            boss[line[0]] = int(line[1])
    return boss

def doTurn(player, boss):
    playerDamage = player["Damage"]
    playerDamage -= boss["Armor"]
    if playerDamage < 0:
        playerDamage = 1
    boss["Hit Points"] -= playerDamage

    bossDamage = boss["Damage"]
    bossDamage -= player["Armor"]
    if bossDamage < 0:
        bossDamage = 1
    player["Hit Points"] -= bossDamage

def isDead(person):
    if person["Hit Points"] <= 0:
        return True
    else:
        return False

def play(player, boss):
    while not isDead(player) and not isDead(boss):
        doTurn(player, boss)
    if isDead(player):
        return "boss wins"
    elif isDead(boss):
        return "player wins"

def calcCheapestWin(player, boss, weaponDict, armorDict, ringDict):
    minCost = sys.maxsize
    for weapon in weaponDict.values():
        for armor in armorDict.values():
            for i in range(1, 3):
                for each in itertools.permutations(ringDict.values(), i):
                    testPlayer = player.copy()
                    testBoss = boss.copy()
                    cost = 0
                    testPlayer["Damage"] += weapon["Damage"]
                    cost += weapon["Cost"]
                    testPlayer["Armor"] += armor["Armor"]
                    cost += armor["Cost"]
                    for ring in each:
                        testPlayer["Damage"] += ring["Damage"]
                        testPlayer["Armor"] += ring["Armor"]
                        cost += ring["Cost"]
                    if cost < minCost:
                        if play(testPlayer, testBoss) == "player wins":
                            minCost = cost
    return minCost

def calcExpensiveLoss(player, boss, weaponDict, armorDict, ringDict):
    maxCost = 0
    for weapon in weaponDict.values():
        for armor in armorDict.values():
            for i in range(1, 3):
                for each in itertools.permutations(ringDict.values(), i):
                    testPlayer = player.copy()
                    testBoss = boss.copy()
                    cost = 0
                    testPlayer["Damage"] += weapon["Damage"]
                    cost += weapon["Cost"]
                    testPlayer["Armor"] += armor["Armor"]
                    cost += armor["Cost"]
                    for ring in each:
                        testPlayer["Damage"] += ring["Damage"]
                        testPlayer["Armor"] += ring["Armor"]
                        cost += ring["Cost"]
                    if cost > maxCost:
                        if play(testPlayer, testBoss) == "boss wins":
                            maxCost = cost
    return maxCost




weaponString = """Weapons:    Cost  Damage  Armor
Dagger        8     4       0
Shortsword   10     5       0
Warhammer    25     6       0
Longsword    40     7       0
Greataxe     74     8       0"""

weaponDict = parseWeapons(weaponString)

armorString = """Armor:      Cost  Damage  Armor
Leather      13     0       1
Chainmail    31     0       2
Splintmail   53     0       3
Bandedmail   75     0       4
Platemail   102     0       5
None          0     0       0"""

armorDict = parseArmor(armorString)

ringString = """Rings:      Cost  Damage  Armor
Damage +1    25     1       0
Damage +2    50     2       0
Damage +3   100     3       0
Defense +1   20     0       1
Defense +2   40     0       2
Defense +3   80     0       3"""

ringDict = parseRings(ringString)

boss = parseFile("textFile.txt")

player = {"Hit Points": 100, "Damage": 0, "Armor": 0}

print(calcCheapestWin(player, boss, weaponDict, armorDict, ringDict))
#111

print(calcExpensiveLoss(player, boss, weaponDict, armorDict, ringDict))
#188