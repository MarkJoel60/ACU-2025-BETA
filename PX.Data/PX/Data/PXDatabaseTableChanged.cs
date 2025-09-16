// Decompiled with JetBrains decompiler
// Type: PX.Data.PXDatabaseTableChanged
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <summary>Delegate for handling SQL Server query notifications</summary>
/// <param name="sender">The source of the event</param>
/// <param name="e">A SqlNotificationEventArgs object that contains the event data</param>
public delegate void PXDatabaseTableChanged();
