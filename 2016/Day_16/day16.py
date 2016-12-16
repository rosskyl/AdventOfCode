def generateRandomData(a, length):
    while len(a) < length:
        b = list(a)[::-1]
        for i in range(len(b)):
            if b[i] == "0":
                b[i] = "1"
            elif b[i] == "1":
                b[i] = "0"
        a = a + "0" + "".join(b)
    return a

def checksum(data, length):
    data = list(data[:length])
    checksum = []
    while len(checksum) % 2 != 1:
        checksum = []
        for i in range(0, len(data), 2):
            if data[i] == data[i+1]:
                checksum.append("1")
            else:
                checksum.append("0")
        data = checksum
    return "".join(checksum)





data = "10111100110001111"
length = 272
data = generateRandomData(data, length)
print(checksum(data, length))
#first solution is 11100110111101110