def factor(num):
    factors = []
    for i in range(1, num//2+1):
        if num % i == 0:
            factors.append(i)
    factors.append(num)
    return factors

def calcNumGifts(houseNum):
    factors = factor(houseNum)
    numGifts = 0
    for num in factors:
        numGifts += (num * 10)
    return numGifts



FinalNumGifts = 34000000
i = 600000
numGifts = calcNumGifts(i)
while numGifts != FinalNumGifts:
    print(i, numGifts)
    i += 1
    numGifts = calcNumGifts(i)
print(i)