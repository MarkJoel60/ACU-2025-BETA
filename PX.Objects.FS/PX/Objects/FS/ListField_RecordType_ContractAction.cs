// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.ListField_RecordType_ContractAction
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public abstract class ListField_RecordType_ContractAction : IBqlField, IBqlOperand
{
  public class ListAtrribute : PXStringListAttribute
  {
    public ListAtrribute()
      : base(new ID.RecordType_ContractAction().ID_LIST, new ID.RecordType_ContractAction().TX_LIST)
    {
    }
  }
}
