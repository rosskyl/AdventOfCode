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
for i in range(len(lines)):
    lines[i] = lines[i].split()
    if isValidTriangle(int(lines[i][0]), int(lines[i][1]), int(lines[i][2])):
        possibleTriangles += 1

print(possibleTriangles, "valid triangles")
#first answer is 869

possibleTriangles2 = 0
for i in range(0, len(lines), 3):
    for j in range(3):
        x = int(lines[i][j])
        y = int(lines[i+1][j])
        z = int(lines[i+2][j])
        if isValidTriangle(x,y,z):
            possibleTriangles2 += 1

print(possibleTriangles2, "valid triangles")
#final answer is 1544