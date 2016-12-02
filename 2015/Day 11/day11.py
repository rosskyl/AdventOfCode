def isValidPassword(password):
    containsStraight1 = False
    containsStraight2 = False

    numPairs = 0
    lastLetter = " "
    lastPair = " "
    for char in password:
        if char == "i" or char == "o" or char == "l":
            return False
        if char == lastLetter and char != lastPair:
            numPairs += 1
            lastPair = char
        if ord(char) == ord(lastLetter) + 1:
            if containsStraight1:
                containsStraight2 = True
            else:
                containsStraight1 = True
        else:
            containsStraight1 = False
        lastLetter = char
    if containsStraight2 and numPairs >= 2:
        return True
    else:
        return False

def incrementPassword(password):
    password = [char for char in password]

    currentPosition = -1
    continueIncrementing = True
    while continueIncrementing:
        if password[currentPosition] == "z":
            password[currentPosition] = "a"
            if currentPosition >= -len(password):
                currentPosition -= 1
            else:
                continueIncrementing = False
        else:
            password[currentPosition] = chr(ord(password[currentPosition])+1)
            continueIncrementing = False
    return "".join(password)

def findNextPassword(password):
    password = incrementPassword(password)
    while not isValidPassword(password):
        password = incrementPassword(password)
    return password

password = "cqjxjnds"
password = findNextPassword(password)
print(password)
#cqjxxyzz

password = findNextPassword(password)
print(password)
#cqkaabcc