from hashlib import md5


def calcMD5(num, string="bgvyzdsv"):
    hash = md5()
    hash.update(string.encode())
    hash.update(str(num).encode())
    return hash.hexdigest()


def checkMD51(md5sum):
    if md5sum[0] == "0" and md5sum[1] == "0" and md5sum[2] == "0"\
      and md5sum[3] == "0" and md5sum[4] == "0" and md5sum[5].isdigit():
        return True
    else:
        return False

def checkMD52(md5sum):
    if md5sum[0] == "0" and md5sum[1] == "0" and md5sum[2] == "0"\
      and md5sum[3] == "0" and md5sum[4] == "0" and md5sum[5] == "0":
        return True
    else:
        return False

num = 100
md5sum = calcMD5(num)
while not checkMD51(md5sum):
    num += 1
    md5sum = calcMD5(num)
print(num)
#254575

num = 100
md5sum = calcMD5(num)
while not checkMD52(md5sum):
    num += 1
    md5sum = calcMD5(num)
print(num)
#1038736