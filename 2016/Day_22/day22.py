def countViablePairs(lines):
    total = 0
    for p1 in lines:
        for p2 in lines:
            if p1 != p2 and p1[2] != "0T":
                used = int(p1[2][:-1])
                avail = int(p2[3][:-1])
                if used < avail:
                    total += 1
    return total








#Filesystem              Size  Used  Avail  Use%

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()
lines.pop(0)
lines.pop(0)

for i in range(len(lines)):
    lines[i] = lines[i].split()

total = countViablePairs(lines)
print(total)
#first answer is 910