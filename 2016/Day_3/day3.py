def isValidTriangle(x,y,z):
    if x + y <= z:
        return False
    elif x + z <= y:
        return False
    elif y + z <= x:
        return False
    else:
        return True



inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()

possibleTriangles = 0
for line in lines:
    line = line.split()
    if isValidTriangle(int(line[0]), int(line[1]), int(line[2])):
        possibleTriangles += 1

print(possibleTriangles, "valid triangles")
#first answer is 869
