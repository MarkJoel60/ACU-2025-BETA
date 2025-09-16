// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.LightDAC.PMTask
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Objects.PM;

#nullable enable
namespace PX.Objects.CS.LightDAC;

/// <summary>the small version of <see cref="T:PX.Objects.PM.PMTask" /></summary>
[PXHidden]
public class PMTask : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [Project(DisplayName = "Project ID", IsKey = true, DirtyRead = true)]
  [PXParent(typeof (Select<Contract, Where<Contract.contractID, Equal<Current<PMTask.projectID>>>>))]
  public virtual int? ProjectID { get; set; }

  [PXDBIdentity]
  public virtual int? TaskID { get; set; }

  [PXDimension("PROTASK")]
  [PXDBString(IsUnicode = true, IsKey = true, InputMask = "")]
  public virtual 
  #nullable disable
  string TaskCD { get; set; }

  [PXDBLocalizableString(250, IsUnicode = true)]
  public virtual string Description { get; set; }

  [PXDBBool]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  public virtual bool? IsCompleted { get; set; }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PMTask.taskID>
  {
  }

  public abstract class taskCD : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.taskCD>
  {
  }

  public abstract class description : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PMTask.description>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.isActive>
  {
  }

  public abstract class isCompleted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  PMTask.isCompleted>
  {
  }
}
