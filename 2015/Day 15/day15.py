import itertools


def parseLine(line, ingredientsDict):
    line = line.rstrip().split()
    ingredient = line[0]
    capacity = int(line[2][:-1])
    durability = int(line[4][:-1])
    flavor = int(line[6][:-1])
    texture = int(line[8][:-1])
    calories = int(line[10])
    ingredientsDict[ingredient] = {"capacity": capacity, "durability": durability, \
        "flavor": flavor, "texture": texture, "calories": calories}

def calcBestIngredients(ingredientsDict, numTeaspoons):
    maxScore = 0
    for each in itertools.combinations_with_replacement(ingredientsDict.keys(), numTeaspoons):
        capacity = 0
        durability = 0
        flavor = 0
        texture = 0
        for ingredient in each:
            capacity += ingredientsDict[ingredient]["capacity"]
            durability += ingredientsDict[ingredient]["durability"]
            flavor += ingredientsDict[ingredient]["flavor"]
            texture += ingredientsDict[ingredient]["texture"]
        if capacity < 0 or durability < 0 or flavor < 0 or texture < 0:
            score = 0
        else:
            score = capacity * durability * flavor * texture

        if score > maxScore:
            maxScore = score
    return maxScore

def calcBestIngredientsCalorieLimit(ingredientsDict, numTeaspoons, calorieLimit):
    maxScore = 0
    for each in itertools.combinations_with_replacement(ingredientsDict.keys(), numTeaspoons):
        capacity = 0
        durability = 0
        flavor = 0
        texture = 0
        calories = 0
        for ingredient in each:
            capacity += ingredientsDict[ingredient]["capacity"]
            durability += ingredientsDict[ingredient]["durability"]
            flavor += ingredientsDict[ingredient]["flavor"]
            texture += ingredientsDict[ingredient]["texture"]
            calories += ingredientsDict[ingredient]["calories"]
        if calories == calorieLimit:
            if capacity < 0 or durability < 0 or flavor < 0 or texture < 0:
                score = 0
            else:
                score = capacity * durability * flavor * texture

            if score > maxScore:
                maxScore = score
    return maxScore



file = open("textFile.txt", "r")
lines = file.readlines()
file.close()


#lines = """Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
#Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3"""
#lines = lines.split("\n")


ingredientsDict ={}
for line in lines:
    parseLine(line, ingredientsDict)


print(calcBestIngredients(ingredientsDict, 100))
#13882464

print(calcBestIngredientsCalorieLimit(ingredientsDict, 100, 500))
#11171160