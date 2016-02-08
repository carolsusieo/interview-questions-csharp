/*
You are given an array with integers between 1 and 1,000,000. 

One integer is in the array twice. How can you determine 
which one? Can you think of a way to do it using little extra memory.

You are given an array with integers between 1 and 1,000,000. 
One integer is missing. How can you determine which one? 
Can you think of a way to do it while iterating through 
the array only once. Is overflow a problem in the solution? Why not?

Returns the largest sum of contiguous integers in the array
Example: if the input is (-10, 2, 3, -2, 0, 5, -15), the largest sum is 8 (2,3,-2,0,5)
int GetLargestContiguousSum(int* anData, int len)
				
Implement Shuffle given an array containing a deck of cards and the number of cards. Now make it O(n).

Return the sum two largest integers in an array
int SumTwoLargest(int* anData, int size)

Sum n largest integers in an array of integers where every integer is between 0 and 9
int SumNLargest(int* anData, int size, int n)
*/


using System;
using System.Collections;
namespace interviewCSharp
{
	public class Arrays
	{
		public Arrays ()
		{
		}
		public int[] populateDup(int[] a)
		{
			// length is 1.000.001
			// fill with 1.000.001 numbers, but only go 
			for(int i = 0; i < a.Length -1; i++)
			{
				if (i < 200000)
					a [i] = i + 1; // don't fill with 0, but with one additional
				else if (i == 200000) {
					// fill this and next with i
					a [i] = i + 1;
					a [i + 1] = i + 1;
				} else {
					a [i + 1] = i;
				}
			}
			return a;
		}
		public int[] populateMiss(int[] a)
		{

			for(int i = 0; i < a.Length + 1; i++)
			{
				if(i < 200000)
					a[i] = i+1; // don't start at 0
				else if(i != 200000)
					a[i-1] = i+1;
			}
			return a;
		}
			// find one duplicate....  using little extra memory - 10000000 size array
			public int FindDuplicateInt()
			{
				int[] array = new int[1000001];
				array = populateDup (array);
				int i,j = 0;
				for (i = 1; i < array.Length; i++)
				{
					for (j = 0; array[i] != array[j]; j++) {
						if(j >= i)
							break;
					}
					if(j < i)
						break;
				}
				return (array[i] == array[j])? array[i]: 0;
			}

			// find missing int iterate through array only once
		public int FindMissingInt()
		{
			// to find the value of n
			//1) - Calculate the sum of all numbers stored in the array. 
			//2) - Subtract the sum from (n * n +1)/2 ; -- Formula : n (n+1)/2.

			//	The result of subtraction is the answer for this question.
			//1 + 2 + 3 + 4= 10 = array size is 4
			// 4 * 5 = 20/2 = 10

				//1 + 3 + 4 = 8;
				//		/8-4 = 2... missing is 2
				// size*2 - sum = answer
				int[] a = new int[999999];
				a = populateMiss (a);
				Int64 sum = 0;
				for(int i = 0; i < a.Length; i++)
					sum += a[i];
				// note that we are coping with overflow here by using the Int64.  C# integer operations don't throw
				// exceptions on overflow... and in this case it would overflow... adding all those integers exceeds limit.
				// max signed is > 2 billion
				// 1.000.000.000.000 / 2
				Int64 hold = (Int64)((Int64)(a.Length + 1) * (Int64)(a.Length + 2))/2;
				// i am 1000000 off.
				return (int) (hold - sum);

		}
		// return the result, and the startIndex and the endIndex that PROVIDES the result
		// I don't get this at all at this point.
		//http://stackoverflow.com/questions/2631726/how-to-determine-the-longest-increasing-subsequence-using-dynamic-programming
		//https://en.wikipedia.org/wiki/Maximum_subarray_problem
		// Kadane's algorithm....  
		public int FindBestSubsequence (int[] source, out int startIndex, out int endIndex)
		{
			int result = int.MinValue;
			int sum = 0;
			int tempStart = 0;
			startIndex = 0;
			endIndex = 0;

			for (int index = 0; index < source.Length; index++)
			{
				sum += source[index];
				if (sum > result)
				{
					result = sum;
					startIndex = tempStart;
					endIndex = index;
				}
				// if sum goes negative
				if (sum < 0)
				{
					sum = 0;
					tempStart = index + 1;
				}
			}
			return result;
		}
		// this get's total correctly,
		public int HighestContiguousSum(int[] inputArray, out int startIndex, out int endIndex)
		{
			int currentSum = inputArray[0];
			int bestSum = inputArray[0];
			startIndex = 0;
			endIndex = 0;
			for (int i = 1; i < inputArray.Length; i++)
			{
				int value = inputArray[i];

				if (bestSum < 0 && bestSum < value)
				{
					// new start of array, wipes out all that's gone before, so reset to this one element
					bestSum = value;
					currentSum = value;
					startIndex = i;
					endIndex = i;
				}
				else if (value > 0 || (value < 0 && value > -1 * currentSum))
				{
					currentSum += value;
					bestSum = Math.Max(currentSum, bestSum);
					if(currentSum == bestSum)
						endIndex = i;
				}
				else if (value <= -1 * currentSum)
				{
					currentSum = 0;
					startIndex = i;
				}
			}

			return bestSum;
		}

		// find 2 largest integers in array, and sum them
		public int sumTwoLargest(int[] arr)
		{
			Array.Sort (arr);
			return arr [arr.Length - 1] + arr [arr.Length - 2];
		}

		public int[] sortIt (int[] arr){
			Array.Sort (arr);
			return arr;
		}

		public int sumNLargest(int[] arr, int n)
		{
			// possibly situations... array size is less than n;
			// what if that's the case, and there are duplicates?  do we sum the duplicates?
			// assume so.
			int [] k = new int[n+1]; // place new one at bottom... when adding them all up, don't add the last one.
			int i;

			// is this default?
			for(i = 0; i < n; i++)
				k[i] = -1;
			for(i = 0; i < arr.Length; i++)
			{
				k[0] = arr[i];
				sortIt(k);
			}
			int sum= 0;
			for(i = n; i > 0; i--)
				sum +=k[i];
			return sum;
		}
	}
}

