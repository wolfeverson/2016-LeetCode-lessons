﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;

namespace LeetCode_
{
    public static class leetCodeSolutions
    {
        static void Main(string[] args)
        {
           
        }

        static public int[] TwoSum(int[] nums, int target)
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

        
        static public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
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
        //AddTwoNumbers
         public class ListNode
        {
             public int val;
             public ListNode next;
             public ListNode(int x) { val = x; }
        }
        #endregion

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
        #region #maxdepth
        //MaxDepth
        public class TreeNode
        {
            public int val;
            public TreeNode left;
            public TreeNode right;
            public TreeNode(int x) { val = x; }
        }
        #endregion

    }
}