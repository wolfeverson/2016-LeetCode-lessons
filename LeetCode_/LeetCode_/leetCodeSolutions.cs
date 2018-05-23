using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;

namespace LeetCode_
{
    public static class leetCodeSolutions
    {
        static void Main(string[] args)
        {
            

        }

        public static int[] TwoSum(int[] nums, int target)
        {
            //https://leetcode.com/problems/two-sum/
            /*Given an array of integers, return indices of the two numbers such that they add up to a specific target.
            You may assume that each input would have exactly one solution.*/
            var dict = new Dictionary<int, int>();
            for (int i = 0; i < nums.Count(); i++)
            {
                int ideal = target - nums[i];
                if (dict.ContainsKey(ideal))
                {
                    return new int[] { dict[ideal], i };
                }
                else
                {
                    if (!dict.ContainsKey(nums[i]))
                    {
                        dict.Add(nums[i], i);
                    }
                }
            }
            return null;
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            //https://leetcode.com/problems/add-two-numbers/
            /*You are given two linked lists representing two non - negative numbers.The digits are stored in reverse order and each of their nodes contain a single digit.Add the two numbers and return it as a linked list.*/
            long maths = ListNodeToString(l1) + ListNodeToString(l2);
            char[] charArray = maths.ToString().ToCharArray();
            Array.Reverse(charArray);
            int val = int.Parse(charArray[0].ToString());
            ListNode response = new ListNode(val);
            ListNode curr = response;
            foreach(char c in charArray)
            {
                val = int.Parse(c.ToString());
                curr.next = new ListNode(val);
                curr = curr.next;
            }

            return response.next;
        }

        public static string StringReverse(string s)
        {
            //https://leetcode.com/problems/reverse-string/
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            s = new string(charArray);
            return s;
        }

        public static int SumofTwoIntegers(int a, int b)
        {
            //https://leetcode.com/problems/sum-of-two-integers/
            //Calculate the sum of two integers a and b, but you are not allowed to use the operator + and -.

            //this is a naive solution.  TODO - bitshifting answer.

            string binA = Convert.ToString(a, 2);
            string binB = Convert.ToString(b, 2);
            int maxlength = 0; //set strings to same length for lazyness.
            if (binA.Length == binB.Length) 
            {
                maxlength = binA.Length;
            }
            else if (binA.Length> binB.Length)
            {
                maxlength = binA.Length;
                while (binB.Length < maxlength )
                {
                    binB = "0" + binB;
                }
            }
            else
            {
                maxlength = binB.Length;
                while (binA.Length < maxlength)
                {
                    binA = "0" + binA;
                }
            }
            string final = "";
            bool carry = false;
            for (int i = maxlength - 1; i >-1; i--)
            {
                string val1 = binA.Substring(i, 1);
                string val2 = binB.Substring(i, 1);
                int flag = 0;
                if (val1 == "1" && val2 == "1" && carry)
                {
                    flag = 3;
                }
                else if (val1 == "1" && val2 == "1" && !carry)
                {
                    flag = 2;
                }
                else if ((val1 == "1" || val2 == "1") && carry)
                {
                    flag = 2;
                }
                else if ((val1 == "1" || val2 == "1") && !carry)
                {
                    flag = 1;
                }
                else if (carry)
                {
                    flag = 1;
                }
                else
                {
                    flag = 0;
                }

                switch (flag)
                { 
                    case 3:
                        final = string.Concat("1", final);
                        carry = true;
                        break;
                    case 2:
                        final = string.Concat("0", final);
                        carry = true;
                        break;
                    case 1:
                        final = string.Concat("1", final);
                        carry = false;
                        break;
                    case 0:
                        final = string.Concat("0", final);
                        carry = false;
                        break;
                }
            }
            if (carry) final = string.Concat("1", final);
            return Convert.ToInt32(final, 2);
        }

        public static bool CanWinNim(int n)
        {
            //https://leetcode.com/problems/nim-game/
            if (n % 4 == 0) return false;
            if (n < 4) return true;
            return true;
        }

        public static int AddDigits(int num)
        {
            //https://leetcode.com/problems/add-digits/
            //Given a non-negative integer num, repeatedly add all its digits until the result has only one digit.

            /*
             * 
             * naive solution...
             * 
            string chars = num.ToString();
            int sum = 0;
            bool isDone = false; 
            while (!isDone)
            {
                string first = chars.Substring(0, 1);
                sum += int.Parse(first);

                if (chars.Length>1)
                {
                    chars = chars.Substring(1);
                }
                else
                {
                    if (sum.ToString().Length > 1)
                    {
                        chars = sum.ToString();
                        sum = 0;
                    }
                    else
                    {
                        isDone = true;
                    }
                }
            }
            return sum;
            */

            //digital root answer
            return 1 + (num - 1) % 9;
        }

