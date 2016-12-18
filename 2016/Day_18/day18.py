def isSafeTile(left, center, right):
    if left == "^" and right == ".":
        return False
    elif left == "." and right == "^":
        return False
    else:
        return True

def createField(firstRow, numRows):
    rows = [list(firstRow)]
    row = 0
    while len(rows) < numRows:
        nextRow = []
        for i in range(len(rows[0])):
            if i == 0 and isSafeTile(".", rows[row][0], rows[row][1]):
                nextRow.append(".")
            elif i == 0:
                nextRow.append("^")
            elif i == len(rows[row])-1 and isSafeTile(rows[row][i-1], rows[row][i], "."):
                nextRow.append(".")
            elif i == len(rows[row])-1:
                nextRow.append("^")
            elif isSafeTile(rows[row][i-1], rows[row][i], rows[row][i+1]):
                nextRow.append(".")
            else:
                nextRow.append("^")
        row += 1
        rows.append(nextRow)
    return rows

def printField(rows):
    for i in range(len(rows)):
        for j in range(len(rows[i])):
            print(rows[i][j], end="")
        print()

def countSafeTiles(rows):
    safe = 0
    for i in range(len(rows)):
        for j in range(len(rows[i])):
            if rows[i][j] == ".":
                safe += 1
    return safe

firstRow = ".^^.^.^^^^"
firstRow = ".^^.^^^..^.^..^.^^.^^^^.^^.^^...^..^...^^^..^^...^..^^^^^^..^.^^^..^.^^^^.^^^.^...^^^.^^.^^^.^.^^.^."
rows = createField(firstRow, 40)
#printField(rows)
print(countSafeTiles(rows))
#first answer is 1951