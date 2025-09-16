// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSessionListItem
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

public class PXSessionListItem
{
  public string User;
  public string ItemID;
  public double Size;
  public System.DateTime DateCreated = System.DateTime.Now;
  public System.DateTime? DateModified;
}
