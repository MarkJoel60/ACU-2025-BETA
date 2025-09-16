// Decompiled with JetBrains decompiler
// Type: PX.Objects.AP.InvoiceRecognition.DAC.BoundFeedback
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Common;
using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.AP.InvoiceRecognition.DAC;

[PXInternalUseOnly]
[PXHidden]
public class BoundFeedback : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString]
  [PXUIField(DisplayName = "Field Bound", Visible = false)]
  public virtual 
  #nullable disable
  string FieldBound { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Table Related", Visible = false)]
  public virtual string TableRelated { get; set; }

  public abstract class fieldBound : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYImportCondition.value>
  {
  }

  public abstract class tableRelated : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  BoundFeedback.tableRelated>
  {
  }
}
