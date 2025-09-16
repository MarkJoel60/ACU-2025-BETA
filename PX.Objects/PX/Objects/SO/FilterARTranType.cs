// Decompiled with JetBrains decompiler
// Type: PX.Objects.SO.FilterARTranType
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.AR;

#nullable enable
namespace PX.Objects.SO;

public class FilterARTranType : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  protected 
  #nullable disable
  string _DocType;

  [PXDBString(3, IsFixed = true)]
  [ARDocType.SOEntryList]
  [PXUIField(DisplayName = "Type")]
  public virtual string DocType
  {
    get => this._DocType;
    set => this._DocType = value;
  }

  public abstract class docType : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  FilterARTranType.docType>
  {
  }
}
