// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.RecipientList
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CS;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.EP;

public class RecipientList : IEnumerable<NotificationRecipient>, IEnumerable
{
  private readonly SortedList<string, NotificationRecipient> items;

  public RecipientList() => this.items = new SortedList<string, NotificationRecipient>();

  public void Add(NotificationRecipient item)
  {
    this.items.Add($"{item.Format}.{this.items.Count.ToString()}", item);
  }

  public IEnumerator<NotificationRecipient> GetEnumerator()
  {
    foreach (KeyValuePair<string, NotificationRecipient> keyValuePair in this.items)
      yield return keyValuePair.Value;
  }

  IEnumerator IEnumerable.GetEnumerator()
  {
    foreach (KeyValuePair<string, NotificationRecipient> keyValuePair in this.items)
      yield return (object) keyValuePair.Value;
  }
}
