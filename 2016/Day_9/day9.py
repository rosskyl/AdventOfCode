def uncompress(line):
    newLine = ""
    index = 0
    while index < len(line):
        if line[index] == "(":
            marker = ""
            while line[index] != ")":
                marker = marker + line[index]
                index += 1
            numChar, repeat = marker[1:].split("x") # ) is not included because while loop stops when it encounters )
            index += 1
            for i in range(int(repeat)):
                newLine = newLine + line[index:index+int(numChar)]
            index += int(numChar)
        else:
            newLine = newLine + line[index]
            index += 1
    return newLine




inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()

line = lines[0]

newLine = uncompress(line)
print(len(newLine))
#first solution is 123908