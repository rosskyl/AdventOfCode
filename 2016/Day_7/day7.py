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

def containsABA(word):
    ABA = []
    for i in range(1, len(word)-1):
        if word[i-1] == word[i+1] and word[i-1] != word[i] and word[i-1] != " " and word[i] != " ":
            ABA.append([word[i-1], word[i]])
    return len(ABA) != 0, ABA

def containsBAB(word, ABA):
    for i in range(1, len(word)-1):
        if word[i-1] == word[i+1] and word[i-1] != word[i] and word[i-1] != " " and word[i] != " ":
            ab = [word[i], word[i-1]]
            for each in ABA:
                if ab == each:
                    return True
    return False

def supportSSL(ip):
    ip = split(ip)
    if containsABA(ip[0])[0]:
        x,ab = containsABA(ip[0])
        if containsBAB(ip[1], ab):
            return True
    return False



lines = """abba[mnop]qrst
abcd[bddb]xyyx
aaaa[qwer]tyui
ioxxoj[asdfgh]zxcvbn"""
lines = lines.splitlines()

inFile = open("input.txt", "r")
lines = inFile.readlines()
inFile.close()

total1 = 0
total2 = 0
for line in lines:
    if supportTLS(line):
        total1 += 1
    if supportSSL(line):
        total2 += 1

print(total1)
#first answer is 118

print(total2)
#final solution is 260