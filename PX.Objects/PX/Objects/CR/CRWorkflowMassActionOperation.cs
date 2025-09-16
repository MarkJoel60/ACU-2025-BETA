// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.CRWorkflowMassActionOperation
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR;

/// <exclude />
public static class CRWorkflowMassActionOperation
{
  public const 
  #nullable disable
  string UpdateSettings = "Update";
  public const string ExecuteAction = "Execute";

  public class ListAttribute : PXStringListAttribute
  {
    public ListAttribute()
      : base(new string[2]{ "Update", "Execute" }, new string[2]
      {
        "Update Settings",
        "Execute Action"
      })
    {
    }
  }

  public class updateSettings : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRWorkflowMassActionOperation.updateSettings>
  {
    public updateSettings()
      : base("Update")
    {
    }
  }

  public class executeAction : 
    BqlType<
    #nullable enable
    IBqlString, string>.Constant<
    #nullable disable
    CRWorkflowMassActionOperation.executeAction>
  {
    public executeAction()
      : base("Execute")
    {
    }
  }
}
