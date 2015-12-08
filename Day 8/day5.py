import codecs

def encode(word):
    newWord = ""
    for char in word:
        if char == "\"":
            newWord += "\\\""
        elif char == "\\":
            newWord += "\\\\"
        else:
            newWord += char
    return newWord


file = open("textFile.txt", "r")
lines = file.readlines()
file.close()


cod = codecs.getdecoder('unicode_escape')

sumCodeChars = 0
sumChars = 0
for line in lines:
    line = line.rstrip()
    sumCodeChars += len(line)
    sumChars += len(cod(line[1:-1])[0])


print(sumCodeChars - sumChars)
#1342

encoder = codecs.getencoder('unicode_escape')

sumCodeChars = 0
sumChars = 0
for line in lines:
    line = line.rstrip()
    sumCodeChars += len(encode(line)) + 2
    sumChars += len(line)

print(sumCodeChars - sumChars)
#2074