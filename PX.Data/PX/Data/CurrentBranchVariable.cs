// Decompiled with JetBrains decompiler
// Type: PX.Data.CurrentBranchVariable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class CurrentBranchVariable : IMacroVariable
{
  public string Name => "@branch";

  public object Resolve(System.Type dataType)
  {
    if (dataType == typeof (int))
      return (object) PXAccess.GetBranchID();
    if (dataType == typeof (string))
      return (object) PXAccess.GetBranchCD(PXAccess.GetBranchID());
    throw new PXException("Variable {0} is not applicable to this field.", new object[1]
    {
      (object) this.Name
    });
  }
}
