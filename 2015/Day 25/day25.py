def calcCode(targetRow, targetCol, startRow=1, startCol=1, startNum=20151125):
    while not (startCol == targetCol and startRow == targetRow):
        startNum = (startNum * 252533) % 33554393
        if startRow == 1:
            startRow += 1
            while startCol > 1:
                startCol -= 1
                startRow += 1
        else:
            startCol += 1
            startRow -= 1
    return startNum




#print(calcCode(5,6))

targetRow = 2978
targetCol = 3083
print(calcCode(targetRow, targetCol))
#2650453