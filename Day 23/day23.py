def parseFile(filename):
    with open(filename, "r") as file:
        lines = file.readlines()
        for i in range(len(lines)):
            lines[i] = lines[i].rstrip().split()
    return lines

def parseLine(line, registers):
    if line[0] == "hlf":
        registers[line[1]] //= 2
        registers["counter"] += 1
    elif line[0] == "tpl":
        registers[line[1]] *= 3
        registers["counter"] += 1
    elif line[0] == "inc":
        registers[line[1]] += 1
        registers["counter"] += 1
    elif line[0] == "jmp":
        registers["counter"] += int(line[1])
    elif line[0] == "jie":
        reg = line[1].rstrip(",")
        if registers[reg] % 2 == 0:
            registers["counter"] += int(line[2])
        else:
            registers["counter"] += 1
    elif line[0] == "jio":
        reg = line[1].rstrip(",")
        if registers[reg] == 1:
            registers["counter"] += int(line[2])
        else:
            registers["counter"] += 1

def doProgram(registers, lines):
    counter = registers["counter"]
    while counter < len(lines):
        parseLine(lines[counter], registers)
        counter = registers["counter"]

lines = parseFile("textFile.txt")


registers = {"a":0, "b":0, "counter":0}

doProgram(registers, lines)

print(registers)
#184

registers = {"a":1, "b":0, "counter":0}

doProgram(registers, lines)

print(registers)
#231