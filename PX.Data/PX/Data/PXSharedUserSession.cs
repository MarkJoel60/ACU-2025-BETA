// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSharedUserSession
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections;
using System.Web;

#nullable disable
namespace PX.Data;

/// <summary>
/// Share collection of items between splitted user sessions( in case, when multiple windows opened )
/// </summary>
public static class PXSharedUserSession
{
  internal static bool IsAvailable => HttpContext.Current?.Session != null;

  public static PXSharedUserSession.SessionItems CurrentUser
  {
    get
    {
      PXSharedUserSession.SessionItems currentUser = (PXSharedUserSession.SessionItems) HttpContext.Current.Items[(object) "SharedSessionInfoItems"];
      if (currentUser == null)
      {
        currentUser = new PXSharedUserSession.SessionItems();
        HttpContext.Current.Items[(object) "SharedSessionInfoItems"] = (object) currentUser;
      }
      return currentUser;
    }
  }

  public class SessionItems
  {
    private Hashtable _items;
    private bool IsDirty;

    public Hashtable Items
    {
      get
      {
        if (this._items == null)
        {
          this._items = PXSessionStateStore.LoadSharedSession();
          PXSessionStateStore.CommitSharedSession(HttpContext.Current);
        }
        return this._items;
      }
    }

    public bool ContainsKey(string key) => this.Items.ContainsKey((object) key);

    public T GetValueByType<T>() => (T) this.Items[(object) typeof (T).Name];

    public void SetValueByType<T>(T v)
    {
      this.SetDirty();
      this.Items[(object) typeof (T).Name] = (object) v;
    }

    public object this[string key]
    {
      get => this.Items[(object) key];
      set
      {
        this.SetDirty();
        this.Items[(object) key] = value;
      }
    }

    public void Add(string key, object value)
    {
      this.SetDirty();
      if (!this.Items.ContainsKey((object) key))
        this.Items.Add((object) key, value);
      else
        this.Items[(object) key] = value;
    }

    public void Remove(string key)
    {
      this.SetDirty();
      if (!this.Items.ContainsKey((object) key))
        return;
      this.Items.Remove((object) key);
    }

    private void SetDirty()
    {
      if (!this.IsDirty)
        this._items = PXSessionStateStore.LoadSharedSession();
      this.IsDirty = true;
    }
  }
}
