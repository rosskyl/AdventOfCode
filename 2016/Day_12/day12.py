lines = """cpy 41 a
inc a
inc a
dec a
jnz a 2
dec a"""
lines = lines.splitlines()



inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()


registers = [0,0,0,0]

index = 0
while index < len(lines):
    instruction = lines[index].split()
    if instruction[0] == "cpy":
        toRegister = ord(instruction[2])-97
        if instruction[1].isdigit():
            registers[toRegister] = int(instruction[1])
        else:
            registers[toRegister] = registers[ord(instruction[1])-97]
    elif instruction[0] == "inc":
        toRegister = ord(instruction[1])-97
        registers[toRegister] += 1
    elif instruction[0] == "dec":
        toRegister = ord(instruction[1])-97
        registers[toRegister] -= 1
    elif instruction[0] == "jnz":
        if instruction[1].isdigit():
            value = int(instruction[1])
        else:
            reg = ord(instruction[1])-97
            value = registers[reg]
        amt = int(instruction[2])
        if value != 0:
            index += amt
            index -= 1
    index += 1



print(registers)
#first solution is 317993