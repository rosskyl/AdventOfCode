
def findTargetWire(lines, circuitDict, target):
    if target in circuitDict.keys():
        return circuitDict[target]
    else:
        for line in lines:
            line = line.rstrip()
            line = line.split()
            resultWire = line[-1]
            if target == resultWire:
                resultWire = line[-1]
                if len(line) == 3:
                    operandWire = line[0]
                    if operandWire.isdigit():
                        operand = int(operandWire)
                    elif operandWire in circuitDict.keys():
                        operand = circuitDict[operandWire]
                    else:
                        operand = findTargetWire(lines, circuitDict, operandWire)

                    circuitDict[resultWire] = operand
                elif line[0] == "NOT":
                    operandWire = line[1]
                    if operandWire.isdigit():
                        operand = int(operandWire)
                    elif operandWire in circuitDict.keys():
                        operand = circuitDict[operandWire]
                    else:
                        operand = findTargetWire(lines, circuitDict, operandWire)

                    circuitDict[resultWire] = 65535 - operand
                else:
                    action = line[1]
                    if action == "AND":
                        operandWire1 = line[0]
                        if operandWire1.isdigit():
                            operand1 = int(operandWire1)
                        elif operandWire1 in circuitDict.keys():
                            operand1 = circuitDict[operandWire1]
                        else:
                            operand1 = findTargetWire(lines, circuitDict, operandWire1)

                        operandWire2 = line[2]
                        if operandWire2.isdigit():
                            operand2 = int(operandWire2)
                        elif operandWire2 in circuitDict.keys():
                            operand2 = circuitDict[operandWire2]
                        else:
                            operand2 = findTargetWire(lines, circuitDict, operandWire2)

                        circuitDict[resultWire] = operand1 & operand2
                    elif action == "OR":
                        operandWire1 = line[0]
                        if operandWire1.isdigit():
                            operand1 = int(operandWire1)
                        elif operandWire1 in circuitDict.keys():
                            operand1 = circuitDict[operandWire1]
                        else:
                            operand1 = findTargetWire(lines, circuitDict, operandWire1)

                        operandWire2 = line[2]
                        if operandWire2.isdigit():
                            operand2 = int(operandWire2)
                        elif operandWire2 in circuitDict.keys():
                            operand2 = circuitDict[operandWire2]
                        else:
                            operand2 = findTargetWire(lines, circuitDict, operandWire2)

                        circuitDict[resultWire] = operand1 | operand2
                    elif action == "LSHIFT":
                        operandWire = line[0]
                        if operandWire.isdigit():
                            operand = int(operandWire)
                        elif operandWire in circuitDict.keys():
                            operand = circuitDict[operandWire]
                        else:
                            operand = findTargetWire(lines, circuitDict, operandWire)

                        shiftNum = int(line[2])
                        circuitDict[resultWire] = operand << shiftNum
                    elif action == "RSHIFT":
                        operandWire = line[0]
                        if operandWire.isdigit():
                            operand = int(operandWire)
                        elif operandWire in circuitDict.keys():
                            operand = circuitDict[operandWire]
                        else:
                            operand = findTargetWire(lines, circuitDict, operandWire)

                        shiftNum = int(line[2])
                        circuitDict[resultWire] = operand >> shiftNum
                    else:
                        print("This should not happend")
        try:
            return circuitDict[target]
        except KeyError:
            print(target)



file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

circuitDict = {}


print(findTargetWire(lines, circuitDict, "a"))
#956

circuitDict = {"b":findTargetWire(lines, circuitDict, "a")}
print(findTargetWire(lines, circuitDict, "a"))
#40149