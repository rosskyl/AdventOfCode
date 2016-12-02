def changeDirection(d, turn):
    #d is the initial direction either n, e, s, or w
    #turn is either R for right or L for left
    #returns the new direction
    dirs = ['n','e','s','w']
    d = dirs.index(d)
    if turn == "R":
        d += 1
    else:
        d -= 1
    if d < 0:
        d += 4
    elif d > 3:
        d -= 4
    return dirs[d]

def move(start, d, amount):
    #start is the coordinate that you start at and is a list of 2 ints
    #d is the direction you are facing now
    #amount is how much you will move
    if d == "n":
        start[1] += amount
    elif d == "s":
        start[1] -= amount
    elif d == "e":
        start[0] += amount
    elif d == "w":
        start[0] -= amount
    return start


inputs = "R3, L2, L2, R4, L1, R2, R3, R4, L2, R4, L2, L5, L1, R5, R2, R2, L1, R4, R1, L5, L3, R4, R3, R1, L1, L5, L4, L2, R5, L3, L4, R3, R1, L3, R1, L3, R3, L4, R2, R5, L190, R2, L3, R47, R4, L3, R78, L1, R3, R190, R4, L3, R4, R2, R5, R3, R4, R3, L1, L4, R3, L4, R1, L4, L5, R3, L3, L4, R1, R2, L4, L3, R3, R3, L2, L5, R1, L4, L1, R5, L5, R1, R5, L4, R2, L2, R1, L5, L4, R4, R4, R3, R2, R3, L1, R4, R5, L2, L5, L4, L1, R4, L4, R4, L4, R1, R5, L1, R1, L5, R5, R1, R1, L3, L1, R4, L1, L4, L4, L3, R1, R4, R1, R1, R2, L5, L2, R4, L1, R3, L5, L2, R5, L4, R5, L5, R3, R4, L3, L3, L2, R2, L5, L5, R3, R4, R3, R4, R3, R1"
#inputs = "R8, R4, R4, R8"
inputs = inputs.split(", ")

start = [0, 0]

d = "n"
for line in inputs:
    d = changeDirection(d, line[0])
    start = move(start, d, int(line[1:]))

print("Ending at", start, "which is", abs(start[0])+abs(start[1]), "blocks away from where you started")
#first answer is 262
