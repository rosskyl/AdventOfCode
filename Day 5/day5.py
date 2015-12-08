def checkWord1(word):
    vowels = "aeiou"
    numVowels = 0
    containsDouble = False
    for i in range(len(word)-1):
        if word[i] in vowels:
            numVowels += 1
        if word[i] == word[i+1]:
            containsDouble = True
        pair = word[i] + word[i+1]
        if pair == "ab" or pair == "cd" or pair == "pq" or pair == "xy":
            return False
    if word[-1] in vowels:
        numVowels += 1
    if numVowels >= 3 and containsDouble:
        return True
    else:
        return False

def checkWord2(word):
    containsRepeat = False
    doubleRepeat = False
    pairs = []
    lastDouble = ""
    for i in range(len(word)-2):
        pairs.append(word[i] + word[i+1])
        if lastDouble == pairs[-1]:
            doubleRepeat = True
        if word[i] == word[i+2]:
            containsRepeat = True
    pairs.append(word[-2] + word[-1])
    singlePairs = set(pairs[:])
    if len(pairs) - 1 == len(singlePairs) and containsRepeat and not doubleRepeat:
        return True
    else:
        return False



file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

sumNiceWords = 0
for line in lines:
    line = line.rstrip()
    if checkWord1(line):
        sumNiceWords += 1

print(sumNiceWords)
#238


sumNiceWords = 0
for line in lines:
    line = line.rstrip()
    if checkWord2(line):
        sumNiceWords += 1

print(sumNiceWords)
#69
# Could never get this one to work