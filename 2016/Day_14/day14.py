import hashlib
import re

def getHash(name):
    m = hashlib.md5()
    m.update(name.encode())
    return m.hexdigest()

def containsTriple(hexHash):
    match = re.findall(r"(.)\1\1", hexHash)
    if len(match) > 0:
        return True, match[0]
    else:
        return False, ""

def containsFive(hexHash):
    match = re.findall(r"(.)\1\1\1\1", hexHash)
    if len(match) > 0:
        return True, match[0]
    else:
        return False, ""

def getKeys(salt):
    keys = []
    potentialKeys = []
    index = 0
    while len(keys) < 64:
        hexHash = getHash(salt + str(index))
        triple, letter = containsTriple(hexHash)
        if triple:
            potentialKeys.append([index, letter])
            five, letter = containsFive(hexHash)
            if five:
                for potKey, potLetter in potentialKeys.copy():
                    if potLetter == letter and potKey + 1000 >= index and index != potKey:
                        keys.append(potKey)
                        potentialKeys.remove([potKey, potLetter])
                    elif index - 1000 > potKey:
                        potentialKeys.remove([potKey, potLetter])
        index += 1
    for i in range(1,1001):
        hexHash = getHash(salt + str(index+i))
        triple, letter = containsTriple(hexHash)
        if triple:
            potentialKeys.append([index+i, letter])
            five, letter = containsFive(hexHash)
            if five:
                for potKey, potLetter in potentialKeys:
                    if potLetter == letter and potKey + 1000 >= (index+i) and (index+i) != potKey:
                        keys.append(potKey)
                        potentialKeys.remove([potKey, potLetter])
                    elif index - 1000 > potKey:
                        potentialKeys.remove([potKey, potLetter])
    keys.sort()
    return keys

#keys = getKeys("abc")
keys = getKeys("yjdafjpo")
print(len(keys))
print(keys[63])

