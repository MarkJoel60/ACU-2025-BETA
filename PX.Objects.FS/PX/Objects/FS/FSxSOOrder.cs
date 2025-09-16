// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxSOOrder
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.CS;
using System;

#nullable enable
namespace PX.Objects.FS;

public class FSxSOOrder : PXCacheExtension<
#nullable disable
PX.Objects.SO.SOOrder>
{
  protected DateTime? _SLAETA;

  public static bool IsActive() => PXAccess.FeatureInstalled<FeaturesSet.serviceManagementModule>();

  [PXDBInt]
  [FSSelector_StaffMember_All(null)]
  [PXDefault]
  [PXUIField(DisplayName = "Assigned to", Visible = false)]
  public virtual int? AssignedEmpID { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Create Service Order", Enabled = false, Visible = false)]
  public virtual bool? SDEnabled { get; set; }

  [PXDBString(4, IsFixed = true, InputMask = ">AAAA")]
  [PXDefault]
  [PXUIField(DisplayName = "Service Order Type", Enabled = false, Required = false, Visible = false)]
  [FSSelectorActiveSrvOrdType]
  public virtual string SrvOrdType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Installed", Enabled = false, Visible = false)]
  public virtual bool? Installed { get; set; }

  [PXDBDateAndTime(UseTimeZone = true, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time")]
  [PXDefault]
  [PXUIField(DisplayName = "Deadline - SLA", Enabled = false, Visible = false)]
  public virtual DateTime? SLAETA
  {
    get => this._SLAETA;
    set
    {
      this.SLAETAUTC = value;
      this._SLAETA = value;
    }
  }

  [PXDBString(15, IsUnicode = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXUIField]
  [PXSelector(typeof (Search<FSServiceOrder.refNbr, Where<FSServiceOrder.srvOrdType, Equal<Current<FSxSOOrder.srvOrdType>>>>))]
  [PXDefault]
  public virtual string ServiceOrderRefNbr { get; set; }

  [PXDBDateAndTime(UseTimeZone = false, PreserveTime = true, DisplayNameDate = "Deadline - SLA Date", DisplayNameTime = "Deadline - SLA Time")]
  [PXDefault]
  [PXUIField(DisplayName = "Deadline - SLA", Enabled = false, Visible = false)]
  public virtual DateTime? SLAETAUTC { get; set; }

  [PXBool]
  [PXDefault]
  [PXUIField(Enabled = false, Visible = false)]
  [SkipSetExtensionVisibleInvisible]
  public bool? IsFSIntegrated { get; set; }

  public abstract class assignedEmpID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  FSxSOOrder.assignedEmpID>
  {
  }

  public abstract class sDEnabled : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxSOOrder.sDEnabled>
  {
  }

  public abstract class srvOrdType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FSxSOOrder.srvOrdType>
  {
  }

  public abstract class installed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxSOOrder.installed>
  {
  }

  public abstract class sLAETA : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSxSOOrder.sLAETA>
  {
  }

  public abstract class serviceOrderRefNbr : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxSOOrder.serviceOrderRefNbr>
  {
  }

  public abstract class sLAETAUTC : BqlType<
  #nullable enable
  IBqlDateTime, DateTime>.Field<
  #nullable disable
  FSxSOOrder.sLAETAUTC>
  {
  }

  public abstract class isFSIntegrated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  FSxSOOrder.isFSIntegrated>
  {
  }
}
