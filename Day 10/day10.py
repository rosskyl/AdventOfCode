def lookAndSayEncode(num):
    newNum = ""

    sumLastNum = 0
    lastNum = num[0]
    for char in num:
        if char == lastNum:
            sumLastNum += 1
        else:
            newNum += str(sumLastNum) + lastNum
            lastNum = char
            sumLastNum = 1

    newNum += str(sumLastNum) + lastNum
    lastNum = ""
    sumLastNum = 0

    return newNum


num = "1113122113"
for i in range(40):
    num = lookAndSayEncode(num)

print(len(num))
#360154

for i in range(10):
    num = lookAndSayEncode(num)

print(len(num))
#5103798