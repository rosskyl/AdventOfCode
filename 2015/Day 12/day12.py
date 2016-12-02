import json

def flatten(myList):
    newList = []
    for item in myList:
        if type(item) == list or type(item) == tuple:
            subList = flatten(item)
            for each in subList:
                newList.append(each)
        elif type(item) == dict:
            subList = flatten(list(item.keys()))
            for each in subList:
                newList.append(each)
            subList = flatten(list(item.values()))
            for each in subList:
                newList.append(each)
        else:
            newList.append(item)
    return newList

def flattenWithoutRed(myList):
    newList = []
    for item in myList:
        if type(item) == list or type(item) == tuple:
            subList = flattenWithoutRed(item)
            for each in subList:
                newList.append(each)
        elif type(item) == dict:
            if ("red" not in item.keys()) and ("red" not in item.values()):
                subList = flattenWithoutRed(list(item.keys()))
                for each in subList:
                    newList.append(each)
                subList = flattenWithoutRed(list(item.values()))
                for each in subList:
                    newList.append(each)
        else:
            newList.append(item)
    return newList

def sumList(myList, withoutRed=False):
    if withoutRed:
        myList = flattenWithoutRed(myList)
    else:
        myList = flatten(myList)
    sum = 0
    for each in myList:
        if type(each) == int:
            sum += each
    return sum





file = open("textFile.txt", "r")
lines = file.readlines()
file.close()

for i in range(len(lines)):
    lines[i] = lines[i].rstrip()
    lines[i] = json.loads(lines[i])




sum = sumList(lines)
print(sum)
#191164

sum = sumList(lines, True)
print(sum)
#87842