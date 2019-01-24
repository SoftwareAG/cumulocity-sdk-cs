namespace Cumulocity.SDK.Client.Rest.Model.Idtype
{
	public class XtId : ID
	{
		public XtId(string id) : base(id)
		{
		}

		// Default constructor is needed for parsing from Json
		public XtId() : base()
		{
		}
	}
}