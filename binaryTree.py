#-*- coding:utf-8 –*
# 从上到下依次打印二叉树最左侧节点
# 可以通过BFS，BFS遍历过程中，知道每一级最左侧的节点

class Node:
    def __init__(self, v):
        self.val = v
        self.left = None
        self.right = None

def bfs(root):
    que = [root]
    result = []
    while que:
        tmp = []
        for i in range(len(que)):
            node = que.pop(0)
            tmp.append(node.val)
            if node.left:
                que.append(node.left)
            if node.right:
                que.append(node.right)
        result.append(tmp[0])
    return result

if __name__ == "__main__":
    # 构建二叉树
    a = Node(2)
    a.left = Node(11)
    a.right = Node(23)

    a.left.left = Node(10)
    a.left.right = Node(15)
    a.right.left = Node(7)
    a.right.left.right = Node(12)
    a.right.right = Node(14)
    a.right.left.right.left = Node(13)

    # 打印最左侧节点
    print(bfs(a))
        
            
