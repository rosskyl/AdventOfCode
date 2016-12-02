def parseLines(lines):
    lights = []
    for line in lines:
        line = line.rstrip()
        lightSub = []
        for char in line:
            if char == "#":
                lightSub.append(1)
            else:
                lightSub.append(0)
        lights.append(lightSub[:])
    lights[0][0] = 1
    lights[0][-1] = 1
    lights[-1][0] = 1
    lights[-1][-1] = 1
    return lights

def countLightsOn(lights):
    sumLightsOn = 0
    for line in lights:
        sumLightsOn += sum(line)
    return sumLightsOn

def countNeighboursOn(lights, row, column):
    sumLightsOn = 0
    if row >= len(lights) or row < 0:
        return -1
    elif column >= len(lights[row]) or column < 0:
        return -1
    elif row == 0 and column == 0:
        sumLightsOn += lights[row+1][column]
        sumLightsOn += lights[row][column+1]
        sumLightsOn += lights[row+1][column+1]
    elif row == 0 and column == len(lights[row]) - 1:
        sumLightsOn += lights[row+1][column]
        sumLightsOn += lights[row][column-1]
        sumLightsOn += lights[row+1][column-1]
    elif row == len(lights) - 1 and column == 0:
        sumLightsOn += lights[row-1][column]
        sumLightsOn += lights[row][column+1]
        sumLightsOn += lights[row-1][column+1]
    elif row == len(lights) - 1 and column == len(lights[row]) - 1:
        sumLightsOn += lights[row-1][column]
        sumLightsOn += lights[row][column-1]
        sumLightsOn += lights[row-1][column-1]
    elif row == 0:
        sumLightsOn += lights[row+1][column]
        sumLightsOn += lights[row][column+1]
        sumLightsOn += lights[row+1][column+1]
        sumLightsOn += lights[row][column-1]
        sumLightsOn += lights[row+1][column-1]
    elif row == len(lights) - 1:
        sumLightsOn += lights[row-1][column]
        sumLightsOn += lights[row][column+1]
        sumLightsOn += lights[row-1][column+1]
        sumLightsOn += lights[row][column-1]
        sumLightsOn += lights[row-1][column-1]
    elif column == 0:
        sumLightsOn += lights[row+1][column]
        sumLightsOn += lights[row][column+1]
        sumLightsOn += lights[row+1][column+1]
        sumLightsOn += lights[row-1][column]
        sumLightsOn += lights[row-1][column+1]
    elif column == len(lights[row]) - 1:
        sumLightsOn += lights[row+1][column]
        sumLightsOn += lights[row][column-1]
        sumLightsOn += lights[row+1][column-1]
        sumLightsOn += lights[row-1][column]
        sumLightsOn += lights[row-1][column-1]
    else:
        sumLightsOn += lights[row+1][column]
        sumLightsOn += lights[row][column+1]
        sumLightsOn += lights[row+1][column+1]
        sumLightsOn += lights[row+1][column-1]
        sumLightsOn += lights[row-1][column]
        sumLightsOn += lights[row][column-1]
        sumLightsOn += lights[row-1][column-1]
        sumLightsOn += lights[row-1][column+1]
    return sumLightsOn


def animate(lights):
    newLights = []
    for i in range(len(lights)):
        subList = []
        for j in range(len(lights[i])):
            if i == 0 and j == 0:
                subList.append(1)
            elif i == len(lights) - 1 and j == 0:
                subList.append(1)
            elif i == 0 and j == len(lights[i]) - 1:
                subList.append(1)
            elif i == len(lights) - 1 and j == len(lights[i]) - 1:
                subList.append(1)
            else:
                lightsOn = countNeighboursOn(lights, i, j)
                if lights[i][j] == 1:
                    if lightsOn == 2 or lightsOn == 3:
                        subList.append(1)
                    else:
                        subList.append(0)
                else:
                    if lightsOn == 3:
                        subList.append(1)
                    else:
                        subList.append(0)
        newLights.append(subList[:])
    return newLights




file = open("textFile.txt", "r")
lines = file.readlines()
file.close()


lights = parseLines(lines)


for i in range(100):
    lights = animate(lights)
print(countLightsOn(lights))
#886