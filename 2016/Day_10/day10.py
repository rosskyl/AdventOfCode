class Bot:
    def __init__(self, tmpNum):
        self.num = tmpNum
        self.values = []
    def setLow(self, target):
        self.low = target
    def setHigh(self, target):
        self.high = target
    def addValue(self, value):
        self.values.append(value)
        return len(self.values) == 2
    def getValues(self):
        if 61 in self.values and 17 in self.values:
            print("Bot",self.num,"comparing",self.values[1],"to",self.values[0])
        return min(self.values), max(self.values)
    def __str__(self):
        return str(self.low) + " " + str(self.high)

def addBot(bot, low, high, bots):
    if len(bots)-1 < bot:
        for i in range(bot-len(bots)+1):
            bots.append(Bot(len(bots)))
    bots[bot].setLow(low)
    bots[bot].setHigh(high)

def giveValue(bot, value):
    result = bots[bot].addValue(value)
    returned = 1
    if result:
        l,h = bots[bot].getValues()
        if bots[bot].low[0] == "bot":
            returned *= giveValue(bots[bot].low[1], l)
        elif bots[bot].low[0] == "output" and bots[bot].low[1] in[0,1,2]:
            returned *= l
        if bots[bot].high[0] == "bot":
            returned *= giveValue(bots[bot].high[1], h)
        elif bots[bot].high[0] == "output" and bots[bot].high[1] in[0,1,2]:
            returned *= h
    return returned




lines = """value 5 goes to bot 2
bot 2 gives low to bot 1 and high to bot 0
value 3 goes to bot 1
bot 1 gives low to output 1 and high to bot 0
bot 0 gives low to output 2 and high to output 0
value 2 goes to bot 2"""
lines = lines.splitlines()

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()


values = []
bots = []

for line in lines:
    line = line.split()
    if line[0] == "value":
        value = int(line[1])
        bot = int(line[-1])
        values.append([bot, value])
    elif line[0] == "bot":
        bot = int(line[1])
        low = [line[5], int(line[6])]
        high = [line[10], int(line[11])]
        addBot(bot, low, high, bots)

total = 1
for line in values:
    total = total * giveValue(line[0], line[1])

#first solution is 118

print(total)
#final solution is 143153