        public static int MaxDepth(TreeNode  root)
        {
            //https://leetcode.com/problems/maximum-depth-of-binary-tree/
            if (root == null) return 0;
            List <TreeNode> list = new List<TreeNode>();
            root.val = 1;
            int maxVal = 0;
            list.Add(root);
            while (list.Count() > 0)
            {
                TreeNode curr = list.First();
                maxVal = curr.val > maxVal ? curr.val : maxVal;
                if (curr.left != null)
                {
                    TreeNode left = curr.left;
                    left.val = curr.val + 1;
                    list.Add(left);
                }
                if (curr.right != null)
                {
                    TreeNode right = curr.right;
                    right.val = curr.val + 1;
                    list.Add(right);
                }
                list.Remove(curr);
            }
            return maxVal;
        }

        public static TreeNode InvertTree(TreeNode root)
        {
            //https://leetcode.com/problems/invert-binary-tree/
            if (root == null) return null;
            TreeNode leftchild = InvertTree(root.left);
            TreeNode rightchild = InvertTree(root.right);
            root.left = rightchild;
            root.right = leftchild;
            return root;
        }

        public static void MoveZeroes(int[] nums)
        {
            //https://leetcode.com/problems/move-zeroes/
            //Given an array nums, write a function to move all 0's to the end of it while maintaining the relative order of the non-zero elements.
            for (int i = 0; i < nums.Count(); i++)
            {  
                int value = nums[i];
                int startindex = i;
                if (value == 0)
                {
                    //grab next non-zero value...
                    int newindex = 0;
                    for (int i2=i+1; i2 < nums.Count() && newindex == 0; i2++)
                    {
                        int temp = nums[i2];
                        if (temp != 0)
                        {
                            newindex = i2;
                        }
                    }
                    //...and swap
                    if (newindex != 0)
                    {
                        nums[i] = nums[newindex];
                        nums[newindex] = 0;
                    }
                }
            }
            return;
        }

        public static int[] IntersectionofTwoArrays(int[] nums1,int[] nums2)
        {
            // https://leetcode.com/problems/intersection-of-two-arrays/
            //Given two arrays, write a function to compute their intersection.
            return nums1.Intersect(nums2).ToArray();
        }

        static void DeleteNodeinLinkedList(ListNode node)
        {
            //https://leetcode.com/problems/delete-node-in-a-linked-list/
            //Delete a node in a singly-linked list without having access to previous node.
            node.val = node.next.val;
            node.next = node.next.next;
            return;
        }

        public static bool IsSameTree(TreeNode p, TreeNode q)
        {
            //https://leetcode.com/problems/same-tree/
            //Given two binary trees, write a function to check if they are equal or not.
            if (p == null && q == null) return true;
            if (p == null || q == null) return false;
            if (p.val != q.val) return false;
            return (IsSameTree(p.right, q.right) && IsSameTree(p.left, q.left));
        }

        public static string ExcelSheetColumnTitle(int n)
        {
            //https://leetcode.com/problems/excel-sheet-column-title/
            if ( n < 1) throw new Exception("Bad Input");
            string output = IntToLetter(n % 26);
            int counter = 1;
            long limit = 26;
            while (n>limit)
            {
                long newvalue = n / (int)(Math.Pow(26, counter));
                newvalue = newvalue % 26;
                if (output.StartsWith("Z")) newvalue = newvalue- 1;
                output = IntToLetter((int)newvalue) + output;
                counter++;
                limit = ((int)Math.Pow(26, counter)) + 26;
                limit = limit < 0 ? long.MaxValue : limit;
            }
            return output;
        }

        public static int ExcelSheetColumnNumber(string s)
        {
            //https://leetcode.com/problems/excel-sheet-column-number/
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            int counter = 1, total = 0;
            foreach (char c in chars)
            {
                int value = LetterToInt(c);
                total += (value * counter);
                counter = counter * 26;
            }
            return total;
        }

        public static bool isAnagram(string s, string t)
        {
            //https://leetcode.com/problems/valid-anagram/
            char[] c1 = s.ToCharArray();
            char[] c2 = t.ToCharArray();
            Array.Sort(c1);
            Array.Sort(c2);
            int length = c1.Length;
            if (c2.Length != length) return false;
            for (int i = 0;i<length;i++)
            {
                if (c1[i] != c2[i]) return false;
            }
            return true;
        }

