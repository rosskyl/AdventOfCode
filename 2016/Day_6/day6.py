def countWord(word, counts):
    #counts is a list of dictionaries
    #each dictionary has the letters and the counts of how often they occur
    for i in range(len(word)):
        if word[i] in counts[i].keys():
            counts[i][word[i]] += 1
        else:
            counts[i][word[i]] = 1

def getWord(counts):
    word = ""
    for letterDict in counts:
        maxV = 0
        maxL = ""
        for key in letterDict.keys():
            if letterDict[key] > maxV:
                maxV = letterDict[key]
                maxL = key
        word = word + maxL
    return word

def getRealWord(counts):
    word = ""
    for letterDict in counts:
        minL = list(letterDict.keys())[0]
        minV = letterDict[minL]
        for key in letterDict.keys():
            if letterDict[key] < minV:
                minV = letterDict[key]
                minL = key
        word = word + minL
    return word

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()

#lines = """eedadn
#drvtee
#eandsr
#raavrd
#atevrs
#tsrnev
#sdttsa
#rasrtv
#nssdts
#ntnada
#svetve
#tesnvt
#vntsnd
#vrdear
#dvrsen
#enarar"""
#lines = lines.splitlines()

counts = [{} for i in lines[0].rstrip()]

for line in lines:
    word = line.rstrip()
    countWord(word, counts)

print(getWord(counts))
#first answer is cyxeoccr

print(getRealWord(counts))
#final answer is batwpask