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

print(getPassword("reyedfim"))
#first solution is f97c354d
