
namespace interviewCSharp
{
using LinkedListExample;
using System;

namespace LinkedListExampleClient
{
	class Program
	{
		static void Main(string[] args)
		{
			List list = new List();

			list.Add("A");
			list.Add("B");
			list.Add("C");
			list.Add("D");
			list.Add("E");
			list.Add("F");
			list.Add("G");
			list.Add("H");

			list.ListNodes();
			Console.WriteLine();

			Console.WriteLine();
			Console.WriteLine("Deleting node 8");
			list.Delete(8);
			list.ListNodes();

			Console.WriteLine();
			Console.WriteLine("Position 5: " + list.Retrieve(5).NodeContent);

			Console.WriteLine();
			Console.WriteLine("Deleting node 5");
			list.Delete(5);

			Console.WriteLine();
			Console.WriteLine("Position 5: " + list.Retrieve(5).NodeContent);

			Console.WriteLine();
			list.ListNodes();

			Console.ReadLine();

			//double linked list
			DoubleLinkedList listdl = new DoubleLinkedList();
			listdl.Insert("1");
			listdl.Insert("2");
			listdl.Insert("3");

			DoubleLink link4 = listdl.Insert("4");
			listdl.Insert("5");
			Console.WriteLine("List: " + listdl);

			listdl.InsertAfter(link4, "[4a]");
			Console.WriteLine("List: " + listdl);
			Console.Read();

			link4 = listdl.Find ("[4a]");
			listdl.Delete (link4);
			Console.WriteLine ("List after delete:" + listdl);

			listdl.Delete ();
			Console.WriteLine ("list after delete first:" + listdl);
			Console.Read ();

			string str = "hello reverse words hello";
			Console.WriteLine(str);
			StringExamples mystring = new StringExamples();
			Console.WriteLine(mystring.reverseWords(str));

			Console.WriteLine("Reverse Recursively");
			Console.WriteLine(mystring.reverseRecursively(str));
			string str2 = "Reverse words in-place";	
			Console.WriteLine(str2);
			Console.WriteLine(mystring.reverseWordsInPlace(str2));
			string str3 = "remove whitespace\t\t\tin place";
			Console.WriteLine(str3);
			Console.WriteLine(mystring.removeWhitespaceInplace(str3));
			string str4 = "AAA BBB CC D EE A";
			Console.WriteLine("remove duplicate characters" + str4);
			Console.Write(mystring.removeDuplicateChars(str4));

			char c=mystring.firstNonRepeatedCharacter(str);
			Console.WriteLine("\nThe first non repeated character in '" + str + "' is :  " + c);



		}
	}
}
}