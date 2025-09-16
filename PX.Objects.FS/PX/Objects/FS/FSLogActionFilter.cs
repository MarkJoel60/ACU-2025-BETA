// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSLogActionFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[PXVirtual]
[Serializable]
public class FSLogActionFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected bool? _IsTravelAction;

  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Action")]
  [PXUnboundDefault("ST")]
  [ListField_LogActions.ListAtrribute]
  public virtual 
  #nullable disable
  string Action { get; set; }

  [PXString(2, IsFixed = true)]
  [PXUIField(DisplayName = "Logging")]
  [PXUnboundDefault("TR")]
  [FSLogTypeAction.List]
  public virtual string Type { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, DisplayNameDate = "Date", DisplayNameTime = "Time")]
  [PXUIField(DisplayName = "Date")]
  public virtual DateTime? LogDateTime { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(Visible = false, Enabled = false)]
  public virtual bool? IsTravelAction => new bool?(this.Type == "TR");

  [PXString(4, IsFixed = true)]
  [PXFormula(typeof (Default<FSLogActionFilter.type>))]
  [PXDefault]
  [PXUIVisible(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.StaffAssignment>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.SrvBasedOnAssignment>>>>))]
  [PXUIRequired(typeof (Where<Current<FSLogActionFilter.action>, Equal<ListField_LogActions.Start>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.StaffAssignment>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.SrvBasedOnAssignment>, And<FSLogActionFilter.type, NotEqual<FSLogTypeAction.Travel>>>>>))]
  [FSSelectorAppointmentSODetID(typeof (Where<FSAppointmentDet.lineType, Equal<FSLineType.Service>, And<FSAppointmentDet.isTravelItem, Equal<Current<FSLogActionFilter.isTravelAction>>, And<FSAppointmentDet.lineRef, IsNotNull, And<Where<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.NotStarted>, Or<FSAppointmentDet.status, Equal<FSAppointmentDet.ListField_Status_AppointmentDet.InProcess>>>>>>>))]
  [PXUIField(DisplayName = "Detail Ref. Nbr.")]
  public virtual string DetLineRef { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  [PXUIField(DisplayName = "Show Only Mine")]
  [PXUIVisible(typeof (Where<FSLogActionFilter.type, NotEqual<FSLogTypeAction.SrvBasedOnAssignment>>))]
  public virtual bool? Me { get; set; }

  [PXBool]
  [PXUnboundDefault(false)]
  public virtual bool? VerifyRequired { get; set; }

  public abstract class action : ListField_LogActions
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLogActionFilter.type>
  {
    public abstract class Values : ListField_LogAction_Type
    {
    }
  }

  public abstract class logDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    FSLogActionFilter.logDateTime>
  {
  }

  public abstract class isTravelAction : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSLogActionFilter.isTravelAction>
  {
  }

  public abstract class detLineRef : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSLogActionFilter.detLineRef>
  {
  }

  public abstract class me : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSLogActionFilter.me>
  {
  }

  public abstract class verifyRequired : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSLogActionFilter.verifyRequired>
  {
  }
}
