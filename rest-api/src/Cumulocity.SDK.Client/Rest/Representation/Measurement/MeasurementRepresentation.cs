using System;
using Cumulocity.SDK.Client.Rest.Model.Idtype;
using Cumulocity.SDK.Client.Rest.Representation.Identity;
using Cumulocity.SDK.Client.Rest.Representation.Inventory;

namespace Cumulocity.SDK.Client.Rest.Representation.Measurement
{
    public class MeasurementRepresentation : AbstractExtensibleRepresentation, ICloneable
    {
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @Null(operation = Command.CREATE) private GId id;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private ManagedObjectRepresentation source;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private DateTime time;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @NotNull(operation = Command.CREATE) private String type;

//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONConverter(type = IDTypeConverter.class) public void setId(GId id)
        public virtual GId Id { set; get; }


        public virtual string Type { set; get; }


        public virtual ExternalIDRepresentation ExternalSource { set; get; }


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(value = "deprecated_Time", ignore = true) @Deprecated public Date getTime()
//	[Obsolete]
//	public virtual DateTime Time
//	{
//		get
//		{
//			return time == null ? null : time.toDate();
//		}
//		set
//		{
//			this.time = value == null ? null : newLocal(value);
//		}
//	}


//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @JSONProperty(value = "time", ignoreIfNull = true) @JSONConverter(type = DateTimeConverter.class) public DateTime getDateTime()
        public virtual DateTime DateTime { get; set; }


        public virtual ManagedObjectRepresentation Source { set; get; }


        //Used in conversion to make sure not to do side effects
        //JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
        //ORIGINAL LINE: @Override protected Object clone() throws CloneNotSupportedException
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}