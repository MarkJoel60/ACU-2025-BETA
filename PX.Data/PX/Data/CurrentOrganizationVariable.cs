// Decompiled with JetBrains decompiler
// Type: PX.Data.CurrentOrganizationVariable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

internal class CurrentOrganizationVariable : IMacroVariable
{
  public string Name => "@company";

  public object Resolve(System.Type dataType)
  {
    if (dataType == typeof (int))
      return (object) PXAccess.GetParentOrganizationID(PXAccess.GetBranchID());
    if (dataType == typeof (string))
    {
      PXAccess.Organization parentOrganization = PXAccess.GetParentOrganization(PXAccess.GetBranchID());
      return parentOrganization == null ? (object) null : (object) parentOrganization.OrganizationCD;
    }
    throw new PXException("Variable {0} is not applicable to this field.", new object[1]
    {
      (object) this.Name
    });
  }
}
