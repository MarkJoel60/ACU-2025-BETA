// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.BackwardCompatibility.DACWithOwnerID
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.CR.BackwardCompatibility;

[PXHidden]
public class DACWithOwnerID : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXOwnerSelector]
  [PXDBInt]
  [PXUIField(DisplayName = "Owner")]
  public virtual int? OwnerID { get; set; }

  public abstract class ownerID : BqlType<IBqlInt, int>.Field<
  #nullable disable
  DACWithOwnerID.ownerID>
  {
  }
}
