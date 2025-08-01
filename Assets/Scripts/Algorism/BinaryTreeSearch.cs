using UnityEngine;

public class BinaryTreeSearch : MonoBehaviour
{
    //중첩 클래스
    public class TreeNode
    {
        public int value;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int value)
        {
            this.value = value;
        }
    }
    //이진 탐색 트리(BST : Binary Search Tree) → O(logn)

    private TreeNode root;
    private string result;

    void Start()
    {
        int[] values = { 8, 3, 10, 1, 6, 14, 4, 7, 13 };
        foreach (var v in values)
            root = Insert(root, v);

        PreOrder(root);
        Debug.Log($"PreOrder : {result.TrimEnd(',')}");

        result = string.Empty;
        InOrder(root);
        Debug.Log($"InOrder : {result.TrimEnd(',')}");

        result = string.Empty;
        PostOrder(root);
        Debug.Log($"PostOrder : {result.TrimEnd(',')}");
    }

    TreeNode Insert(TreeNode node, int value)
    {
        if (node == null)
            return new TreeNode(value);
        if (value < node.value)
            node.left = Insert(node.left, value);
        else
            node.right = Insert(node.right, value);
        return node;
    }

    // 전위 순회: 부모 → 왼쪽 → 오른쪽
    void PreOrder(TreeNode node)
    {
        if (node == null)
            return;
        result += $"{node.value} ,";
        PreOrder(node.left);
        PreOrder(node.right);
    }

    // 중위 순회: 왼쪽 → 부모 → 오른쪽
    void InOrder(TreeNode node)
    {
        if (node == null)
            return;
        InOrder(node.left);
        result += $"{node.value} ,";
        InOrder(node.right);
    }

    // 후위 순회: 왼쪽 → 오른쪽 → 부모
    void PostOrder(TreeNode node)
    {
        if (node == null)
            return;
        PostOrder(node.left);
        PostOrder(node.right);
        result += $"{node.value} ,";
    }
}