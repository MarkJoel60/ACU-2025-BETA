// Decompiled with JetBrains decompiler
// Type: PX.SM.AuWorkflowFormCustomizedExtension
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class AuWorkflowFormCustomizedExtension : PXCacheExtension<
#nullable disable
AUWorkflowForm>
{
  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DisplayNameCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? ColumnsCustomized { get; set; } = new bool?(false);

  [PXDBBool]
  [PXDefault(false)]
  public virtual bool? DacTypeCustomized { get; set; } = new bool?(false);

  public abstract class displayNameCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AuWorkflowFormCustomizedExtension.displayNameCustomized>
  {
  }

  public abstract class columnsCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AuWorkflowFormCustomizedExtension.columnsCustomized>
  {
  }

  public abstract class dacTypeCustomized : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    AuWorkflowFormCustomizedExtension.dacTypeCustomized>
  {
  }
}
