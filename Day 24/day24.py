import itertools, sys


def parseFile(filename):
    packages = []
    with open(filename, "r") as file:
        lines = file.readlines()
    for line in lines:
        line = line.rstrip()
        packages.append(int(line))
    return packages

def calcQuantum(firstPackages):
    quantumEntanglement = int()
    quantumEntanglement = 1
    for package in firstPackages:
        quantumEntanglement *= package
    return quantumEntanglement

def checkCompartment(packageList, packages, compartments):
    combinedWeight = sum(packages) // compartments
    if sum(packageList) != combinedWeight:
        return False
    else:
        return True

def intersect(list1, list2):
    newList = []
    for item in list1:
        if item not in list2:
            newList.append(item)
    return newList

def findPackageConfigThree(packages):
    minQuantum = int()
    minQuantum = sys.maxsize
    minLen = len(packages)
    minFirstCompartment = []
    minSecondCompartment = []
    minThirdCompartment = []
    for i in range(1, 7):
        for config in itertools.combinations(packages, i):
            if checkCompartment(config, packages, 3):
                remainingPackages = intersect(packages, config)
                for j in range(1, len(remainingPackages)-1):
                    for config2 in itertools.combinations(remainingPackages, j):
                        if checkCompartment(config2, packages, 3):
                            remainingPackages = intersect(remainingPackages, config2)
                            if checkCompartment(remainingPackages, packages, 3):
                                if len(config) < minLen:
                                    minLen = len(config)
                                    minFirstCompartment = config[:]
                                    minSecondCompartment = config2[:]
                                    minThirdCompartment = remainingPackages[:]
                                    minQuantum = calcQuantum(config)
                                elif len(config) == minLen:
                                    quantum = int()
                                    quantum = calcQuantum(config)
                                    if quantum < minQuantum:
                                        minQuantum = quantum
                                        minFirstCompartment = config[:]
                                        minSecondCompartment = config2[:]
                                        minThirdCompartment = remainingPackages[:]
    print(minFirstCompartment)
    print(minSecondCompartment)
    print(minThirdCompartment)
    print("min len", minLen)
    print("quantum", minQuantum)
    return minQuantum

def findPackageConfigFour(packages):
    minQuantum = int()
    minQuantum = sys.maxsize
    minLen = len(packages)
    minFirstCompartment = []
    minSecondCompartment = []
    minThirdCompartment = []
    minFourthCompartment = []
    for i in range(1, 6):
        for config in itertools.combinations(packages, i):
            if checkCompartment(config, packages, 4) and len(config) < minLen:
                remainingPackages = intersect(packages, config)
                for j in range(1, len(remainingPackages)-2):
                    for config2 in itertools.combinations(remainingPackages, j):
                        if checkCompartment(config2, packages, 4):
                            remainingPackages2 = intersect(remainingPackages, config2)
                            for k in range(1, len(remainingPackages2)-1):
                                for config3 in itertools.combinations(remainingPackages2, k):
                                    if checkCompartment(config3, packages, 4):
                                        remainingPackages3 = intersect(remainingPackages2, config3)
                                        if checkCompartment(remainingPackages3, packages, 4):
                                            if len(config) < minLen:
                                                minLen = len(config)
                                                minFirstCompartment = config[:]
                                                minSecondCompartment = config2[:]
                                                minThirdCompartment = config3[:]
                                                minFourthCompartment = remainingPackages3[:]
                                                minQuantum = calcQuantum(config)
                                            elif len(config) == minLen:
                                                quantum = int()
                                                quantum = calcQuantum(config)
                                                if quantum < minQuantum:
                                                    minQuantum = quantum
                                                    minFirstCompartment = config[:]
                                                    minSecondCompartment = config2[:]
                                                    minThirdCompartment = config3[:]
                                                    minFourthCompartment = remainingPackages3[:]
    print(minFirstCompartment)
    print(minSecondCompartment)
    print(minThirdCompartment)
    print(minFourthCompartment)
    print("min len", minLen)
    print("quantum", minQuantum)
    return minQuantum



packages = parseFile("textFile.txt")

#packages = [1,2,3,4,5,7,8,9,10,11]



#print(findPackageConfigThree(packages))
#11266889531

print("--------------------------")

print(findPackageConfigFour(packages))
#77387711