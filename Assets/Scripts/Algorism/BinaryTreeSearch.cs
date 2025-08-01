using UnityEngine;

public class BinaryTreeSearch : MonoBehaviour
{
    //��ø Ŭ����
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
    //���� Ž�� Ʈ��(BST : Binary Search Tree) �� O(logn)

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

    // ���� ��ȸ: �θ� �� ���� �� ������
    void PreOrder(TreeNode node)
    {
        if (node == null)
            return;
        result += $"{node.value} ,";
        PreOrder(node.left);
        PreOrder(node.right);
    }

    // ���� ��ȸ: ���� �� �θ� �� ������
    void InOrder(TreeNode node)
    {
        if (node == null)
            return;
        InOrder(node.left);
        result += $"{node.value} ,";
        InOrder(node.right);
    }

    // ���� ��ȸ: ���� �� ������ �� �θ�
    void PostOrder(TreeNode node)
    {
        if (node == null)
            return;
        PostOrder(node.left);
        PostOrder(node.right);
        result += $"{node.value} ,";
    }
}