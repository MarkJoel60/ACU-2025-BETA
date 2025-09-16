// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RoomMaint
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;

#nullable disable
namespace PX.Objects.FS;

public class RoomMaint : PXGraph<RoomMaint, FSRoom>
{
  public PXSelect<FSRoom, Where<FSRoom.branchLocationID, Equal<Optional<FSRoom.branchLocationID>>>> RoomRecords;

  [PXDefault]
  [PXDBInt(IsKey = true)]
  [PXUIField(DisplayName = "Branch Location ID")]
  [PXSelector(typeof (FSBranchLocation.branchLocationID), SubstituteKey = typeof (FSBranchLocation.branchLocationCD), DescriptionField = typeof (FSBranchLocation.descr))]
  protected virtual void FSRoom_BranchLocationID_CacheAttached(PXCache sender)
  {
  }

  [PXDefault]
  [PXDBString(10, IsUnicode = true, IsKey = true, InputMask = ">AAAAAAAAAA")]
  [PXUIField]
  [PXSelector(typeof (Search<FSRoom.roomID, Where<FSRoom.branchLocationID, Equal<Current<FSRoom.branchLocationID>>>>))]
  protected virtual void FSRoom_RoomID_CacheAttached(PXCache sender)
  {
  }
}
