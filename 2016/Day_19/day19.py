class Elf:
    def __init__(self, num, prev=None, next=None):
        self.num = num
        self.prev = prev
        self.next = next

    def delete(self):
        self.prev.next = self.next
        self.next.prev = self.prev

def createElves(numElves):
    prev = None
    elves = []
    for i in range(numElves):
        tmpElf = Elf(i+1, prev)
        if prev != None:
            prev.next = tmpElf
        elves.append(tmpElf)
        prev = tmpElf
    elves[0].prev = elves[-1]
    elves[-1].next = elves[0]
    return elves

def removeElvesPart1(elf1):
    next = elf1.next
    curr = elf1
    while curr != next:
        next.delete()
        curr = curr.next
        next = curr.next
    return curr.num

def removeElvesPart2(elf1, midElf, numElves):
    curr = elf1
    while curr != curr.next:
        #print("deleting", midElf.num)
        midElf.delete()
        midElf = midElf.next
        if numElves % 2 == 1:
            midElf = midElf.next
        numElves -= 1
        curr = curr.next
    return curr.num

numElves = 3012210
#numElves = 5
elves = createElves(numElves)
print("Done creating elves")
#print(removeElvesPart1(elves[0]))
#first answer is 1830117

print(removeElvesPart2(elves[0], elves[numElves//2], numElves))
#final answer is 1417887