// Decompiled with JetBrains decompiler
// Type: PX.Api.SYServiceSchema
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
public class SYServiceSchema : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public 
  #nullable disable
  string ProcessMessage;

  [PXDBString(15, IsKey = true, InputMask = "")]
  [PXDefault(typeof (SYWebService.serviceID))]
  [PXParent(typeof (Select<SYWebService, Where<SYWebService.serviceID, Equal<Current<SYServiceSchema.serviceID>>>>))]
  public string ServiceID { get; set; }

  [PXDBString(8, IsKey = true, IsFixed = true, InputMask = ">CC.CC.CC.CC")]
  [PXDefault]
  [PXUIField(DisplayName = "Screen ID")]
  public string ScreenID { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsIncluded { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Generated", Enabled = false)]
  public virtual bool? IsGenerated { get; set; }

  [PXDBBool]
  [PXDefault(typeof (SYWebService.isImport))]
  [PXUIField(DisplayName = "Import")]
  public virtual bool? IsImport { get; set; }

  [PXDBBool]
  [PXDefault(typeof (SYWebService.isExport))]
  [PXUIField(DisplayName = "Export")]
  public virtual bool? IsExport { get; set; }

  [PXDBBool]
  [PXDefault(typeof (SYWebService.isSubmit))]
  [PXUIField(DisplayName = "Submit")]
  public virtual bool? IsSubmit { get; set; }

  [PXString(InputMask = "", IsUnicode = true)]
  [PXUIField(DisplayName = "Title", Enabled = false)]
  public string Title { get; set; }

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  public abstract class serviceID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYServiceSchema.serviceID>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYServiceSchema.screenID>
  {
  }

  public abstract class isIncluded : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYServiceSchema.isIncluded>
  {
  }

  public abstract class isGenerated : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYServiceSchema.isGenerated>
  {
  }

  public abstract class isImport : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYServiceSchema.isImport>
  {
  }

  public abstract class isExport : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYServiceSchema.isExport>
  {
  }

  public abstract class isSubmit : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYServiceSchema.isSubmit>
  {
  }

  public abstract class title : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYServiceSchema.title>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYServiceSchema.noteID>
  {
  }
}