        public static int MajorityElement(int[] nums)
        {
            //https://leetcode.com/problems/majority-element/
            //Given an array of size n, find the majority element. The majority element is the element that appears more than ⌊ n/2 ⌋ times.
            //You may assume that the array is non-empty and the majority element always exist in the array.
            var list = new Dictionary<int, int>();
            foreach (int i in nums)
            {
                if (list.ContainsKey(i))
                {
                    list[i]++;
                }
                else
                {
                    list.Add(i, 1);
                }
            }
            int target = nums.Length / 2;
            foreach(KeyValuePair<int,int> kvp in list)
            {
                if (kvp.Value > target) return kvp.Key;
            }
            return 0;
        }

        public static bool ContainsDuplicates(int[] nums)
        {
            //https://leetcode.com/problems/contains-duplicate/
            var list = new Dictionary<int, int>();
            foreach (int i in nums)
            {
                if (list.ContainsKey(i))
                {
                    return true;
                }
                else
                {
                    list.Add(i, 1);
                }
            }
            return false;
        }

        public static ListNode ReverseList(ListNode head)
        {
            //https://leetcode.com/problems/reverse-linked-list/
            ListNode prev = null, curr = head;
            while(curr !=null)
            {
                ListNode next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
            return prev;
        }

        public static bool IsPowerofThree(int n)
        {
            //https://leetcode.com/problems/power-of-three/
            if (n < 1) return false;
            if (n == 1) return true;
            while (n % 3 == 0)
            {
                n /= 3;
            }
            return n == 1;
        }

        public static int HammingWeight(uint n)
        {
            //https://leetcode.com/problems/number-of-1-bits/
            string binary = Convert.ToString(n, 2);
            int counter = 0;
            foreach(char c in binary)
            {
                if (c.ToString() == "1") counter++;
            }
            return counter;
        }

        public static bool HappyNumbers(int n)
        {
            var list = new Dictionary<int, int>();
            while (n!=1)
            {
                n = SumSquaresOfDigits(n);
                if (list.ContainsKey(n))
                {
                    list[n]++;
                    if (list[n]>10)//   10?  How does one define the infinite?
                    {
                        return false;
                    }
                }
                else
                {
                    list.Add(n, 1);
                }
            }
            return true;
        }

        public static ListNode RemoveDuplicatesfromSortedList(ListNode head)
        {
            //https://leetcode.com/problems/remove-duplicates-from-sorted-list/
            if (head == null) return null;
            ListNode curr = head;
            while(curr.next != null)
            {
                if (curr.next.val == curr.val)
                {
                    curr.next = curr.next.next;
                    //retest...
                }
                else
                {
                    curr = curr.next;
                }
            }
            return head;
        }

        public static int ReverseInteger(int x)
        {
            //https://leetcode.com/problems/reverse-integer/
            string s = x.ToString();
            int response = 0;
            bool isNegative = false;
            if (s.StartsWith("-"))
            {
                isNegative = true;
                s = s.Replace("-", "");
            }
            s = StringReverse(s);
            if (isNegative)
            {
                s = "-" + s;
            }
            try
            {
                response = int.Parse(s);
            }
            catch
            {
                response = 0; //int overflow catch...
            }
            return response;
            
        }

        public static int MyAtoi(string str)
        {
            //https://leetcode.com/problems/string-to-integer-atoi/
            int dummy = 0, output = 0;
            if (str == null) return 0;
            str = str.Trim();
            if (str == string.Empty) return 0;
            bool isNegative = false;
            if (str.StartsWith("-"))
            {
                isNegative = true;
                str = str.Substring(1);
            }
            else if(str.StartsWith("+"))
            {
                str = str.Substring(1);
            }
            string validnums = "";
            bool isStillNumbers = true;
            foreach(char c in str)
            {
                if (isStillNumbers)
                {
                    if (int.TryParse(c.ToString(), out dummy))
                    {
                        validnums += c.ToString();
                    }
                    else
                    {
                        isStillNumbers = false;
                    }
                }
            }

            if (validnums == "") return 0;
            try
            {
                output = int.Parse(validnums);
                if (isNegative) output *= -1;
                return output;
            }
            catch (OverflowException)
            {
                if (isNegative) return -2147483648;//neg max is +1 from pos max
                return 2147483647;
            }
        }

        public static bool IsPalindrome(int x)
        {
            //https://leetcode.com/problems/palindrome-number/
            //negatives are not palindromes!
            return StringReverse(x.ToString()) == x.ToString();
        }

        public static ListNode MergeTwoSortedLists(ListNode l1, ListNode l2)
        {
            //https://leetcode.com/problems/merge-two-sorted-lists/
            if (l1 == null && l2 == null) return null;
            if (l1 == null) return l2;
            if (l2 == null) return l1;
            ListNode response;
            ListNode curr;
            if (l1.val <= l2.val) //Initialization
            {
                response = new ListNode(l1.val);
                l1 = l1.next;
            }
            else
            {
                response = new ListNode(l2.val);
                l2 = l2.next;
            }

            curr = response;
            while (l1 != null && l2 != null)
            {
                if (l1.val <= l2.val)
                {
                    curr.next = new ListNode(l1.val);
                    curr = curr.next;
                    l1 = l1.next;
                }
                else
                {
                    curr.next = new ListNode(l2.val);
                    curr = curr.next;
                    l2 = l2.next;
                }
            }

            //catch any remainders - already sorted
            if (l1 != null) curr.next = l1;
            if (l2 != null) curr.next = l2;

            return response;
        }

        public static bool LinkedListCycle(ListNode head)
        {
            //https://leetcode.com/problems/linked-list-cycle
            if (head == null) return false;
            if (head.next == null) return false;
            ListNode l1 = head;
            ListNode l2 = head.next;
            for (int i = 0; i < int.MaxValue; i++)
            {
                try
                {
                    l1 = l1.next;
                    l2 = l2.next.next;
                    if (l1 == l2) return true;
                }
                catch (NullReferenceException)
                {
                    return false;
                }

            }
            return false;
        }

        public static string ReverseVowelsofString(string s)
        {
            //https://leetcode.com/problems/reverse-vowels-of-a-string/
            //Write a function that takes a string as input and reverse only the vowels of a string.
            var vowels = new List<string>() {"A", "E", "I", "O", "U"};
            var chars = s.ToCharArray();
            int start = 0, end = s.Length - 1;
            while( start < end)
            {
                while (start < end && !vowels.Contains(chars[start].ToString().ToUpper()))
                {
                    start++;
                }

                while (start < end && !vowels.Contains(chars[end].ToString().ToUpper()))
                {
                    end--;
                }
                char cStart = chars[start];
                chars[start] = chars[end];
                chars[end] = cStart;
                start++;
                end--;
            }
            return new string(chars);
        }

        public static int RemoveElement(int[] nums, int val)
        {
            //https://leetcode.com/problems/remove-element/
            //copy of move zeroes to end... 
            for (int i = 0; i < nums.Count(); i++)
            {
                int value = nums[i];
                int startindex = i;
                if (value == val)
                {
                    //grab next non-val value...
                    int newindex = 0;
                    for (int i2 = i + 1; i2 < nums.Count() && newindex == 0; i2++)
                    {
                        int temp = nums[i2];
                        if (temp != val)
                        {
                            newindex = i2;
                        }
                    }
                    //...and swap
                    if (newindex != 0)
                    {
                        nums[i] = nums[newindex];
                        nums[newindex] = val;
                    }
                }
            }
            int badcount = 0;
            //reiterate through to verify "bad" count.
            foreach (int i in nums.Reverse())
            {
                if (i == val)
                {
                    badcount++;
                }
                else
                {
                    return nums.Length - badcount;
                }
            }
            return nums.Length - badcount;
        }

        public static bool HasPathSum(TreeNode root, int sum)
        {
            //https://leetcode.com/problems/path-sum/
            //Given a binary tree and a sum, determine if the tree has a root-to-leaf path such that adding up all the values along the path equals the given sum.
            if (root == null) return false;
            var list = new List<TreeNode> {root};
            while (list.Count > 0)
            {
                var curr = list.First();
                list.Remove(curr);
                if (curr.val == sum && curr.left == null && curr.right == null) return true; //must be at end of list!
                if (curr.left != null)
                {
                    curr.left.val += curr.val;
                    list.Add(curr.left);
                }
                if (curr.right != null)
                {
                    curr.right.val += curr.val;
                    list.Add(curr.right);
                }
            }
            return false;
        }

        public static bool IsValidSudoku(char[,] board)
        {
            //https://leetcode.com/problems/valid-sudoku/  
            //3 validation waves - could be make more efficient(but less readable) to condense.
            //hor
            var list = new List<char>();

            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    char val = board[x, y];
                    if (list.Contains(val) && val != '.')
                    {
                        return false;
                    }
                    else
                    {
                        list.Add(val);
                    }
                }
                list.Clear();
            }

