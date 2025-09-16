// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSxUserPreferences
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using PX.SM;

#nullable enable
namespace PX.Objects.FS;

public class FSxUserPreferences : PXCacheExtension<
#nullable disable
UserPreferences>
{
  [PXDBInt]
  [PXUIField(DisplayName = "Default Branch Location", FieldClass = "SERVICEMANAGEMENT")]
  [PXSelector(typeof (Search<FSBranchLocation.branchLocationID, Where<FSBranchLocation.branchID, Equal<Current<UserPreferences.defBranchID>>>>), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  [PXFormula(typeof (Default<UserPreferences.defBranchID>))]
  [PXDefault]
  public virtual int? DfltBranchLocationID { get; set; }

  [PXDBString(4, IsUnicode = true, IsFixed = true)]
  [PXUIField(DisplayName = "Default Service Order Type", FieldClass = "SERVICEMANAGEMENT")]
  [FSSelectorSrvOrdTypeNOTQuote]
  [PXDefault]
  public virtual string DfltSrvOrdType { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Select Service Order Type on Creation from Calendars", FieldClass = "SERVICEMANAGEMENT")]
  public virtual bool? AskForSrvOrdTypeInCalendars { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Track Location")]
  public virtual bool? TrackLocation { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXDefault(5)]
  [PXUIField(DisplayName = "Tracking Frequency", Enabled = false)]
  public virtual short? Interval { get; set; }

  [PXDBShort(MinValue = 1)]
  [PXDefault(250)]
  [PXUIField(DisplayName = "Distance Frequency", Enabled = false)]
  public virtual short? Distance { get; set; }

  public abstract class dfltBranchLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    FSxUserPreferences.dfltBranchLocationID>
  {
  }

  public abstract class dfltSrvOrdType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    FSxUserPreferences.dfltSrvOrdType>
  {
  }

  public abstract class askForSrvOrdTypeInCalendars : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    FSxUserPreferences.askForSrvOrdTypeInCalendars>
  {
  }

  public abstract class trackLocation : IBqlField, IBqlOperand
  {
  }

  public abstract class interval : IBqlField, IBqlOperand
  {
  }

  public abstract class distance : IBqlField, IBqlOperand
  {
  }
}
