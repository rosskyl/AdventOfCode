def isWall(x, y):
    num = x*x + 3*x + 2*x*y + y + y*y + 1364
    num = str(bin(num))
    if num.count("1") % 2 == 0:
        return False
    else:
        return True

def createGraph(endX, endY):
    graph = {}
    for x in range(endX+1):
        for y in range(endY+1):
            if not isWall(x,y):
                connected = []
                if x-1 >= 0 and not isWall(x-1, y):
                    connected.append((x-1, y))
                if y-1 >= 0 and not isWall(x, y-1):
                    connected.append((x, y-1))
                if x+1 < endX+1 and not isWall(x+1, y):
                    connected.append((x+1, y))
                if y+1 < endY+1 and not isWall(x, y+1):
                    connected.append((x, y+1))
                graph[(x,y)] = connected
    return graph

def getMin(visited):
    coord = (-1,-1)
    minV = 10**10
    for key, val in visited.items():
        if val[1] > 0 and not val[0] and val[1] < minV:
            minV = val[1]
            coord = key
    return coord

def djikstras1(graph):
    start = (1,1)
    goal = (31, 39)
    maxX = max([key[0] for key in graph.keys()])+1
    maxY = max([key[1] for key in graph.keys()])+1
    visited = {}
    for x in range(maxX):
        for y in range(maxY):
            visited[(x,y)] = [False, -1]
    visited[start] = [True, 0]
    current = start
    while not visited[goal][0]:
        for edge in graph[current]:
            if not visited[edge][0]:
                visited[edge][1] = visited[current][1] + 1
        current = getMin(visited)
        visited[current][0] = True
    return visited[goal][1]

def djikstras2(graph):
    start = (1,1)
    goal = 50
    locations = []
    maxX = max([key[0] for key in graph.keys()])+1
    maxY = max([key[1] for key in graph.keys()])+1
    visited = {}
    for x in range(maxX):
        for y in range(maxY):
            visited[(x,y)] = [False, -1]
    visited[start] = [True, 0]
    current = start
    currentV = 0
    while currentV <= 50:
        for edge in graph[current]:
            if not visited[edge][0]:
                visited[edge][1] = visited[current][1] + 1
        current = getMin(visited)
        visited[current][0] = True
        currentV = visited[current][1]
        locations.append(current)
    return locations

def printGraph(graph):
    maxX = max([key[0] for key in graph.keys()])
    maxY = max([key[1] for key in graph.keys()])
    for y in range(maxY):
        for x in range(maxX):
            if (x,y) in graph.keys():
                print(".", end="")
            else:
                print("#", end="")
        print()

graph = createGraph(150, 150)
#printGraph(graph)

print(djikstras1(graph))
#first answer is 86
print(len(djikstras2(graph)))
#final answer is 127