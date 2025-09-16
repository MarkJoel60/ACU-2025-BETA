// Decompiled with JetBrains decompiler
// Type: PX.Api.SYWebService
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYWebService : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(15, IsKey = true, InputMask = ">CCCCCCCCCCCCCCC")]
  [PXDefault]
  [PXUIField(DisplayName = "Service ID", Visibility = PXUIVisibility.SelectorVisible)]
  [PXSelector(typeof (SYWebService.serviceID))]
  public 
  #nullable disable
  string ServiceID { get; set; }

  [PXDBString(IsUnicode = true)]
  [PXUIField(DisplayName = "Description", Visibility = PXUIVisibility.SelectorVisible)]
  public string Description { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Include Untyped")]
  public virtual bool? IncludeUntyped { get; set; }

  [PXDBDate(PreserveTime = true)]
  [PXUIField(DisplayName = "System Time", Enabled = false)]
  public virtual System.DateTime? DateGenerated { get; set; }

  [PXDBString(50, IsUnicode = true)]
  [PXUIField(DisplayName = "System Version", Enabled = false)]
  public string SysVerWhenGenerated { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Is Generated", Enabled = false)]
  public virtual bool? IsGenerated { get; set; }

  [PXDBText(IsUnicode = true)]
  [PXUIField(DisplayName = "WSDL")]
  public string WSDL { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Import")]
  public virtual bool? IsImport { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Export")]
  public virtual bool? IsExport { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Submit")]
  public virtual bool? IsSubmit { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXString(IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Current System Version", Enabled = false)]
  public string CurSysVer { get; set; }

  public abstract class serviceID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYWebService.serviceID>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYWebService.description>
  {
  }

  public abstract class includeUntyped : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYWebService.includeUntyped>
  {
  }

  public abstract class dateGenerated : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYWebService.dateGenerated>
  {
  }

  public abstract class sysVerWhenGenerated : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYWebService.sysVerWhenGenerated>
  {
  }

  public abstract class isGenerated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYWebService.isGenerated>
  {
  }

  public abstract class wSDL : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYWebService.wSDL>
  {
  }

  public abstract class isImport : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYWebService.isImport>
  {
  }

  public abstract class isExport : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYWebService.isExport>
  {
  }

  public abstract class isSubmit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYWebService.isSubmit>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYWebService.noteID>
  {
  }

  public abstract class curSysVer : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYWebService.curSysVer>
  {
  }
}
