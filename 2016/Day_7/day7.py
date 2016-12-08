def containsABBA(word):
    for i in range(1,len(word)-2):
        if word[i] == word[i+1] and word[i-1] == word[i+2] and word[i] != word[i-1] and word[i] != " " and word[i-1] != " ":
            return True
    return False

def split(address):
    outer = ""
    middle = ""
    level = 0
    for letter in address:
        if letter == "[":
            level += 1
            middle = middle + " "
        elif letter == "]":
            level -= 1
            if level == 0:
                outer = outer + " "
            else:
                middle = middle + " "
        elif level == 0:
            outer = outer + letter
        else:
            middle = middle + letter
    return outer, middle

def supportTLS(ip):
    ip = split(ip)
    if containsABBA(ip[1]):
        return False
    elif containsABBA(ip[0]):
        return True
    else:
        return False



lines = """abba[mnop]qrst
abcd[bddb]xyyx
aaaa[qwer]tyui
ioxxoj[asdfgh]zxcvbn"""
lines = lines.splitlines()

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()

total = 0
for line in lines:
    if supportTLS(line):
        total += 1

print(total)
#first answer is 118
