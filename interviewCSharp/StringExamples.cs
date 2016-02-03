using System;

namespace interviewCSharp
{
	public class StringExamples
	{
		public StringExamples ()
		{

		}

			/** Most popular:
	 *   Reverse words in a string (words are separated by one or more spaces). 
	 *   Now do it in-place. 
	 *   @param args not used.
	 */


		public String reverseWords(String s) {
			if (s == null || s.Length == 0) {
				return "";
			}	 
			// split to words by space
			String[] arr = s.Split (' ');
			for (int  j = 0; j < arr.Length; j++)
				Console.WriteLine (arr [j]);
			String sb = "";
			for (int i = arr.Length - 1; i >= 0; --i) {
				if (!(arr[i].Equals(""))) {
					sb = sb.Insert (sb.Length, arr [i]);
					sb = sb.Insert(sb.Length," ");
				}
			}
			Console.WriteLine ("length" + sb.Length);
			return sb.Length == 0 ? "" : sb.Substring( 0,sb.Length -1);
		
		}

			// 
			public string reverseWordsInPlace(string s)
			{
				return "not possible, strings are immutable";
			}

			//C# strings are immutable. new strings are made... 
			public static string Reverse( string s )
			{
				char[] charArray = s.ToCharArray();
				Array.Reverse( charArray );
				return new string( charArray );
			}


			public string removeWhitespaceInplace(string intext)
			{
				return "not possible, strings are immutable";
			}

			public string reverseRecursively(string s)
			{
				if (s.Length > 0)
					return s[s.Length - 1] + reverseRecursively(s.Substring(0, s.Length - 1));
				else
					return s;
			}			
		   //("AAA BBB AA" -> "A B A")
			public string removeDuplicateChars(string intext)
			{
				int sindex= 0;
			    char [] tempbuf = intext.ToCharArray();

				char temp = tempbuf[sindex++];
				int orgindex = sindex + 1;
				while (orgindex < intext.Length)
				{
					if(temp != tempbuf[orgindex]){
						temp = tempbuf[orgindex];
						tempbuf[sindex++] =temp;
					}
					orgindex++;
				}
				while(sindex < intext.Length )
					tempbuf[sindex++] = ' ';
				return new string(tempbuf);

			}

			public char firstNonRepeatedCharacter(String str)
			{

				char result = str[0];
				for (int index = 0; index < str.Length; index++)
				{
					if (str.LastIndexOf(str[index]) == str.IndexOf(str[index]))
					{
						result = str[index];
						break;
					}
				}		
				return result;
			}

	}
}

