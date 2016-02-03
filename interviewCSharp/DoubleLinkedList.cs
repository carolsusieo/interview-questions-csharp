using System;
using System.Text;
namespace interviewCSharp
{
	public class DoubleLink
	{
	    public string Title { get; set; }
		public DoubleLink PreviousLink { get; set; }
		public DoubleLink NextLink { get; set; }

		public DoubleLink(string title)
		{
			Title = title;
		}

		public override string ToString()
		{
			return Title;
		}
	}

	public class DoubleLinkedList
	{
		private DoubleLink _first;
		public bool IsEmpty
		{
			get
			{
				return _first == null;
			}
		}
		public DoubleLinkedList()
		{
			_first = null;
		}

		public DoubleLink Insert(string title)
		{
			// Creates a link, sets its link to the first item and then makes this the first item in the list.
			DoubleLink link = new DoubleLink(title);
			link.NextLink = _first;
			if (_first != null)
				_first.PreviousLink = link;
			_first = link;
			return link;
		}

		public DoubleLink Delete()
		{
			// Gets the first item, and sets it to be the one it is linked to
			DoubleLink temp = _first;
			if (_first != null)
			{
				_first = _first.NextLink;
				if (_first != null)
					_first.PreviousLink = null;
			}
			return temp;
		}
		public DoubleLink Delete(DoubleLink link)
		{
			DoubleLink temp = _first;
			if (link == temp || link == null)
				temp =  Delete ();
			else {
				if (link.PreviousLink != null) {
					link.PreviousLink.NextLink = link.NextLink;
				}
				if (link.NextLink != null)
					link.NextLink.PreviousLink = link.PreviousLink;
				temp = link.PreviousLink;
			}
			return temp;
		}
		public DoubleLink Find(string title)
		{
			DoubleLink currentLink = _first;
			while (currentLink != null && !currentLink.Equals(title) && currentLink.Title.CompareTo(title) != 0 )
				currentLink = currentLink.NextLink;
			return currentLink;
		}
		public override string ToString()
		{
			DoubleLink currentLink = _first;
			StringBuilder builder = new StringBuilder();
			while (currentLink != null)
			{
				builder.Append(currentLink);
				currentLink = currentLink.NextLink;
			}
			return builder.ToString();
		}

		///// New operations
		public void InsertAfter(DoubleLink link, string title)
		{
			if (link == null || string.IsNullOrEmpty(title))
				return;
			DoubleLink newLink = new DoubleLink(title);
			newLink.PreviousLink = link;
			// Update the 'after' link's next reference, so its previous points to the new one
			if (link.NextLink != null)
				link.NextLink.PreviousLink = newLink;
			// Steal the next link of the node, and set the after so it links to our new one
			newLink.NextLink = link.NextLink;
			link.NextLink = newLink;
		}

	}
}

