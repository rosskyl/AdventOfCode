def checkTime(discs, time):
    for positions, start in discs:
        time += 1
        if (start + time) % positions != 0:
            return False
    return True


#discs = [[5,4], [2,1]]
discs = [[17,5],[19,8],[7,1],[13,7],[5,1],[3,0]]



time = 0
while not checkTime(discs, time):
    time += 1

print(time)
#first answer is 16824
