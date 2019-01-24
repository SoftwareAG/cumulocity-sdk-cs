using Cumulocity.SDK.Client.Rest.Representation.Operation;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Cumulocity.SDK.Client.Rest.API.DeviceControl.Autopoll
{
	public class OperationsQueue : BlockingCollection<OperationRepresentation>
	{

		private static OperationsQueue instance = null;

		// Singleton access - change if moved to Spring
		public static OperationsQueue Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new OperationsQueue();
				}
				return instance;
			}
		}

		public bool Add(OperationRepresentation arg0)
		{
			if (contains(arg0))
			{
				return false;
			}
			return base.TryAdd(arg0);
		}

		public bool contains(Object arg0)
		{
			//if not instance of OperationRepresentation then not contains
			if (!(arg0 is OperationRepresentation))
			{
				return false;
			}
			OperationRepresentation operation = (OperationRepresentation)arg0;

			//iterate over all elements in queue and compare theirs ids
			//var iterator = base.GetConsumingEnumerable();
			foreach (var current in base.ToArray())
			{
				//if element in list have the same id, we know list contains it
				if (current != null && current.Id != null && current.Id.Equals(operation.Id))
				{
					return true;
				}
			}

			//if no match, then list doesn't contain that element
			return false;
		}

		public bool addAll(ICollection<OperationRepresentation> arg0)
		{
			bool result = false;
			foreach (OperationRepresentation operation in arg0)
			{
				result = result || Add(operation);
			}
			return result;
		}
	}
}