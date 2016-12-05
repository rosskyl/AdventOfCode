import hashlib

def getHash(name):
    m = hashlib.md5()
    m.update(name.encode())
    return m.hexdigest()

def isValidHash(hash):
    if hash[0:5] == "00000":
        return True
    else:
        return False

def getNextChar(name, start):
    hash = getHash(name + str(start))
    while not isValidHash(hash):
        start += 1
        hash = getHash(name + str(start))
    return hash[5], start

def getPassword(name):
    password = ""
    nextNum = 1
    for i in range(8):
        nextChar, nextNum = getNextChar(name, nextNum)
        print(nextChar)
        nextNum += 1
        password = password + nextChar
    return password

def isRealValidHash(hash):
    if isValidHash(hash) and hash[5].isdigit() and int(hash[5]) < 8:
        return True
    else:
        return False

def getRealNextChar(name, start):
    hash = getHash(name + str(start))
    while not isRealValidHash(hash):
        start += 1
        hash = getHash(name + str(start))
    return int(hash[5]), hash[6], start

def getRealPassword(name):
    password = ["-" for i in range(8)]
    nextNum = 1
    pos, char, nextNum = getRealNextChar(name, nextNum)
    while "-" in password:
        nextNum += 1
        if password[pos] == "-":
            print(pos, char, nextNum)
            password[pos] = char
        pos, char, nextNum = getRealNextChar(name, nextNum)
    return "".join(password)


#print(getPassword("reyedfim"))
#first solution is f97c354d

print(getRealPassword("reyedfim"))
#final answer is 863dde27