            //vert
            list.Clear();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    char val = board[y, x];
                    if (list.Contains(val) && val != '.')
                    {
                        return false;
                    }
                    else
                    {
                        list.Add(val);
                    }
                }
                list.Clear();
            }

            //9cubes?
            var dict = new Dictionary<int, List<char>>();
            for (int i = 1; i < 10; i++)
            {
                dict.Add(i, new List<char>());
            }
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    int cube = 0;
                    char val = board[x, y];
                    if (x > -1 && x <3)
                    {
                        cube = 1;
                    }
                    else if (x > 2 && x < 6)
                    {
                        cube = 2;
                    }
                    else if (x > 5 && x < 9)
                    {
                        cube = 3;
                    }
                    if (y > 2) cube += 3;
                    if (y > 5) cube += 3;
                    if (dict[cube].Contains(val) && val != '.')
                    {
                        return false;
                    }
                    else
                    {
                        dict[cube].Add(val);
                    }
                }
            }









            return true;
        }

        public static int[] PlusOne(int[] digits)
        {
            //https://leetcode.com/problems/plus-one/
            //Given a non-negative number represented as an array of digits, plus one to the number.
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                if (digits[i] == 9 && i !=0)
                {
                    digits[i] = 0;
                    if (i != 0) continue;
                    int[] one = new int[] { 1 };
                    return one.Concat(digits).ToArray();
                }
                else
                {
                    digits[i] = digits[i] + 1;
                    i = -1;
                }
            }
            return digits;
        }

        public static int MinimumDepth(TreeNode root)
        {
            //https://leetcode.com/problems/minimum-depth-of-binary-tree/
            if (root == null) return 0;
            List<TreeNode> list = new List<TreeNode>();
            root.val = 1;
            int minVal = int.MaxValue;
            list.Add(root);
            while (list.Count() > 0)
            {
                TreeNode curr = list.First();
                
                if (curr.left == null && curr.right == null)
                {
                    minVal = curr.val < minVal ? curr.val : minVal;
                }
                if (curr.left != null)
                {
                    TreeNode left = curr.left;
                    left.val = curr.val + 1;
                    list.Add(left);
                }
                if (curr.right != null)
                {
                    TreeNode right = curr.right;
                    right.val = curr.val + 1;
                    list.Add(right);
                }
                list.Remove(curr);
            }
            return minVal;
        }

        public static string BullsAndCows(string secret, string guess)
        {
            //https://leetcode.com/problems/bulls-and-cows/
            int bulls = 0, cows = 0;
            //bulls are easy....
            for(int i = 0;i<secret.Length;i++)
            {
                if (secret[i] == guess[i])
                {
                    bulls++;
                }
            }
            //cows?
            var ListS = secret.ToCharArray().ToList();
            var ListG = guess.ToCharArray().ToList();
            for (int i = 0; i < 10; i++)
            {
                char val = char.Parse(i.ToString());
                if (ListS.Contains(val) && ListG.Contains(val))
                {
                    int countS = ListS.Count(c => c == val);
                    int countG = ListG.Count(c => c == val);
                    cows += Math.Min(countS, countG);
                }
            }
            cows -= bulls;
            return  bulls + "A"  + cows+ "B";
        }







        #region HappyNumbers
        public static int SumSquaresOfDigits(int n)
        {
            int total = 0;
            string s = n.ToString();
            foreach (char c in s)
            {
                int val = int.Parse(c.ToString());
                total += val * val;
            }
            return total;
        }
        #endregion
        #region excelhelpers
        public static int LetterToInt(char input)
        {
            int ascii = Convert.ToInt32(input);
            return ascii - 64;
        }

        public static string IntToLetter(int input)
        {
            if (input > 26 || input < 0) throw new Exception("Bad Input");
            if (input == 0) return "Z";
            int ascii = 64 + input;
            char response = Convert.ToChar(ascii);
            return response.ToString();
        }
        #endregion
        #region AddTwoNumbers 
        //AddTwoNumbers
        public static long ListNodeToString(ListNode curr)
        {
            string value = curr.val.ToString();
            while (curr.next != null)
            {
                curr = curr.next;
                value += curr.val.ToString();
            }
            value = StringReverse(value);
            return long.Parse(value);
        }
        #endregion

        //ReverseLinkedList
        //AddTwoNumbers
        //DeleteNodeInLinkedList
        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int x) { val = x; }
        }
        //IsSameTree
        //MaxDepth
        //InvertBinaryTree
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }

    }
}
